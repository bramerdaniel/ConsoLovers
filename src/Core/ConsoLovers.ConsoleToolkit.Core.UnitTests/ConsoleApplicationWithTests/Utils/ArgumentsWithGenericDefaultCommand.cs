namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.ConsoleApplicationWithTests.Utils
{
   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

   public class ArgumentsWithGenericDefaultCommand 
   {
      [Command("Execute", "e", IsDefaultCommand = true)]
      public GenericExecuteCommand Execute { get; set; }
   }
}