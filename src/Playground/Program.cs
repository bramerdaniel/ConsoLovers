namespace Playground;

using ConsoLovers.ConsoleToolkit.Core;

public static class Program
{
   public static async Task Main()
   {
      await ConsoleApplicationManager.For<PlaygroundApp>()
         .RunAsync();

      Console.ReadLine();
   }
}