namespace Playground;

using ConsoLovers.ConsoleToolkit.Core;
using ConsoLovers.ConsoleToolkit.Core.Middleware;

using Microsoft.Extensions.DependencyInjection;

public static class Program
{
   public static void Main()
   {
      var executable = ConsoleApplication.WithArguments<ApplicationArgs>()
         .UseServiceProviderFactory(new DefaultServiceProviderFactory())
         // .UseApplicationLogic(Execute)
         //.AddMiddleware(typeof(TryCatchMiddleware))
         //.AddMiddleware<ApplicationArgs, RepeatMiddleware>()
         // .Run();
         .Run(t => throw new InvalidOperationException($"That went wrong {t}"));

      Console.ReadLine();
   }

   private static Task Execute(ApplicationArgs args, CancellationToken cancellationToken)
   {
      Console.WriteLine("Executed with func");
      return Task.CompletedTask;
   }
}

public class RepeatMiddleware : Middleware<ApplicationArgs>
{
   public override async Task ExecuteAsync(IExecutionContext<ApplicationArgs> context, CancellationToken cancellationToken)
   {
      for (int i = 0; i < 5; i++)
      {
         await Next(context, cancellationToken);
         //context.ApplicationArguments.DeleteCommand.Arguments.User.Arguments.Force =
         //   !context.ApplicationArguments.DeleteCommand.Arguments.User.Arguments.Force;
      }

      // context.ApplicationArguments.DeleteCommand.Arguments.User.Arguments.UserName = "Calvin";
      await Next(context, cancellationToken);
   }
}

public class TryCatchMiddleware : Middleware<ApplicationArgs>
{
   private readonly IConsole console;

   public TryCatchMiddleware(IConsole console)
   {
      this.console = console ?? throw new ArgumentNullException(nameof(console));
   }

   public override async Task ExecuteAsync(IExecutionContext<ApplicationArgs> context, CancellationToken cancellationToken)
   {
      try
      {
         context.ParsedArguments.RemoveFirst("removeMe");

         await Next(context, cancellationToken);


      }
      catch (Exception)
      {
         console.WriteLine("--- Catch --->", ConsoleColor.Red);
      }
   }
}