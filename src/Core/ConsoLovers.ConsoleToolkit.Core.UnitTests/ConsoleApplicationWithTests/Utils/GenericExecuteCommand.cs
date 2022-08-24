// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GenericExecuteCommand.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.ConsoleApplicationWithTests.Utils
{
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