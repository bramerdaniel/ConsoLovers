// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SelectorStyles.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

public class SelectorStyles
{
   /// <summary>Gets or sets the style the selected item will have.</summary>
   public RenderingStyle SelectionStyle { get; set; } = DefaultStyles.SelectionStyle;

   /// <summary>Gets or sets the style the item the mouse is over will have.</summary>
   public RenderingStyle MouseOverStyle { get; set; } = DefaultStyles.MouseOverStyle;
}