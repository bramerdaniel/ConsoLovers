// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RenderTests.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.UnitTests.ControlsTests.MessageDisplayTests;

using ConsoLovers.ConsoleToolkit.UnitTests.Setups;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class RenderTests
{
   [TestMethod]
   public void EnsureListWithSelectionIsRenderedCorrectly()
   {
      var list = Setup.MessageDisplay()
         .WithTitle("Title")
         .WithMessage("Argument can not be null")
         .Done();

      var renderer = Setup.TestRenderer()
         .WithConsoleWidth(100)
         .Done();

      renderer.Render(list).Should().Be(@"Title: Argument can not be null");
      
      renderer = Setup.TestRenderer()
         .WithConsoleWidth(23)
         .Done();

      renderer.Render(list).Should().Be(@"
Title: Argument can not
       be null".Trim());
   }
}