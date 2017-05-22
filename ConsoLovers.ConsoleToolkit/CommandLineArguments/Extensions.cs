// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.CommandLineArguments
{
   using System;
   using System.Linq;
   using System.Reflection;

   public static class Extensions
   {
      #region Public Methods and Operators

      public static bool IsCommandType(this Type type)
      {
         var command = type.GetInterface(typeof(ICommand).FullName);
         if (command != null)
            return true;

         command = type.GetInterface(typeof(ICommand<>).FullName);
         return command != null;
      }

      public static T GetAttribute<T>(this PropertyInfo propertyInfo) where T : Attribute
      {
         return propertyInfo.GetCustomAttributes(typeof(T), true).FirstOrDefault() as T;
      }

      #endregion
   }
}