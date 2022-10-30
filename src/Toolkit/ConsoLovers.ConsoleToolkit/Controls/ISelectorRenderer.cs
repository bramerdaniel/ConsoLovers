// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ISelectorRenderer.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

using System.Collections.Generic;

internal interface ISelectorRenderer : IKeyInputHandler
{
   IEnumerable<Segment> RenderLine(IRenderContext context, int line);

   MeasuredSize Measure(int availableWidth);
}