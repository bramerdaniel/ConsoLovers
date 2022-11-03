// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MeasureTests.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.UnitTests.ControlsTests.TextTests;

using System;

using ConsoLovers.ConsoleToolkit.Controls;
using ConsoLovers.ConsoleToolkit.UnitTests.Setups;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class MeasureTests
{
   [TestMethod]
   public void EnsureSingleLineTextIsMeasuredCorrectly()
   {
      var text = Setup.Text().WithValue("A length of 14")
         .Done();

      var size = text.Measure(null, 100);
      size.Height.Should().Be(1);
      size.Width.Should().Be(14);
   }

   [TestMethod]
   public void EnsureMultilineTextIsMeasuredCorrectly()
   {
      var text = Setup.Text().WithValue($"FirstLine{Environment.NewLine}SecondLine")
         .Done();

      var size = text.Measure(null, 100);
      size.Height.Should().Be(2);
      size.Width.Should().Be(10);
   }

   [TestMethod]
   public void EnsureTextWithRightAlignmentIsMeasuredCorrectly()
   {
      var text = Setup.Text().WithValue("Value")
         .WithAlignment(Alignment.Right)
         .Done();

      var size = text.Measure(null, 10);
      size.Height.Should().Be(1);
      size.Width.Should().Be(5);
   }

   [TestMethod]
   [Ignore]
   public void EnsureTextWithCenterAlignmentIsMeasuredCorrectly()
   {
      var text = Setup.Text().WithValue("Value")
         .WithAlignment(Alignment.Center)
         .Done();

      var size = text.Measure(null, 10);
      size.Height.Should().Be(1);
      size.Width.Should().Be("  Value".Length);
   }
}