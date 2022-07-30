namespace Playground;

using ConsoLovers.ConsoleToolkit.Core;
using ConsoLovers.ConsoleToolkit.Core.Input;

public static class Program
{
   public static async Task Main()
   {
      var app = await ConsoleApplicationManager
         .For<PlaygroundApp>()
         .RunAsync(CancellationToken.None);

      if (app.Arguments?.Wait ?? true)
         Console.ReadLine();
   }
}