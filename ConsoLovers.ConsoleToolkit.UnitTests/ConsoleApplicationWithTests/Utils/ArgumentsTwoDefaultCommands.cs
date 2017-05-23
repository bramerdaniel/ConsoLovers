namespace ConsoLovers.UnitTests.ConsoleApplicationWithTests.Utils
{
   using ConsoLovers.ConsoleToolkit.CommandLineArguments;

   public class ArgumentsTwoDefaultCommands
   {
      [Command("Generic", "g", IsDefaultCommand = true)]
      public GenericExecuteCommand Execute { get; set; }

      [Command("Deault", "d", IsDefaultCommand = true)]
      public DefaultExecuteCommand DeaultCommand { get; set; }
   }

   public class ArgumentsWithoutDefaultCommands
   {
      [Command("Generic", "g")]
      public GenericExecuteCommand Execute { get; set; }

      [Command("Deault", "d")]
      public DefaultExecuteCommand DeaultCommand { get; set; }

      [Argument("string", "s")]
      public string String { get; set; }
   }
}