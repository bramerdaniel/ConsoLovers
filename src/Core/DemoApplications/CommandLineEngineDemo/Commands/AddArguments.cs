namespace CommandLineEngineDemo.Commands
{
   using ConsoLovers.ConsoleToolkit.Core;

   internal class AddArguments
   {
      [Option("Wait", "w", Shared = true)]
      [HelpText("Waits for key press", "None")]
      [DetailedHelpText(ResourceKey = nameof(Properties.Resources.Wait_DetailedHelp))]
      public bool Wait { get; set; }

      [Argument("Path", "p", Index = 0)]
      // [HelpText("The path to the thing that should be executed.")]
      [HelpText(ResourceKey = nameof(Properties.Resources.Execute_Path_Help))]
      [DetailedHelpText(ResourceKey = nameof(Properties.Resources.Execute_Path_DetailedHelp))]
      public string Path { get; set; }

      [Option("Code", "c")]
      [HelpText("Code is an option", "None")]
      public bool Code { get; set; }
   }
}