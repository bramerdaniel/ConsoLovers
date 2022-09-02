// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SimpleCommand.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace CommandLineEngineDemo.Commands.Remove
{
   using System;

   using ConsoLovers.ConsoleToolkit.Core;

   internal class SimpleCommand : ICommand
   {
      #region Constants and Fields

      private readonly IConsole console;

      #endregion

      #region Constructors and Destructors

      public SimpleCommand(IConsole console)
      {
         this.console = console ?? throw new ArgumentNullException(nameof(console));
      }

      #endregion

      #region ICommand Members

      public void Execute()
      {
         console.WriteLine("Simple command was executed");
      }

      #endregion
   }
}