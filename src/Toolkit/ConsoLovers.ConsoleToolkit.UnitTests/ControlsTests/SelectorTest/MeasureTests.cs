// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MeasureTests.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.UnitTests.ControlsTests.SelectorTest;

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
      var list = Setup.Selector<string>()
         .WithOrientation(Orientation.Horizontal)
         .WithoutSelector()
         .WithItem("Yes")
         .WithItem("No")
         .Done();

      list.Measure(int.MaxValue).Height.Should().Be(1);

      list.Selector = "↑";
      list.Measure(int.MaxValue).Height.Should().Be(2);

      list.Orientation = Orientation.Vertical;
      list.Measure(int.MaxValue).Height.Should().Be(2);

      list.Add("Cancel");
      list.Measure(int.MaxValue).Height.Should().Be(3);
   }

   [TestMethod]
   public void EnsureHorizontalWidthIsComputedCorrectly()
   {
      var list = Setup.Selector<string>()
         .WithOrientation(Orientation.Horizontal)
         .WithoutSelector()
         .WithItem("Yes")
         .WithItem("No")
         .Done();

      list.Measure(int.MaxValue).Width.Should().Be(6);
   }
}