// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CopyCommandArgs.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace XCopyApplication.Commands
{
   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

   public class CopyCommandArgs
   {
      [Argument("DestinationFile", "d", Required = true)]
      [HelpText("The path to the destination the file should be copied to.", nameof(Properties.Resources.DestinationFileHelpText), Priority = 100)]
      public string DestinationFile { get; set; }

      [Argument("SourceFile", "s", Required = true)]
      [HelpText("The path to the file that should be copied", nameof(Properties.Resources.SourceFileFileHelpText), Priority = 10)]
      public string SourceFile { get; set; }

      [Option("OverwriteExisting" ,"o")]
      [HelpText("If set to true an allready existing file will be overwritten.")]
      public bool OverwriteExisting { get; set; }
   }
}