﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReflectionExtensions.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments.Parsing;
using ConsoLovers.ConsoleToolkit.Core.Localization;

using Microsoft.Extensions.DependencyInjection;

using ParameterInfo = ConsoLovers.ConsoleToolkit.Core.CommandLineArguments.ParameterInfo;

public static class ReflectionExtensions
{
   [SuppressMessage("sonar", "S3011:Reflection should not be used to increase accessibility of classes, methods, or fields")]
   internal static IEnumerable<KeyValuePair<PropertyInfo, CommandLineAttribute>> GetPropertiesWithAttributes(this Type type)
   {
      foreach (var property in type.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
      {
         var commandLineAttribute = property.GetAttribute<CommandLineAttribute>();
         if (commandLineAttribute != null)
            yield return new KeyValuePair<PropertyInfo, CommandLineAttribute>(property, commandLineAttribute);
      }
   }

   public static T GetAttribute<T>(this PropertyInfo propertyInfo) where T : Attribute
   {
      return propertyInfo.GetCustomAttributes<T>(true).FirstOrDefault();
   }

   internal static IServiceCollection AddRequiredServices([JetBrains.Annotations.NotNull] this IServiceCollection serviceCollection)
   {
      var argumentReflector = new ArgumentReflector();
      serviceCollection.AddSingleton<IArgumentReflector>(argumentReflector);
      serviceCollection.AddSingleton(argumentReflector);

      EnsureServiceAndImplementation<ICommandLineArgumentParser, CommandLineArgumentParser>(serviceCollection);
      EnsureServiceAndImplementation<ICommandLineEngine, CommandLineEngine>(serviceCollection);
      EnsureServiceAndImplementation<ICommandExecutor, CommandExecutor>(serviceCollection);
      EnsureServiceAndImplementation<ILocalizationService, DefaultLocalizationService>(serviceCollection);
      EnsureServiceAndImplementation<IConsole, ConsoleProxy>(serviceCollection);

      return serviceCollection;
   }

   private static void EnsureServiceAndImplementation<TService, TImplementation>(IServiceCollection serviceCollection)
   where TImplementation : TService
   {
      var serviceType = typeof(TService);
      var implementationType = typeof(TImplementation);
      if (TryAddSingleton(serviceCollection, serviceType, implementationType))
         serviceCollection.AddSingleton(implementationType, x => x.GetService(serviceType));
   }

   private static bool TryAddSingleton(IServiceCollection serviceCollection, Type serviceType, Type implementationType)
   {
      if (serviceCollection.Any(x => x.ServiceType == serviceType))
         return false;

      serviceCollection.AddSingleton(serviceType, implementationType);
      return true;
   }

   internal static void AddApplicationTypes([JetBrains.Annotations.NotNull] this IServiceCollection serviceCollection, [JetBrains.Annotations.NotNull] Type applicationType)
   {
      if (serviceCollection == null)
         throw new ArgumentNullException(nameof(serviceCollection));
      if (applicationType == null)
         throw new ArgumentNullException(nameof(applicationType));

      serviceCollection.AddSingleton(applicationType);

      var argumentType = applicationType.GetInterface("IApplication`1");
      if (argumentType != null)
      {
         foreach (var typeArgument in argumentType.GenericTypeArguments)
            serviceCollection.AddArgumentTypes(typeArgument);
      }
   }

   /// <summary>Adds the application type and the required commands and argument classes.</summary>
   /// <typeparam name="TApplication">The type of the application.</typeparam>
   /// <param name="serviceCollection">The service collection.</param>
   /// <exception cref="System.ArgumentNullException">serviceCollection</exception>
   internal static void AddApplicationTypes<TApplication>([JetBrains.Annotations.NotNull] this IServiceCollection serviceCollection)
   where TApplication : class
   {
      serviceCollection.AddApplicationTypes(typeof(TApplication));
   }

   internal static IServiceCollection AddArgumentTypes<TArgument>([JetBrains.Annotations.NotNull] this IServiceCollection serviceCollection)
   {
      return serviceCollection.AddArgumentTypes(typeof(TArgument));
   }

   internal static IServiceCollection AddArgumentTypes([JetBrains.Annotations.NotNull] this IServiceCollection serviceCollection, Type argumentType)
   {
      if (serviceCollection == null)
         throw new ArgumentNullException(nameof(serviceCollection));

      if (argumentType != null)
      {
         return serviceCollection.AddArgumentTypesInternal(argumentType, new HashSet<Type>());
      }

      return serviceCollection;
   }

   private static IServiceCollection AddArgumentTypesInternal([JetBrains.Annotations.NotNull] this IServiceCollection serviceCollection, Type argumentType, HashSet<Type> addedTypes)
   {
      if (addedTypes.Contains(argumentType))
         return serviceCollection;

      serviceCollection.AddTransient(argumentType);
      addedTypes.Add(argumentType);

      var argumentReflector = serviceCollection.Where(x => x.ImplementationInstance is IArgumentReflector)
         .Select(x => (IArgumentReflector)x.ImplementationInstance)
         .First();

      var argumentInfo = argumentReflector.GetTypeInfo(argumentType);

      AddCommandTypes(serviceCollection, argumentInfo, addedTypes);
      AddValidatorTypes(serviceCollection, argumentInfo, addedTypes);

      return serviceCollection;
   }

   private static void AddCommandTypes(IServiceCollection serviceCollection, ArgumentClassInfo argumentInfo, HashSet<Type> addedTypes)
   {
      foreach (var commandInfo in argumentInfo.CommandInfos)
      {
         if (!addedTypes.Contains(commandInfo.ParameterType))
         {
            serviceCollection.AddTransient(commandInfo.ParameterType);
            addedTypes.Add(commandInfo.ParameterType);

            if (commandInfo.ArgumentType != null)
               serviceCollection.AddArgumentTypesInternal(commandInfo.ArgumentType, addedTypes);
         }
      }
   }

   private static void AddValidatorTypes(IServiceCollection serviceCollection, ArgumentClassInfo argumentInfo, HashSet<Type> registeredTypes)
   {
      foreach (ParameterInfo property in argumentInfo.Properties)
      {
         if (property is ArgumentInfo argument)
         {
            var validatorAttribute = argument.ValidatorAttribute;
            if (validatorAttribute != null && !registeredTypes.Contains(validatorAttribute.Type))
            {
               serviceCollection.AddTransient(validatorAttribute.Type);
               registeredTypes.Add(validatorAttribute.Type);
            }
         }
      }
   }
}