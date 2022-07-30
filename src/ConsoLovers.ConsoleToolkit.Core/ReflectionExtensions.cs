// --------------------------------------------------------------------------------------------------------------------
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

using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

public static class ReflectionExtensions
{
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

   public static T GetAttribute<T>(this PropertyInfo propertyInfo) where T : Attribute
   {
      return propertyInfo.GetCustomAttributes<T>(true).FirstOrDefault();
   }
}
