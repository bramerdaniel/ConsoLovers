// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MapperBase.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
   using System;
   using System.Collections.Generic;
   using System.Globalization;
   using System.Linq;
   using System.Reflection;

   /// <summary>Non generic base class for the <see cref="ArgumentMapper{T}"/> that provides static helper methods</summary>
   public abstract class MapperBase
   {
      #region Methods

      internal static bool SetArgumentValue<T>(T instance, MappingInfo mappingInfo, IDictionary<string, CommandLineArgument> arguments)
      {
         PropertyInfo propertyInfo = mappingInfo.PropertyInfo;
         CommandLineAttribute attribute = mappingInfo.CommandLineAttribute;
         var count = 0;
         bool trim = attribute.TrimQuotation();

         CommandLineArgument argument;
         string stringValue;

         foreach (var name in mappingInfo.GetNames())
         {
            if (arguments.TryGetValue(name, out argument))
            {
               if (argument.Value == null)
                  throw new CommandLineArgumentException($"The value of the argument '{argument.Name}' was not specified.") { Reason = ErrorReason.ArgumentWithoutValue };

               stringValue = GetValue(argument.Value, trim);
               propertyInfo.SetValue(instance, ConvertValue(propertyInfo.PropertyType, stringValue, (t, v) => CreateErrorMessage(t, v, name)), null);
               mappingInfo.CommandLineArgument = argument;
               argument.Mapped = true;

               if (!mappingInfo.IsShared())
                  arguments.Remove(name);

               count++;
            }
         }

         if (TryGetByIndex(arguments, mappingInfo, out var entry))
         {
            argument = entry.Value;
            if (argument != null && argument.Value == null)
            {
               stringValue = GetValue(argument.Name, trim);

               propertyInfo.SetValue(instance, ConvertValue(propertyInfo.PropertyType, stringValue, (t, v) => CreateErrorMessage(t, v, attribute.Name)), null);
               arguments.Remove(entry.Key);
               count++;
            }
         }

         if (count > 1)
            throw new AmbiguousCommandLineArgumentsException($"The value for the argument '{mappingInfo.Name}' was specified multiple times.");

         if (count == 0 && attribute.IsRequired())
            throw new MissingCommandLineArgumentException(mappingInfo.Name);

         return count > 0;
      }

      internal static bool SetOptionValue<T>(T instance, MappingInfo mappingInfo, IDictionary<string, CommandLineArgument> arguments)
      {
         foreach (var name in mappingInfo.GetNames())
         {
            if (arguments.TryGetValue(name, out var argument))
            {
               if (argument.Value != null)
                  throw new CommandLineArgumentException($"The option '{argument.Name}' was specified with a value. This is not allowed for option.")
                  {
                     Reason = ErrorReason.OptionWithValue
                  };

               mappingInfo.PropertyInfo.SetValue(instance, true, null);
               mappingInfo.CommandLineArgument = argument;
               argument.Mapped = true;

               if (!mappingInfo.IsShared())
                  arguments.Remove(name);

               return true;
            }
         }

         return false;
      }

      /// <summary>Converts the given string value to the expected target type when possible.</summary>
      /// <param name="targetType">Type of the target.</param>
      /// <param name="value">The value.</param>
      /// <param name="createErrorMessage">The function that is called when an error message for the <see cref="CommandLineArgumentException"/> is required.</param>
      /// <returns></returns>
      /// <exception cref="CommandLineArgumentException"></exception>
      protected internal static object ConvertValue(Type targetType, string value, Func<Type, string, string> createErrorMessage)
      {
         if (targetType.IsGenericType && targetType.GetGenericTypeDefinition() == typeof(Nullable<>))
         {
            if (string.IsNullOrEmpty(value) || value.Equals("null", StringComparison.InvariantCultureIgnoreCase))
               return null;

            targetType = targetType.GetGenericArguments().First();
         }

         if (targetType.IsEnum)
         {
            try
            {
               return Enum.Parse(targetType, value);
            }

            // ReSharper disable CatchAllClause
            catch (Exception ex)
            {
               // ReSharper restore CatchAllClause
               var names = Enum.GetNames(targetType);
               var realName = names.FirstOrDefault(n => n.Equals(value, StringComparison.InvariantCultureIgnoreCase));
               if (realName == null)
                  throw new CommandLineArgumentException(createErrorMessage(targetType, value), ex);

               return Enum.Parse(targetType, realName);
            }
         }

         if (string.IsNullOrEmpty(value) && targetType == typeof(bool))
            return true;

         try
         {
            return Convert.ChangeType(value, targetType, CultureInfo.InvariantCulture);
         }
         catch (Exception ex)
         {
            throw new CommandLineArgumentException(createErrorMessage(targetType, value), ex);
         }
      }

      protected static string CreateErrorMessage(Type targetType, string value, string name)
      {
         if (targetType.IsEnum)
         {
            var names = Enum.GetNames(targetType);
            var possibleValues = $"Possible values are {string.Join(", ", names)}.".Replace($", {names.Last()}", $" and {names.Last()}");
            return $"The value {value} of parameter {name} can not be converted into the expected type {targetType.FullName}. {possibleValues}";
         }

         return $"The value {value} of parameter {name} can not be converted into the expected type {targetType.FullName}";
      }

      private static string GetValue(string originalValue, bool trim)
      {
         return trim ? originalValue.Trim('"', '\'') : originalValue;
      }

      private static bool TryGetByIndex(IDictionary<string, CommandLineArgument> arguments, MappingInfo mappingInfo, out KeyValuePair<string, CommandLineArgument> argument)
      {
         var index = mappingInfo.CommandLineAttribute.GetIndex();
         if (index >= 0)
         {
            foreach (var commandLineArgument in arguments)
            {
               if (commandLineArgument.Value.Index == index)
               {
                  var betterNameMatch = mappingInfo.GetNameMatch(commandLineArgument.Value.Name);
                  if (betterNameMatch == null)
                  {
                     argument = commandLineArgument;
                     return true;
                  }

                  break;
               }
            }
         }

         argument = new KeyValuePair<string, CommandLineArgument>();
         return false;
      }

      #endregion
   }
}