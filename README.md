# ConsoLovers.ConsoleToolkit
The ConsoLovers.ConsoleToolkit is a collection of several independant tools that can be used to write console applications (better and faster).
<p align="center">
  <img alt="VS Code in action" src="https://github.com/bramerdaniel/ConsoLovers/blob/master/src/Documentation/ConsoleMenuExplorer.png">
</p>

## Main features are:
* A [ConsoleMenu](https://github.com/bramerdaniel/ConsoLovers/blob/master/src/ConsoLovers.ConsoleToolkit/Menu/ConsoleMenu.cs) that supports Keyboard and Mouse selection.
  The menu is extremely customizable, supports color themes, index navigation and much more...
* A CommandLineEngine that offers parsing of command line arguments and mapping them to properties of a class 
* A [InputBox](https://github.com/bramerdaniel/ConsoLovers/tree/documentation/Documentation/InputBox) class that can be used for password scenarios.
* The [ConsoleApplicationManager](https://github.com/bramerdaniel/ConsoLovers/blob/master/src/ConsoLovers.ConsoleToolkit/ConsoleApplicationManager.cs) and the [ConsoleApplicationWith](https://github.com/bramerdaniel/ConsoLovers/blob/master/src/ConsoLovers.ConsoleToolkit/ConsoleApplicationWith.cs). A base class for console applications to get rid of the static context, normal console application is runnin in.

```csharp
   class Program
   {
      private static void Main(string[] args)
      {
         new ConsoleApplicationManager().Run(typeof(MyProgramLogic), args);
      }
   }
   
   internal class MyProgramLogic : ConsoleApplicationWith<MyArguments>
   {
      // Entry point for  non static logic.
      public override void RunWith(MyArguments arguments)
      {
         if (!File.Exists(arguments.Path))
            Console.WriteLine("Path must point to an existing file");
         // some cool logic here...
      }
   }
   
   // The arguments class your args are mapped to
   internal class MyArguments
   {
      [Argument("Path", "p")]
      public string Path { get; set; }
   }
```

## Usage
The ConsoleToolkit is also available on nuget [here](https://www.nuget.org/packages/ConsoLovers.ConsoleToolkit)
or simply search for 'ConsoleToolkit'.

If a third party reference is not allowed in your project, you could also download the sources 
and just pick the classes you need (this should work in the most cases without any problems). 

## Contribution
Feel free to create a pull request to include new features. 
For development Visual Studio 2017 is required. (or any tool for compiling C#7)

## License
There is no licence required.
