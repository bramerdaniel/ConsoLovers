// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Map.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.ArgumentEngine
{
   using System;
   using System.Collections.Generic;
   using System.Diagnostics.CodeAnalysis;

   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
   using ConsoLovers.ConsoleToolkit.Core.UnitTests.Setups;

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
         var target = Setup.ArgumentMapper().ForType<Arguments>().Done();
         var dictionary = new Dictionary<string, CommandLineArgument>(StringComparer.InvariantCulture) { { "first", new CommandLineArgument { Value = "45" } } };
         var result = target.Map(dictionary);

         result.First.Should().NotBe(45);

         dictionary.Count.Should().Be(1);
      }

      [TestMethod]
      public void EnsureArgumentsAreMappedCorrectly()
      {
         var target = Setup.ArgumentMapper().ForType<Arguments>().Done();
         var dictionary = new Dictionary<string, CommandLineArgument>(StringComparer.InvariantCultureIgnoreCase)
         {
            { "First", new CommandLineArgument { Value = "45" } },
            { "sec", new CommandLineArgument { Value = "true" } },
            { "3rd", new CommandLineArgument { Value = "Nick O'Teen" } }
         };
         var result = target.Map(dictionary);

         result.First.Should().Be(45);
         result.Second.Should().BeTrue();
         result.Third.Should().Be("Nick O'Teen");

         dictionary.Count.Should().Be(0);
      }

      [TestMethod]
      public void EnsureMultipleSameAliasesAreDetected()
      {
         var target = Setup.ArgumentMapper().ForType<InvalidAliases>().Done();
         target.Invoking(t => t.Map(new Dictionary<string, CommandLineArgument>())).ShouldThrow<CommandLineAttributeException>();

         var target2 = Setup.ArgumentMapper().ForType<InvalidNameAliases>().Done();
         target2.Invoking(t => t.Map(new Dictionary<string, CommandLineArgument>())).ShouldThrow<CommandLineAttributeException>();

         var target3 = Setup.ArgumentMapper().ForType<InvalidOptionArgument>().Done();
         target3.Invoking(t => t.Map(new Dictionary<string, CommandLineArgument>())).ShouldThrow<CommandLineAttributeException>();
      }

      [TestMethod]
      public void EnsureOptionsAreMappedCorrectly()
      {
         var target = Setup.ArgumentMapper().ForType<Options>().Done();
         var dictionary = new Dictionary<string, CommandLineArgument>(StringComparer.InvariantCultureIgnoreCase)
         {
            { "First", new CommandLineArgument { Value = null } },
            { "sec", new CommandLineArgument { Value = null } },
            { "3rd", new CommandLineArgument { Value = null } }
         };

         var result = target.Map(dictionary);

         result.First.Should().BeTrue();
         result.Second.Should().BeTrue();
         result.Third.Should().BeTrue();

         dictionary.Count.Should().Be(0);
      }

      [TestMethod]
      public void EnsureOptionsAreMappedCorrectlyEvenForNullOrEmptyEntries()
      {
         var target = Setup.ArgumentMapper().ForType<Options>().Done();
         var dictionary = new Dictionary<string, CommandLineArgument>(StringComparer.InvariantCultureIgnoreCase)
         {
            { "First", new CommandLineArgument() },
            { "sec", new CommandLineArgument() },
            { "3rd", new CommandLineArgument() }
         };

         Options result = target.Map(dictionary);

         result.First.Should().BeTrue();
         result.Second.Should().BeTrue();
         result.Third.Should().BeTrue();

         dictionary.Count.Should().Be(0);
      }

      [TestMethod]
      public void EnsureOptionsCanOnlyBeMappedToBoolenProperties()
      {
         var target = Setup.ArgumentMapper().ForType<Options>().Done();
         var dictionary = new Dictionary<string, CommandLineArgument>(StringComparer.InvariantCultureIgnoreCase)
         {
            { "Inv", new CommandLineArgument { Value = "AnyString" } }
         };

         target.Invoking(t => t.Map(dictionary)).ShouldThrow<CommandLineArgumentException>().Where(e => e.Reason == ErrorReason.OptionWithValue);

         var second = new Dictionary<string, CommandLineArgument>(StringComparer.InvariantCultureIgnoreCase)
         {
            { "sec", new CommandLineArgument { Value = "true" } }
         };

         target.Invoking(t => t.Map(second)).ShouldThrow<CommandLineArgumentException>().Where(e => e.Reason == ErrorReason.OptionWithValue);
      }


      [TestMethod]
      public void EnsureDuplicateUsageOfAliasIsFound()
      {
         var target = Setup.ArgumentMapper().ForType<Arguments>().Done();
         var dictionary = new Dictionary<string, CommandLineArgument>(StringComparer.InvariantCultureIgnoreCase)
         {
            { "Third", new CommandLineArgument{ Value = "FALSE"} },
            { "3rd", new CommandLineArgument{ Value = "TRUE"} },
         };

         target.Invoking(t => t.Map(dictionary)).ShouldThrow<AmbiguousCommandLineArgumentsException>();
      }


      [TestMethod]
      public void EnsureUnsusedArgumentStaysInTheArgumentsDictionary()
      {
         var target = Setup.ArgumentMapper().ForType<Arguments>().Done();
         var dictionary = new Dictionary<string, CommandLineArgument>(StringComparer.InvariantCultureIgnoreCase)
         {
            { "First", new CommandLineArgument { Value = "45" } },
            { "invalid", new CommandLineArgument { Value = "45" } }
         };
         var result = target.Map(dictionary);

         result.First.Should().Be(45);

         dictionary.Count.Should().Be(1);
         dictionary.ContainsKey("invalid").Should().BeTrue();
      }

      [TestMethod]
      public void EnsureUnsusedOptionStaysInTheArgumentsDictionary()
      {
         var target = Setup.ArgumentMapper().ForType<Options>().Done();
         var dictionary = new Dictionary<string, CommandLineArgument>(StringComparer.InvariantCultureIgnoreCase)
         {
            { "First", new CommandLineArgument() },
            { "sec", new CommandLineArgument() },
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

         [Argument]
         public int First { get; set; }

         [Argument("sec")]
         public bool Second { get; set; }

         [Argument("Third", "3rd")]
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

         [Option("a", "b", "c")]
         public bool Multi { get; set; }

         [Option]
         public bool First { get; set; }

         [Option("Invalid", "inv")]
         public string Invalid { get; set; }

         [Option("sec")]
         public bool Second { get; set; }

         [Option("Third", "3rd")]
         public bool Third { get; set; }

         #endregion
      }
   }
}