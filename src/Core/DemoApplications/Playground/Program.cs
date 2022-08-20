namespace Playground;

using ConsoLovers.ConsoleToolkit.Core;

using Microsoft.Extensions.DependencyInjection;

public static class Program
{
   public static async Task Main()
   {
      var executable = await ConsoleApplication.WithArguments<ApplicationArgs>()
         .UseServiceProviderFactory(new DefaultServiceProviderFactory())
         .UseApplicationLogic(Execute)
         .RunAsync();
      
      Console.ReadLine();
   }

   private static Task Execute(ApplicationArgs args, CancellationToken cancellationToken)
   {
      Console.WriteLine("Executed with func");
      return Task.CompletedTask;
   }
}