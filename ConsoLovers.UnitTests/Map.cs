// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Map.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.UnitTests
{
   using System.Diagnostics.CodeAnalysis;

   using ConsoLovers.ConsoleToolkit.CommandLineArguments;

   using FluentAssertions;

   using JetBrains.Annotations;

   using Microsoft.VisualStudio.TestTools.UnitTesting;

   [TestClass]
   [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
   public class Map
   {
      #region Public Methods and Operators

      [TestMethod]
      public void MapInvalidTypesShouldThrowException()
      {
         this.Invoking(x => CommandLineParser.Map<Arguments>(new[] { "Enum:Null" })).ShouldThrow<CommandLineArgumentException>().WithMessage(
            "The value Null of parameter Enum can not be converted into the expected type ConsoLovers.UnitTests.Boolenum. Possible values are True and False.");

         this.Invoking(x => CommandLineParser.Map<Arguments>(new[] { "ali:TRUE" })).ShouldThrow<CommandLineArgumentException>().WithMessage(
            "The value TRUE of parameter Alias can not be converted into the expected type System.Int32");

         this.Invoking(x => CommandLineParser.Map<Arguments>(new[] { "Integer:TRUE" })).ShouldThrow<CommandLineArgumentException>().WithMessage(
            "The value TRUE of parameter Integer can not be converted into the expected type System.Int32");

         this.Invoking(x => CommandLineParser.Map<Arguments>(new[] { "ElBool:Tuere" })).ShouldThrow<CommandLineArgumentException>().WithMessage(
            "The value Tuere of parameter ElBool can not be converted into the expected type System.Boolean");

         this.Invoking(x => CommandLineParser.Map<Arguments>(new[] { "Dooby:Twentyfive" })).ShouldThrow<CommandLineArgumentException>().WithMessage(
            "The value Twentyfive of parameter Dooby can not be converted into the expected type System.Double");
      }

      [TestMethod]
      public void MapNamedArgumentWithMultipleAliases()
      {
         var arguments = CommandLineParser.Map<Arguments>(new[] { " -Alias=5" });
         arguments.Alias.Should().Be(5);

         arguments = CommandLineParser.Map<Arguments>(new[] { " -a=6" });
         arguments.Alias.Should().Be(6);

         arguments = CommandLineParser.Map<Arguments>(new[] { " -al=7" });
         arguments.Alias.Should().Be(7);

         arguments = CommandLineParser.Map<Arguments>(new[] { " -ali=8" });
         arguments.Alias.Should().Be(8);
      }

      [TestMethod]
      public void MapNamedBoolean()
      {
         var arguments = CommandLineParser.Map<Arguments>(new[] { " -ElBool=True" });
         arguments.Boolean.Should().Be(true);

         arguments = CommandLineParser.Map<Arguments>(new[] { " -ElBool:TRUE " });
         arguments.Boolean.Should().Be(true);
      }

      [TestMethod]
      public void MapNamedEnum()
      {
         var arguments = CommandLineParser.Map<Arguments>(new[] { "-Enum=False " });
         arguments.Boolenum.Should().Be(Boolenum.False);

         arguments = CommandLineParser.Map<Arguments>(new[] { "-Enum:TRUE" });
         arguments.Boolenum.Should().Be(Boolenum.True);
      }

      [TestMethod]
      public void MapNamedInteger()
      {
         var arguments = CommandLineParser.Map<Arguments>(new[] { "-Integer=45 " });
         arguments.Integer.Should().Be(45);

         arguments = CommandLineParser.Map<Arguments>(new[] { "-Integer:45" });
         arguments.Integer.Should().Be(45);
      }

      [TestMethod]
      public void MapNamedString()
      {
         var arguments = CommandLineParser.Map<Arguments>(new[] { "-Name=Hans" });
         arguments.String.Should().Be("Hans");

         arguments = CommandLineParser.Map<Arguments>(new[] { "-Name:Hans " });
         arguments.String.Should().Be("Hans");
      }

      [TestMethod]
      public void MapOption()
      {
         var arguments = CommandLineParser.Map<Options>(new[] { " -Debug" });
         arguments.Debug.Should().BeTrue();
         arguments.Release.Should().BeFalse();

         arguments = CommandLineParser.Map<Options>(new[] { " -Release" });
         arguments.Debug.Should().BeFalse();
         arguments.Release.Should().BeTrue();
      }

      [TestMethod]
      public void MapOptionWithMultipleAliases()
      {
         var arguments = CommandLineParser.Map<Options>(new[] { " -blias=5" });
         arguments.Blias.Should().Be(5);

         arguments = CommandLineParser.Map<Options>(new[] { " /b=6" });
         arguments.Blias.Should().Be(6);

         arguments = CommandLineParser.Map<Options>(new[] { " -Bl=7" });
         arguments.Blias.Should().Be(7);

         arguments = CommandLineParser.Map<Options>(new[] { " -BLI=8" });
         arguments.Blias.Should().Be(8);
      }

      [TestMethod]
      public void MapStringWithQuotes()
      {
         var arguments = CommandLineParser.Map<Arguments>(new[] { " -Name=C:\\Path\\File.txt" });
         arguments.String.Should().Be(@"C:\Path\File.txt");

         arguments = CommandLineParser.Map<Arguments>(new[] { " -Name=C=\\Path\\File.txt" });
         arguments.String.Should().Be(@"C=\Path\File.txt");

         arguments = CommandLineParser.Map<Arguments>(new[] { " -Name:C=\\Path\\File.txt" });
         arguments.String.Should().Be(@"C=\Path\File.txt");

         arguments = CommandLineParser.Map<Arguments>(new[] { " -Name:C:\\Path\\File.txt" });
         arguments.String.Should().Be(@"C:\Path\File.txt");
      }

      [TestMethod]
      public void ParseMissingRequiredArgumentsShouldThrowException()
      {
         this.Invoking(t => CommandLineParser.Map<RequiredArguments>(new[] { string.Empty })).ShouldThrow<MissingCommandLineArgumentException>().Where(e => e.Argument == "Name");
      }

      #endregion

      public class Arguments
      {
         #region Public Properties

         [Argument("Alias", "a", "al", "ali")]
         public int Alias { get; [UsedImplicitly] set; }

         [Argument("ElBool")]
         public bool Boolean { get; [UsedImplicitly] set; }

         [Argument("Enum")]
         public Boolenum Boolenum { get; [UsedImplicitly] set; }

         [Argument("Dooby")]
         public double Double { get; set; }

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

   public enum Boolenum
   {
      True,

      False
   }
}