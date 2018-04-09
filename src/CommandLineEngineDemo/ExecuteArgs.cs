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

      [Argument("Path", "p", Required = true)]
      // [HelpText("The path to the thing that should be executed.")]
      [HelpText(ResourceKey = nameof(Properties.Resources.Execute_Path_Help))]
      [DetailedHelpText(ResourceKey = nameof(Properties.Resources.Execute_Path_DetailedHelp))]
      public string Path { get; set; }

      #endregion

   }
}