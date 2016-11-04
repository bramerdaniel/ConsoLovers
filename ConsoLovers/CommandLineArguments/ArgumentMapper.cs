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
   using System.Text;

   /// <summary>Class that can map a dictionary to an instance of a class, filling the properties.</summary>
   /// <typeparam name="T">The type of the class to create</typeparam>
   public class ArgumentMapper<T> : MapperBase , IArgumentMapper<T> 
   {
      #region Public Methods and Operators

      /// <summary>Maps the give argument dictionary to a new created instance.</summary>
      /// <param name="arguments">The arguments to map.</param>
      /// <returns>The instance of the class, the command line argument were mapped to</returns>
      /// <exception cref="System.IO.InvalidDataException">Option attribute can only be applied to boolean properties</exception>
      /// <exception cref="InvalidDataException">Option attribute can only be applied to boolean properties</exception>
      public T Map(IDictionary<string, string> arguments)
      {
         var instance = Activator.CreateInstance<T>();
         return Map(arguments, instance);
      }

      /// <summary>Maps the give argument dictionary to the given instance.</summary>
      /// <param name="arguments">The arguments to map.</param>
      /// <param name="instance">The instance to map the arguments to.</param>
      /// <returns>The instance of the class, the command line argument were mapped to</returns>
      /// <exception cref="System.IO.InvalidDataException">Option attribute can only be applied to boolean properties</exception>
      /// <exception cref="InvalidDataException">Option attribute can only be applied to boolean properties</exception>
      public T Map(IDictionary<string, string> arguments, T instance)
      {
         var usedNames = new Dictionary<string, PropertyInfo>();

         foreach (var propertyInfo in typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public))
         {
            var commandLineAttribute = (CommandLineAttribute)propertyInfo.GetCustomAttributes(typeof(CommandLineAttribute), true).FirstOrDefault();
            var argumentAttribute = commandLineAttribute as ArgumentAttribute;
            var commandAttributes = propertyInfo.GetCustomAttributes(typeof(CommandAttribute), true);

            if (commandAttributes.Any())
            {
               var commandCount = 0;
               bool optional = false;
               var attributes = commandAttributes.Cast<CommandAttribute>().ToArray();

               foreach (var commandAttribute in attributes)
               {
                  optional |= commandAttribute.Optional;
                  EnsureUnique<T>(usedNames, commandAttribute, propertyInfo);
                  if (SetPropertyValue(instance, propertyInfo, arguments, commandAttribute))
                     commandCount++;
               }

               if (commandCount == 1)
                  continue;

               var attributeStrings = attributes.Select(a => a.Name).OrderBy(n => n).Aggregate((acc, cur) => acc + "|" + cur);

               if (commandCount > 1)
                  throw new CommandLineArgumentException($"The command '{attributeStrings}' must be used exclusive.");
               if (!optional)
                  throw new CommandLineArgumentException($"The command '{attributeStrings}' must be used.");
            }
            else if (commandLineAttribute != null)
            {
               var propertyName = commandLineAttribute.Name ?? propertyInfo.Name;
               EnsureUnique<T>(usedNames, commandLineAttribute, propertyInfo);

               var trim = argumentAttribute != null && argumentAttribute.TrimQuotation;

               if (!SetPropertyValue(instance, propertyInfo, arguments, commandLineAttribute, trim))
               {
                  if (argumentAttribute != null && argumentAttribute.Required)
                     throw new MissingCommandLineArgumentException(propertyName);
               }
            }
         }

         return instance;
      }

      public string UnMap(T instance)
      {
         StringBuilder result = new StringBuilder();

         PropertyInfo[] propertyInfos = typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.Public);

         PropertyInfo command = propertyInfos.SingleOrDefault(x => x.GetCustomAttributes(typeof(CommandAttribute), true).Any());
         if (command != null)
         {
            var commandInt = (int)command.GetValue(instance, null);
            if (commandInt != default(int))
               result.Append("-" + command.GetValue(instance, null) + " ");
         }

         IEnumerable<PropertyInfo> arguments = propertyInfos.Where(x => x.GetCustomAttributes(typeof(ArgumentAttribute), true).Any());
         foreach (var argument in arguments)
         {
            var obj = argument.GetValue(instance, null);
            if (obj != null)
            {
               var stringValue = obj.ToString();
               if (!string.IsNullOrEmpty(stringValue))
               {
                  var argAttribute = (ArgumentAttribute)argument.GetCustomAttributes(typeof(ArgumentAttribute), true).First();
                  var p = !string.IsNullOrEmpty(argAttribute.Name) ? argAttribute.Name : argument.Name;

                  //int
                  int castResult;
                  var tryParse = int.TryParse(stringValue, out castResult);
                  if (tryParse)
                  {
                     if (castResult == default(int))
                        continue;
                  }

                  result.Append(p + "=\"" + stringValue + "\" ");
               }
            }
         }

         IEnumerable<PropertyInfo> boolOptions = propertyInfos.Where(x => x.GetCustomAttributes(typeof(OptionAttribute), true).Any());
         foreach (var option in boolOptions)
         {
            if (option.PropertyType == typeof(bool))
            {
               bool usedOption = (bool)option.GetValue(instance, null);
               if (usedOption)
                  result.Append("-" + ((OptionAttribute)option.GetCustomAttributes(typeof(OptionAttribute), true).First()).Name + " ");
            }
         }

         return result.ToString().TrimEnd(' ');
      }

      #endregion
   }
}