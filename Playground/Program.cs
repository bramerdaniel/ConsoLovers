using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground
{
   using ConsoLovers.ConsoleToolkit;
   using ConsoLovers.ConsoleToolkit.Menu;

   class Program
   {
      static void Main(string[] args)
      {
         Console.Write("Enter your password : ");
         var password = new InputMask().ReadLine('#');
         Console.WriteLine($"Password: {password}");

         Console.Write("Enter your password : ");
         password = new InputMask().ReadLine(6, '.');
         Console.WriteLine($"Password: {password}");


         Console.Write("Enter exit : ");
         new InputMask { MaskingCompleted = WhenInputIsExit}.Read(4, '.');
         Console.WriteLine($" ==> return");

         Console.ReadLine();
      }

      private static bool WhenInputIsExit(ConsoleKeyInfo key, string input)
      {
         return input == "exit";
      }
   }
}
