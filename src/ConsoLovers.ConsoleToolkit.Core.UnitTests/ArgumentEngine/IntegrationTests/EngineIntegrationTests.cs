// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NestedCommandTests.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.ArgumentEngine.IntegrationTests;

using System;
using System.Collections;
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
   public void EnsureIndexedArgumentsWorkForNestedCommands()
   {
      var result = CreateArgumentFromString<RootArgs>("add user Delete");
      result.Add.Arguments.User.Should().NotBeNull();
      result.Add.Arguments.User.Arguments.UserName.Should().Be("Delete");
   }

   [TestMethod]
   public void SpecialCaseTest()
   {

      var result = CreateArgumentFromString<RootArgs>("delete permission delete");

      result.Delete.Arguments.Permission.Arguments.Permission.Should().Be("delete");
   }

   [TestMethod]
   public void SpecialCaseTest2()
   {
      var result = CreateArgumentFromString<RootArgs>("delete permission permission=Allowed");

      result.Delete.Arguments.Role.Should().BeNull();
      result.Delete.Arguments.Role.Should().BeNull();
      result.Delete.Arguments.Permission.Should().NotBeNull();

      result.Delete.Arguments.Permission.Arguments.Permission.Should().Be("Allowed");
   }

   [TestMethod]
   public void EnsureApplicationOptionsAreMappedCorrectly()
   {
      var result = CreateArgumentFromString<RootArgs>("delete role name=Admin -force");

      result.Force.Should().BeTrue();

      result.Delete.Arguments.Role.Arguments.RoleName.Should().Be("Admin");

      // As the force option is shared, the force should also be set on the command
      result.Delete.Arguments.Role.Arguments.Force.Should().BeTrue();
   }

   private static T CreateArgumentFromString<T>(string commandLine)
      where T : class, new()
   {
      return CreateArgumentFromString<T>(commandLine, out _);
   }

   private static T CreateArgumentFromString<T>(string commandLine, out IList<CommandLineArgument> unhandledArguments)
      where T : class, new()
   {
      var args = new T();
      var engine = Setup.CommandLineEngine().Done();
      var notHandledArguments = new List<CommandLineArgument>();

      try
      {
         engine.UnhandledCommandLineArgument += OnUnhandled;
         var result = engine.Map(commandLine, args);
         unhandledArguments = notHandledArguments;
         return result;
      }
      finally
      {
         engine.UnhandledCommandLineArgument -= OnUnhandled;
      }

      void OnUnhandled(object sender, CommandLineArgumentEventArgs e)
      {
         notHandledArguments.Add(e.Argument);
      }
   }



   #endregion
}