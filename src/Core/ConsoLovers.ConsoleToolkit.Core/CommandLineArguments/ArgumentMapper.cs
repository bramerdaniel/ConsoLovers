// --------------------------------------------------------------------------------------------------------------------
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
                  ValidateProperty(instance, mapping.PropertyInfo);

                  MappedCommandLineArgument?.Invoke(this, new MapperEventArgs(mapping.CommandLineArgument, mapping.PropertyInfo, instance));
               }
            }
            else
            {
               var wasSet = SetArgumentValue(instance, mapping, arguments);
               if (wasSet)
               {
                  sharedArguments.Add(mapping.CommandLineArgument);
                  ValidateProperty(instance, mapping.PropertyInfo);

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
            var firstParameter = methodInfo.GetParameters().FirstOrDefault();
            if (firstParameter != null && firstParameter.ParameterType == type)
            {
               return methodInfo;
            }
         }

         return null;
      }

      private void CheckForUnmappedArguments(ICommandLineArguments arguments, HashSet<CommandLineArgument> sharedArguments, T instance)
      {
         if (arguments.Count <= 0)
            return;

         foreach (var argument in arguments.Where(arg => !sharedArguments.Contains(arg)))
            UnmappedCommandLineArgument?.Invoke(this, new MapperEventArgs(argument, null, instance));
      }

      private void ValidateProperty(T arguments, PropertyInfo propertyInfo)
      {
         var propertiesToValidate = propertyInfo.GetCustomAttributes<ArgumentValidatorAttribute>(true).ToArray();
         if (!propertiesToValidate.Any())
            return;

         foreach (var attribute in propertiesToValidate)
         {
            var instance = serviceProvider.GetService(attribute.Type);
            if (instance != null)
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

               MethodInfo validationMethod = GetValidationMethod(attribute, propertyInfo.PropertyType);

               try
               {
                  validationMethod.Invoke(instance, new[] { propertyInfo.GetValue(arguments, null) });
               }
               catch (TargetInvocationException e)
               {
                  throw e.InnerException ?? e;
               }
            }
         }
      }

      #endregion
   }
}