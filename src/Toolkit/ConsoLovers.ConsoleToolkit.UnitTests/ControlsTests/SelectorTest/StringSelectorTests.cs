// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringSelectorTests.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.UnitTests.ControlsTests.SelectorTest;

using ConsoLovers.ConsoleToolkit.Controls;
using ConsoLovers.ConsoleToolkit.UnitTests.Setups;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class StringSelectorTests
{
   #region Public Methods and Operators

   [TestMethod]
   public void EnsureListWithSelectionIsRenderedCorrectly()
   {
      var list = Setup.Selector<string>()
         .WithItem("First")
         .WithItem("Second")
         .WithItem("Third")
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
   public void EnsureListWithTextIsRenderedCorrectly()
   {
      var list = Setup.Selector<string>()
         .WithItem("First")
         .WithItem("Second")
         .Done();

      var renderedText = Setup.TestRenderer()
         .Render(list);

      renderedText.Should().Be(@"> First
  Second");
   }

   [TestMethod]
   public void EnsureSelectorWithRuleIsRenderedCorrectly()
   {
      var list = Setup.Selector<string>()
         .WithItem("First")
         .WithItem(null, new Rule(null))
         .WithItem("Second")
         .Done();

      var renderedText = Setup.TestRenderer()
         .WithConsoleWidth(10)
         .Render(list);

      renderedText.Should().Be(@"> First
  --------
  Second");
   }

   [TestMethod]
   public void EnsureHorizontalOrientationIsRenderedCorrectly()
   {
      var list = Setup.Selector<string>()
         .WithItem("Yes")
         .WithItem("No")
         .WithItem("Cancel")
         .WithOrientation(Orientation.Horizontal)
         .Done();

      var renderer = Setup.TestRenderer()
         .WithConsoleWidth(10)
         .Done();

      var expected = @"
Yes No Cancel
^          ".TrimStart();

      renderer.Render(list).Should().Be(expected);

      renderer.Reset();
      list.SelectedIndex = 1;

      expected = @"
Yes No Cancel
    ^       ".TrimStart();

      renderer.Render(list).Should().Be(expected);
   }

   [TestMethod]
   public void EnsureHorizontalOrientationCanBeRenderedWithoutSelector()
   {
      var list = Setup.Selector<string>()
         .WithItem("Yes")
         .WithItem("No")
         .WithItem("Cancel")
         .WithOrientation(Orientation.Horizontal)
         .WithSelector(string.Empty)
         .Done();

      var renderedText = Setup.TestRenderer()
         .Render(list);

      var expected = @"
Yes No Cancel".TrimStart();

      renderedText.Should().Be(expected);
   }
   
   [TestMethod]
   public void EnsureNullCanBeUsedAsValue()
   {
      var list = Setup.Selector<string>()
         .WithItem("Yes")
         .WithItem("No")
         .WithItem(null)
         .WithOrientation(Orientation.Horizontal)
         .WithSelector(string.Empty)
         .Done();

      var renderer = Setup.TestRenderer().Done();
      renderer.Render(list).Should().Be("Yes No null".Trim());

      renderer.Reset();
      list.Items[2].Template = new CText("<NULL>");
      renderer.Render(list).Should().Be("Yes No <NULL>".Trim());
   }



   #endregion
}