namespace XCopyDemo
{
   using ConsoLovers.ConsoleToolkit.CommandLineArguments;

   internal class CopyArguments
   {
      [IndexedArgument(0)]
      [Argument("SourceFile", "s", Required = true)]
      [HelpText("The path to the source file that should be copied", null)]
      public string SourceFile { get; set; }

      [IndexedArgument(1)]
      [Argument("DestinationFile", "d", Required = true)]
      [HelpText("The destination path where the file should be copied to", null)]
      public string DestinationFile { get; set; }

      [HelpText("If set to true, an existing file will be overwritten. Otherwise it will fail.", null)]
      [Option("OverrideExisting", "o")]
      public bool OverrideExisting { get; set; }

      [Option("WaitForEnterKey", "w")]
      public bool WaitForEnterKey { get; set; }

      [Option("Silent", "q")]
      public bool Silent { get; set; }
   }
}