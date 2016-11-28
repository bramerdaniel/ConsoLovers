// // --------------------------------------------------------------------------------------------------------------------
// // <copyright file="NormalizeArguments.cs" company="ConsoLovers">
// //   Copyright (c) ConsoLovers  2015 - 2016
// // </copyright>
// // --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.UnitTests.CommandLineParserTests
{
   using System.Diagnostics.CodeAnalysis;
   using System.Linq;

   using ConsoLovers.ConsoleToolkit.CommandLineArguments;

   using FluentAssertions;

   using Microsoft.VisualStudio.TestTools.UnitTesting;

   [TestClass]
   [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
   public class NormalizeArguments
   {
      #region Public Methods and Operators

      [TestMethod]
      public void EnsureCorrectArgumentsAreNormalizedCorrectly()
      {
         var result = CommandLineArgumentParser.NormalizeArguments("a=5").ToList();
         result.Count.Should().Be(1);
         result.Contains("a=5").Should().BeTrue();
      }

      [TestMethod]
      public void EnsureSpacedArgumentsAreCorrected()
      {
         var result = CommandLineArgumentParser.NormalizeArguments("a", "=", "5").ToList();
         result.Count.Should().Be(1);
         result.Contains("a=5").Should().BeTrue();
      }

      [TestMethod]
      public void EnsureSpacedArgumentsAreCorrectedWithMultipleArguments()
      {
         var result = CommandLineArgumentParser.NormalizeArguments("-level", "=", "55", "/debug", ":", "true").ToList();
         result.Count.Should().Be(2);
         result.Contains("-level=55").Should().BeTrue();
         result.Contains("/debug:true").Should().BeTrue();
      }

      [TestMethod]
      public void EnsureSpacedEqualWithoutValueWorks()
      {
         var result = CommandLineArgumentParser.NormalizeArguments("a", "=").ToList();
         result.Count.Should().Be(1);
         result.Contains("a=").Should().BeTrue();
      }

      #endregion
   }
}