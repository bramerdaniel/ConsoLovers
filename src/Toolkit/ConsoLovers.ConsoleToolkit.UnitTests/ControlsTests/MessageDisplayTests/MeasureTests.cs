// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MeasureTests.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.UnitTests.ControlsTests.MessageDisplayTests;

using ConsoLovers.ConsoleToolkit.UnitTests.Setups;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class MeasureTests
{
   [TestMethod]
   public void EnsureHeightIsMeasuredCorrectly()
   {
      var list = Setup.MessageDisplay()
         .WithTitle("Title")
         .WithMessage("Message")
         .Done();

      var size = list.Measure(int.MaxValue);
      size.Height.Should().Be(1);
      size.MinWidth.Should().Be(5 + 2 + 7);
   }

   [TestMethod]
   public void EnsureIsMeasuredCorrectlyWhenMultipleLinesAreRequired()
   {
      var list = Setup.MessageDisplay()
         .WithTitle("Title")
         .WithMessage("to long my friend")
         .Done();

      var size = list.Measure(17);
      size.Height.Should().Be(2);
      size.MinWidth.Should().Be(17);
   }

   [TestMethod]
   public void EnsureIsMeasuredCorrectly2()
   {
      var list = Setup.MessageDisplay()
         .WithTitle("Title")
         .WithMessage("to friend my long")
         .Done();

      var size = list.Measure(17);
      size.Height.Should().Be(2);
      size.MinWidth.Should().Be(16);
   }
}