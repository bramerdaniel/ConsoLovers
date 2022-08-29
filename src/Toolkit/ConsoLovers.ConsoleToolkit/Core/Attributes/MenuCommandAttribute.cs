// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MenuCommandAttribute.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   using System;

   /// <summary>Attribute for a consolovers command that is displayed in a consolovers menu</summary>
   /// <seealso cref="MenuAttribute"/>
   [AttributeUsage(AttributeTargets.Property)]
   public class MenuCommandAttribute : MenuAttribute
   {
      #region Constructors and Destructors

      public MenuCommandAttribute(string displayName)
      {
         DisplayName = displayName;
      }

      public MenuCommandAttribute()
      {
      }

      #endregion

      #region Public Properties

      public string DisplayName { get; set; }

      public CommandVisibility Visibility { get; set; } = CommandVisibility.NotSpecified;
      
      public int DisplayOrder{ get; set; } = int.MaxValue / 2;

      /// <summary>Gets or sets the edit mode of the argument in the menu.</summary>
      public ArgumentInitializationModes ArgumentInitialization { get; set; }

      #endregion
   }

   public enum CommandVisibility
   {
      /// <summary>The default value when the value was not specified in the <see cref="MenuCommandAttribute"/></summary>
      NotSpecified,

      Visible,
      Hidden,
   }
}