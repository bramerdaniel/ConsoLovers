// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRenderContext.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls
{
   using System.Collections.Generic;

   public interface IRenderContext
   {
      RenderSize Measure(IRenderable renderable, int availableWidth);
      
      IEnumerable<Segment> RenderLine(IRenderable renderable, int line);

      RenderSize GetMeasuredSize(IRenderable renderable);
   }
}