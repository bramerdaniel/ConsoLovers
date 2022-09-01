// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArgumentInitializationScopeAttribute.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   using System;

   [AttributeUsage(AttributeTargets.Class)]
   public class ArgumentInitializationScopeAttribute : MenuAttribute
   {
      // TODO Write unit test for this attribute functionality 

      #region Public Properties

      public bool Shared { get; set; } = true;

      #endregion
   }
}