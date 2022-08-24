﻿// --------------------------------------------------------------------------------------------------------------------
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

      public string DisplayName { get; set; }

      #endregion
   }
}