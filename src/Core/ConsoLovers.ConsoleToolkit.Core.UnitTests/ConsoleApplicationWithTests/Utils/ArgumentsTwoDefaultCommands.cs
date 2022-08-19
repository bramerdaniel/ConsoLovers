namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.ConsoleApplicationWithTests.Utils
{
   using System.Threading;
   using System.Threading.Tasks;

   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

   public class ArgumentsTwoDefaultCommands
   {
      [Command("Generic", "g", IsDefaultCommand = true)]
      public GenericExecuteCommand Execute { get; set; }

      [Command("Deault", "d", IsDefaultCommand = true)]
      public DefaultExecuteCommand DeaultCommand { get; set; }
   }

   public class ArgumentsWithoutDefaultCommands : IApplicationLogic
   {
      [Command("Generic", "g")]
      public GenericExecuteCommand Execute { get; set; }

      [Command("Deault", "d")]
      public DefaultExecuteCommand DefaultCommand { get; set; }

      [Argument("string", "s")]
      public string String { get; set; }

      public Task ExecuteAsync<T>(T arguments, CancellationToken cancellationToken)
      {
         Executed = true;
         return Task.CompletedTask;
      }

      public static bool Executed { get; private set; }
   }
}