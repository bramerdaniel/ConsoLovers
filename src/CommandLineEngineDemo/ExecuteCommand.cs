// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExecuteCommand.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace CommandLineEngineDemo
{
   using System;

   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

   internal class ExecuteCommand : ICommand<ExecuteArgs>
   {
      #region ICommand Members

      public void Execute()
      {
         Console.WriteLine($"CommandName = Execute, Path= {Arguments.Path}{GetLogLevel()}{GetUnnamed()}{GetWait()}");
         if (Arguments.Wait)
            Console.ReadLine();
      }

      private string GetWait()
      {
         return Arguments.Wait ? $", Wait={Arguments.Wait}" : string.Empty;
      }

      private string GetUnnamed()
      {
         return Arguments.Unnamed == null ? string.Empty : $", Unnamed={Arguments.Unnamed}";
      }

      private string GetLogLevel()
      {
         return Arguments.LogLevel == null ? string.Empty : $", LogLevel={Arguments.LogLevel}";
      }

      #endregion

      #region ICommand<ExecuteArgs> Members

      public ExecuteArgs Arguments { get; set; }

      #endregion
   }
}