// --------------------------------------------------------------------------------------------------------------------
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
         .WithItem((Text)"Yes")
         .WithItem(new Border(new Text("Oha")))
         .WithItem(new Text($"Very {Environment.NewLine}long text"))
         .WithItem(new Border(new Text($"Very {Environment.NewLine}long text")))
         .WithItem((Text)"No")
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