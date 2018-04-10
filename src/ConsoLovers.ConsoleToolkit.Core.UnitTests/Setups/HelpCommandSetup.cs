// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HelpCommandSetup.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.Setups
{
   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

   using Moq;

   public class HelpCommandSetup
   {
      #region Constants and Fields

      private ICommandLineEngine commandLineEngine;

      private IConsole console;

      #endregion

      #region Public Methods and Operators

      public HelpCommand Done()
      {
         if (commandLineEngine == null)
            commandLineEngine = new Mock<ICommandLineEngine>().Object;

         if (console == null)
            console = new Mock<IConsole>().Object;

         return new HelpCommand(commandLineEngine, console, null);
      }

      public HelpCommandSetup WithEngineMock(out Mock<ICommandLineEngine> engineMock)
      {
         commandLineEngine = Setup.MockFor().CommandLineEngine().Done();

         engineMock = Mock.Get(commandLineEngine);

         return this;
      }

      #endregion
   }
}