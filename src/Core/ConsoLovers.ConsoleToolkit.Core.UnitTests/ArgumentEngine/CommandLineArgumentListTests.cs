// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandLineArgumentListTests.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.ArgumentEngine;

using ConsoLovers.ConsoleToolkit.Core.UnitTests.Setups;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class CommandLineArgumentListTests
{
   [TestMethod]
   public void EnsureCloneWorksCorrectly()
   {
      var target = Setup.CommandLineArguments()
         .Add("Name", "Hans")
         .Add("Flag", "true")
         .Done();

      var clone = target.Clone();

      clone.Should().BeEquivalentTo(target);
   }

   [TestMethod]
   public void EnsureListsDoNotInfluenceEachOther()
   {
      var target = Setup.CommandLineArguments()
         .Add("Name", "Hans")
         .Add("Flag", "true")
         .Done();

      var clone = target.Clone();
      
      target.RemoveFirst("Name");
      target.Should().HaveCount(1);
      clone.Should().HaveCount(2);
   }

}