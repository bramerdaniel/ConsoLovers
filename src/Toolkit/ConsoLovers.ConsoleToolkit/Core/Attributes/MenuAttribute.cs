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
      protected MenuAttribute()
      {
      }

      #endregion
   }
}