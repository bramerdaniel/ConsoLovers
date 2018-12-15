// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExecuteArgs.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace CommandLineEngineDemo
{
   using System;
   using System.Resources;

   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

   internal class ExecuteArgs 
   {
      #region Public Properties

      [Argument("Path", "p", Required = true, Index = 0)]
      // [HelpText("The path to the thing that should be executed.")]
      [HelpText(ResourceKey = nameof(Properties.Resources.Execute_Path_Help))]
      [DetailedHelpText(ResourceKey = nameof(Properties.Resources.Execute_Path_DetailedHelp))]
      public string Path { get; set; }

      [Argument("LogLevel", "ll" , Index = 1, Shared = true)]
      public string LogLevel { get; set; }

      [Argument(2)]
      public string Unnamed { get; set; }

      [Option("wait", "w", Shared = true)]
      public bool Wait { get; set; }

      #endregion

   }
}