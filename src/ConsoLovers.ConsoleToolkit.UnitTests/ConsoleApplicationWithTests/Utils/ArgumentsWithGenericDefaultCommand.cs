namespace ConsoLovers.UnitTests.ConsoleApplicationWithTests.Utils
{
   using ConsoLovers.ConsoleToolkit.CommandLineArguments;

   public class ArgumentsWithGenericDefaultCommand 
   {
      [Command("Execute", "e", IsDefaultCommand = true)]
      public GenericExecuteCommand Execute { get; set; }
   }
}