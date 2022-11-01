// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RenderSize.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

using System.Diagnostics;

[DebuggerDisplay("Height: {Height}, Width: {Width}")]
public struct RenderSize
{
   #region Public Properties

   /// <summary>Gets the height the <see cref="IRenderable"/> wants to have for the given available width.</summary>
   public int Height { get; set; }
   
   /// <summary>Gets the minimum width.</summary>
   public int Width { get; set; }

   /// <summary>Gets the empty.</summary>
   public static RenderSize Empty { get; } = new() { Height = 0, Width = 0 };

   #endregion
}