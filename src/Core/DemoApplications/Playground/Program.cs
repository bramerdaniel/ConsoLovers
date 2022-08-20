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
         .AddMiddleware(typeof(TrycatchMiddleware))
         .AddMiddleware(typeof(RepeatMiddleware))
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
         await Next(context, cancellationToken);
   }
}

public class TrycatchMiddleware : Middleware<IExecutionContext<ApplicationArgs>>
{
   public override async Task Execute(IExecutionContext<ApplicationArgs> context, CancellationToken cancellationToken)
   {
      for (int i = 0; i < 5; i++)
         await Next(context, cancellationToken);
   }
}