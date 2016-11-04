// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Parse.cs" company="ConsoLovers">
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
   public class Parse : ParserTestBase
   {
      #region Public Methods and Operators

      [TestMethod]
      public void ParseMultipleNamedParameters()
      {
         var arguments = Parse(new[] { "-Ä=4", "/Ü:5.5", "-Ö:Peter" });
         arguments.ContainsKey("ä").Should().BeTrue();
         arguments.ContainsKey("ü").Should().BeTrue();
         arguments.ContainsKey("ö").Should().BeTrue();

         arguments["Ä"].Should().Be("4");
         arguments["Ü"].Should().Be("5.5");
         arguments["Ö"].Should().Be("Peter");
      }

      [TestMethod]
      public void ParseMultipleOptions()
      {
         var arguments = Parse(new[] { "-d", "/a", "-x" });
         arguments.ContainsKey("d").Should().BeTrue();
         arguments.ContainsKey("a").Should().BeTrue();
         arguments.ContainsKey("x").Should().BeTrue();

         arguments["D"].Should().Be("true");
         arguments["A"].Should().Be("true");
         arguments["X"].Should().Be("true");
      }

      /// <summary>Parses the named string.</summary>
      [TestMethod]
      public void ParseNamedArgument()
      {
         var arguments = Parse(new[] { "-Name=Hans" });
         arguments.ContainsKey("Name").Should().BeTrue();
         arguments.ContainsKey("name").Should().BeTrue();
         arguments.ContainsKey("NAME").Should().BeTrue();

         arguments["Name"].Should().Be("Hans");

         arguments = Parse(new[] { "-Name:Olga" });
         arguments.ContainsKey("Name").Should().BeTrue();
         arguments.ContainsKey("name").Should().BeTrue();
         arguments.ContainsKey("NAME").Should().BeTrue();

         arguments["Name"].Should().Be("Olga");
      }

      [TestMethod]
      public void ParseOption()
      {
         var arguments = Parse(new[] { " -Debug" });
         arguments.ContainsKey("Debug").Should().BeTrue();
         arguments.ContainsKey("debug").Should().BeTrue();
         arguments.ContainsKey("DEBUG").Should().BeTrue();
         arguments.ContainsKey("Release").Should().BeFalse();
         arguments.ContainsKey("release").Should().BeFalse();
         arguments.ContainsKey("RELEASE").Should().BeFalse();

         arguments["DEBUg"].Should().Be("true");

         arguments = Parse(new[] { " -Release" });
         arguments.ContainsKey("Debug").Should().BeFalse();
         arguments.ContainsKey("debug").Should().BeFalse();
         arguments.ContainsKey("DEBUG").Should().BeFalse();
         arguments.ContainsKey("Release").Should().BeTrue();
         arguments.ContainsKey("release").Should().BeTrue();
         arguments.ContainsKey("RELEASE").Should().BeTrue();

         arguments["ReleasE"].Should().Be("true");
      }

      #endregion
   }
}