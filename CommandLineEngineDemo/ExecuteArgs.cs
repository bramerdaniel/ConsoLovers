// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExecuteArgs.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace CommandLineEngineDemo
{
   using System;
   using System.Resources;

   using ConsoLovers.ConsoleToolkit.CommandLineArguments;

   internal class ExecuteArgs 
   {
      #region Public Properties

      [Argument("Path", "p", Required = true)]
      [HelpText("ExecuteArgs_PathHelp", "The path to the thing that should be executed.")]
      public string Path { get; set; }

      #endregion

   }
}