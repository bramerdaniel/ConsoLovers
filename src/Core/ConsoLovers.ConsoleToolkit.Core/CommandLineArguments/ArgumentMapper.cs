﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArgumentMapper.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Reflection;
   using ConsoLovers.ConsoleToolkit.Core;
   using ConsoLovers.ConsoleToolkit.Core.Exceptions;

   using JetBrains.Annotations;

   using Microsoft.Extensions.DependencyInjection;

   /// <inheritdoc cref="IArgumentMapper{T}"/>
   /// <summary>Class that can map a dictionary to an instance of a class, filling the properties.</summary>
   /// <typeparam name="T">The type of the class to create</typeparam>
   public class ArgumentMapper<T> : MapperBase, IArgumentMapper<T>
      where T : class
   {
      #region Constants and Fields

      private readonly IServiceProvider serviceProvider;

      #endregion

      #region Constructors and Destructors

      public ArgumentMapper([NotNull] IServiceProvider serviceProvider)
      {
         this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
      }

      #endregion

      #region Public Events

      /// <summary>Occurs when command line argument could be mapped to a specific property of the specified class of type.</summary>
      public event EventHandler<MapperEventArgs> MappedCommandLineArgument;

      /// <summary>Occurs when a command line argument of the given arguments dictionary could not be mapped to a arguments member</summary>
      public event EventHandler<MapperEventArgs> UnmappedCommandLineArgument;

      #endregion

      #region IArgumentMapper<T> Members

      public T Map(ICommandLineArguments arguments)
      {
         var instance = serviceProvider.GetService<T>();
         return Map(arguments, instance);
      }

      public T Map(ICommandLineArguments arguments, T instance)
      {
         var sharedArguments = new HashSet<CommandLineArgument>();
         foreach (var mapping in MappingList.FromType<T>())
         {
            if (mapping.IsOption())
            {
               var wasSet = SetOptionValue(instance, mapping, arguments);
               if (wasSet)
               {
                  sharedArguments.Add(mapping.CommandLineArgument);
                  ValidateProperty(instance, mapping);

                  MappedCommandLineArgument?.Invoke(this, new MapperEventArgs(mapping.CommandLineArgument, mapping.PropertyInfo, instance));
               }
            }
            else
            {
               var wasSet = SetArgumentValue(instance, mapping, arguments);
               if (wasSet)
               {
                  sharedArguments.Add(mapping.CommandLineArgument);
                  ValidateProperty(instance, mapping);

                  MappedCommandLineArgument?.Invoke(this, new MapperEventArgs(mapping.CommandLineArgument, mapping.PropertyInfo, instance));
               }
            }
         }

         CheckForUnmappedArguments(arguments, sharedArguments, instance);
         return instance;
      }

      #endregion

      #region Methods

      private static MethodInfo GetValidationMethod(ArgumentValidatorAttribute attribute, Type type)
      {
         foreach (var methodInfo in attribute.Type.GetMethods().Where(x => x.Name == "Validate"))
         {
            var parameters = methodInfo.GetParameters();
            if (HasCorrectSignature(parameters, type))
               return methodInfo;
         }

         return null;
      }

      private static bool HasCorrectSignature(System.Reflection.ParameterInfo[] parameters, Type type)
      {
         if (parameters.Length != 2)
            return false;

         return parameters[0].ParameterType == typeof(IValidationContext) && parameters[1].ParameterType == type;
      }

      private void CheckForUnmappedArguments(ICommandLineArguments arguments, HashSet<CommandLineArgument> sharedArguments, T instance)
      {
         if (arguments.Count <= 0)
            return;

         foreach (var argument in arguments.Where(arg => !sharedArguments.Contains(arg)))
            UnmappedCommandLineArgument?.Invoke(this, new MapperEventArgs(argument, null, instance));
      }

      private void ValidateProperty(T arguments, MappingInfo mappingInfo)
      {
         var propertyInfo = mappingInfo.PropertyInfo;

         var propertiesToValidate = propertyInfo.GetCustomAttributes<ArgumentValidatorAttribute>(true).ToArray();
         if (!propertiesToValidate.Any())
            return;

         foreach (var attribute in propertiesToValidate)
         {
            var instance = GetOrCreateValidator(attribute);
            if (instance != null)
            {
               var validationMethod = GetValidationMethod(attribute, propertyInfo);

               try
               {
                  var context = new ValidationContext(mappingInfo.Name, propertyInfo, mappingInfo.CommandLineAttribute);
                  validationMethod.Invoke(instance, new[] { context , propertyInfo.GetValue(arguments, null) });
               }
               catch (TargetInvocationException e)
               {
                  throw e.InnerException ?? e;
               }
            }
         }
      }

      private static MethodInfo GetValidationMethod(ArgumentValidatorAttribute attribute, PropertyInfo propertyInfo)
      {
         var validatorName = typeof(IArgumentValidator<T>).Name;

         var validatorInterfaces = attribute.Type.GetInterfaces().Where(i => i.Name == validatorName).ToArray();
         if (validatorInterfaces.Length == 0)
            throw new InvalidValidatorUsageException($"The validator {attribute.Type} does not implement the {validatorName} interface.")
            {
               Reason = ErrorReason.NoValidatorImplementation
            };

         Type type = validatorInterfaces.FirstOrDefault(i => i.GenericTypeArguments.FirstOrDefault() == propertyInfo.PropertyType);
         if (type == null)
            throw new InvalidValidatorUsageException(
               $"The specified validator '{attribute.Type.FullName}' does not support the validation of the type '{propertyInfo.PropertyType}'.")
            {
               Reason = ErrorReason.InvalidValidatorImplementation
            };

         var validationMethod = GetValidationMethod(attribute, propertyInfo.PropertyType);
         if(validationMethod == null)
            throw new InvalidValidatorUsageException(
               $"The specified validator '{attribute.Type.FullName}' does not define a validate method with the matching signature.")
            {
               Reason = ErrorReason.Unknown
            };

         return validationMethod;
      }

      private object GetOrCreateValidator(ArgumentValidatorAttribute attribute)
      {
         if (attribute.Type == attribute.GetType())
            return attribute;

         return ActivatorUtilities.CreateInstance(serviceProvider, attribute.Type);
      }

      #endregion
   }
}