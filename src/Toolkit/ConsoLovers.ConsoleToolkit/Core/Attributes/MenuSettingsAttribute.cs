// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MenuSettingsAttribute.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   using System;

   [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
   public class MenuSettingsAttribute : MenuVisibleAttribute
   {
      #region Constructors and Destructors

      public MenuSettingsAttribute(string displayName)
      {
         DisplayName = displayName;
      }
      public MenuSettingsAttribute()
      {
      }

      #endregion

      #region Public Properties

      public string DisplayName { get; set; }

      public bool Visible { get; set; } = true;

      #endregion
   }
}