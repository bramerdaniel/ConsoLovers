// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InvalidationEventArgs.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

using System;

/// <summary>Event args containing the information about what was invalidated, and needs to be rendered again</summary>
/// <seealso cref="System.EventArgs"/>
public class InvalidationEventArgs : EventArgs
{
   #region Constructors and Destructors

   /// <summary>Initializes a new instance of the <see cref="InvalidationEventArgs"/> class.</summary>
   /// <param name="scope">The scope.</param>
   public InvalidationEventArgs(InvalidationScope scope)
   {
      Scope = scope;
   }

   #endregion

   #region Public Properties

   public InvalidationScope Scope { get; }

   #endregion
}

public enum InvalidationScope
{
   /// <summary>Only the style has changed, therefor only the raising <see cref="IRenderable"/> will be redrawn</summary>
   Style,

   All
}