## InputBox

```csharp
   class Program
   {
      private static void Main(string[] args)
      {
         new ConsoleApplicationManager().Run(typeof(ProgramLogic), args);
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


