﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Map.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.ArgumentEngine
{
   using System;
   using System.Collections.Generic;
   using System.Diagnostics.CodeAnalysis;
   using ConsoLovers.ConsoleToolkit.Core.Exceptions;
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
         var result = target.Map(CommandLineArgumentList.FromDictionary(dictionary));

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
         var result = target.Map(CommandLineArgumentList.FromDictionary(dictionary));

         result.First.Should().Be(45);
         result.Second.Should().BeTrue();
         result.Third.Should().Be("Nick O'Teen");
      }

      [TestMethod]
      public void EnsureMultipleSameAliasesAreDetected()
      {
         var target = Setup.ArgumentMapper().ForType<InvalidAliases>().Done();
         target.Invoking(t => t.Map(new CommandLineArgumentList())).Should().Throw<CommandLineAttributeException>();

         var target2 = Setup.ArgumentMapper().ForType<InvalidNameAliases>().Done();
         target2.Invoking(t => t.Map(new CommandLineArgumentList())).Should().Throw<CommandLineAttributeException>();

         var target3 = Setup.ArgumentMapper().ForType<InvalidOptionArgument>().Done();
         target3.Invoking(t => t.Map(new CommandLineArgumentList())).Should().Throw<CommandLineAttributeException>();
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

         var result = target.Map(CommandLineArgumentList.FromDictionary(dictionary));

         result.First.Should().BeTrue();
         result.Second.Should().BeTrue();
         result.Third.Should().BeTrue();
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

         Options result = target.Map(CommandLineArgumentList.FromDictionary(dictionary));

         result.First.Should().BeTrue();
         result.Second.Should().BeTrue();
         result.Third.Should().BeTrue();
      }

      [TestMethod]
      public void EnsureOptionsCanOnlyBeMappedToBoolaenProperties()
      {
         var target = Setup.ArgumentMapper().ForType<Options>().Done();
         var dictionary = new Dictionary<string, CommandLineArgument>(StringComparer.InvariantCultureIgnoreCase)
         {
            { "Inv", new CommandLineArgument { Value = "AnyString" } }
         };

         target.Invoking(t => t.Map(CommandLineArgumentList.FromDictionary(dictionary))).Should().Throw<CommandLineArgumentException>().Where(e => e.Reason == ErrorReason.OptionWithValue);

         var second = new Dictionary<string, CommandLineArgument>(StringComparer.InvariantCultureIgnoreCase)
         {
            { "sec", new CommandLineArgument { Value = "true" } }
         };

         target.Invoking(t => t.Map(CommandLineArgumentList.FromDictionary(second))).Should().Throw<CommandLineArgumentException>().Where(e => e.Reason == ErrorReason.OptionWithValue);
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

         target.Invoking(t => t.Map(CommandLineArgumentList.FromDictionary(dictionary))).Should().Throw<AmbiguousCommandLineArgumentsException>();
      }


      [TestMethod]
      public void EnsureUnusedArgumentStaysInTheArgumentsDictionary()
      {
         var target = Setup.ArgumentMapper().ForType<Arguments>().Done();
         var dictionary = new Dictionary<string, CommandLineArgument>(StringComparer.InvariantCultureIgnoreCase)
         {
            { "First", new CommandLineArgument { Value = "45" } },
            { "invalid", new CommandLineArgument { Value = "45" } }
         };

         var argumentList = CommandLineArgumentList.FromDictionary(dictionary);

         var result = target.Map(argumentList);

         result.First.Should().Be(45);

         argumentList.Count.Should().Be(1);
         argumentList.ContainsName("invalid").Should().BeTrue();
      }

      [TestMethod]
      public void EnsureUnusedArgumentStaysInTheArgumentsList()
      {
         var target = Setup.ArgumentMapper().ForType<Arguments>().Done();
         var dictionary = new Dictionary<string, CommandLineArgument>(StringComparer.InvariantCultureIgnoreCase)
         {
            { "First", new CommandLineArgument { Value = "45" } },
            { "invalid", new CommandLineArgument { Value = "45" } }
         };

         var argumentList = CommandLineArgumentList.FromDictionary(dictionary);
         
         var result = target.Map(argumentList);

         result.First.Should().Be(45);

         argumentList.Count.Should().Be(1);
         argumentList.ContainsName("invalid").Should().BeTrue();
      }

      [TestMethod]
      public void EnsureUnusedOptionStaysInTheArgumentsDictionary()
      {
         var target = Setup.ArgumentMapper().ForType<Options>().Done();
         var dictionary = new Dictionary<string, CommandLineArgument>(StringComparer.InvariantCultureIgnoreCase)
         {
            { "First", new CommandLineArgument() },
            { "sec", new CommandLineArgument() },
            { "3rd", new CommandLineArgument() },
            { "unmaped", new CommandLineArgument() }
         };

         var argumentList = CommandLineArgumentList.FromDictionary(dictionary);

         var result = target.Map(argumentList);

         result.First.Should().BeTrue();
         result.Second.Should().BeTrue();
         result.Third.Should().BeTrue();

         argumentList.Count.Should().Be(1);
         argumentList.ContainsName("unmaped").Should().BeTrue();
      }

      [TestMethod]
      public void EnsurePathCanBeMappedToIndexedArgument()
      {
         var target = Setup.ArgumentMapper().ForType<ArgumentsWithIndex>().Done();
         var dictionary = new Dictionary<string, CommandLineArgument>(StringComparer.InvariantCultureIgnoreCase)
         {
            { @"D:\HelloWorld\Hansi.txt", new CommandLineArgument { Name = @"D:\HelloWorld\Hansi.txt", Value = null, Index = 0, OriginalString = @"D:\HelloWorld\Hansi.txt"} }
         };

         var result = target.Map(CommandLineArgumentList.FromDictionary(dictionary));

         result.Path.Should().Be("D:\\HelloWorld\\Hansi.txt");
      }

      [TestMethod]
      public void EnsureIndexedArgumetnsAreMappedCorrectlyEvenIfOrderOfAttributeIsWrong()
      {
         var target = Setup.ArgumentMapper().ForType<ArgumentsWithIndex>().Done();
         var dictionary = new Dictionary<string, CommandLineArgument>(StringComparer.InvariantCultureIgnoreCase)
         {
            { "ThePath", new CommandLineArgument { Name = "ThePath", Index = 0, OriginalString = "ThePath"} },
            { "TheValue", new CommandLineArgument { Name = "TheValue", Index = 1, OriginalString = "TheValue"} }
         };

         var result = target.Map(CommandLineArgumentList.FromDictionary(dictionary));

         result.Path.Should().Be("ThePath");
         result.Value.Should().Be("TheValue");
      }

      [TestMethod]
      public void EnsureIndexedArgumentsAreNotSetWhenBetterArgumentMatchIsAvailable()
      {
         var target = Setup.ArgumentMapper().ForType<ArgumentsWithIndex>().Done();
         var dictionary = new Dictionary<string, CommandLineArgument>(StringComparer.InvariantCultureIgnoreCase)
         {
            { "ThePath", new CommandLineArgument { Name = "ThePath", Index = 0 } },
            { "Third", new CommandLineArgument { Name = "Third", Value = "bam", Index = 1} }
         };

         var result = target.Map(CommandLineArgumentList.FromDictionary(dictionary));

         result.Path.Should().Be("ThePath");
         result.Value.Should().BeNull();
         result.Third.Should().Be("bam");
         result.Wait.Should().BeFalse();
      }

      [TestMethod]
      public void EnsureIndexedArgumentsAreNotSetWhenBetterOptionMatchIsAvailable()
      {
         var target = Setup.ArgumentMapper().ForType<ArgumentsWithIndex>().Done();
         var dictionary = new Dictionary<string, CommandLineArgument>(StringComparer.InvariantCultureIgnoreCase)
         {
            { "ThePath", new CommandLineArgument { Name = "ThePath", Index = 0 } },
            { "Wait", new CommandLineArgument { Name = "Wait" , Index = 1} }
         };

         var result = target.Map(CommandLineArgumentList.FromDictionary(dictionary));

         result.Path.Should().Be("ThePath");
         result.Value.Should().BeNull();
         result.Third.Should().BeNull();
         result.Wait.Should().BeTrue();
      }

      #endregion

      public class ArgumentsWithIndex
      {
         #region Public Properties

         [Argument("Path", Index = 0)]
         public string Path { get; set; }

         [Argument("Value", Index = 1)]
         public string Value { get; set; }

         [Argument("Third", Index = 2)]
         public string Third { get; set; }

         [Option("Wait")]
         public bool Wait { get; set; }

         #endregion
      }

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