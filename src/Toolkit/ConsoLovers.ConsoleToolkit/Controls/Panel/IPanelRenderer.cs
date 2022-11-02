// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IPanelRenderer.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

using System.Collections.Generic;

public interface IPanelRenderer
{
   IEnumerable<Segment> RenderLine(IRenderContext context, int line);

   RenderSize Measure(int availableWidth);
}