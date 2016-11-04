// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MapCommandTests.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.UnitTests.ArgumentEngine
{
   using System;
   using System.Collections.Generic;

   using ConsoLovers.ConsoleToolkit.CommandLineArguments;

   using FluentAssertions;

   using Microsoft.VisualStudio.TestTools.UnitTesting;

   [TestClass]
   public class MapCommandTests
   {
      #region Public Methods and Operators

      [TestMethod]
      public void AliasCommandUsageTest()
      {
         var commandTestClass = new CommandTestClass();
         var dictionary = new Dictionary<string, string> { { "B", "true" } };
         var argumentMapper = new ArgumentMapper<CommandTestClass>();
         var result = argumentMapper.Map(dictionary, commandTestClass);
         Assert.AreEqual(result.TheCommand, TestCommandType.Backup);
      }

      [TestMethod]
      public void MultipleCommandUsageErrorTest()
      {
         var commandTestClass = new CommandTestClass();
         var dictionary = new Dictionary<string, string> { { "Backup", "true" }, { "Restore", "true" } };
         var argumentMapper = new ArgumentMapper<CommandTestClass>();
         argumentMapper.Invoking(x => x.Map(dictionary, commandTestClass)).ShouldThrow<CommandLineArgumentException>().WithMessage(
            "The command 'Backup|Init|Restore' must be used exclusive.");
      }

      [TestMethod]
      public void MultipleSameCommandUsageErrorTest()
      {
         var commandTestClass = new CommandTestClass();
         var dictionary = new Dictionary<string, string> { { "Backup", "true" }, { "B", "true" } };
         var argumentMapper = new ArgumentMapper<CommandTestClass>();
         argumentMapper.Invoking(x => x.Map(dictionary, commandTestClass)).ShouldThrow<CommandLineArgumentException>().WithMessage(
            "Multiple aruments for 'Backup'-command is not allowed.");
      }

      [TestMethod]
      public void NoCommandUsageErrorTest()
      {
         var commandTestClass = new CommandTestClass();
         var dictionary = new Dictionary<string, string>();
         var argumentMapper = new ArgumentMapper<CommandTestClass>();
         argumentMapper.Invoking(x => x.Map(dictionary, commandTestClass)).ShouldThrow<CommandLineArgumentException>().WithMessage(
            "The command 'Backup|Init|Restore' must be used.");
      }

      [TestMethod]
      public void SingleCommandUsageTest()
      {
         var commandTestClass = new CommandTestClass();
         var dictionary = new Dictionary<string, string> { { "Backup", "true" } };
         var argumentMapper = new ArgumentMapper<CommandTestClass>();
         var result = argumentMapper.Map(dictionary, commandTestClass);
         Assert.AreEqual(result.TheCommand, TestCommandType.Backup);
      }

      #endregion
   }

   internal class CommandTestClass
   {
      #region Public Properties

      [Command("Backup", "B")]
      [Command("Init", "I")]
      [Command("Restore", "R")]
      public TestCommandType TheCommand { get; set; }

      #endregion
   }

   [Flags]
   internal enum TestCommandType
   {
      None = 0,

      Backup = 1,

      Init = 2,

      Restore = 4
   }
}