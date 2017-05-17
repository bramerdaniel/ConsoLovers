namespace ConsoLovers.UnitTests.ArgumentEngine
{
   using ConsoLovers.ConsoleToolkit.CommandLineArguments;

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

      #endregion
   }
}