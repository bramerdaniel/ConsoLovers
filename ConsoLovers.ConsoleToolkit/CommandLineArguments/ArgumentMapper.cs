// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArgumentMapper.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.CommandLineArguments
{
   using System;
   using System.Collections.Generic;
   using System.IO;
   using System.Linq;
   using System.Reflection;

   using JetBrains.Annotations;

   /// <summary>Class that can map a dictionary to an instance of a class, filling the properties.</summary>
   /// <typeparam name="T">The type of the class to create</typeparam>
   public class ArgumentMapper<T> : MapperBase, IArgumentMapper<T>
      where T : class
   {
      private readonly IEngineFactory engineFactory;

      #region Public Methods and Operators

      public ArgumentMapper([NotNull] IEngineFactory engineFactory)
      {
         if (engineFactory == null)
            throw new ArgumentNullException(nameof(engineFactory));
         this.engineFactory = engineFactory;
      }

      public T Map(IDictionary<string, CommandLineArgument> arguments, T instance)
      {
         var usedNames = new Dictionary<string, PropertyInfo>();

         foreach (var propertyInfo in typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public))
         {
            var commandLineAttribute = (CommandLineAttribute)propertyInfo.GetCustomAttributes(typeof(CommandLineAttribute), true).FirstOrDefault();
            if (commandLineAttribute == null)
               continue;

            var indexedArgument = commandLineAttribute as IndexedArgumentAttribute;
            if (indexedArgument != null)
            {
               var argument = arguments.Values.FirstOrDefault(x => x.Index == indexedArgument.Index);
               if (argument == null)
               {
                  if (indexedArgument.Required)
                     throw new MissingCommandLineArgumentException(propertyInfo.Name);
               }
               else
               {
                  propertyInfo.SetValue(instance, ConvertValue(propertyInfo.PropertyType, argument.Name, (t, v) => CreateErrorMessage(t, v, argument.Name)), null);
               }

               continue;
            }

            var argumentAttribute = commandLineAttribute as ArgumentAttribute;

            var propertyName = commandLineAttribute.Name ?? propertyInfo.Name;
            EnsureUnique<T>(usedNames, commandLineAttribute, propertyInfo);

            var trim = argumentAttribute != null && argumentAttribute.TrimQuotation;

            if (!SetPropertyValue(instance, propertyInfo, arguments, commandLineAttribute, trim))
            {
               if (argumentAttribute != null && argumentAttribute.Required)
                  throw new MissingCommandLineArgumentException(propertyName);
            }
         }

         return instance;
      }


      #endregion
      /// <summary>Maps the give argument dictionary to a new created instance.</summary>
      /// <param name="arguments">The arguments to map.</param>
      /// <returns>The instance of the class, the command line argument were mapped to</returns>
      /// <exception cref="System.IO.InvalidDataException">Option attribute can only be applied to boolean properties</exception>
      /// <exception cref="InvalidDataException">Option attribute can only be applied to boolean properties</exception>
      public T Map(IDictionary<string, CommandLineArgument> arguments)
      {
         var instance = engineFactory.CreateInstance<T>();
         return Map(arguments, instance);
      }
   }
}