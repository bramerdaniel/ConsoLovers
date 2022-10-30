// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ListTests.cs" company="ConsoLovers">
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
public class ListTests
{
   [TestMethod]
   public void EnsureListWithTextIsRenderedCorrectly()
   {
      var list = Setup.List()
         .WithItem(CText.FromString("First"))
         .WithItem(CText.FromString("Second"))
         .Done();

      var renderedText = Setup.TestRenderer()
         .WithConsoleWidth(10)
         .Render(list);

      renderedText.Should().Be(@"> First
  Second");
   }

   [TestMethod]
   public void EnsureListWithSelectionIsRenderedCorrectly()
   {
      var list = Setup.List()
         .WithItem(CText.FromString("First"))
         .WithItem(CText.FromString("Second"))
         .WithItem(CText.FromString("Third"))
         .WithSelectedIndex(1)
         .Done();

      var renderedText = Setup.TestRenderer()
         .WithConsoleWidth(10)
         .Render(list);

      renderedText.Should().Be(@"  First
> Second
  Third");
   }

   [TestMethod]
   public void EnsureTextAndBordersAreRenderedCorrectly()
   {
      var list = Setup.List()
         .WithItem((CText)"Yes")
         .WithItem(new CBorder(new CText("Oha")))
         .WithItem(new CText($"Very {Environment.NewLine}long text"))
         .WithItem(new CBorder(new CText($"Very {Environment.NewLine}long text")))
         .WithItem((CText)"No")
         .Done();

      var renderedText = Setup.TestRenderer()
         .Render(list);

      renderedText.Should().Be(@"> Yes
  ┌───┐
  │Oha│
  └───┘
  Very 
  long text
  ┌─────────┐
  │Very     │
  │long text│
  └─────────┘
  No");
   }

   [TestMethod]
   public void EnsureListWithRuleIsRenderedCorrectly()
   {
      var list = Setup.List()
         .WithItem(CText.FromString("First"))
         .WithItem(new CRule(null))
         .WithItem(CText.FromString("Second"))
         .Done();

      var renderedText = Setup.TestRenderer()
         .WithConsoleWidth(10)
         .Render(list);

      renderedText.Should().Be(@"> First
  --------
  Second");
   }
}