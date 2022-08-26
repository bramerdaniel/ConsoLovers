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
   [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
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
      
      public int DisplayOrder{ get; set; }

      /// <summary>Gets or sets the edit mode of the argument in the menu.</summary>
      public ArgumentInitializationModes ArgumentInitializationMode { get; set; }

      #endregion
   }
}