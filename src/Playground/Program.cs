namespace Playground;

using ConsoLovers.ConsoleToolkit.Core;
using ConsoLovers.ConsoleToolkit.Core.Input;

public static class Program
{
   public static async Task Main()
   {
      Console.WriteLine(new InputBox<int>("integer: ", 34).ReadLine());
      Console.WriteLine(new InputBox<double>("double: ", 34.54).ReadLine());
      Console.WriteLine(new InputBox<string>("password: ", "Amdin"){ IsPassword = true }.ReadLine());

      await ConsoleApplicationManager.For<PlaygroundApp>()
         .RunAsync();

      Console.ReadLine();
   }
}