// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Parse.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.CommandLineParserTests
{
   using System;
   using System.Collections.Generic;
   using System.Diagnostics.CodeAnalysis;

   using ConsoLovers.ConsoleToolkit.Core.UnitTests.ArgumentEngine;

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
         arguments.ContainsName("d").Should().BeTrue();
         arguments.ContainsName("a").Should().BeTrue();
         arguments.ContainsName("x").Should().BeTrue();

         arguments["D"].Value.Should().BeNull();
         arguments["A"].Value.Should().BeNull();
         arguments["X"].Value.Should().BeNull();
      }

      [TestMethod]
      public void EnsureMultipleParametersAreParsedCorrectly()
      {
         var arguments = Parse("-Ä=4", "/Ü:5.5", "-Ö:Peter");
         arguments.ContainsName("ä").Should().BeTrue();
         arguments.ContainsName("ü").Should().BeTrue();
         arguments.ContainsName("ö").Should().BeTrue();

         arguments["Ä"].Value.Should().Be("4");
         arguments["Ü"].Value.Should().Be("5.5");
         arguments["Ö"].Value.Should().Be("Peter");
      }

      [TestMethod]
      public void ParseCommand()
      {
         var arguments = Parse("Command");
         arguments.ContainsName("Command").Should().BeTrue();
         arguments.ContainsName("command").Should().BeTrue();
         arguments.ContainsName("COMMAND").Should().BeTrue();

         arguments["COMMAND"].Value.Should().BeNull();
      }

      /// <summary>Parses the named string.</summary>
      [TestMethod]
      public void ParseNamedArgument()
      {
         var arguments = Parse("-Name=Hans");
         arguments.ContainsName("Name").Should().BeTrue();
         arguments.ContainsName("name").Should().BeTrue();
         arguments.ContainsName("NAME").Should().BeTrue();

         arguments["Name"].Value.Should().Be("Hans");

         arguments = Parse("-Name:Olga");
         arguments.ContainsName("Name").Should().BeTrue();
         arguments.ContainsName("name").Should().BeTrue();
         arguments.ContainsName("NAME").Should().BeTrue();

         arguments["Name"].Value.Should().Be("Olga");
      }

      [TestMethod]
      public void ParseAPathThatShouldBecomeIndexedArgumentLater()
      {
         var arguments = Parse(@"D:\HelloWorld\Hansi.txt");
         arguments.ContainsName("D").Should().BeTrue();

         arguments["D"].OriginalString.Should().Be(@"D:\HelloWorld\Hansi.txt");
      }

      [TestMethod]
      public void ParseOption()
      {
         var arguments = Parse(" -Debug");
         arguments.ContainsName("Debug").Should().BeTrue();
         arguments.ContainsName("debug").Should().BeTrue();
         arguments.ContainsName("DEBUG").Should().BeTrue();
         arguments.ContainsName("Release").Should().BeFalse();
         arguments.ContainsName("release").Should().BeFalse();
         arguments.ContainsName("RELEASE").Should().BeFalse();

         arguments["DEBUg"].Value.Should().BeNull();

         arguments = Parse(" -Release");
         arguments.ContainsName("Debug").Should().BeFalse();
         arguments.ContainsName("debug").Should().BeFalse();
         arguments.ContainsName("DEBUG").Should().BeFalse();
         arguments.ContainsName("Release").Should().BeTrue();
         arguments.ContainsName("release").Should().BeTrue();
         arguments.ContainsName("RELEASE").Should().BeTrue();

         arguments["ReleasE"].Value.Should().BeNull();
      }

      #endregion

      #region Methods

      private static void AssertContains(ICommandLineArguments arguments, string expectedKey, string expectedValue = null)
      {
         arguments.ContainsName(expectedKey).Should().BeTrue();
         arguments.ContainsName(expectedKey.ToUpper()).Should().BeTrue();
         arguments.ContainsName(expectedKey.ToLower()).Should().BeTrue();

         var argument = arguments[expectedKey];
         string.Equals(argument.Name, expectedKey, StringComparison.InvariantCultureIgnoreCase).Should().BeTrue();
         argument.Value.Should().Be(expectedValue);
      }

      #endregion
   }
}