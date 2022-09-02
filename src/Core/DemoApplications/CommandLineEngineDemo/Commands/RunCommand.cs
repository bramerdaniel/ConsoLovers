// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RunCommand.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace CommandLineEngineDemo.Commands
{
   using System;

   using ConsoLovers.ConsoleToolkit.Core;

   public class RunCommand : ICommand
   {
      #region Constants and Fields

      private readonly IConsole console;

      #endregion

      #region Constructors and Destructors

      public RunCommand(IConsole console)
      {
         this.console = console ?? throw new ArgumentNullException(nameof(console));
      }

      #endregion

      #region ICommand Members

      public void Execute()
      {
         console.WriteLine("Run command was executed");
      }

      #endregion
   }
}