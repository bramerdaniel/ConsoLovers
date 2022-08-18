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
      var app = await ConsoleApplicationManager
         .For<PlaygroundApp>()
         .UseServiceProviderFactory(new DefaultServiceProviderFactory())
         .RunAsync(CancellationToken.None);

      if (app.Arguments?.Wait ?? true)
         Console.ReadLine();
   }
}