// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.CommandLineArguments
{
   using System;
   using System.Linq;

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

      #endregion
   }
}