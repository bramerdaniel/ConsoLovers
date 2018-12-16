namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.ArgumentEngine
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
      public void EnsureCommandIsParsedCorrectly()
      {
         var applicationArgs = new ApplicationArgs();
         var dictionary = new Dictionary<string, CommandLineArgument>
         {
            { "execute", new CommandLineArgument { Name = "execute" } },
            { "path", new CommandLineArgument { Name = "path" , Value = "C:\\temp"} }
         };

         var commandMapper = new CommandMapper<ApplicationArgs>(Setup.EngineFactory().Done());
         var result = commandMapper.Map(dictionary, applicationArgs);

         result.Execute.Should().NotBeNull();
      }

      [TestMethod]
      public void EnsureCommandIsMappedCorrectlyEvenIfNoNameIsSpecified()
      {
         var applicationArgs = new CommandsWithoutName();
         var dictionary = new Dictionary<string, CommandLineArgument>
         {
            { "Execute", new CommandLineArgument { Name = "Execute" } },
         };

         var commandMapper = new CommandMapper<CommandsWithoutName>(Setup.EngineFactory().Done());
         var result = commandMapper.Map(dictionary, applicationArgs);

         result.Execute.Should().NotBeNull();
      }

      [TestMethod]
      public void EnsureEventIsRaisedWhenCommandIsMapped()
      {
         var applicationArgs = new CommandsWithName();
         var commandLineArgument = new CommandLineArgument { Name = "name" };
         var property = typeof(CommandsWithName).GetProperty(nameof(CommandsWithName.Execute));
         var dictionary = new Dictionary<string, CommandLineArgument>{{ commandLineArgument.Name, commandLineArgument }};

         var commandMapper = new CommandMapper<CommandsWithName>(Setup.EngineFactory().Done());
         commandMapper.MonitorEvents();

         var result = commandMapper.Map(dictionary, applicationArgs);

         result.Execute.Should().NotBeNull();
         commandMapper.ShouldRaise(nameof(CommandMapper<CommandsWithName>.MappedCommandLineArgument))
            .WithArgs<MapperEventArgs>(args => args.Argument == commandLineArgument)
            .WithArgs<MapperEventArgs>(args => args.PropertyInfo == property);
      }
      
      [TestMethod]
      public void EnsureEventIsRaisedWhenHelpCommandIsMapped()
      {
         var applicationArgs = new CommandsWithName();
         var commandLineArgument = new CommandLineArgument { Name = "?" };
         var dictionary = new Dictionary<string, CommandLineArgument>{{ "?", commandLineArgument }};

         var commandMapper = new CommandMapper<CommandsWithName>(Setup.EngineFactory().Done());
         commandMapper.MonitorEvents();

         var result = commandMapper.Map(dictionary, applicationArgs);

         result.Help.Should().NotBeNull();
         commandMapper.ShouldRaise(nameof(CommandMapper<CommandsWithName>.MappedCommandLineArgument))
            .WithArgs<MapperEventArgs>(args => args.Argument == commandLineArgument);
      }

      class CommandsWithoutName
      {
         [Command]
         // ReSharper disable once UnusedAutoPropertyAccessor.Local
         public Command Execute { get; set; }
      }

      class CommandsWithName
      {
         [Command("name")]
         // ReSharper disable once UnusedAutoPropertyAccessor.Local
         public Command Execute { get; set; }

         [Command("?")]
         // ReSharper disable once UnusedAutoPropertyAccessor.Local
         public HelpCommand Help { get; set; }
      }

      #endregion
   }
}