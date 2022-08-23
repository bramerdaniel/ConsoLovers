namespace XCopyApplication
{
   using ConsoLovers.ConsoleToolkit.Core;
   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

   using global::XCopyApplication.Commands;

   [HelpTextProvider(typeof(XCopyArguments))]
   public class XCopyArguments : TypeHelpProvider
   {
      public override void WriteTypeHeader(TypeHelpRequest helpRequest)
      {
         Console.WriteLine(" ╔══════════════════════════╗");
         Console.WriteLine(" ║  XCOPY ARGUMENT HELP     ║");
         Console.WriteLine(" ╚══════════════════════════╝");
         Console.WriteLine();
      }

      public override void WriteTypeFooter(TypeHelpRequest helpRequest)
      {
         Console.WriteLine();
         Console.WriteLine("►───────────────────────────────────────────────────────────────────────────◄");
      }

      [Command("copy", "c", IsDefaultCommand = true)]
      [HelpText("Copies the source to the destination.")]
      public CopyCommand Copy { get; set; }

      [Command("move", "m")]
      [HelpText("Moves the given source to the destination.")]
      public MoveCommand Move { get; set; }

      [Command("delete", "d")]
      [HelpText("Deletes the given path.")]
      public DeleteCommand Delete { get; set; }

      [Command("Help", "h", "?")]
      [HelpText("Displays the help you are watching at the moment.", "None")]
      public HelpCommand Help { get; set; }

      [Option("Wait", "w")]
      [HelpText("Waits for key press after the executed command has finished", "None")]
      [DetailedHelpText("This is normally only used for error diagosis.")]
      public bool Wait { get; set; }
   }
}