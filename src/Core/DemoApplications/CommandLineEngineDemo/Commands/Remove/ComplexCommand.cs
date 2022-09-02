// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ComplexCommand.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace CommandLineEngineDemo.Commands.Remove
{
   using System;

   using ConsoLovers.ConsoleToolkit.Core;

   internal class ComplexCommand : ICommand<ComplexCommand.ComplexArgs>
   {
      #region Constants and Fields

      private readonly IConsole console;

      #endregion

      #region Constructors and Destructors

      public ComplexCommand(IConsole console)
      {
         this.console = console ?? throw new ArgumentNullException(nameof(console));
      }

      #endregion

      #region ICommand<ComplexArgs> Members

      public void Execute()
      {
         console.WriteLine("Complex command was executed");
      }

      public ComplexArgs Arguments { get; set; }

      #endregion

      internal class ComplexArgs
      {
      }
   }
}