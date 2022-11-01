// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MeasureTests.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.UnitTests.ControlsTests.StackTraceDisplayTests;

using System;
using System.Diagnostics;

using ConsoLovers.ConsoleToolkit.Controls;
using ConsoLovers.ConsoleToolkit.UnitTests.Setups;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class MeasureTests
{
   [TestMethod]
   public void EnsureHeightIsMeasuredCorrectly()
   {
      var stackTrace = CreateStackTrace(4);

      var target = new StackTraceDisplay(stackTrace);

      var size = target.Measure(120);
      size.Height.Should().Be(4);
      size.Width.Should().Be(10);
   }

   private StackTrace CreateStackTrace(int depth)
   {
      try
      {
         RecursiveCall(depth - 2);
         throw new ArgumentException(nameof(depth));
      }
      catch (Exception e)
      {
         return new StackTrace(e);
      }
   }

   private int RecursiveCall(int depth)
   {
      if (depth == 0)
         throw new InvalidOperationException("End of recursion");
      
      depth--;
      return RecursiveCall(depth);
   }
}