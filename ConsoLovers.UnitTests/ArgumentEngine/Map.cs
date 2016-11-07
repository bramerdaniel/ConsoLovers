// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Map.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.UnitTests.ArgumentEngine
{
   using System;
   using System.Collections.Generic;
   using System.Diagnostics.CodeAnalysis;

   using ConsoLovers.ConsoleToolkit.CommandLineArguments;

   using FluentAssertions;

   using Microsoft.VisualStudio.TestTools.UnitTesting;

   [TestClass]
   [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
   public class Map
   {
      #region Public Methods and Operators

      [TestMethod]
      public void CaseSensitiveArgumentTest()
      {
         var target = new ArgumentMapper<Arguments>();
         var dictionary = new Dictionary<string, CommandLineArgument>(StringComparer.InvariantCulture) { { "first", new CommandLineArgument { Value = "45" } } };
         var result = target.Map(dictionary);

         result.First.Should().NotBe(45);

         dictionary.Count.Should().Be(1);
      }

      [TestMethod]
      public void EnsureArgumentsAreMappedCorrectly()
      {
         var target = new ArgumentMapper<Arguments>();
         var dictionary = new Dictionary<string, CommandLineArgument>(StringComparer.InvariantCultureIgnoreCase) { { "First", new CommandLineArgument { Value = "45" } }, { "sec", new CommandLineArgument { Value = "true" } }, { "3rd", new CommandLineArgument { Value = "Nick O'Teen" } } };
         var result = target.Map(dictionary);

         result.First.Should().Be(45);
         result.Second.Should().BeTrue();
         result.Third.Should().Be("Nick O'Teen");

         dictionary.Count.Should().Be(0);
      }

      [TestMethod]
      public void EnsureMultipleSameAliasesAreDetected()
      {
         var target = new ArgumentMapper<InvalidAliases>();
         target.Invoking(t => t.Map(new Dictionary<string, CommandLineArgument>())).ShouldThrow<CommandLineAttributeException>();

         var target2 = new ArgumentMapper<InvalidNameAliases>();
         target2.Invoking(t => t.Map(new Dictionary<string, CommandLineArgument>())).ShouldThrow<CommandLineAttributeException>();

         var target3 = new ArgumentMapper<InvalidOptionArgument>();
         target3.Invoking(t => t.Map(new Dictionary<string, CommandLineArgument>())).ShouldThrow<CommandLineAttributeException>();
      }

      [TestMethod]
      public void EnsureOptionsAreMappedCorrectly()
      {
         var target = new ArgumentMapper<Options>();
         var dictionary = new Dictionary<string, CommandLineArgument>(StringComparer.InvariantCultureIgnoreCase) { { "First", new CommandLineArgument { Value = "true" } }, { "sec", new CommandLineArgument { Value = "true" } }, { "3rd", new CommandLineArgument { Value = "true" } } };
         var result = target.Map(dictionary);

         result.First.Should().BeTrue();
         result.Second.Should().BeTrue();
         result.Third.Should().BeTrue();

         dictionary.Count.Should().Be(0);
      }

      [TestMethod]
      public void EnsureOptionsAreMappedCorrectlyEvenForNullOrEmptyEntries()
      {
         var target = new ArgumentMapper<Options>();
         var dictionary = new Dictionary<string, CommandLineArgument>(StringComparer.InvariantCultureIgnoreCase) { { "First", new CommandLineArgument() }, { "sec", new CommandLineArgument { Value = string.Empty } }, { "3rd", new CommandLineArgument() } };
         Options result = target.Map(dictionary);

         result.First.Should().BeTrue();
         result.Second.Should().BeTrue();
         result.Third.Should().BeTrue();

         dictionary.Count.Should().Be(0);
      }

      [TestMethod]
      public void EnsureUnsusedArgumentStaysInTheArgumentsDictionary()
      {
         var target = new ArgumentMapper<Arguments>();
         var dictionary = new Dictionary<string, CommandLineArgument>(StringComparer.InvariantCultureIgnoreCase) { { "First", new CommandLineArgument { Value = "45" } }, { "invalid", new CommandLineArgument { Value = "45" } } };
         var result = target.Map(dictionary);

         result.First.Should().Be(45);

         dictionary.Count.Should().Be(1);
         dictionary.ContainsKey("invalid").Should().BeTrue();
      }

      [TestMethod]
      public void EnsureUnsusedOptionStaysInTheArgumentsDictionary()
      {
         var target = new ArgumentMapper<Options>();
         var dictionary = new Dictionary<string, CommandLineArgument>(StringComparer.InvariantCultureIgnoreCase)
         {
            { "First", new CommandLineArgument() },
            { "sec", new CommandLineArgument { Value = string.Empty } },
            { "3rd", new CommandLineArgument() },
            { "unmaped", new CommandLineArgument() }
         };

         var result = target.Map(dictionary);

         result.First.Should().BeTrue();
         result.Second.Should().BeTrue();
         result.Third.Should().BeTrue();

         dictionary.Count.Should().Be(1);
         dictionary.ContainsKey("unmaped").Should().BeTrue();
      }

      #endregion

      public class Arguments
      {
         #region Public Properties

         [Option]
         public int First { get; set; }

         [Option("sec")]
         public bool Second { get; set; }

         [Option("Third", "3rd")]
         public string Third { get; set; }

         #endregion
      }

      public class InvalidAliases
      {
         #region Public Properties

         [Argument("first", "a")]
         public string First { get; set; }

         [Argument("second", "a")]
         public string Second { get; set; }

         #endregion
      }

      public class InvalidNameAliases
      {
         #region Public Properties

         [Argument("sec")]
         public bool First { get; set; }

         [Argument("second", "sec")]
         public string Second { get; set; }

         #endregion
      }

      public class InvalidOptionArgument
      {
         #region Public Properties

         [Option("sec")]
         public bool First { get; set; }

         [Argument("second", "sec")]
         public string Second { get; set; }

         #endregion
      }

      public class Options
      {
         #region Public Properties

         [Option]
         public bool First { get; set; }

         [Option("sec")]
         public bool Second { get; set; }

         [Option("Third", "3rd")]
         public bool Third { get; set; }

         #endregion
      }
   }
}