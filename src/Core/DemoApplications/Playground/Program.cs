namespace Playground;

using System.ComponentModel.Design;

using Autofac.Extensions.DependencyInjection;

using ConsoLovers.ConsoleToolkit.Core;
using ConsoLovers.ConsoleToolkit.Core.Input;

using Microsoft.Extensions.DependencyInjection;

public static class Program
{
   public static async Task Main()
   {
      ConsoleApplicationManager
         .For<AppWithoutArgs>()
         .Run();

      Environment.Exit(0);

      var app = await ConsoleApplicationManager
         .For<PlaygroundApp>()
         .UseServiceProviderFactory(new DefaultServiceProviderFactory())
         // .ShowHelpWithoutArguments()
         .RunAsync(CancellationToken.None);

      Console.ReadLine();
   }
}

public class AppWithoutArgs : IApplication
{
   public void Run()
   {
      Console.WriteLine("Done");
      Console.ReadLine();
   }

   public Task RunAsync(CancellationToken cancellationToken)
   {
      Console.WriteLine("Done Async");
      Console.ReadLine();
      return Task.CompletedTask;
   }
}