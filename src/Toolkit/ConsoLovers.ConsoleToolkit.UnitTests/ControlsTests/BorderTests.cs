// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BorderTests.cs" company="ConsoLovers">
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
public class BorderTests
{
   #region Public Methods and Operators

   [TestMethod]
   public void EnsureBorderTypeIsRespected()
   {
      var border = Setup.Border()
         .WithContent(new CText("Some TEXT"))
         .WithBorder(Borders.Doubled)
         .Done();

      var renderer = Setup.TestRenderer().Done();

      var renderedText = renderer
         .Render(border);

      var value = @"
╔═════════╗
║Some TEXT║
╚═════════╝
".Trim();

      renderedText.Should().Be(value);
   }

   [TestMethod]
   public void EnsureBorderWithLeftRightPaddingIsRenderedCorrectly()
   {
      var border = Setup.Border()
         .WithContent(new CText($"First{Environment.NewLine}Second"))
         .WithPadding(new Thickness(2, 0, 3, 0))
         .Done();

      var renderer = Setup.TestRenderer().Done();

      var renderedText = renderer
         .Render(border);

      var value = @"
┌───────────┐
│  First    │
│  Second   │
└───────────┘
".Trim();

      renderedText.Should().Be(value);
   }

   [TestMethod]
   public void EnsureBorderWithMultilineTextIsRenderedCorrectly()
   {
      var border = Setup.Border()
         .WithContent(new CText($"First{Environment.NewLine}Second"))
         .Done();

      var renderer = Setup.TestRenderer().Done();

      var renderedText = renderer
         .Render(border);

      var value = @"
┌──────┐
│First │
│Second│
└──────┘
".Trim();

      renderedText.Should().Be(value);
   }

   [TestMethod]
   public void EnsureBorderWithTextIsRenderedCorrectly()
   {
      var border = Setup.Border()
         .WithContent(new CText("X"))
         .Done();

      var renderer = Setup.TestRenderer().Done();

      var renderedText = renderer
         .Render(border);

      var value = @"
┌─┐
│X│
└─┘
".Trim();

      renderedText.Should().Be(value);
   }

   [TestMethod]
   public void EnsureBorderWithTobBottomPaddingIsRenderedCorrectly()
   {
      var border = Setup.Border()
         .WithContent(new CText($"First{Environment.NewLine}Second"))
         .WithPadding(new Thickness(0, 1, 0, 2))
         .Done();

      var renderer = Setup.TestRenderer().Done();

      var renderedText = renderer
         .Render(border);

      var value = @"
┌──────┐
│      │
│First │
│Second│
│      │
│      │
└──────┘
".Trim();

      renderedText.Should().Be(value);
   }

   [TestMethod]
   public void EnsureLongTextIsRenderedCorrectly()
   {
      var border = Setup.Border()
         .WithContent(new CText("Long text but OK"))
         .Done();

      var renderer = Setup.TestRenderer().Done();

      var renderedText = renderer
         .Render(border);

      var value = @"
┌────────────────┐
│Long text but OK│
└────────────────┘
".Trim();

      renderedText.Should().Be(value);
   }

   [TestMethod]
   public void EnsureSingleLineTextIsRenderedCorrectly()
   {
      var border = Setup.Border()
         .WithContent(null)
         .Done();

      var renderer = Setup.TestRenderer().Done();

      var renderedText = renderer
         .Render(border);

      var value = @"
┌┐
└┘
".Trim();

      renderedText.Should().Be(value);
   }

   #endregion
}