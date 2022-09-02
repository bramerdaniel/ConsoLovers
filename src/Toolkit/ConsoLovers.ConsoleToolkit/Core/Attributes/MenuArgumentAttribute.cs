// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MenuArgumentAttribute.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   using System;

   /// <summary>Attribute for a consolovers command that is displayed in a consolovers menu</summary>
   /// <seealso cref="MenuAttribute"/>
   [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
   public class MenuArgumentAttribute : MenuAttribute
   {
      #region Constructors and Destructors

      public MenuArgumentAttribute(string displayName)
      {
         DisplayName = displayName;
      }

      public MenuArgumentAttribute()
      {
      }

      #endregion

      #region Public Properties

      /// <summary>Gets or sets the display name that is used for the menu.</summary>
      public string DisplayName { get; set; }

      /// <summary>Gets or sets the sort order for displaying the attribute in the menu.</summary>
      public int DisplayOrder { get; set; } = int.MaxValue / 2;

      /// <summary>Gets or sets a value indicating whether this argument is a password.</summary>
      public bool IsPassword { get; set; } = false;

      /// <summary>Gets or sets the visibility of the argument.</summary>
      public ArgumentVisibility Visibility { get; set; } = ArgumentVisibility.NotSpecified;

      /// <summary>Gets or sets a user hint text that is displayed during initialization of the argument.</summary>
      public string InitializationHint { get; set; }

      #endregion
   }

   [Flags]
   public enum ArgumentVisibility
   {
      Hidden = 0,
      NotSpecified = 0x1,
      InMenu = 0x2,
      InInitialization = 0x4
   }
}