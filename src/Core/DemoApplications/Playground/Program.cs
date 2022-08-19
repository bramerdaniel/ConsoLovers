namespace Playground;

using System.ComponentModel.Design;

using Autofac.Extensions.DependencyInjection;

using ConsoLovers.ConsoleToolkit.Core;
using ConsoLovers.ConsoleToolkit.Core.Input;

using Microsoft.Extensions.DependencyInjection;

public static class Program
{
   public static void Main()
   {

      var app = ConsoleApplicationManager
         .For<PlaygroundApp>()
         .UseServiceProviderFactory(new DefaultServiceProviderFactory())
         .UseApplicationLogic<PlaygroundApp, ApplicationArgs>(Execute)
         .Run();

      Console.ReadLine();

      ConsoleApplicationManager
         .For(typeof(AppWithoutArgs))
         .Run();

      Environment.Exit(0);


   }

   private static Task Execute(ApplicationArgs args, CancellationToken cancellationToken)
   {
      Console.WriteLine("Executed with func");
      return Task.CompletedTask;
   }
}

public class AppWithoutArgs : IApplication
{
   public Task RunAsync(CancellationToken cancellationToken)
   {
      Console.WriteLine("Done Async");
      Console.ReadLine();
      return Task.CompletedTask;
   }
}