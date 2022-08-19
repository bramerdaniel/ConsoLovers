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
      ConsoleApplicationManager
         .For(typeof(AppWithoutArgs))
         .Run();

      Environment.Exit(0);

      var app = ConsoleApplicationManager
         .For<PlaygroundApp>()
         .UseServiceProviderFactory(new DefaultServiceProviderFactory())
         .Run();

      Console.ReadLine();

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