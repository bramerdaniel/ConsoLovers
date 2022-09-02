// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CopyCommandArgs.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace XCopyApplication.Commands
{
   using System.IO;

   using ConsoLovers.ConsoleToolkit.Core;
   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
   using ConsoLovers.ConsoleToolkit.Core.Exceptions;

   public class CopyCommandArgs
   {
      [Argument("DestinationFile", "d", Required = true)]
      [HelpText("The path to the destination the file should be copied to.", nameof(Properties.Resources.DestinationFileHelpText), Priority = 100)]
      [DetailedHelpText("The path to the destination file.\r\n  This must include the file name and may not point to a folder only.")]
      [NotNullOrWhitespace]
      public string DestinationFile { get; set; }

      [Argument("SourceFile", "s", Required = true)]
      [HelpText("The path to the file that should be copied", nameof(Properties.Resources.SourceFileFileHelpText), Priority = 10)]
      [ArgumentValidator(typeof(PathIsRootedValidator))]
      [NotNullOrWhitespace]
      public string SourceFile { get; set; }

      [Option("OverwriteExisting" ,"o")]
      [HelpText("If set to true an already existing file will be overwritten.")]
      public bool OverwriteExisting { get; set; }
   }

   public class PathIsRootedValidator : IArgumentValidator<string>
   {
      public void Validate(string value)
      {
         if (!Path.IsPathRooted(value))
            throw new CommandLineArgumentValidationException("Relative paths are not supported.");
      }
   }
}