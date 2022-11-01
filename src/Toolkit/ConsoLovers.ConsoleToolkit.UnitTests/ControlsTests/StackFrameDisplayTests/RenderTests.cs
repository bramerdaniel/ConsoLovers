// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RenderTests.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.UnitTests.ControlsTests.StackFrameDisplayTests;

using System.Diagnostics;

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
      var target = new StackFrameDisplay(new StackFrame());
      target.RemoveSegment("Namespace");

      var renderer = Setup.TestRenderer()
         .WithConsoleWidth(int.MaxValue)
         .Done();

      renderer.Render(target).Should().Be("   at void RenderTests.EnsureNormalStackTraceIsRenderedCorrectly()");
   }

   [TestMethod]
   public void EnsureStackTraceWithFileNameIsRenderedCorrectly()
   {
      var target = new StackFrameDisplay(new StackFrame("Filename.cs", 123));
      target.RemoveSegment("Namespace");

      var renderer = Setup.TestRenderer()
         .WithConsoleWidth(int.MaxValue)
         .Done();

      renderer.Render(target).Should().Be("   at void RenderTests.EnsureStackTraceWithFileNameIsRenderedCorrectly() in Filename.cs:123");
   }
}