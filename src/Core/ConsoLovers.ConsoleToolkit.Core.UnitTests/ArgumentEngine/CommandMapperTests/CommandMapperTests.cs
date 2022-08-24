// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandMapperTests.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.ArgumentEngine.CommandMapperTests
{
   using System.Collections.Generic;

   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
   using ConsoLovers.ConsoleToolkit.Core.UnitTests.ArgumentEngine.TestData;
   using ConsoLovers.ConsoleToolkit.Core.UnitTests.Setups;

   using FluentAssertions;

   using Microsoft.VisualStudio.TestTools.UnitTesting;

   [TestClass]
   public class CommandMapperTests : EngineTestBase
   {
      #region Public Methods and Operators

      [TestMethod]
      public void EnsureCommandIsMappedCorrectlyEvenIfNoNameIsSpecified()
      {
         var applicationArgs = new CommandsWithoutName();
    
         var argumentList = Setup.CommandLineArguments()
            .Add(new CommandLineArgument { Name = "Execute" })
            .Done();

         var commandMapper = Setup.CommandMapper<CommandsWithoutName>().WithDefaults().Done();
         var result = commandMapper.Map(argumentList, applicationArgs);

         result.Execute.Should().NotBeNull();
      }

      [TestMethod]
      public void EnsureCommandIsParsedCorrectly()
      {
         var applicationArgs = new ApplicationArgs();

         var argumentList = Setup.CommandLineArguments()
            .Add(new CommandLineArgument { Name = "execute" })
            .Add(new CommandLineArgument { Name = "path", Value = "C:\\temp" })
            .Done();

         var commandMapper = Setup.CommandMapper<ApplicationArgs>().WithDefaults().Done();
         var result = commandMapper.Map(argumentList, applicationArgs);

         result.Execute.Should().NotBeNull();
      }

      [TestMethod]
      public void EnsureEventIsRaisedWhenCommandIsMapped()
      {
         var applicationArgs = new CommandsWithName();

         var commandLineArgument = new CommandLineArgument { Name = "name" };
         var argumentList = Setup.CommandLineArguments()
            .Add(commandLineArgument)
            .Done();

         var property = typeof(CommandsWithName).GetProperty(nameof(CommandsWithName.Execute));
         
         var commandMapper = Setup.CommandMapper<CommandsWithName>().WithDefaults().Done();
         var monitor = commandMapper.Monitor();

         var result = commandMapper.Map(argumentList, applicationArgs);

         result.Execute.Should().NotBeNull();
         monitor.Should().Raise(nameof(CommandMapper<CommandsWithName>.MappedCommandLineArgument))
            .WithArgs<MapperEventArgs>(args => args.Argument == commandLineArgument)
            .WithArgs<MapperEventArgs>(args => args.PropertyInfo == property);
      }

      [TestMethod]
      public void EnsureEventIsRaisedWhenHelpCommandIsMapped()
      {
         var applicationArgs = new CommandsWithName();
         var commandLineArgument = new CommandLineArgument { Name = "?" };
         var argumentList = Setup.CommandLineArguments()
            .Add(commandLineArgument)
            .Done();

         var commandMapper = Setup.CommandMapper<CommandsWithName>().WithDefaults().Done();
         
         var monitor = commandMapper.Monitor();

         var result = commandMapper.Map(argumentList, applicationArgs);

         result.Help.Should().NotBeNull();
         monitor.Should().Raise(nameof(CommandMapper<CommandsWithName>.MappedCommandLineArgument))
            .WithArgs<MapperEventArgs>(args => args.Argument == commandLineArgument);
      }

      #endregion

      class CommandsWithName
      {
         #region Public Properties

         [Command("name")]
         // ReSharper disable once UnusedAutoPropertyAccessor.Local
         public Command Execute { get; set; }

         [Command("?")]
         // ReSharper disable once UnusedAutoPropertyAccessor.Local
         public HelpCommand Help { get; set; }

         #endregion
      }

      class CommandsWithoutName
      {
         #region Public Properties

         [Command]
         // ReSharper disable once UnusedAutoPropertyAccessor.Local
         public Command Execute { get; set; }

         #endregion
      }
   }
}