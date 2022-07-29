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

      #region Public Events

      /// <summary>Occurs when command line argument could be mapped to a specific property of the specified class of type.</summary>
      // TODO raise it
      public event EventHandler<MapperEventArgs> MappedCommandLineArgument;

      /// <summary>Occurs when a command line argument of the given arguments dictionary could not be mapped to a arguments member</summary>
      public event EventHandler<MapperEventArgs> UnmappedCommandLineArgument;

      #endregion

      #region IArgumentMapper<T> Members


      [Obsolete("Use overload with CommandLineArgumentList")]
      public T Map(IDictionary<string, CommandLineArgument> arguments, T instance)
      { 
         return Map(CommandLineArgumentList.FromDictionary(arguments), instance);

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




      public T Map(CommandLineArgumentList arguments)
      {
         var instance = engineFactory.CreateInstance<T>();
         return Map(arguments, instance);
      }

      public T Map(CommandLineArgumentList arguments, T instance)
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

      /// <inheritdoc/>
      /// <summary>Maps the give argument dictionary to a new created instance.</summary>
      /// <param name="arguments">The arguments to map.</param>
      /// <returns>The instance of the class, the command line argument were mapped to</returns>
      /// <exception cref="T:System.IO.InvalidDataException">Option attribute can only be applied to boolean properties</exception>
      /// <exception cref="T:System.IO.InvalidDataException">Option attribute can only be applied to boolean properties</exception>
      [Obsolete("Use overload with CommandLineArgumentList")]
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

      private void CheckForUnmappedArguments(IDictionary<string, CommandLineArgument> arguments, HashSet<CommandLineArgument> sharedArguments, T instance)
      {
         if (arguments.Count <= 0)
            return;

         foreach (var argument in arguments.Values.Where(arg => !sharedArguments.Contains(arg)))
            UnmappedCommandLineArgument?.Invoke(this, new MapperEventArgs(argument, null, instance));
      }


      private void CheckForUnmappedArguments(CommandLineArgumentList arguments, HashSet<CommandLineArgument> sharedArguments, T instance)
      {
         if (arguments.Count <= 0)
            return;

         foreach (var argument in arguments.Where(arg => !sharedArguments.Contains(arg)))
            UnmappedCommandLineArgument?.Invoke(this, new MapperEventArgs(argument, null, instance));
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