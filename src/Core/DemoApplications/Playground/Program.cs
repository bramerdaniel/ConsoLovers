namespace Playground;

using System.Security.Cryptography.X509Certificates;

using ConsoLovers.ConsoleToolkit.Core;
using ConsoLovers.ConsoleToolkit.Core.Middleware;

using Microsoft.Extensions.DependencyInjection;

public static class Program
{
   public static void Main()
   {
      var executable = ConsoleApplication.WithArguments<ApplicationArgs>()
         //.UseServiceProviderFactory(new DefaultServiceProviderFactory())
         // .UseApplicationLogic(Execute)
         //.AddMiddleware(typeof(TryCatchMiddleware))
         //.AddMiddleware<ApplicationArgs, RepeatMiddleware>()
         // .Run();
         .ConfigureMapping(o =>
         {
            o.UnhandledArgumentsBehavior = UnhandledArgumentsBehaviors.UseCustomHandler | UnhandledArgumentsBehaviors.LogToConsole;
         })
         .AddService(x => x.AddSingleton<IMappingHandler<ApplicationArgs>>(new Handler()))
         .Run(t => throw new InvalidOperationException("No command could be was executed"));

      Console.ReadLine();
   }
}

public class Handler : IMappingHandler<ApplicationArgs>
{
   public bool TryMap(ApplicationArgs arguments, CommandLineArgument argument)
   {
      return false;
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