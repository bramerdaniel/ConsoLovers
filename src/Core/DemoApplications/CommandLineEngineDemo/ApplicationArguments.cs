// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Commands.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace CommandLineEngineDemo
{
   using CommandLineEngineDemo.Commands;

   using ConsoLovers.ConsoleToolkit.Core;
   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

   internal class ApplicationArguments : LoggerArgs
   {
      #region Public Properties

      [Command("crash", "c")]
      [HelpText("Crashes the application for demo.", "None")]
      public CrashCommand Crash { get; set; }

      [Command("CustomArgumentHelp", "cah")]
      [HelpText("This is the default help.", "None")]
      public CustomizedArgumentsHelpCommand CustomArgumentHelp { get; set; }

      [Command("CustomHelp", "ch")]
      [HelpText("This is the default help.", "None")]
      public CustomHelpCommand CustomHelp { get; set; }

      [Command("Execute", "e", IsDefaultCommand = true)]
      [HelpText("Executes the command.", "None", Priority = 20)]
      public ExecuteCommand Execute { get; set; }

      [Command("DoNothing", "dn")]
      [HelpText("Executes the command but does nothing.", "None", Priority = 30)]
      public ExecuteCommand DoNothing { get; set; }

      [Command("Add", "a")]
      [HelpText("Just anotherCommand.", "None", Priority = 30)]
      public AddCommand Add{ get; set; }

      [Command("Help", "?")]
      [HelpText("Displays the help you are watching at the moment.", "None")]
      public HelpCommand Help { get; set; }

      [Option("Wait", "w")]
      [HelpText("Waits for key press", "None")]
      [DetailedHelpText(ResourceKey = nameof(Properties.Resources.Wait_DetailedHelp))]
      public bool Wait { get; set; }

      #endregion
   }

   internal class LoggerArgs
   {
      [Argument("LogLevel", "ll")]
      public string LogLevel { get; set; }

      [Argument("LogFile", "lf")]
      [HelpText("Log file of the application")]
      public string LogFile { get; set; }
   }
}