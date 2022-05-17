// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Map.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests
{
   using System;
   using System.Diagnostics.CodeAnalysis;

   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
   using ConsoLovers.ConsoleToolkit.Core.UnitTests.ArgumentEngine;

   using FluentAssertions;

   using JetBrains.Annotations;

   using Microsoft.VisualStudio.TestTools.UnitTesting;

   [TestClass]
   [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
   public class Map : EngineTestBase
   {
      #region Public Methods and Operators

      [TestMethod]
      public void EnsureOnlyPropertiesWithAttributesGetMapped()
      {
         var arguments = GetTarget().Map<UglyArgs>(new[] { " -AValue=45" });
         arguments.XValue.Should().Be(45);
         arguments.AValue.Should().Be(0);
      }

      [TestMethod]
      public void EnsureUnprocessedArgumentsRaisesEventCorrectly()
      {
         var engine = GetTarget();
         var monitor = engine.Monitor();
         engine.Map<Arguments>(new[] { " -Integer=1", "-SpellingError=5" });

         monitor.Should().Raise(nameof(CommandLineEngine.UnhandledCommandLineArgument))
            .WithArgs<CommandLineArgumentEventArgs>(e => e.Argument.Name == "SpellingError" && e.Argument.Value == "5" && e.Argument.Index == 1);

         engine = GetTarget();
         monitor = engine.Monitor();
         engine.Map<Arguments>(new[] { "-SpellingError=5", " -Integer=1" });

         monitor.Should().Raise(nameof(CommandLineEngine.UnhandledCommandLineArgument))
            .WithArgs<CommandLineArgumentEventArgs>(e => e.Argument.Name == "SpellingError" && e.Argument.Value == "5" && e.Argument.Index == 0);
      }

      [TestMethod]
      public void MapIndexedArguments()
      {
         var path = "\"C:\\Path\\File.txt\"";
         var name = "Nick Oteen";
         var arguments = GetTarget().Map<IndexedArguments>(new[] { path, name });
         arguments.Path.Should().Be(path);
         arguments.Name.Should().Be(name);
      }

      [TestMethod]
      public void MapIndexedAndNamesArguments()
      {
         var path = "\"C:\\Path\\File.txt\"";
         var name = "Nick Oteen";
         var arguments = GetTarget().Map<IndexedAndNamesArguments>(new[] { path, name });
         arguments.Path.Should().Be(path);
         arguments.Name.Should().Be(name);
      }

      [TestMethod]
      public void MapInvalidTypesShouldThrowException()
      {
         this.Invoking(x => GetTarget().Map<Arguments>(new[] { "Enum:Null" })).Should().Throw<CommandLineArgumentException>().WithMessage(
            "The value Null of parameter Enum can not be converted into the expected type " + typeof(Boolenum).FullName + ". Possible values are True and False.");

         this.Invoking(x => GetTarget().Map<Arguments>(new[] { "ali:TRUE" })).Should().Throw<CommandLineArgumentException>()
            .WithMessage("The value TRUE of parameter ali can not be converted into the expected type System.Int32");

         this.Invoking(x => GetTarget().Map<Arguments>(new[] { "Integer:TRUE" })).Should().Throw<CommandLineArgumentException>()
            .WithMessage("The value TRUE of parameter Integer can not be converted into the expected type System.Int32");

         this.Invoking(x => GetTarget().Map<Arguments>(new[] { "ElBool:Tuere" })).Should().Throw<CommandLineArgumentException>()
            .WithMessage("The value Tuere of parameter ElBool can not be converted into the expected type System.Boolean");

         this.Invoking(x => GetTarget().Map<Arguments>(new[] { "Dooby:Twentyfive" })).Should().Throw<CommandLineArgumentException>()
            .WithMessage("The value Twentyfive of parameter Dooby can not be converted into the expected type System.Double");
      }

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
      public void MapNamedEnum()
      {
         var arguments = GetTarget().Map<Arguments>(new[] { "-Enum=False " });
         arguments.Boolenum.Should().Be(Boolenum.False);

         arguments = GetTarget().Map<Arguments>(new[] { "-Enum:TRUE" });
         arguments.Boolenum.Should().Be(Boolenum.True);
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

         arguments = GetTarget().Map<Arguments>(new[] { "-Name:\"Hans\"" });
         arguments.String.Should().Be("\"Hans\"");
      }

      [TestMethod]
      public void MapNamedStringWithQuotations()
      {
         var arguments = GetTarget().Map<Arguments>(new[] { "-Trimmed:\"TheValue\"" });
         arguments.Trimmed.Should().Be("TheValue");

         arguments = GetTarget().Map<Arguments>(new[] { "-Trimmed:\"\"" });
         arguments.Trimmed.Should().BeEmpty();
      }

      [TestMethod]
      public void MapNullableEnum()
      {
         var arguments = GetTarget().Map<Arguments>(new[] { "-Nullinger=True" });
         arguments.Nullinger.Should().Be(Boolenum.True);

         arguments = GetTarget().Map<Arguments>(new[] { "-Nullinger=False" });
         arguments.Nullinger.Should().Be(Boolenum.False);

         arguments = GetTarget().Map<Arguments>(new[] { "-Name:Hans" });
         arguments.Nullinger.Should().Be(null);
         arguments = GetTarget().Map<Arguments>(new[] { "-Nullinger:Null" });
         arguments.Nullinger.Should().Be(null);
         arguments = GetTarget().Map<Arguments>(new[] { "-Nullinger:null" });
         arguments.Nullinger.Should().Be(null);
      }

      [TestMethod]
      public void MapNullableIntegers()
      {
         var arguments = GetTarget().Map<Arguments>(new[] { "-NullInteg=25" });
         arguments.NullInteg.Should().Be(25);

         arguments = GetTarget().Map<Arguments>(new[] { "-Name:Hans" });
         arguments.NullInteg.Should().Be(null);
         arguments = GetTarget().Map<Arguments>(new[] { "-Nullinger:Null" });
         arguments.NullInteg.Should().Be(null);
         arguments = GetTarget().Map<Arguments>(new[] { "-Nullinger:null" });
         arguments.NullInteg.Should().Be(null);
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
      public void MappingACaseSensitiveValuesWorksCorrectly()
      {
         GetTarget().Map<Arguments>(new[] { " -Enum=true" }).Boolenum.Should().Be(Boolenum.True);
         GetTarget().Map<Arguments>(new[] { " -Enum=TRue" }).Boolenum.Should().Be(Boolenum.True);
         GetTarget().Map<Arguments>(new[] { " -ElBool=TRUE" }).Boolean.Should().Be(true);
         GetTarget().Map<Arguments>(new[] { " -ElBool=tRUE" }).Boolean.Should().Be(true);
      }

      [TestMethod]
      public void MappingAmbiguousOptionsShouldThrowException()
      {
         var arguments = GetTarget().Map<Options>(new[] { "-A", "-Amb", "-am" });
         arguments.Ambiguous.Should().Be(true);
      }

      [TestMethod]
      public void MappingANamedBooleanArgumentWithoutValueMustThrowException()
      {
         GetTarget().Invoking(t => t.Map<Arguments>(new[] { " -ElBool" })).Should().Throw<CommandLineArgumentException>().Where(e => e.Reason == ErrorReason.ArgumentWithoutValue);
      }

      [TestMethod]
      public void MappingANamedBooleanArgumentWithoutValueMustThrowExceptionForAliasAlso()
      {
         GetTarget().Invoking(t => t.Map<Arguments>(new[] { " -ali" })).Should().Throw<CommandLineArgumentException>().Where(e => e.Reason == ErrorReason.ArgumentWithoutValue);
      }

      [TestMethod]
      public void MappingAnOptionWithValueMustThrowException()
      {
         GetTarget().Invoking(t => t.Map<Options>(new[] { "-Debug=true" })).Should().Throw<CommandLineArgumentException>();
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
         this.Invoking(t => GetTarget().Map<RequiredArguments>(new[] { string.Empty })).Should().Throw<MissingCommandLineArgumentException>().Where(e => e.Argument == "Name");
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

         [Argument("Nullinger")]
         public Boolenum? Nullinger { get; [UsedImplicitly] set; }

         [Argument("NullInteg")]
         public int? NullInteg { get; [UsedImplicitly] set; }

         [Argument("Name")]
         public string String { get; [UsedImplicitly] set; }

         [Argument("Trimmed", TrimQuotation = true)]
         public string Trimmed { get; [UsedImplicitly] set; }

         #endregion
      }

      public class IndexedArguments
      {
         #region Public Properties

         [Argument(1)]
         public string Name { get; [UsedImplicitly] set; }

         [Argument(0)]
         public string Path { get; [UsedImplicitly] set; }

         #endregion
      }

      public class IndexedAndNamesArguments
      {
         #region Public Properties
         
         [Argument("Name", Index = 1)]
         public string Name { get; [UsedImplicitly] set; }
         
         [Argument("Path", Index = 0)]
         public string Path { get; [UsedImplicitly] set; }

         #endregion
      }

      public class Options
      {
         #region Public Properties

         [Option("Ambiguous ", "a", "am", "amb")]
         public bool Ambiguous { get; [UsedImplicitly] set; }

         [Argument("Blias", "b", "bl", "bli")]
         public int Blias { get; [UsedImplicitly] set; }

         [Option]
         public bool Debug { get; [UsedImplicitly] set; }

         [Option]
         public Nullable<bool> Nully { get; set; }

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

      public class UglyArgs
      {
         #region Public Properties

         public int AValue { get; [UsedImplicitly] set; }

         [Argument("AValue")]
         public int XValue { get; [UsedImplicitly] set; }

         #endregion
      }
   }

   public enum Boolenum
   {
      True,

      False
   }
}