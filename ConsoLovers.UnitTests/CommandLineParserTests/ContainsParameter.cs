// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContainsParameter.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.UnitTests.CommandLineParserTests
{
   using System.Diagnostics.CodeAnalysis;

   using ConsoLovers.ConsoleToolkit.CommandLineArguments;

   using FluentAssertions;

   using Microsoft.VisualStudio.TestTools.UnitTesting;

   [TestClass]
   [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
   public class ContainsParameter : ParserTestBase
   {
      #region Public Methods and Operators

      [TestMethod]
      public void ContainsParameterShouldBeFalse()
      {
         var arguments = Parse(new[] { "-Name:Hans" });
         arguments.ContainsKey("Name").Should().BeTrue();
      }

      [TestMethod]
      public void ContainsParameterShouldBeTrue()
      {
         var arguments = Parse(new[] { "-FullName:Hans" });
         arguments.ContainsKey("Name").Should().BeFalse();
      }

      #endregion

      #region Methods


      #endregion

      public class Args
      {
         #region Public Properties

         [Argument]
         public string Name { get; set; }

         #endregion
      }
   }
}