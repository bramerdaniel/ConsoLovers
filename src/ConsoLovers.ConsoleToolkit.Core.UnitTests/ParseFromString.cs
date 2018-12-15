// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ParseFromString.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests
{
   using System;
   using System.Collections.Generic;
   using System.Diagnostics.CodeAnalysis;
   using System.Linq;

   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments.Parsing;
   using ConsoLovers.ConsoleToolkit.Core.UnitTests.ArgumentEngine;
   using ConsoLovers.ConsoleToolkit.Core.UnitTests.Setups;

   using FluentAssertions;

   using Microsoft.VisualStudio.TestTools.UnitTesting;

   [TestClass]
   [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
   public class ParseFromString : ParserTestBase
   {
      #region Public Methods and Operators

      [TestMethod]
      public void ParsePathArgumentWithName()
      {
         var target = Setup.CommandLineArgumentParser().Done();

         var originalString = @"Path=D:\Temp\file.txt";
         var path = target.GetSingleArgument(originalString);

         path.Index.Should().Be(0);
         path.Name.Should().Be("Path");
         path.Value.Should().Be(@"D:\Temp\file.txt");
         path.OriginalString.Should().Be(originalString);
      }

      [TestMethod]
      public void ParsePathArgumentWithoutNameUsingQuotes()
      {
         var target = Setup.CommandLineArgumentParser().Done();

         var originalString = "\"D:\\Temp\\file.txt\"";
         var path = target.GetSingleArgument(originalString);

         path.Index.Should().Be(0);
         path.Name.Should().Be(@"D:\Temp\file.txt");
         path.Value.Should().Be(null);
         path.OriginalString.Should().Be(originalString);
      }

      [TestMethod]
      public void ParsePathArgumentWithoutNameWithoutQuotes()
      {
         var target = Setup.CommandLineArgumentParser().Done();

         var originalString = @"D:\Temp\file.txt";
         var path = target.GetSingleArgument(originalString);

         path.Index.Should().Be(0);
         path.Name.Should().Be("D");
         path.Value.Should().Be(@"\Temp\file.txt");
         path.OriginalString.Should().Be(originalString);
      }

      [TestMethod]
      public void ParsePathArgumentWithSpacesWithoutQuotes()
      {
         var target = Setup.CommandLineArgumentParser().Done();

         var originalString = @"D:\Temp dir\file.txt";
         var args = target.GetArguments(originalString);
         args.Should().HaveCount(2);

         args[0].Index.Should().Be(0);
         args[0].Name.Should().Be("D");
         args[0].Value.Should().Be(@"\Temp");
         args[0].OriginalString.Should().Be(@"D:\Temp");

         args[1].Index.Should().Be(1);
         args[1].Name.Should().Be(@"dir\file.txt");
         args[1].Value.Should().Be(null);
         args[1].OriginalString.Should().Be(@"dir\file.txt");
      }

      [TestMethod]
      public void ParsePathArgumentWithNameAndSpacesWithoutQuotes()
      {
         var target = Setup.CommandLineArgumentParser().Done();

         var originalString = @"Name=D:\Temp dir\file.txt";
         var args = target.GetArguments(originalString);
         args.Should().HaveCount(2);

         args[0].Index.Should().Be(0);
         args[0].Name.Should().Be("Name");
         args[0].Value.Should().Be(@"D:\Temp");
         args[0].OriginalString.Should().Be(@"Name=D:\Temp");

         args[1].Index.Should().Be(1);
         args[1].Name.Should().Be(@"dir\file.txt");
         args[1].Value.Should().Be(null);
         args[1].OriginalString.Should().Be(@"dir\file.txt");
      }

      [TestMethod]
      public void ParseArgumentWithEscapedQuotes()
      {
         var target = Setup.CommandLineArgumentParser().Done();

         var originalString = "Name=\" \\\"hans\\\" \"";
         var name = target.GetSingleArgument(originalString);

         name.Index.Should().Be(0);
         name.Name.Should().Be("Name");
         name.Value.Should().Be(" \"hans\" ");
         name.OriginalString.Should().Be(originalString);
      }
      
      [TestMethod]
      public void ParseSpecialCaseWithWhitespaceAfterEquals()
      {
         var target = Setup.CommandLineArgumentParser().Done();

         var originalString = "Name= \"Huba Sep\"";
         var args = target.GetArguments(originalString);
         args.Should().HaveCount(2);

         args[0].Index.Should().Be(0);
         args[0].Name.Should().Be("Name");
         args[0].Value.Should().Be(null);
         args[0].OriginalString.Should().Be("Name=");

         args[1].Index.Should().Be(1);
         args[1].Name.Should().Be("Huba Sep");
         args[1].Value.Should().Be(null);
         args[1].OriginalString.Should().Be("\"Huba Sep\"");
      }

      [TestMethod]
      public void ParseSpecialCaseWithNameSeparatorsAndWhitespaceInValue()
      {
         var target = Setup.CommandLineArgumentParser().Done();

         var originalString = "ex:\" ex : ex \"";
         var args = target.GetArguments(originalString);
         args.Should().HaveCount(1);

         args[0].Index.Should().Be(0);
         args[0].Name.Should().Be("ex");
         args[0].Value.Should().Be(" ex : ex ");
         args[0].OriginalString.Should().Be(originalString);
      }

      [TestMethod]
      public void ParseValueContainingAQuote()
      {
         var target = Setup.CommandLineArgumentParser().Done();

         var originalString = "Name=\"Meier\\\"hans\"";
         var args = target.GetArguments(originalString);
         args.Should().HaveCount(1);

         args[0].Index.Should().Be(0);
         args[0].Name.Should().Be("Name");
         args[0].Value.Should().Be("Meier\"hans");
         args[0].OriginalString.Should().Be(originalString);

         originalString = "\"Meier\\\"hans\"";
         args = target.GetArguments(originalString);
         args.Should().HaveCount(1);

         args[0].Index.Should().Be(0);
         args[0].Value.Should().Be(null);
         args[0].Name.Should().Be("Meier\"hans");
         args[0].OriginalString.Should().Be(originalString);
      }

      [TestMethod]
      public void ParseValueContainingAQuoteAndWhitespaces()
      {
         var target = Setup.CommandLineArgumentParser().Done();

         var originalString = "Name=\"me \\\" ha\"";
         var args = target.GetArguments(originalString);
         args.Should().HaveCount(1);

         args[0].Index.Should().Be(0);
         args[0].Name.Should().Be("Name");
         args[0].Value.Should().Be("me \" ha");
         args[0].OriginalString.Should().Be(originalString);

         originalString = "\"me \\\" ha\"";
         args = target.GetArguments(originalString);
         args.Should().HaveCount(1);

         args[0].Index.Should().Be(0);
         args[0].Name.Should().Be("me \" ha");
         args[0].Value.Should().Be(null);
         args[0].OriginalString.Should().Be(originalString);
      }

      [TestMethod]
      public void ParseCompletelyQuotesArgument()
      {
         var target = Setup.CommandLineArgumentParser().Done();

         var originalString = "\"Name=value\"";
         var args = target.GetArguments(originalString);
         args.Should().HaveCount(1);

         args[0].Index.Should().Be(0);
         args[0].Name.Should().Be("Name=value");
         args[0].Value.Should().Be(null);
         args[0].OriginalString.Should().Be(originalString);
      }

      
      [TestMethod]
      public void ParseSpecialCaseNameSeparatorsInValue()
      {
         var target = Setup.CommandLineArgumentParser().Done();

         var originalString = "ex:\"ex:ex\"";
         var args = target.GetArguments(originalString);
         args.Should().HaveCount(1);

         args[0].Index.Should().Be(0);
         args[0].Name.Should().Be("ex");
         args[0].Value.Should().Be("ex:ex");
         args[0].OriginalString.Should().Be(originalString);
      } 
      
      [TestMethod]
      public void EnsureNameContainingAnArgumentSignIsPossible()
      {
         var target = Setup.CommandLineArgumentParser().Done();

         var originalString = "my-Arg=someValue";
         var args = target.GetSingleArgument(originalString);

         args.Index.Should().Be(0);
         args.Name.Should().Be("my-Arg");
         args.Value.Should().Be("someValue");
         args.OriginalString.Should().Be(originalString);

         originalString = "my/Arg=someValue";
         args = target.GetSingleArgument(originalString);

         args.Index.Should().Be(0);
         args.Name.Should().Be("my/Arg");
         args.Value.Should().Be("someValue");
         args.OriginalString.Should().Be(originalString);
      }

      [TestMethod]
      public void EnsureNestedQuotedArgumentsAreParsedCorrectly()
      {
         var target = Setup.CommandLineArgumentParser().Done();

         // expected 1 argument with name=argument and value=key:oder=value
         var originalString = "argument:\"key:oder=value\""; 
         var arg = target.GetSingleArgument(originalString);

         arg.Index.Should().Be(0);
         arg.Name.Should().Be("argument");
         arg.Value.Should().Be("key:oder=value");
         arg.OriginalString.Should().Be(originalString);

         // expected 1 argument with name=execute and value=execute:execute
         originalString = "execute:\"execute:execute\""; 
         arg = target.GetSingleArgument(originalString);

         arg.Index.Should().Be(0);
         arg.Name.Should().Be("execute");
         arg.Value.Should().Be("execute:execute");
         arg.OriginalString.Should().Be(originalString);
      }
      
      [TestMethod]
      public void ParseSomeUglySpecialCases()
      {
         var target = Setup.CommandLineArgumentParser().Done();

         // => expected 1 argument with name=execute, value=execute:-execute=13/abc
         var originalString = "-execute=execute:-execute=13/abc";
         var arg = target.GetSingleArgument(originalString);

         arg.Index.Should().Be(0);
         arg.Name.Should().Be("execute");
         arg.Value.Should().Be("execute:-execute=13/abc");
         arg.OriginalString.Should().Be(originalString);

         // expected 1 argument with  name=execute and value= execute -execute=13 
         originalString = "execute:\" execute -execute=13 \""; 
         arg = target.GetSingleArgument(originalString);

         arg.Index.Should().Be(0);
         arg.Name.Should().Be("execute");
         arg.Value.Should().Be(" execute -execute=13 ");
         arg.OriginalString.Should().Be(originalString);
      }

      [TestMethod]
      public void EnsureAllKindOfArgumentsAreParsedCorrectly()
      {
         var target = Setup.CommandLineArgumentParser().Done();

         var arguments = target.ParseArguments(@"Merge File=D:\Temp\file.txt -p -o");

         AssertContains(arguments, "Merge", null, 0);
         AssertContains(arguments, "File", @"D:\Temp\file.txt", 1);
         AssertContains(arguments, "P", null, 2);
         AssertContains(arguments, "O", null, 3);
      }

      [TestMethod]
      public void EnsureMultipleOptionsAreParsedCorrectly()
      {
         var target = Setup.CommandLineArgumentParser().Done();

         var arguments = target.ParseArguments("-d /a -x");
         arguments.ContainsKey("d").Should().BeTrue();
         arguments.ContainsKey("a").Should().BeTrue();
         arguments.ContainsKey("x").Should().BeTrue();

         arguments["D"].Value.Should().BeNull();
         arguments["A"].Value.Should().BeNull();
         arguments["X"].Value.Should().BeNull();
      }

      [TestMethod]
      public void EnsureMultipleParametersAreParsedCorrectly()
      {
         var target = Setup.CommandLineArgumentParser().Done();

         var arguments = target.ParseArguments("-Ä=4 /Ü:5.5 -Ö:Peter");
         arguments.ContainsKey("ä").Should().BeTrue();
         arguments.ContainsKey("ü").Should().BeTrue();
         arguments.ContainsKey("ö").Should().BeTrue();

         arguments["Ä"].Value.Should().Be("4");
         arguments["Ü"].Value.Should().Be("5.5");
         arguments["Ö"].Value.Should().Be("Peter");
      }

      [TestMethod]
      public void EnsureParsingOfAPathWorksCorrectly()
      {
         var target = Setup.CommandLineArgumentParser().Done();

         var originalString = "\"D:\\Temp\\file.txt\"";
         var arguments = target.GetArguments(originalString);
         arguments.Should().HaveCount(1);

         arguments[0].Index.Should().Be(0);
         arguments[0].Name.Should().Be("D:\\Temp\\file.txt");
         arguments[0].Value.Should().Be(null);
         arguments[0].OriginalString.Should().Be(originalString);
      }

      [TestMethod]
      public void EnsureAllDifferentArgumentInicatorsWorkCorrectly()
      {
         var target = Setup.CommandLineArgumentParser().Done();

         var originalString = "-Name:Hans";
         var arguments = target.GetSingleArgument(originalString);
         arguments.Index.Should().Be(0);
         arguments.Name.Should().Be("Name");
         arguments.Value.Should().Be("Hans");
         arguments.OriginalString.Should().Be(originalString);

         originalString = "/Name:Hans";
         arguments = target.GetSingleArgument(originalString);
         arguments.Index.Should().Be(0);
         arguments.Name.Should().Be("Name");
         arguments.Value.Should().Be("Hans");
         arguments.OriginalString.Should().Be(originalString);

         originalString = "Name:Hans";
         arguments = target.GetSingleArgument(originalString);
         arguments.Index.Should().Be(0);
         arguments.Name.Should().Be("Name");
         arguments.Value.Should().Be("Hans");
         arguments.OriginalString.Should().Be(originalString);
      }
      
      [TestMethod]
      public void EnsureArgumentInicatorsArePossibleInValue()
      {
         var target = Setup.CommandLineArgumentParser().Done();

         var originalString = "Name:-Ha-ns-";
         var arguments = target.GetSingleArgument(originalString);
         arguments.Index.Should().Be(0);
         arguments.Name.Should().Be("Name");
         arguments.Value.Should().Be("-Ha-ns-");
         arguments.OriginalString.Should().Be(originalString);

         originalString = "Name:/Ha/ns/";
         arguments = target.GetSingleArgument(originalString);
         arguments.Index.Should().Be(0);
         arguments.Name.Should().Be("Name");
         arguments.Value.Should().Be("/Ha/ns/");
         arguments.OriginalString.Should().Be(originalString);
      }

      [TestMethod]
      public void EnsureParsingOfSingleOptionWorksCorrectly()
      {
         var target = Setup.CommandLineArgumentParser().Done();

         var arguments = target.ParseArguments("-Enabled");
         arguments.Count.Should().Be(1);
         AssertContains(arguments, "Enabled", null, 0);

         arguments = target.ParseArguments("/Enabled");
         arguments.Count.Should().Be(1);
         AssertContains(arguments, "Enabled", null, 0);

         arguments = target.ParseArguments("Enabled");
         arguments.Count.Should().Be(1);
         AssertContains(arguments, "Enabled", null, 0);
      }

      [TestMethod]
      public void EnsureParsingOfSingleParameterWorksCorrectly()
      {
         var target = Setup.CommandLineArgumentParser().Done();

         var arguments = target.ParseArguments("-Name:Hans");
         arguments.Count.Should().Be(1);
         AssertContains(arguments, "Name", "Hans", 0);
      }

      [TestMethod]
      public void ParseAPathThatShouldBecomeIndexedArgumentLater()
      {
         var target = Setup.CommandLineArgumentParser().Done();

         var arguments = target.ParseArguments(@"D:\HelloWorld\Hansi.txt");
         arguments.ContainsKey("D").Should().BeTrue();

         arguments["D"].OriginalString.Should().Be(@"D:\HelloWorld\Hansi.txt");
      }

      [TestMethod]
      public void ParseCommand()
      {
         var target = Setup.CommandLineArgumentParser().Done();

         var arguments = target.ParseArguments("Command");
         arguments.ContainsKey("Command").Should().BeTrue();
         arguments.ContainsKey("command").Should().BeTrue();
         arguments.ContainsKey("COMMAND").Should().BeTrue();

         arguments["COMMAND"].Value.Should().BeNull();
      }

      /// <summary>Parses the named string.</summary>
      [TestMethod]
      public void ParseNamedArgument()
      {
         var target = Setup.CommandLineArgumentParser().Done();

         var arguments = target.ParseArguments("-Name=Hans");
         arguments.ContainsKey("Name").Should().BeTrue();
         arguments.ContainsKey("name").Should().BeTrue();
         arguments.ContainsKey("NAME").Should().BeTrue();

         arguments["Name"].Value.Should().Be("Hans");

         arguments = target.ParseArguments("-Name:Olga");
         arguments.ContainsKey("Name").Should().BeTrue();
         arguments.ContainsKey("name").Should().BeTrue();
         arguments.ContainsKey("NAME").Should().BeTrue();

         arguments["Name"].Value.Should().Be("Olga");
      }

      [TestMethod]
      public void ParseOption()
      {
         var target = Setup.CommandLineArgumentParser().Done();

         var arguments = target.ParseArguments(" -Debug");
         arguments.ContainsKey("Debug").Should().BeTrue();
         arguments.ContainsKey("debug").Should().BeTrue();
         arguments.ContainsKey("DEBUG").Should().BeTrue();
         arguments.ContainsKey("Release").Should().BeFalse();
         arguments.ContainsKey("release").Should().BeFalse();
         arguments.ContainsKey("RELEASE").Should().BeFalse();

         arguments["DEBUg"].Value.Should().BeNull();

         arguments = target.ParseArguments(" -Release");
         arguments.ContainsKey("Debug").Should().BeFalse();
         arguments.ContainsKey("debug").Should().BeFalse();
         arguments.ContainsKey("DEBUG").Should().BeFalse();
         arguments.ContainsKey("Release").Should().BeTrue();
         arguments.ContainsKey("release").Should().BeTrue();
         arguments.ContainsKey("RELEASE").Should().BeTrue();

         arguments["ReleasE"].Value.Should().BeNull();
      }

      #endregion

      #region Methods

      private static void AssertContains(IDictionary<string, CommandLineArgument> arguments, string expectedKey, string expectedValue = null, int index = -1)
      {
         arguments.Should().ContainKey(expectedKey);
         arguments.Should().ContainKey(expectedKey.ToLower());
         arguments.Should().ContainKey(expectedKey.ToUpper());

         var argument = arguments[expectedKey];

         string.Equals(argument.Name, expectedKey, StringComparison.InvariantCultureIgnoreCase).Should().BeTrue();
         argument.Value.Should().Be(expectedValue);

         argument.Index.Should().Be(index);
      }

      #endregion
   }

   public static class ParserExtensions
   {
      public static CommandLineArgument GetSingleArgument(this CommandLineArgumentParser paser, string args)
      {
         var arguments = paser.ParseArguments(args);
         return arguments.Values.Single(arg => arg.Index == 0);
      }

      public static IList<CommandLineArgument> GetArguments(this CommandLineArgumentParser paser, string args)
      {
         var arguments = paser.ParseArguments(args);
         return arguments.Values.ToArray();
      }
   }
}