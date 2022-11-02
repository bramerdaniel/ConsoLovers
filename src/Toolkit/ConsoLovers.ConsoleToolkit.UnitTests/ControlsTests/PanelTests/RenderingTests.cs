// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StackPanelTests.cs" company="ConsoLovers">
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
public class RenderingTests
{
   [TestMethod]
   public void EnsureBorderWithMultilineTextIsRenderedCorrectly()
   {
      var border = Setup.Panel()
         .WithChild(new Border(new CText($"First{Environment.NewLine}Second")))
         .WithChild(new Border(new CText($"First{Environment.NewLine}Second")))
         .Done();

      var renderer = Setup.TestRenderer().Done();

      var renderedText = renderer
         .Render(border);

      var value = @"
┌──────┐┌──────┐
│First ││First │
│Second││Second│
└──────┘└──────┘
".Trim();

      renderedText.Should().Be(value);
   }

   [TestMethod]
   public void EnsureBordersAndTextIsRenderedCorrectly()
   {
      var border = Setup.Panel()
         .WithChild(new Border(new CText($"First{Environment.NewLine}Second")))
         .WithChild(new CText($"{Environment.NewLine}First{Environment.NewLine}Second"))
         .WithChild(new Border(new CText($"First{Environment.NewLine}Second")))
         .Done();

      var renderer = Setup.TestRenderer().Done();

      var renderedText = renderer
         .Render(border);

      var value = @"
┌──────┐      ┌──────┐
│First │First │First │
│Second│Second│Second│
└──────┘      └──────┘
".Trim();

      renderedText.Should().Be(value);
   }

   [TestMethod]
   [Ignore]
   public void EnsureOverflowIsRenderedCorrectly()
   {
      var border = Setup.Panel()
         .WithChild(new Border(new CText("xxx")))
         .WithChild(new Border(new CText("abc")))
         .WithChild(new Border(new CText("123")))
         .Done();

      var renderer = Setup.TestRenderer()
         .WithConsoleWidth(11)
         .Done();

      var renderedText = renderer
         .Render(border);

      var value = @"
┌───┐┌───┐
│xxx││abc│
└───┘└───┘
┌───┐
│123│
└───┘
".Trim();

      renderedText.Should().Be(value);
   }

   [TestMethod]
   public void EnsureTextAndListIsRenderedCorrectly()
   {
      var selector = Setup.Selector<string>()
         .WithItem("Yes")
         .WithItem("No")
         .WithItem("Cancel")
         .WithOrientation(Orientation.Horizontal)
         .WithoutSelector()
         .Done();

      var border = Setup.Panel()
         .WithChild(new CText("Label : "))
         .WithChild(selector)
         .Done();

      var renderer = Setup.TestRenderer().Done();

      var renderedText = renderer
         .Render(border);

      renderedText.Should().Be("Label : Yes No Cancel");
   }

}