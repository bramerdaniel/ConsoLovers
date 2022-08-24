namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.ArgumentEngine
{
   using ConsoLovers.ConsoleToolkit.Core.UnitTests.ArgumentEngine.TestData;
   using ConsoLovers.ConsoleToolkit.Core.UnitTests.Setups;

   using FluentAssertions;

   using Microsoft.VisualStudio.TestTools.UnitTesting;

   [TestClass]
   public class ArgumentInfoTests
   {
      #region Public Methods and Operators

      [TestMethod]
      public void EnsureHasCommandsIsComputedCorrectly()
      {
         var argumentInfo = Setup.ArgumentClassInfo()
            .FromType(typeof(ApplicationArgs))
            .Done();
         
         argumentInfo.HasCommands.Should().BeTrue();

         argumentInfo = Setup.ArgumentClassInfo()
            .FromType(typeof(ExecuteArgs))
            .Done();

         argumentInfo.HasCommands.Should().BeFalse();
      }

      [TestMethod]
      public void EnsureHelpCommandIsSetCorrectly()
      {
         var argumentInfo = Setup.ArgumentClassInfo()
            .FromType(typeof(CommandClassWithHelp))
            .Done();
         
         argumentInfo.HelpCommand.Should().NotBeNull();
      }

      [TestMethod]
      public void EnsurePropertiesAreSetCorrectly()
      {
         var argumentInfo = Setup.ArgumentClassInfo()
            .FromType(typeof(CommandClassWithHelp))
            .Done();
         
         argumentInfo.Properties.Should().HaveCount(4);
      }

      [TestMethod]
      public void EnsureCommandsAreSetCorrectly()
      {
         var argumentInfo = Setup.ArgumentClassInfo()
            .FromType(typeof(CommandClassWithHelp))
            .Done();

         argumentInfo.CommandInfos.Should().HaveCount(2);
      }

      [TestMethod]
      public void EnsureDefaultCommandIsSetCorrectly()
      {
         var argumentInfo = Setup.ArgumentClassInfo()
            .FromType(typeof(CommandClassWithDefault))
            .Done();
         
         argumentInfo.CommandInfos.Should().HaveCount(2);
         argumentInfo.DefaultCommand.Should().NotBeNull();
      }

      [TestMethod]
      public void EnsureInternalCommandsAreComputedCorrectly()
      {
         var argumentInfo = Setup.ArgumentClassInfo()
            .FromType(typeof(CommandClassWithInternalCommands))
            .Done();

         argumentInfo.CommandInfos.Should().HaveCount(3);
         argumentInfo.Properties.Should().HaveCount(5);
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

      private class CommandClassWithDefault
      {
         [Command("Default", "d", IsDefaultCommand = true)]
         public Command DefaultCommand { get; set; }

         [Command("Execute", "e")]
         public Command Execute { get; set; }

         [Argument("Path", "p")]
         public string Path { get; set; }

         [Option("Wait", "w")]
         public bool Wait{ get; set; }
      }

      private class CommandClassWithInternalCommands
      {
         [Command("Default", "d")]
         public Command Run{ get; set; }

         [Command("Execute", "e")]
         internal Command Execute { get; set; }

         [Command("Add", "a")]
         protected internal Command Add { get; set; }

         [Argument("Path", "p")]
         internal string Path { get; set; }

         [Option("Wait", "w")]
         public bool Wait { get; set; }
      }

      #endregion
   }
}