namespace Net6Playground
{
   using System;

   internal class Program
   {
      static void Main(string[] args)
      {
         Console.Clear();

#if NET472_OR_GREATER
         Console.WriteLine(".Net 472");
#endif
#if NET6_0_OR_GREATER
         Console.WriteLine(".Net 6");
#endif

         var commandLine = Environment.CommandLine;
         Console.WriteLine(commandLine);
         Console.WriteLine();

         var commandLineArgs = Environment.GetCommandLineArgs();
         //foreach (var arg in commandLineArgs)
         //   Console.WriteLine($"-{arg}");

         Console.ReadLine();
      }
   }
}