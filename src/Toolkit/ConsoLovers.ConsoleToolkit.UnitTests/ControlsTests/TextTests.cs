// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TextTests.cs" company="ConsoLovers">
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
public class TextTests
{
   [TestMethod]
   public void EnsureSingleLineTextIsRenderedCorrectly()
   {
      var text = Setup.Text().WithValue("Value")
         .Done();

      var renderedText = new TestRenderer().Render(text);
      renderedText.Should().Be("Value");
   }

   [TestMethod]
   public void EnsureMultilineTextIsRenderedCorrectly()
   {
      var text = Setup.Text().WithValue($"FirstLine{Environment.NewLine}SecondLine")
         .Done();

      var renderedText = new TestRenderer().Render(text);
      renderedText.Should().Be($"FirstLine{Environment.NewLine}SecondLine");
   }

   [TestMethod]
   public void EnsureTextWithRightAlignmentIsRenderedCorrectly()
   {
      var text = Setup.Text().WithValue("Value")
         .WithAlignment(Alignment.Right)
         .Done();

      var renderedText = Setup.TestRenderer()
         .WithConsoleWidth(10)
         .Render(text);

      renderedText.Should().Be("     Value");
   }

   [TestMethod]
   public void EnsureTextWithCenterAlignmentIsRenderedCorrectly()
   {
      var text = Setup.Text().WithValue("Value")
         .WithAlignment(Alignment.Center)
         .Done();

      var renderedText = Setup.TestRenderer()
         .WithConsoleWidth(10)
         .Render(text);

      renderedText.Should().Be("  Value");
   }

   [TestMethod]
   public void EnsureSingleLineTextIsWrappedCorrectly()
   {
      var text = Setup.Text().WithValue("Value Is To Long")
         .Done();

      var renderedText = Setup.TestRenderer()
         .WithConsoleWidth(10)
         .Render(text);

      renderedText.Should().Be(@"Value Is
To Long");
   }

   [TestMethod]
   public void EnsureSingleLineTextIsTrimmedCorrectlyIfItCanNotBeWrapped()
   {
      var text = Setup.Text().WithValue("ValueIsToLongAndMustBeWrapped")
         .Done();

      var renderedText = Setup.TestRenderer()
         .WithConsoleWidth(10)
         .Render(text);

      renderedText.Should().Be(@"ValueIsToL
ongAndMust
BeWrapped");
   }
}