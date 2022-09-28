namespace Playground;

using ConsoLovers.ConsoleToolkit.Core;

using Microsoft.Extensions.DependencyInjection;

public static class Program
{
   public static void Main()
   {
      var executable = ConsoleApplication.WithArguments<ApplicationArgs>()
         .AddService(s => s.AddTransient<IAsyncShutdownHandler, ShutdownHandler>())
         .Run();
 
      Console.ReadLine();
   }
}

public class ShutdownHandler : IAsyncShutdownHandler
{
   public ShutdownHandler()
   {
      
   }

   public void NotifyShutdown(IExecutionResult executionResult)
   {
      

   }

   public Task NotifyShutdownAsync(IExecutionResult executionResult)
   {
      return Task.CompletedTask;
   }
}