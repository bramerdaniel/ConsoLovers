// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MockSetup.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.UnitTests.Setups;

using ConsoLovers.ConsoleToolkit.UnitTests.ControlsTests;

public class MockSetup
{
   public TestRenderContext RenderContext()
   {
      return new TestRenderContext();
   }
}