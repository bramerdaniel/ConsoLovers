// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MeasuredSize.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

public struct MeasuredSize
{
   #region Public Properties

   /// <summary>Gets the height the <see cref="IRenderable"/> wants to have for the given available width.</summary>
   public int Height { get; set; }
   
   /// <summary>Gets the minimum width.</summary>
   public int MinWidth { get; set; }

   #endregion
}