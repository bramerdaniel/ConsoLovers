using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Playground
{
   using ConsoLovers.ConsoleToolkit;
   using ConsoLovers.ConsoleToolkit.Console;
   using ConsoLovers.ConsoleToolkit.Menu;

   class Program
   {
      static void Main(string[] args)
      {
         string password;


         Console.Write("Enter your password : ");
         var mask = '#';
         var length = 8;
         var placeHolder = '.';
         password = new InputMask().ReadLine(mask, length, placeHolder);
         Console.WriteLine($"Password: {password}");

         Console.Write("Enter your password : ");
         password = new InputMask().ReadLine(6, '.');
         Console.WriteLine($"Password: {password}");


         Console.Write("Enter exit : ");
         new InputMask { MaskingCompleted = WhenInputIsExit }.Read(4, '.');
         Console.WriteLine($" ==> return");

         Console.ReadLine();
      }

      private static bool WhenInputIsExit(ConsoleKeyInfo key, string input)
      {
         return input == "exit";
      }
   }
}
