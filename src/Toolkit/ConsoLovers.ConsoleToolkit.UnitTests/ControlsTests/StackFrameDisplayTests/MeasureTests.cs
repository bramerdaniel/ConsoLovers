﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MeasureTests.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.UnitTests.ControlsTests.StackFrameDisplayTests;

using System;
using System.Diagnostics;

using ConsoLovers.ConsoleToolkit.Controls;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class MeasureTests
{
   #region Public Methods and Operators

   [TestMethod]
   public void EnsureHeightIsMeasuredCorrectly()
   {
      var frame = new StackFrame("Test.cs", 25);
      var target = new StackFrameDisplay(frame);

      target.Measure(int.MaxValue).Height.Should().Be(1);
      // target.Measure(5).Height.Should().Be(2);
   }

   [TestMethod]
   public void EnsureWidthIsMeasuredCorrectly()
   {
      var frame = new StackFrame("Test.cs", 25);
      var target = new StackFrameDisplay(frame);

      target.Measure(int.MaxValue).Width.Should().Be(1);
      // target.Measure(5).Height.Should().Be(2);
   }

   #endregion

   #region Methods

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

   #endregion
}