// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestRendererSetup.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.UnitTests.Setups;

using ConsoLovers.ConsoleToolkit.Controls;
using ConsoLovers.ConsoleToolkit.UnitTests.ControlsTests;

using FluentSetups;

[FluentSetup(typeof(TestRenderer))]
public partial class TestRendererSetup
{
   public string Render(IRenderable renderable)
   {
      return Done().Render(renderable);
   }
}