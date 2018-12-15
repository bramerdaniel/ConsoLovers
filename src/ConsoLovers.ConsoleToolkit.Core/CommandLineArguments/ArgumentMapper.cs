// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArgumentMapper.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Reflection;

   using JetBrains.Annotations;

   /// <inheritdoc cref="IArgumentMapper{T}"/>
   /// <summary>Class that can map a dictionary to an instance of a class, filling the properties.</summary>
   /// <typeparam name="T">The type of the class to create</typeparam>
   public class ArgumentMapper<T> : MapperBase, IArgumentMapper<T>
      where T : class
   {
      #region Constants and Fields

      private readonly IObjectFactory engineFactory;

      #endregion

      #region Constructors and Destructors

      public ArgumentMapper([NotNull] IObjectFactory engineFactory)
      {
         this.engineFactory = engineFactory ?? throw new ArgumentNullException(nameof(engineFactory));
      }

      #endregion

      #region IArgumentMapper<T> Members

      public T Map(IDictionary<string, CommandLineArgument> arguments, T instance)
      {
         HashSet<CommandLineArgument> sharedArguments = new HashSet<CommandLineArgument>();

         foreach (var mapping in MappingList.FromType<T>())
         {
            if (mapping.IsOption())
            {
               var wasSet = SetOptionValue(instance, mapping, arguments);
               if (wasSet)
               {
                  sharedArguments.Add(mapping.CommandLineArgument);
                  ValidateProperty(instance, mapping.PropertyInfo);
               }
            }
            else
            {
               var wasSet = SetArgumentValue(instance, mapping, arguments);
               if (wasSet)
               {
                  sharedArguments.Add(mapping.CommandLineArgument);
                  ValidateProperty(instance, mapping.PropertyInfo);
               }
            }
         }

         CheckForUnmappedArguments(arguments, sharedArguments);
         return instance;
      }

      private void CheckForUnmappedArguments(IDictionary<string, CommandLineArgument> arguments, HashSet<CommandLineArgument> sharedArguments)
      {
         if (arguments.Count <= 0)
            return;

         foreach (var argument in arguments.Values.Where(arg => !sharedArguments.Contains(arg)))
         {
            UnmappedCommandLineArgument?.Invoke(this, new UnmappedCommandLineArgumentEventArgs(argument));
         }
      }

      /// <summary>Occurs when a command line argument of the given arguments dictionary could not be mapped to a arguments member</summary>
      public event EventHandler<UnmappedCommandLineArgumentEventArgs> UnmappedCommandLineArgument;

      /// <inheritdoc/>
      /// <summary>Maps the give argument dictionary to a new created instance.</summary>
      /// <param name="arguments">The arguments to map.</param>
      /// <returns>The instance of the class, the command line argument were mapped to</returns>
      /// <exception cref="T:System.IO.InvalidDataException">Option attribute can only be applied to boolean properties</exception>
      /// <exception cref="T:System.IO.InvalidDataException">Option attribute can only be applied to boolean properties</exception>
      public T Map(IDictionary<string, CommandLineArgument> arguments)
      {
         var instance = engineFactory.CreateInstance<T>();
         return Map(arguments, instance);
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

      private void ValidateProperty(T arguments, PropertyInfo propertyInfo)
      {
         foreach (var attribute in propertyInfo.GetCustomAttributes<ArgumentValidatorAttribute>(true))
         {
            var instance = engineFactory.CreateInstance(attribute.Type);
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