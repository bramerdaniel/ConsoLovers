// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MapArgumentTests.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.ArgumentEngine
{
   using System.Collections.Generic;

   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
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
         var dictionary = new Dictionary<string, CommandLineArgument>
         {
            { "AliasName2", new CommandLineArgument { Value = "TheNameValue" } },
            { "RequiredArgument", new CommandLineArgument { Value = "RequiredArgumentValue" } }
         };

         var target = Setup.ArgumentMapper().ForType<ArgumentTestClass>().Done();
         var result = target.Map(CommandLineArgumentList.FromDictionary(dictionary), commandTestClass);

         Assert.AreEqual(result.NamedArgument, "TheNameValue");
      }

      [TestMethod]
      public void NamedArgumentAliasTest()
      {
         var commandTestClass = new ArgumentTestClass();
         var dictionary = new Dictionary<string, CommandLineArgument>
         {
            { "AliasName1", new CommandLineArgument { Value = "TheNameValue" } },
            { "RequiredArgument", new CommandLineArgument { Value = "RequiredArgumentValue" } }
         };

         var argumentMapper = Setup.ArgumentMapper().ForType<ArgumentTestClass>().Done();
         var result = argumentMapper.Map(CommandLineArgumentList.FromDictionary(dictionary), commandTestClass);

         Assert.AreEqual(result.NamedArgument, "TheNameValue");
      }

      [TestMethod]
      public void NotUsedRequiredArgumentErrorTest()
      {
         var commandTestClass = new ArgumentTestClass();
         var dictionary = new Dictionary<string, CommandLineArgument>();

         var argumentMapper = Setup.ArgumentMapper().ForType<ArgumentTestClass>().Done();
         argumentMapper.Invoking(x => x.Map(CommandLineArgumentList.FromDictionary(dictionary), commandTestClass)).Should().Throw<MissingCommandLineArgumentException>();
      }

      [TestMethod]
      public void RegularUsageTest()
      {
         var commandTestClass = new ArgumentTestClass();
         var dictionary = new Dictionary<string, CommandLineArgument>
         {
            { "SimpleArgument", new CommandLineArgument { Value = "\"SimpleArgumentValue\"" } },
            { "TheName", new CommandLineArgument { Value = "TheNameValue" } },
            { "TrimmedArgument", new CommandLineArgument { Value = "TrimmedArgumentValue" } },
            { "RequiredArgument", new CommandLineArgument { Value = "RequiredArgumentValue" } }
         };
         var argumentMapper = Setup.ArgumentMapper().ForType<ArgumentTestClass>().Done();
         var result = argumentMapper.Map(CommandLineArgumentList.FromDictionary(dictionary), commandTestClass);

         Assert.AreEqual(result.SimpleArgument, "\"SimpleArgumentValue\"");
         Assert.AreEqual(result.NamedArgument, "TheNameValue");
         Assert.AreEqual(result.TrimmedArgument, "TrimmedArgumentValue");
         Assert.AreEqual(result.RequiredArgument, "RequiredArgumentValue");
      }

      [TestMethod]
      public void TrimmedArgumentTest()
      {
         var commandTestClass = new ArgumentTestClass();
         var dictionary = new Dictionary<string, CommandLineArgument>
         {
            { "TrimmedArgument", new CommandLineArgument { Value = "\"UntrimmedValue\"" } },
            { "RequiredArgument", new CommandLineArgument { Value = "RequiredArgumentValue" } }
         };

         var argumentMapper = Setup.ArgumentMapper().ForType<ArgumentTestClass>().Done();
         var result = argumentMapper.Map(CommandLineArgumentList.FromDictionary(dictionary), commandTestClass);

         Assert.AreEqual(result.TrimmedArgument, "UntrimmedValue");
      }

      [TestMethod]
      public void EnsureUnmappedArgumentsRaiseUnmappedCommandLineArgumentEvent()
      {
         var args = new ArgumentTestClass();
         var dictionary = new Dictionary<string, CommandLineArgument>
         {
            { "RequiredArgument", new CommandLineArgument { Value = "RequiredArgumentValue" } },
            { "MisspelledArgument", new CommandLineArgument { Name = "MisspelledArgument", Value = "AnyValue" } }
         };
         var target = Setup.ArgumentMapper()
            .ForType<ArgumentTestClass>()
            .Done();

         var monitor = target.Monitor();
         var result = target.Map(CommandLineArgumentList.FromDictionary(dictionary), args);

         Assert.AreEqual(result.RequiredArgument, "RequiredArgumentValue");
         monitor.Should().Raise(nameof(IArgumentMapper<ArgumentTestClass>.UnmappedCommandLineArgument))
            .WithArgs<MapperEventArgs>(e => e.Argument.Name == "MisspelledArgument" && e.Argument.Value == "AnyValue");
      }
      
      [TestMethod]
      public void EnsureMappedArgumentsRaiseMappedCommandLineArgumentEvent()
      {
         var args = new ArgumentTestClass();
         var argument = new CommandLineArgument { Name = "RequiredArgument",Value = "Olla" };
         var property = typeof(ArgumentTestClass).GetProperty(nameof(ArgumentTestClass.RequiredArgument));
         var dictionary = new Dictionary<string, CommandLineArgument>{{ argument.Name, argument }};
         var target = Setup.ArgumentMapper()
            .ForType<ArgumentTestClass>()
            .Done();

         var monitor = target.Monitor();
         var result = target.Map(CommandLineArgumentList.FromDictionary(dictionary), args);

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
         var dictionary = new Dictionary<string, CommandLineArgument>{{ argument.Name, argument }};
         var target = Setup.ArgumentMapper()
            .ForType<OptionsTestClass>()
            .Done();

         var monitor = target.Monitor();
         var result = target.Map(CommandLineArgumentList.FromDictionary(dictionary), args);

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