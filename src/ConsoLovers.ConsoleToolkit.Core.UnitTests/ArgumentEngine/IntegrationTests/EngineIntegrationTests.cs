// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NestedCommandTests.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.ArgumentEngine.IntegrationTests;

using System.Collections.Generic;

using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
using ConsoLovers.ConsoleToolkit.Core.UnitTests.Setups;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public partial class EngineIntegrationTests 
{
   #region Public Methods and Operators
   
   [TestMethod]
   public void SpecialCaseTest()
   {
      
      var result = Mapp<RootArgs>("delete permission delete");

      result.Delete.Arguments.Permission.Arguments.Permission.Should().Be("Allowed");
   }
   
   [TestMethod]
   public void SpecialCaseTest2()
   {
      
      var result = Mapp<RootArgs>("delete permission permission=Allowed");

      result.Delete.Arguments.Permission.Arguments.Permission.Should().Be("Allowed");
   }



   private static T Mapp<T>(string commandLine)
      where T : class, new()
   {
      var args = new T();
      var engine = Setup.CommandLineEngine().Done();
      return engine.Map<T>(commandLine, args);
   }


   #endregion
}