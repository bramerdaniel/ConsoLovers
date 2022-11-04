// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RenderTests.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.UnitTests.ControlsTests.BlockTests;

using ConsoLovers.ConsoleToolkit.UnitTests.Setups;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class RenderTests
{
   #region Public Methods and Operators

   [TestMethod]
   public void EnsureBlockIsRenderedCorrectly()
   {
      var border = Setup.Rectangle()
         .WithHeight(4)
         .WithWidth(3)
         .WithValue('█')
         .Done();

      var renderer = Setup.TestRenderer().Done();

      var renderedText = renderer
         .Render(border);

      var value = @"
███
███
███
███
".Trim();

      renderedText.Should().Be(value);
   }

   [TestMethod]
   public void EnsureHorizontalBlockIsRenderedCorrectly()
   {
      var border = Setup.Rectangle()
         .WithHeight(2)
         .WithWidth(4)
         .WithValue('*')
         .Done();

      var renderer = Setup.TestRenderer().Done();
      var renderedText = renderer.Render(border);

      var value = @"
****
****
".Trim();

      renderedText.Should().Be(value);
   }

   #endregion
}