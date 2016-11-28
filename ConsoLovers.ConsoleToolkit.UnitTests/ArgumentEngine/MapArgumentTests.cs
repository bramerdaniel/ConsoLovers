// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MapArgumentTests.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.UnitTests.ArgumentEngine
{
   using System.Collections.Generic;

   using ConsoLovers.ConsoleToolkit.CommandLineArguments;

   using FluentAssertions;

   using Microsoft.VisualStudio.TestTools.UnitTesting;

   [TestClass]
   public class MapArgumentTests
   {
      #region Public Methods and Operators

      [TestMethod]
      public void NamedArgumentAlias2Test()
      {
         var commandTestClass = new ArgumentTestClass();
         var dictionary = new Dictionary<string, CommandLineArgument>
         {
            { "AliasName2", new CommandLineArgument { Value = "TheNameValue" } },
            { "RequiredArgument", new CommandLineArgument { Value = "RequiredArgumentValue" } }
         };

         var argumentMapper = new ArgumentMapper<ArgumentTestClass>();
         var result = argumentMapper.Map(dictionary, commandTestClass);

         Assert.AreEqual(result.NamedArgument, "TheNameValue");
      }

      [TestMethod]
      public void NamedArgumentAliasTest()
      {
         var commandTestClass = new ArgumentTestClass();
         var dictionary = new Dictionary<string, CommandLineArgument>
         {
            { "AliasName1", new CommandLineArgument { Value = "TheNameValue" } },
            { "RequiredArgument", new CommandLineArgument { Value = "RequiredArgumentValue" } }
         };

         var argumentMapper = new ArgumentMapper<ArgumentTestClass>();
         var result = argumentMapper.Map(dictionary, commandTestClass);

         Assert.AreEqual(result.NamedArgument, "TheNameValue");
      }

      [TestMethod]
      public void NotUsedRequiredArgumentErrorTest()
      {
         var commandTestClass = new ArgumentTestClass();
         var dictionary = new Dictionary<string, CommandLineArgument>();
         var argumentMapper = new ArgumentMapper<ArgumentTestClass>();
         argumentMapper.Invoking(x => x.Map(dictionary, commandTestClass)).ShouldThrow<MissingCommandLineArgumentException>();
      }

      [TestMethod]
      public void RegularUsageTest()
      {
         var commandTestClass = new ArgumentTestClass();
         var dictionary = new Dictionary<string, CommandLineArgument>
         {
            { "SimpleArgument", new CommandLineArgument { Value = "\"SimpleArgumentValue\"" } },
            { "TheName", new CommandLineArgument { Value = "TheNameValue" } },
            { "TrimmedArgument", new CommandLineArgument { Value = "TrimmedArgumentValue" } },
            { "RequiredArgument", new CommandLineArgument { Value = "RequiredArgumentValue" } }
         };
         var argumentMapper = new ArgumentMapper<ArgumentTestClass>();
         var result = argumentMapper.Map(dictionary, commandTestClass);

         Assert.AreEqual(result.SimpleArgument, "\"SimpleArgumentValue\"");
         Assert.AreEqual(result.NamedArgument, "TheNameValue");
         Assert.AreEqual(result.TrimmedArgument, "TrimmedArgumentValue");
         Assert.AreEqual(result.RequiredArgument, "RequiredArgumentValue");
      }

      [TestMethod]
      public void TrimmedArgumentTest()
      {
         var commandTestClass = new ArgumentTestClass();
         var dictionary = new Dictionary<string, CommandLineArgument>
         {
            { "TrimmedArgument", new CommandLineArgument { Value = "\"UntrimmedValue\"" } },
            { "RequiredArgument", new CommandLineArgument { Value = "RequiredArgumentValue" } }
         };

         var argumentMapper = new ArgumentMapper<ArgumentTestClass>();
         var result = argumentMapper.Map(dictionary, commandTestClass);

         Assert.AreEqual(result.TrimmedArgument, "UntrimmedValue");
      }

      #endregion
   }

   internal class ArgumentTestClass
   {
      #region Public Properties

      [Argument("TheName", "AliasName1", "AliasName2")]
      public string NamedArgument { get; set; }

      [Argument(Required = true)]
      public string RequiredArgument { get; set; }

      [Argument]
      public string SimpleArgument { get; set; }

      [Argument(TrimQuotation = true)]
      public string TrimmedArgument { get; set; }

      #endregion
   }
}