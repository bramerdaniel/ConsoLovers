// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RenderTests.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.UnitTests.ControlsTests.TableTests;

using System.Threading.Tasks;

using ConsoLovers.ConsoleToolkit.Controls;
using ConsoLovers.ConsoleToolkit.UnitTests.Setups;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using VerifyMSTest;

[TestClass]
public class RenderTests : VerifyBase
{
   [TestMethod]
   public async Task EnsureSingleEmptyColumnIsRenderedCorrectly()
   {
      var table = Setup.Table()
         .Done();

      table.AddColumns(new Text("First"));

      var renderer = Setup.TestRenderer().Done();

      var renderedText = renderer.Render(table);

      await Verify(renderedText);
   }

   [TestMethod]
   public async Task EnsureFullTableIsRenderedCorrectly()
   {
      var table = Setup.Table()
         .Done();

      table.AddColumns(new Text("First"), new Text("Second"), new Text("Third"));
      table.AddRow(new Text("1"), new Text("2"), new Text("3"));

      var renderer = Setup.TestRenderer().Done();

      var renderedText = renderer.Render(table);

      await Verify(renderedText);
   }


}