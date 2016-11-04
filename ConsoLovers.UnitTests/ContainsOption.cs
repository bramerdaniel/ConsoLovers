// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContainsOption.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.UnitTests
{
   using System.Diagnostics.CodeAnalysis;

   using ConsoLovers.ConsoleToolkit.CommandLineArguments;
   using ConsoLovers.UnitTests.ArgumentEngine;

   using FluentAssertions;

   using Microsoft.VisualStudio.TestTools.UnitTesting;

   [TestClass]
   [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
   public class ContainsOption : ParserTestBase
   {
      #region Public Methods and Operators

      [TestMethod]
      public void ContainsOptionShouldBeFalse()
      {
         var arguments = Parse(new[] { "-other" });
         arguments.ContainsKey("enabled").Should().BeFalse();
      }

      [TestMethod]
      public void ContainsOptionShouldBeTrue()
      {
         var arguments = Parse(new[] { "-enabled" });
         arguments.ContainsKey("enabled").Should().BeTrue();
      }

      #endregion

      public class Args
      {
         #region Public Properties

         [Argument("Enabled")]
         public bool Enabled { get; set; }

         #endregion
      }
   }
}