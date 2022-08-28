// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MenuAttribute.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   using System;

   [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
   public abstract class MenuAttribute : Attribute
   {
      #region Constructors and Destructors

      /// <summary>Initializes a new instance of the <see cref="MenuAttribute"/> class.</summary>
      /// <param name="visibleInMenu">if set to <c>true</c> [visibleInMenu].</param>
      protected MenuAttribute(bool visibleInMenu)
      {
         VisibleInMenu = visibleInMenu;
      }

      /// <summary>Initializes a new instance of the <see cref="MenuAttribute"/> class.</summary>
      protected MenuAttribute()
         : this(true)
      {
      }

      #endregion

      #region Public Properties

      /// <summary>Gets or sets a value indicating whether the decorated command/argument is visibleInMenu or not.</summary>
      public bool VisibleInMenu { get; set; }

      #endregion
   }
}