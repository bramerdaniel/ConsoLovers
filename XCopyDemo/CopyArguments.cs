namespace XCopyDemo
{
   using ConsoLovers.ConsoleToolkit.CommandLineArguments;

   internal class CopyArguments
   {
      [IndexedArgument(0)]
      [Argument("SourceFile", "s", Required = true)]
      [HelpText(null,"The path to the source file that should be copied")]
      public string SourceFile { get; set; }

      [IndexedArgument(1)]
      [Argument("DestinationFile", "d", Required = true)]
      [HelpText(null, "The destination path where the file should be copied to")]
      public string DestinationFile { get; set; }

      [HelpText(null, "If set to true, an existing file will be overwritten. Otherwise it will fail.")]
      [Option("OverrideExisting", "o")]
      public bool OverrideExisting { get; set; }

      [Option("WaitForEnterKey", "w")]
      public bool WaitForEnterKey { get; set; }

      [Option("Silent", "q")]
      public bool Silent { get; set; }
   }
}