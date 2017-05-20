// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CrashCommand.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace CommandLineEngineDemo
{
   using System;

   using ConsoLovers.ConsoleToolkit.CommandLineArguments;

   [HelpText("None", "This is the detailed help for the crash command. Should you realy do that this was.")]
   internal class CrashCommand : ICommand
   {
      #region ICommand Members

      public void Execute()
      {
         throw new InvalidOperationException("The command crashed");
      }

      #endregion
   }
}