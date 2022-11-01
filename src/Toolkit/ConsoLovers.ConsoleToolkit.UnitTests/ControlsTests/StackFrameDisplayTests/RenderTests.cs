// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RenderTests.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.UnitTests.ControlsTests.StackFrameDisplayTests;

using ConsoLovers.ConsoleToolkit.Controls;
using ConsoLovers.ConsoleToolkit.UnitTests.Setups;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class RenderTests
{
   [TestMethod]
   public void EnsureNormalStackTraceIsRenderedCorrectly()
   {
      var frame = StackFrameGenerator.CreateNormal();
      var target = new StackFrameDisplay(frame);

      var renderer = Setup.TestRenderer()
         .WithConsoleWidth(int.MaxValue)
         .Done();

      renderer.Render(target).Should().Be("at void NormalMethod() in StackFrameGenerator.cs:41");
   }
}