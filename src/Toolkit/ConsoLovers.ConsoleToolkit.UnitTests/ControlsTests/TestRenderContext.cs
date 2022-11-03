// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestRenderContext.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.UnitTests.ControlsTests;

using System.Collections.Generic;

using ConsoLovers.ConsoleToolkit.Controls;

public class TestRenderContext : IRenderContext
{
   public RenderSize Measure(IRenderable renderable, int availableWidth)
   {
      return renderable.Measure(this, availableWidth);
   }

   public IEnumerable<Segment> RenderLine(IRenderable renderable, int line)
   {
      throw new System.NotImplementedException();
   }

   public RenderSize GetMeasuredSize(IRenderable renderable)
   {
      throw new System.NotImplementedException();
   }
}