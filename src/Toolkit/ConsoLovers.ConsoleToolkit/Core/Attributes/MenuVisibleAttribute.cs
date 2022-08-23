// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MenuVisibleAttribute.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   using System;

   [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
   public class MenuVisibleAttribute : Attribute
   {
      #region Constructors and Destructors

      /// <summary>Initializes a new instance of the <see cref="MenuVisibleAttribute"/> class.</summary>
      /// <param name="visible">if set to <c>true</c> [visible].</param>
      public MenuVisibleAttribute(bool visible)
      {
         Visible = visible;
      }

      /// <summary>Initializes a new instance of the <see cref="MenuVisibleAttribute"/> class.</summary>
      public MenuVisibleAttribute()
         : this(true)
      {
      }

      #endregion

      #region Public Properties

      /// <summary>Gets or sets a value indicating whether the decorated command/argument is visible or not.</summary>
      public bool Visible { get; set; }

      #endregion
   }
}