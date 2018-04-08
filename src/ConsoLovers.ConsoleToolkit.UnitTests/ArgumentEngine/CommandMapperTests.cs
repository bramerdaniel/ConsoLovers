namespace ConsoLovers.UnitTests.ArgumentEngine
{
   using System.Collections.Generic;

   using ConsoLovers.ConsoleToolkit.CommandLineArguments;
   using ConsoLovers.UnitTests.ArgumentEngine.TestData;
   using ConsoLovers.UnitTests.Setups;

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

      class CommandsWithoutName
      {
         [Command]
         // ReSharper disable once UnusedAutoPropertyAccessor.Local
         public Command Execute { get; set; }
      }

      #endregion
   }
}