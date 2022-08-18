namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.ConsoleApplicationWithTests.Utils
{
   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

   public class TestCommandArguments
   {
      [Argument("string", "s")]
      public string String{ get; set; }

      [Argument("int", "i")]
      public int Int { get; set; }
   }
}