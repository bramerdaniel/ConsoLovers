// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IInteractiveRenderable.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

using System;

public interface IInteractiveRenderable : IRenderable
{
   /// <summary>Occurs when state of the <see cref="IRenderable"/> has changed and it needs to be rendered again.</summary>
   event EventHandler Invalidated ;
}