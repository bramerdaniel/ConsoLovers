// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NormalizeArguments.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.CommandLineParserTests
{
   using System.Diagnostics.CodeAnalysis;
   using System.Linq;

   using ConsoLovers.ConsoleToolkit.Core.UnitTests.ArgumentEngine;

   using FluentAssertions;

   using Microsoft.VisualStudio.TestTools.UnitTesting;

   [TestClass]
   [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
   public class NormalizeArguments : ParserTestBase
   {
      #region Public Methods and Operators

      [TestMethod]
      public void EnsureCorrectArgumentsAreNormalizedCorrectly()
      {
         var result = GetTarget().NormalizeArguments("name=hans").ToList();
         result.Count.Should().Be(1);
         result.Contains("name=hans").Should().BeTrue();
      }

      [TestMethod]
      public void EnsureSpacedArgumentsAreCorrected()
      {
         var result = GetTarget().NormalizeArguments("a=", "5").ToList();
         result.Count.Should().Be(1);
         result.Contains("a=5").Should().BeTrue();

         result = GetTarget().NormalizeArguments("a", "=5").ToList();
         result.Count.Should().Be(1);
         result.Contains("a=5").Should().BeTrue();


         result = GetTarget().NormalizeArguments("a", "=", "5").ToList();
         result.Count.Should().Be(1);
         result.Contains("a=5").Should().BeTrue();
      }

      [TestMethod]
      public void EnsureEmptyStringIsRecognizedAsValue()
      {
         var result = GetTarget().NormalizeArguments("a=\"\"", "b=").ToList();
         result.Count.Should().Be(2);
         result.Contains("a=\"\"").Should().BeTrue();
         result.Contains("b=").Should().BeTrue();
      }


      [TestMethod]
      public void EnsureExpectedBehaviourForValuesEndingWithEqualsSign()
      {
         var result = GetTarget().NormalizeArguments("a=", "myEqualSign=").ToList();
         result.Count.Should().Be(1);
         result.Contains("a=myEqualSign=").Should().BeTrue();
      }

      [TestMethod]
      public void EnsureSpacedArgumentsAreCorrectedWithMultipleArguments()
      {
         var result = GetTarget().NormalizeArguments("-level", "=", "55", "/debug", ":", "true").ToList();
         result.Count.Should().Be(2);
         result.Contains("-level=55").Should().BeTrue();
         result.Contains("/debug:true").Should().BeTrue();
      }

      [TestMethod]
      public void EnsureSpacedEqualWithoutValueWorks()
      {
         var result = GetTarget().NormalizeArguments("a", "=").ToList();
         result.Count.Should().Be(1);
         result.Contains("a=").Should().BeTrue();
      }

      #endregion
   }
}