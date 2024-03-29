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
using ConsoLovers.ConsoleToolkit.Core;
using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments.Parsing;
using ConsoLovers.ConsoleToolkit.Core.Services;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

public static class ReflectionExtensions
{
   #region Public Methods and Operators

   public static T GetAttribute<T>(this PropertyInfo propertyInfo)
      where T : Attribute
   {
      return propertyInfo.GetAttribute<T>(true);
   }

   public static T GetAttribute<T>(this PropertyInfo propertyInfo, bool inherit)
      where T : Attribute
   {
      return propertyInfo.GetAttribute<T>(inherit, () => null);
   }

   public static T GetAttribute<T>(this PropertyInfo propertyInfo, bool inherit, Func<T> defaultValue)
      where T : Attribute
   {
      return propertyInfo.GetCustomAttributes<T>(inherit).FirstOrDefault() ?? defaultValue();
   }

   public static T GetAttribute<T>(this Type type)
      where T : Attribute
   {
      return type.GetAttribute<T>(true);
   }

   public static T GetAttribute<T>(this Type type, bool inherit)
      where T : Attribute
   {
      return type.GetCustomAttributes<T>(inherit).FirstOrDefault();
   }

   #endregion

   #region Methods

   internal static void AddApplicationTypes([JetBrains.Annotations.NotNull] this IServiceCollection serviceCollection,
      [JetBrains.Annotations.NotNull] Type applicationType)
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
   /// <exception cref="System.ArgumentNullException">ServiceCollection</exception>
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
         var addedTypes = new HashSet<Type>();
         if (typeof(IApplicationLogic).IsAssignableFrom(argumentType))
         {
            serviceCollection.AddSingleton(argumentType);
            serviceCollection.AddSingleton(typeof(IApplicationLogic), s => s.GetService(argumentType));
            addedTypes.Add(argumentType);
         }

         return serviceCollection.AddArgumentTypesInternal(argumentType, addedTypes);
      }

      return serviceCollection;
   }

   internal static IServiceCollection AddRequiredServices([JetBrains.Annotations.NotNull] this IServiceCollection serviceCollection)
   {
      var argumentReflector = new ArgumentReflector();
      serviceCollection.AddSingleton<IArgumentReflector>(argumentReflector);
      serviceCollection.AddSingleton(argumentReflector);

      EnsureSingleton<ICommandLineArgumentParser, CommandLineArgumentParser>(serviceCollection);
      EnsureSingleton<ICommandLineEngine, CommandLineEngine>(serviceCollection);
      EnsureSingleton<IExecutionEngine, ExecutionEngine>(serviceCollection);
      EnsureSingleton<IExecutionResult, ExecutionResult>(serviceCollection);
      EnsureSingleton<IExceptionHandler, ExceptionHandler>(serviceCollection);
      EnsureSingleton<IExitCodeHandler, DefaultExitCodeHandler>(serviceCollection);
      EnsureSingleton<IShutdownNotifier, ShutdownNotifier>(serviceCollection);
      EnsureSingleton<ILocalizationService, DefaultLocalizationService>(serviceCollection);
      EnsureSingleton<IConsole, ConsoleProxy>(serviceCollection);
      serviceCollection.TryAddSingleton(serviceCollection);

      return serviceCollection;
   }

   internal static IServiceCollection EnsureSingleton<TService, TImplementation>(this IServiceCollection serviceCollection)
      where TImplementation : TService
   {
      var serviceType = typeof(TService);
      var implementationType = typeof(TImplementation);
      if (TryAddSingleton(serviceCollection, serviceType, implementationType))
         serviceCollection.AddSingleton(implementationType, x => x.GetService(serviceType));

      return serviceCollection;
   }

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

   [SuppressMessage("sonar", "S3011:Reflection should not be used to increase accessibility of classes, methods, or fields")]
   internal static IEnumerable<KeyValuePair<PropertyInfo, T>> GetPropertiesWithAttributes<T>(this Type type)
   where T : Attribute
   {
      foreach (var property in type.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
      {
         var attribute = property.GetAttribute<T>(true);
         if (attribute != null)
            yield return new KeyValuePair<PropertyInfo, T>(property, attribute);
      }
   }

   private static IServiceCollection AddArgumentTypesInternal([JetBrains.Annotations.NotNull] this IServiceCollection serviceCollection,
      Type argumentType, HashSet<Type> addedTypes)
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

   private static bool TryAddSingleton(IServiceCollection serviceCollection, Type serviceType, Type implementationType)
   {
      if (serviceCollection.Any(x => x.ServiceType == serviceType))
         return false;

      serviceCollection.AddSingleton(serviceType, implementationType);
      return true;
   }

   #endregion
}