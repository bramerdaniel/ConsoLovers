// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMouseAware.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

/// <summary>Interface for <see cref="IInteractiveRenderable"/> that want to know if the mouse is over them</summary>
public interface IMouseAware
{
   /// <summary>Gets or sets a value indicating whether the mouse is over the item or not.
   /// This is set by the rendering engine.</summary>
   public bool IsMouseOver { get; set; }
}