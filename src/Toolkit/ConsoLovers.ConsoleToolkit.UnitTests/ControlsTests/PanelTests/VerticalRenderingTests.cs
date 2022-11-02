// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VerticalRenderingTests.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.UnitTests.ControlsTests.PanelTests;

using System;

using ConsoLovers.ConsoleToolkit.Controls;
using ConsoLovers.ConsoleToolkit.UnitTests.Setups;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class VerticalRenderingTests
{
   [TestMethod]
   public void EnsureBordersAreRenderedCorrectly()
   {
      var border = Setup.Panel()
         .WithOrientation(Orientation.Vertical)
         .WithChild(new Border(new CText($"First{Environment.NewLine}Second")))
         .WithChild(new Border(new CText($"First{Environment.NewLine}Second")))
         .Done();

      var renderer = Setup.TestRenderer().Done();
      var renderedText = renderer.Render(border);

      var value = @"
┌──────┐
│First │
│Second│
└──────┘
┌──────┐
│First │
│Second│
└──────┘
".Trim();

      renderedText.Should().Be(value);
   }

   [TestMethod]
   public void EnsureNestedPanelsWithBordersAreRenderedCorrectly()
   {
      var first = Setup.Panel()
         .WithOrientation(Orientation.Vertical)
         .WithChild(new Border(new CText($"ABC{Environment.NewLine}DEFG")))
         .WithChild(new Border(new CText($"Longer{Environment.NewLine}Than ABC")))
         .Done();

      var inner = Setup.Panel()
         .WithOrientation(Orientation.Horizontal)
         .WithChild(first)
         .WithChild(new Border(new CText($"Moment{Environment.NewLine}Alabama")))
         .Done();

      var renderer = Setup.TestRenderer().Done();
      var renderedText = renderer.Render(inner);

      var value = @"
┌────┐    ┌───────┐
│ABC │    │Moment │
│DEFG│    │Alabama│
└────┘    └───────┘
┌────────┐         
│Longer  │         
│Than ABC│         
└────────┘         
".TrimEnd('\r', '\n').TrimStart();

      renderedText.Should().Be(value);
   }
  
   [TestMethod]
   public void EnsurePanelsAndTextAreRenderedCorrectly()
   {
      var panel = Setup.Panel()
         .WithOrientation(Orientation.Vertical)
         .WithChild(new CText("A long text"))
         .WithChild(new Border(new CText("Peter")))
         .Done();

      var renderer = Setup.TestRenderer().Done();
      var renderedText = renderer.Render(panel);

      var value = @"
A long text
┌─────┐
│Peter│
└─────┘      
".Trim();

      renderedText.Should().Be(value);
   }

}