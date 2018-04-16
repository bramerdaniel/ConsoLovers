namespace XCopyApplication.Commands
{
   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

   public class DeleteCommandArgs
   {
      [Argument("Path", Required = true)]
      public string Path { get; set; }
   }
}