namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.ConsoleApplicationWithTests.Utils
{
   using System.Threading;
   using System.Threading.Tasks;

   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

   public class ArgumentsWithGenericDefaultCommand 
   {
      [Command("Execute", "e", IsDefaultCommand = true)]
      public GenericExecuteCommand Execute { get; set; }

   }
}