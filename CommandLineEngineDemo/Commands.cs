// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Commands.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace CommandLineEngineDemo
{
   using ConsoLovers.ConsoleToolkit.CommandLineArguments;

   internal class Commands
   {
      #region Public Properties

      [Command("crash", "c")]
      [HelpText("Crashes the application for demo.", "None")]
      public CrashCommand Crash { get; set; }

      [Command("CustomHelp", "ch")]
      [HelpText("This is the default help.", "None")]
      public CustomHelpCommand CustomHelp { get; set; }

      [Command("CustomArgumentHelp", "cah")]
      [HelpText("This is the default help.", "None")]
      public CustomizedArgumentsHelpCommand CustomArgumentHelp { get; set; }

      [Command("Execute", "e", IsDefaultCommand = true)]
      [HelpText("Executes the command.", "None", Priority = 20)]
      public ExecuteCommand Execute { get; set; }

      [Command("Help", "?")]
      [HelpText("Displays the help you are watching at the moment.", "None")]
      public HelpCommand Help { get; set; }

      [Option("Wait", "w")]
      [HelpText("Waits for key press", "None")]
      public bool Wait { get; set; }

      #endregion
   }
}