﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GenericSelectorTests.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.UnitTests.ControlsTests.SelectorTest;

using System;

using ConsoLovers.ConsoleToolkit.Controls;
using ConsoLovers.ConsoleToolkit.UnitTests.Setups;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class GenericSelectorTests
{
   [TestMethod]
   public void EnsureTypeCanBeIRenderableAndAllAreRenderedCorrectly()
   {
      var list = Setup.Selector<IRenderable>()
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

}