// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestRenderContext.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.UnitTests.ControlsTests;

using ConsoLovers.ConsoleToolkit.Controls;

public class TestRenderContext : IRenderContext
{
   public RenderSize Measure(IRenderable renderable, int availableWidth)
   {
      return renderable.Measure(this, availableWidth);
   }
}