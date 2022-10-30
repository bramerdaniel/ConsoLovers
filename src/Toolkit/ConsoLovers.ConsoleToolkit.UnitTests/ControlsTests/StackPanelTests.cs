// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StackPanelTests.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.UnitTests.ControlsTests;

using System;

using ConsoLovers.ConsoleToolkit.Controls;
using ConsoLovers.ConsoleToolkit.UnitTests.Setups;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class StackPanelTests
{
   [TestMethod]
   public void EnsureBorderWithMultilineTextIsRenderedCorrectly()
   {
      var border = Setup.StackPanel()
         .WithChild(new Border(new Text($"First{Environment.NewLine}Second")))
         .WithChild(new Border(new Text($"First{Environment.NewLine}Second")))
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
      var border = Setup.StackPanel()
         .WithChild(new Border(new Text($"First{Environment.NewLine}Second")))
         .WithChild(new Text($"{Environment.NewLine}First{Environment.NewLine}Second"))
         .WithChild(new Border(new Text($"First{Environment.NewLine}Second")))
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

}