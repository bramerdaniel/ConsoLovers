

namespace Playground;

using ConsoLovers.ConsoleToolkit.Core;

public static class Program
{
   public static async Task Main()
   {
      ConsoleApplicationManager.For<PlaygroundApp>()
         .Run();

      Console.ReadLine();
   }

}