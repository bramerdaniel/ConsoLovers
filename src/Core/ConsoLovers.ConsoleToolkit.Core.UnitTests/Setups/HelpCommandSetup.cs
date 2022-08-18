// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HelpCommandSetup.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
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

      private ILocalizationService localizationService;

      #endregion

      #region Public Methods and Operators

      public HelpCommand Done()
      {
         if (commandLineEngine == null)
            commandLineEngine = new Mock<ICommandLineEngine>().Object;

         if (console == null)
            console = new Mock<IConsole>().Object;

         return new HelpCommand(commandLineEngine, localizationService ?? new Mock<ILocalizationService>().Object, console, new ArgumentReflector());
      }

      public HelpCommandSetup WithEngineMock(out Mock<ICommandLineEngine> engineMock)
      {
         commandLineEngine = Setup.MockFor().CommandLineEngine().Done();

         engineMock = Mock.Get(commandLineEngine);

         return this;
      }

      public HelpCommandSetup WithLocalizationService(ILocalizationService value)
      {
         localizationService = value;
         return this;
      }

      #endregion
   }
}