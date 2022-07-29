namespace Playground;

using ConsoLovers.ConsoleToolkit.Core;
using ConsoLovers.ConsoleToolkit.Core.Input;

public static class Program
{
   public static async Task Main()
   {
      await ConsoleApplicationManager.For<PlaygroundApp>()
         .RunAsync();
      
   }
}