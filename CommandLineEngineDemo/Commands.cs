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
      [HelpText("None", "Crashes the application for demo.")]
      public CrashCommand Crash { get; set; }

      [Command("CustomHelp", "ch")]
      [HelpText("None", "This is the default help.")]
      public CustomHelpCommand CustomHelp { get; set; }

      [Command("CustomArgumentHelp", "cah")]
      [HelpText("None", "This is the default help.")]
      public CustomizedArgumentsHelpCommand CustomArgumentHelp { get; set; }

      [Command("Execute", "e", IsDefaultCommand = true)]
      [HelpText("None", "Executes the command.")]
      public ExecuteCommand Execute { get; set; }

      [Command("Help", "?")]
      [HelpText("None", "Displays the help you are watching at the moment.")]
      public HelpCommand Help { get; set; }

      [Option("Wait", "w")]
      [HelpText("None", "Waits for key press")]
      public bool Wait { get; set; }

      #endregion
   }
}