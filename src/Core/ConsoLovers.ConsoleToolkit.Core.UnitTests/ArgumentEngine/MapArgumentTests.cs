// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MapArgumentTests.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.ArgumentEngine
{
   using System.Collections.Generic;

   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
   using ConsoLovers.ConsoleToolkit.Core.Exceptions;
   using ConsoLovers.ConsoleToolkit.Core.UnitTests.Setups;

   using FluentAssertions;

   using Microsoft.VisualStudio.TestTools.UnitTesting;

   [TestClass]
   public class MapArgumentTests
   {
      #region Public Methods and Operators

      [TestMethod]
      public void NamedArgumentAlias2Test()
      {
         var commandTestClass = new ArgumentTestClass();
         var argumentList = Setup.CommandLineArguments()
            .Add("AliasName2", "TheNameValue")
            .Add("RequiredArgument", "RequiredArgumentValue")
            .Done();

         var target = Setup.ArgumentMapper().ForType<ArgumentTestClass>().Done();
         var result = target.Map(argumentList, commandTestClass);

         Assert.AreEqual("TheNameValue", result.NamedArgument);
      }

      [TestMethod]
      public void NamedArgumentAliasTest()
      {
         var commandTestClass = new ArgumentTestClass();

         var argumentList = Setup.CommandLineArguments()
            .Add("AliasName1", "TheNameValue")
            .Add("RequiredArgument", "RequiredArgumentValue")
            .Done();

         var argumentMapper = Setup.ArgumentMapper().ForType<ArgumentTestClass>().Done();
         var result = argumentMapper.Map(argumentList, commandTestClass);

         Assert.AreEqual("TheNameValue", result.NamedArgument);
      }

      [TestMethod]
      public void NotUsedRequiredArgumentErrorTest()
      {
         var commandTestClass = new ArgumentTestClass();

         var argumentList = Setup.CommandLineArguments()
            .Done();

         var argumentMapper = Setup.ArgumentMapper().ForType<ArgumentTestClass>().Done();
         argumentMapper.Invoking(x => x.Map(argumentList, commandTestClass))
            .Should().Throw<MissingCommandLineArgumentException>();
      }

      [TestMethod]
      public void RegularUsageTest()
      {
         var commandTestClass = new ArgumentTestClass();

         var argumentList = Setup.CommandLineArguments()
            .Add("SimpleArgument", "\"SimpleArgumentValue\"")
            .Add("TheName", "TheNameValue")
            .Add("TrimmedArgument", "TrimmedArgumentValue")
            .Add("RequiredArgument", "RequiredArgumentValue")
            .Done();

         var argumentMapper = Setup.ArgumentMapper()
            .ForType<ArgumentTestClass>()
            .Done();

         var result = argumentMapper.Map(argumentList, commandTestClass);

         Assert.AreEqual("\"SimpleArgumentValue\"", result.SimpleArgument);
         Assert.AreEqual("TheNameValue", result.NamedArgument);
         Assert.AreEqual("TrimmedArgumentValue", result.TrimmedArgument);
         Assert.AreEqual("RequiredArgumentValue", result.RequiredArgument);
      }

      [TestMethod]
      public void TrimmedArgumentTest()
      {
         var commandTestClass = new ArgumentTestClass();
         var argumentList = Setup.CommandLineArguments()
            .Add("TrimmedArgument", "\"UntrimmedValue\"")
            .Add("RequiredArgument", "RequiredArgumentValue")
            .Done();

         var argumentMapper = Setup.ArgumentMapper().ForType<ArgumentTestClass>().Done();
         var result = argumentMapper.Map(argumentList, commandTestClass);

         Assert.AreEqual("UntrimmedValue", result.TrimmedArgument);
      }

      [TestMethod]
      public void EnsureUnmappedArgumentsRaiseUnmappedCommandLineArgumentEvent()
      {
         var args = new ArgumentTestClass();
         var argumentList = Setup.CommandLineArguments()
            .Add("RequiredArgument", "RequiredArgumentValue")
            .Add("MisspelledArgument", "AnyValue")
            .Done();

         var target = Setup.ArgumentMapper()
            .ForType<ArgumentTestClass>()
            .Done();

         var monitor = target.Monitor();
         var result = target.Map(argumentList, args);

         Assert.AreEqual("RequiredArgumentValue", result.RequiredArgument);
         monitor.Should().Raise(nameof(IArgumentMapper<ArgumentTestClass>.UnmappedCommandLineArgument))
            .WithArgs<MapperEventArgs>(e => e.Argument.Name == "MisspelledArgument" && e.Argument.Value == "AnyValue");
      }

      [TestMethod]
      public void EnsureMappedArgumentsRaiseMappedCommandLineArgumentEvent()
      {
         var args = new ArgumentTestClass();
         var argument = new CommandLineArgument { Name = "RequiredArgument", Value = "Olla" };
         var property = typeof(ArgumentTestClass).GetProperty(nameof(ArgumentTestClass.RequiredArgument));
         
         var argumentList = Setup.CommandLineArguments()
            .Add(argument)
            .Done();

         var target = Setup.ArgumentMapper()
            .ForType<ArgumentTestClass>()
            .Done();

         var monitor = target.Monitor();
         var result = target.Map(argumentList, args);

         result.RequiredArgument.Should().Be(argument.Value);
         monitor.Should().Raise(nameof(IArgumentMapper<ArgumentTestClass>.MappedCommandLineArgument))
            .WithArgs<MapperEventArgs>(e => e.Argument == argument)
            .WithArgs<MapperEventArgs>(e => e.PropertyInfo == property);
      }

      [TestMethod]
      public void EnsureMappedOptionsRaiseMappedCommandLineArgumentEvent()
      {
         var args = new OptionsTestClass();
         var argument = new CommandLineArgument { Name = "Wait" };
         var property = typeof(OptionsTestClass).GetProperty(nameof(OptionsTestClass.Wait));
         var target = Setup.ArgumentMapper()
            .ForType<OptionsTestClass>()
            .Done();

         var argumentList = Setup.CommandLineArguments()
            .Add(argument)
            .Done();

         var monitor = target.Monitor();
         var result = target.Map(argumentList, args);

         result.Wait.Should().BeTrue();
         monitor.Should().Raise(nameof(IArgumentMapper<OptionsTestClass>.MappedCommandLineArgument))
            .WithArgs<MapperEventArgs>(e => e.Argument == argument)
            .WithArgs<MapperEventArgs>(e => e.PropertyInfo == property);
      }

      #endregion
   }

   internal class ArgumentTestClass
   {
      #region Public Properties

      [Argument("TheName", "AliasName1", "AliasName2")]
      public string NamedArgument { get; set; }

      [Argument(Required = true)]
      public string RequiredArgument { get; set; }

      [Argument]
      public string SimpleArgument { get; set; }

      [Argument(TrimQuotation = true)]
      public string TrimmedArgument { get; set; }

      #endregion
   }

   internal class OptionsTestClass
   {
      #region Public Properties

      [Option]
      public bool Wait { get; set; }

      #endregion
   }
}