// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Parse.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.UnitTests
{
   using System;
   using System.Collections.Generic;
   using System.Diagnostics.CodeAnalysis;

   using ConsoLovers.ConsoleToolkit.CommandLineArguments;
   using ConsoLovers.UnitTests.ArgumentEngine;

   using FluentAssertions;

   using Microsoft.VisualStudio.TestTools.UnitTesting;

   [TestClass]
   [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
   public class Parse : ParserTestBase
   {
      #region Public Methods and Operators

      [TestMethod]
      public void EnsureParsingOfSingleOptionWorksCorrectly()
      {
         var arguments = Parse("-Enabled");
         arguments.Count.Should().Be(1);
         AssertContains(arguments, "Enabled");

         arguments = Parse("/Enabled");
         arguments.Count.Should().Be(1);
         AssertContains(arguments, "Enabled");

         arguments = Parse("Enabled");
         arguments.Count.Should().Be(1);
         AssertContains(arguments, "Enabled");
      }

      [TestMethod]
      public void EnsureParsingOfSingleParameterWorksCorrectly()
      {
         var arguments = Parse("-Name:Hans");
         arguments.Count.Should().Be(1);
         AssertContains(arguments, "Name", "Hans");
      }

      [TestMethod]
      public void EnsureParsingOfAPathWorksCorrectly()
      {
         var arguments = Parse(@"""D:\Temp\file.txt""");
         arguments.Count.Should().Be(1);
         AssertContains(arguments, @"""D:\Temp\file.txt""");
      }

      [TestMethod]
      public void EnsureAllKindOfArgumentsAreParsedCorrectly()
      {
         var arguments = Parse("Merge", @"File=D:\Temp\file.txt", "-p", "-o");

         AssertContains(arguments, "Merge");
         AssertContains(arguments, "File", @"D:\Temp\file.txt");
         AssertContains(arguments, "P");
         AssertContains(arguments, "O");
      }

      [TestMethod]
      public void EnsureMultipleOptionsAreParsedCorrectly()
      {
         var arguments = Parse("-d", "/a", "-x");
         arguments.ContainsKey("d").Should().BeTrue();
         arguments.ContainsKey("a").Should().BeTrue();
         arguments.ContainsKey("x").Should().BeTrue();

         arguments["D"].Value.Should().Be("true");
         arguments["A"].Value.Should().Be("true");
         arguments["X"].Value.Should().Be("true");
      }

      [TestMethod]
      public void EnsureMultipleParametersAreParsedCorrectly()
      {
         var arguments = Parse("-Ä=4", "/Ü:5.5", "-Ö:Peter");
         arguments.ContainsKey("ä").Should().BeTrue();
         arguments.ContainsKey("ü").Should().BeTrue();
         arguments.ContainsKey("ö").Should().BeTrue();

         arguments["Ä"].Value.Should().Be("4");
         arguments["Ü"].Value.Should().Be("5.5");
         arguments["Ö"].Value.Should().Be("Peter");
      }

      [TestMethod]
      public void ParseCommand()
      {
         var arguments = Parse("Command");
         arguments.ContainsKey("Command").Should().BeTrue();
         arguments.ContainsKey("command").Should().BeTrue();
         arguments.ContainsKey("COMMAND").Should().BeTrue();

         arguments["COMMAND"].Value.Should().Be("true");
      }

      /// <summary>Parses the named string.</summary>
      [TestMethod]
      public void ParseNamedArgument()
      {
         var arguments = Parse("-Name=Hans");
         arguments.ContainsKey("Name").Should().BeTrue();
         arguments.ContainsKey("name").Should().BeTrue();
         arguments.ContainsKey("NAME").Should().BeTrue();

         arguments["Name"].Value.Should().Be("Hans");

         arguments = Parse("-Name:Olga");
         arguments.ContainsKey("Name").Should().BeTrue();
         arguments.ContainsKey("name").Should().BeTrue();
         arguments.ContainsKey("NAME").Should().BeTrue();

         arguments["Name"].Value.Should().Be("Olga");
      }

      [TestMethod]
      public void ParseOption()
      {
         var arguments = Parse(" -Debug");
         arguments.ContainsKey("Debug").Should().BeTrue();
         arguments.ContainsKey("debug").Should().BeTrue();
         arguments.ContainsKey("DEBUG").Should().BeTrue();
         arguments.ContainsKey("Release").Should().BeFalse();
         arguments.ContainsKey("release").Should().BeFalse();
         arguments.ContainsKey("RELEASE").Should().BeFalse();

         arguments["DEBUg"].Value.Should().Be("true");

         arguments = Parse(" -Release");
         arguments.ContainsKey("Debug").Should().BeFalse();
         arguments.ContainsKey("debug").Should().BeFalse();
         arguments.ContainsKey("DEBUG").Should().BeFalse();
         arguments.ContainsKey("Release").Should().BeTrue();
         arguments.ContainsKey("release").Should().BeTrue();
         arguments.ContainsKey("RELEASE").Should().BeTrue();

         arguments["ReleasE"].Value.Should().Be("true");
      }

      #endregion

      #region Methods

      private static void AssertContains(IDictionary<string, CommandLineArgument> arguments, string expectedKey, string expectedValue = "true")
      {
         arguments.Should().ContainKey(expectedKey);
         arguments.Should().ContainKey(expectedKey.ToLower());
         arguments.Should().ContainKey(expectedKey.ToUpper());

         var argument = arguments[expectedKey];
         string.Equals(argument.Name, expectedKey, StringComparison.InvariantCultureIgnoreCase).Should().BeTrue();
         argument.Value.Should().Be(expectedValue);
      }

      #endregion
   }
}