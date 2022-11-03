// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IntegrationTests.cs" company="ConsoLovers">
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
public class IntegrationTests
{
   [TestMethod]
   public void IntegrationTest1()
   {
      var pannel = Setup.Panel()
         .WithChild(new Border("<"))
         .WithChild(new Border(">"))
         .Done();
      
      var padding = new Padding(pannel, new Thickness(2, 1));
      
      var renderedText = Setup.TestRenderer()
         .WithConsoleWidth(100)
         .Render(padding);

      renderedText.Should().Be(@"
          
  ┌─┐┌─┐  
  │<││>│  
  └─┘└─┘  
          
".Trim('\r', '\n'));

   }
}