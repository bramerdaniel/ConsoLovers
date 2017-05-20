// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MapperBase.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.CommandLineArguments
{
   using System;
   using System.Collections.Generic;
   using System.Globalization;
   using System.Linq;
   using System.Reflection;

   /// <summary>Non generic base cclass for the <see cref="ArgumentMapper{T}"/> that provides static helper methods</summary>
   public abstract class MapperBase
   {
      #region Methods

      internal static void EnsureUnique<T>(Dictionary<string, PropertyInfo> usedNames, CommandLineAttribute commandLineAttribute, PropertyInfo propertyInfo)
      {
         var names = new List<string> { commandLineAttribute.Name ?? propertyInfo.Name };
         names.AddRange(commandLineAttribute.Aliases);

         foreach (var name in names)
         {
            PropertyInfo firstProperty;
            if (usedNames.TryGetValue(name, out firstProperty))
            {
               var message = $"The properties '{firstProperty.Name}' and '{propertyInfo.Name}' of the class '{typeof(T).Name}' define both a name (or alias) called '{name}'";
               throw new CommandLineAttributeException(message) { Name = name, FirstProperty = firstProperty, SecondProperty = propertyInfo };
            }
         }

         foreach (var name in names)
         {
            usedNames[name] = propertyInfo;
         }
      }

      internal static bool SetPropertyValue<T>(T instance, PropertyInfo propertyInfo, IDictionary<string, CommandLineArgument> arguments, CommandLineAttribute attribute, bool trim = false)
      {
         var count = 0;
         var propertyName = attribute.Name ?? propertyInfo.Name;

         string stringValue;
         CommandLineArgument argument;
         if (arguments.TryGetValue(propertyName, out argument))
         {
            stringValue = GetValue(argument.Value, trim);

            propertyInfo.SetValue(instance, ConvertValue(propertyInfo.PropertyType, stringValue, (t, v) => CreateErrorMessage(t, v, propertyName)), null);
            arguments.Remove(propertyName);
            count++;
         }

         foreach (var alias in attribute.Aliases)
         {
            if (arguments.TryGetValue(alias, out argument))
            {
               stringValue = GetValue(argument.Value, trim);

               propertyInfo.SetValue(instance, ConvertValue(propertyInfo.PropertyType, stringValue, (t, v) => CreateErrorMessage(t, v, propertyName)), null);
               arguments.Remove(alias);
               count++;
            }
         }

         if (count > 1)
            throw new CommandLineArgumentException($"Multiple aruments for '{propertyName}'-command is not allowed.");
         return count == 1;
      }

      /// <summary>Converts the given string value to the expected target type when possible.</summary>
      /// <param name="targetType">Type of the target.</param>
      /// <param name="value">The value.</param>
      /// <param name="createErrorMessage">The function that is called when an error message for the <see cref="CommandLineArgumentException"/> is required.</param>
      /// <returns></returns>
      /// <exception cref="CommandLineArgumentException"></exception>
      protected internal static object ConvertValue(Type targetType, string value, Func<Type, string, string> createErrorMessage)
      {
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

      #endregion
   }
}