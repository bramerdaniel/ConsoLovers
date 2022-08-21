namespace Playground;

using ConsoLovers.ConsoleToolkit.Core;
using ConsoLovers.ConsoleToolkit.Core.Middleware;
using ConsoLovers.ConsoleToolkit.Core.Services;

using Microsoft.Extensions.DependencyInjection;

public static class Program
{
   public static async Task Main()
   {
      var executable = await ConsoleApplication.WithArguments<ApplicationArgs>()
         //.UseServiceProviderFactory(new DefaultServiceProviderFactory())
         .UseApplicationLogic(Execute)
         .AddMiddleware(typeof(TryCatchMiddleware))
         .AddMiddleware<ApplicationArgs, RepeatMiddleware>()
         .RunAsync();

      Console.ReadLine();
   }

   private static Task Execute(ApplicationArgs args, CancellationToken cancellationToken)
   {
      Console.WriteLine("Executed with func");
      return Task.CompletedTask;
   }
}

public class RepeatMiddleware : Middleware<IExecutionContext<ApplicationArgs>>
{
   public override async Task Execute(IExecutionContext<ApplicationArgs> context, CancellationToken cancellationToken)
   {
      for (int i = 0; i < 5; i++)
      {
         await Next(context, cancellationToken);
         context.ApplicationArguments.DeleteCommand.Arguments.User.Arguments.Force =
            !context.ApplicationArguments.DeleteCommand.Arguments.User.Arguments.Force;
      }

      context.ApplicationArguments.DeleteCommand.Arguments.User.Arguments.UserName = "Calvin";
      await Next(context, cancellationToken);
   }
}

public class TryCatchMiddleware : Middleware<IExecutionContext<ApplicationArgs>>
{
   private readonly IConsole console;

   public TryCatchMiddleware(IConsole console)
   {
      this.console = console ?? throw new ArgumentNullException(nameof(console));
   }

   public override async Task Execute(IExecutionContext<ApplicationArgs> context, CancellationToken cancellationToken)
   {
      try
      {
         console.WriteLine("--- Try --->", ConsoleColor.Yellow);

         await Next(context, cancellationToken);

         console.WriteLine("--- Ok --->", ConsoleColor.Green);
      }
      catch (Exception e)
      {
         console.WriteLine("--- Catch --->", ConsoleColor.Red);
      }
   }
}