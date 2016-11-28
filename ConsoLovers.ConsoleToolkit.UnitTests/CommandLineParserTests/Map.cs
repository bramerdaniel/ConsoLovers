// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Map.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.UnitTests.CommandLineParserTests
{
   using System.Diagnostics.CodeAnalysis;

   using ConsoLovers.ConsoleToolkit.CommandLineArguments;

   using FluentAssertions;

   using JetBrains.Annotations;

   using Microsoft.VisualStudio.TestTools.UnitTesting;

   [TestClass]
   [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
   public class Map : ParserTestBase
   {
      #region Public Methods and Operators

      [TestMethod]
      public void MapNamedArgumentWithMultipleAliases()
      {
         var arguments = GetTarget().Map<Arguments>(new[] { " -Alias=5" });
         arguments.Alias.Should().Be(5);

         arguments = GetTarget().Map<Arguments>(new[] { " -a=6" });
         arguments.Alias.Should().Be(6);

         arguments = GetTarget().Map<Arguments>(new[] { " -al=7" });
         arguments.Alias.Should().Be(7);

         arguments = GetTarget().Map<Arguments>(new[] { " -ali=8" });
         arguments.Alias.Should().Be(8);
      }

      [TestMethod]
      public void MapNamedBoolean()
      {
         var arguments = GetTarget().Map<Arguments>(new[] { " -ElBool=True" });
         arguments.Boolean.Should().Be(true);

         arguments = GetTarget().Map<Arguments>(new[] { " -ElBool:TRUE " });
         arguments.Boolean.Should().Be(true);
      }

      [TestMethod]
      public void MapNamedInteger()
      {
         var arguments = GetTarget().Map<Arguments>(new[] { "-Integer=45 " });
         arguments.Integer.Should().Be(45);

         arguments = GetTarget().Map<Arguments>(new[] { "-Integer:45" });
         arguments.Integer.Should().Be(45);
      }

      [TestMethod]
      public void MapNamedString()
      {
         var arguments = GetTarget().Map<Arguments>(new[] { "-Name=Hans" });
         arguments.String.Should().Be("Hans");

         arguments = GetTarget().Map<Arguments>(new[] { "-Name:Hans " });
         arguments.String.Should().Be("Hans");
      }

      [TestMethod]
      public void MapOption()
      {
         var arguments = GetTarget().Map<Options>(new[] { " -Debug" });
         arguments.Debug.Should().BeTrue();
         arguments.Release.Should().BeFalse();

         arguments = GetTarget().Map<Options>(new[] { " -Release" });
         arguments.Debug.Should().BeFalse();
         arguments.Release.Should().BeTrue();
      }

      [TestMethod]
      public void MapOptionWithMultipleAliases()
      {
         var arguments = GetTarget().Map<Options>(new[] { " -blias=5" });
         arguments.Blias.Should().Be(5);

         arguments = GetTarget().Map<Options>(new[] { " /b=6" });
         arguments.Blias.Should().Be(6);

         arguments = GetTarget().Map<Options>(new[] { " -Bl=7" });
         arguments.Blias.Should().Be(7);

         arguments = GetTarget().Map<Options>(new[] { " -BLI=8" });
         arguments.Blias.Should().Be(8);
      }

      [TestMethod]
      public void MapStringWithQuotes()
      {
         var arguments = GetTarget().Map<Arguments>(new[] { " -Name=C:\\Path\\File.txt" });
         arguments.String.Should().Be(@"C:\Path\File.txt");

         arguments = GetTarget().Map<Arguments>(new[] { " -Name=C=\\Path\\File.txt" });
         arguments.String.Should().Be(@"C=\Path\File.txt");

         arguments = GetTarget().Map<Arguments>(new[] { " -Name:C=\\Path\\File.txt" });
         arguments.String.Should().Be(@"C=\Path\File.txt");

         arguments = GetTarget().Map<Arguments>(new[] { " -Name:C:\\Path\\File.txt" });
         arguments.String.Should().Be(@"C:\Path\File.txt");
      }

      [TestMethod]
      public void ParseMissingRequiredArgumentsShouldThrowException()
      {
         this.Invoking(t => GetTarget().Map<RequiredArguments>(new[] { string.Empty })).ShouldThrow<MissingCommandLineArgumentException>().Where(e => e.Argument == "Name");
      }

      #endregion

      public class Arguments
      {
         #region Public Properties

         [Argument("Alias", "a", "al", "ali")]
         public int Alias { get; [UsedImplicitly] set; }

         [Argument("ElBool")]
         public bool Boolean { get; [UsedImplicitly] set; }

         [Argument]
         public int Integer { get; [UsedImplicitly] set; }

         [Argument("Name")]
         public string String { get; [UsedImplicitly] set; }

         #endregion
      }

      public class Options
      {
         #region Public Properties

         [Argument("Blias", "b", "bl", "bli")]
         public int Blias { get; [UsedImplicitly] set; }

         [Option]
         public bool Debug { get; [UsedImplicitly] set; }

         [Option]
         public bool Release { get; [UsedImplicitly] set; }

         #endregion
      }

      public class RequiredArguments
      {
         #region Public Properties

         [Argument(Required = true)]
         [UsedImplicitly]
         public string Name { get; set; }

         #endregion
      }
   }
}