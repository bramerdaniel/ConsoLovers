// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Extensions.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
   using System;
   using JetBrains.Annotations;

   public static class Extensions
   {
      #region Public Methods and Operators

      public static bool IsCommandType([NotNull] this Type type)
      {
         if (type == null)
            throw new ArgumentNullException(nameof(type));

         var command = type.GetInterface(typeof(ICommandBase).FullName!);
         return command != null;
      }

      public static bool IsRequired(this CommandLineAttribute attribute)
      {
         return attribute is ArgumentAttribute argumentAttribute && argumentAttribute.Required;
      }

      public static int GetIndex(this CommandLineAttribute attribute)
      {
         return attribute is ArgumentAttribute argumentAttribute ? argumentAttribute.Index : -1;
      }

      public static bool TrimQuotation(this CommandLineAttribute attribute)
      {
         return attribute is ArgumentAttribute argumentAttribute && argumentAttribute.TrimQuotation;
      }

      #endregion
   }
}