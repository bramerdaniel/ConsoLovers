// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GenericExecuteCommand.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.ConsoleApplicationWithTests.Utils
{
   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

   using Microsoft.VisualStudio.TestTools.UnitTesting;

   public class GenericExecuteCommand : ICommand<TestCommandArguments>
   {
      #region ICommand<TestCommandArguments> Members

      public void Execute()
      {
         Executed = true;
      }

      public TestCommandArguments Arguments { get; set; }

      #endregion

      #region Public Properties

      public bool Executed { get; private set; }

      #endregion

      #region Public Methods and Operators

      public void EnsureExecuted()
      {
         Assert.IsTrue(Executed, "Command was not executed as expected");
      }

      #endregion
   }
}