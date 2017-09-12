// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExecuteCommand.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace CommandLineEngineDemo
{
   using System;

   using ConsoLovers.ConsoleToolkit.CommandLineArguments;

   internal class ExecuteCommand : ICommand<ExecuteArgs>
   {
      #region ICommand Members

      public void Execute()
      {
         Console.WriteLine($"CommandName = Execute, Path= {Arguments.Path}");
         Console.ReadLine();
      }

      #endregion

      #region ICommand<ExecuteArgs> Members

      public ExecuteArgs Arguments { get; set; }

      #endregion
   }
}