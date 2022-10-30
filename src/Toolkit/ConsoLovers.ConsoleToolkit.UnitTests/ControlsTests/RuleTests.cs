// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RuleTests.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.UnitTests.ControlsTests;

using ConsoLovers.ConsoleToolkit.Controls;
using ConsoLovers.ConsoleToolkit.UnitTests.Setups;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class RuleTests
{
   [TestMethod]
   public void EnsureRuleIsRenderedCorrectly()
   {
      var rule = Setup.Rule()
         .WithText(null)
         .Done();

      var renderedText = Setup.TestRenderer()
         .WithConsoleWidth(15)
         .Render(rule);

      renderedText.Should().Be("---------------");

      renderedText = Setup.TestRenderer()
         .WithConsoleWidth(10)
         .Render(rule);
      renderedText.Should().Be("----------");
   }

   [TestMethod]
   public void EnsureRuleWithTextIsRenderedCorrectly()
   {
      var rule = Setup.Rule()
         .WithText("Hello Again")
         .Done();

      var renderedText = Setup.TestRenderer()
         .WithConsoleWidth(21)
         .Render(rule);

      renderedText.Should().Be("-----Hello Again-----");
   }

   [TestMethod]
   public void EnsureRuleInsideBorderIsRenderedCorrectly()
   {
      var rule = Setup.Rule()
         .WithText(null)
         .Done();

      var renderedText = Setup.TestRenderer()
         .WithConsoleWidth(13)
         .Render(new CBorder(rule));

      var expected = @"
┌───────────┐
│-----------│
└───────────┘".Trim();

      renderedText.Should().Be(expected);
   }
}