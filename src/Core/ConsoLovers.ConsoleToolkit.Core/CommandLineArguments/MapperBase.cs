// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MapperBase.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
   using ConsoLovers.ConsoleToolkit.Core.Exceptions;
   using System;
   using System.Globalization;
   using System.Linq;

   /// <summary>Non generic base class for the <see cref="ArgumentMapper{T}"/> that provides static helper methods</summary>
   public abstract class MapperBase
   {
      #region Methods

      internal static bool SetArgumentValue<T>(T instance, MappingInfo mappingInfo, CommandLineArgumentList arguments)
      {
         if (mappingInfo.IsCommand())
            return false;

         var propertyInfo = mappingInfo.PropertyInfo;
         var attribute = mappingInfo.CommandLineAttribute;
         var count = 0;

         string stringValue;

         foreach (var name in mappingInfo.GetNames())
         {
            if (arguments.TryGetValue(name, out var argument))
            {
               if (argument.Value == null)
               {
                  if (mappingInfo.HasIndex && !argument.HasArgumentSign())
                  {
                     mappingInfo.DisableNameMatch();
                     break;
                  }

                  throw new CommandLineArgumentException($"The value of the argument '{argument.Name}' was not specified.") { Reason = ErrorReason.ArgumentWithoutValue };
               }

               stringValue = GetValue(argument.Value, attribute.TrimQuotation());
               propertyInfo.SetValue(instance, ConvertValue(propertyInfo.PropertyType, stringValue, (t, v) => CreateErrorMessage(t, v, name)), null);
               mappingInfo.CommandLineArgument = argument;
               argument.Mapped = true;

               if (!mappingInfo.IsShared())
                  arguments.Remove(argument);

               count++;
            }
         }

         if (count == 0 && TryGetByIndex(arguments, mappingInfo, out var argumentByIndex))
         {
            if (argumentByIndex != null && argumentByIndex.Value == null)
            {
               stringValue = GetValue(argumentByIndex.Name, attribute.TrimQuotation());

               propertyInfo.SetValue(instance, ConvertValue(propertyInfo.PropertyType, stringValue, (t, v) => CreateErrorMessage(t, v, attribute.Name)), null);
               mappingInfo.CommandLineArgument = argumentByIndex;
               arguments.Remove(argumentByIndex);
               count++;
            }
         }

         if (count > 1)
            throw new AmbiguousCommandLineArgumentsException($"The value for the argument '{mappingInfo.Name}' was specified multiple times.");

         if (count == 0 && attribute.IsRequired())
            throw new MissingCommandLineArgumentException(mappingInfo.Name);

         return count > 0;
      }

      internal static bool SetOptionValue<T>(T instance, MappingInfo mappingInfo, CommandLineArgumentList arguments)
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
                  arguments.Remove(argument);

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

      private static bool TryGetByIndex(CommandLineArgumentList arguments, MappingInfo mappingInfo, out CommandLineArgument argument)
      {
         var index = mappingInfo.CommandLineAttribute.GetIndex();
         if (index >= 0)
         {
            foreach (var commandLineArgument in arguments)
            {
               if (commandLineArgument.Index == index)
               {
                  var betterNameMatch = mappingInfo.GetNameMatch(commandLineArgument.Name);
                  if (betterNameMatch == null)
                  {
                     argument = commandLineArgument;
                     return true;
                  }

                  break;
               }
            }
         }

         argument = null;
         return false;
      }

      #endregion
   }
}