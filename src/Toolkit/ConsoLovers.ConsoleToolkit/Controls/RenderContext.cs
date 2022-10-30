// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RenderContext.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

public class RenderContext : IRenderContext
{
   public int AvailableWidth { get; set; }

   /// <summary>Gets or sets the size.</summary>
   public MeasuredSize Size { get; set; }
}