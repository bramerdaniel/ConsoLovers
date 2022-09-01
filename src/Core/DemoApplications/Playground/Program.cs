namespace Playground;

using System.Security.Cryptography.X509Certificates;

using ConsoLovers.ConsoleToolkit.Core;

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