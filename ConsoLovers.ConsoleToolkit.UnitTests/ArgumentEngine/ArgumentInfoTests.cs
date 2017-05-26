namespace ConsoLovers.UnitTests.ArgumentEngine
{
   using ConsoLovers.ConsoleToolkit.CommandLineArguments;
   using ConsoLovers.UnitTests.ArgumentEngine.TestData;

   using FluentAssertions;

   using Microsoft.VisualStudio.TestTools.UnitTesting;

   [TestClass]
   public class ArgumentInfoTests
   {
      #region Public Methods and Operators

      [TestMethod]
      public void EnsureHasCommandsIsComputedCorrectly()
      {
         var argumentInfo = new ArgumentClassInfo(typeof(ApplicationArgs));
         argumentInfo.HasCommands.Should().BeTrue();

         argumentInfo = new ArgumentClassInfo(typeof(ExecuteArgs));
         argumentInfo.HasCommands.Should().BeFalse();
      }

      [TestMethod]
      public void EnsureHelpCommandIsSetCorrectly()
      {
         var argumentInfo = new ArgumentClassInfo(typeof(CommandClassWithHelp));
         argumentInfo.HelpCommand.Should().NotBeNull();
      }

      [TestMethod]
      public void EnsurePropertiesAreSetCorrectly()
      {
         var argumentInfo = new ArgumentClassInfo(typeof(CommandClassWithHelp));
         argumentInfo.Properties.Should().HaveCount(4);
      }

      [TestMethod]
      public void EnsureCommandsAreSetCorrectly()
      {
         var argumentInfo = new ArgumentClassInfo(typeof(CommandClassWithHelp));
         argumentInfo.CommandInfos.Should().HaveCount(2);
      }

      private class CommandClassWithHelp
      {
         [Command("Help", "?")]
         public HelpCommand HelpCommand { get; set; }

         [Command("Execute", "e")]
         public Command Execute { get; set; }

         [Argument("Path", "p")]
         public string Path { get; set; }

         [Option("Wait", "w")]
         public bool Wait{ get; set; }
      }

      #endregion
   }
}