using System;

namespace InputMaskDemo
{
   using ConsoLovers.ConsoleToolkit;

   class Program
   {
      static void Main(string[] args)
      {
         Console.Title = "Input mask demo application";

         #region Normal usage

         Console.WriteLine("Standard usage ");
         Console.Write("Enter password: ");
         var password = new InputMask().ReadLine();
         Console.WriteLine($"Password: {password}");
         Console.WriteLine();

         #endregion

         #region Usage with length restriction

         Console.WriteLine("Usage with length restriction");
         Console.Write("Enter your password : ");
         password = new InputMask().ReadLine(6, '.');
         Console.WriteLine($"Password: {password}");
         Console.WriteLine();

         #endregion

         #region Usage with length restriction and custom mask

         Console.WriteLine("Usage with length restriction and custom mask");
         Console.Write("Enter your password : ");
         password = new InputMask().ReadLine('#', 8, '.');
         Console.WriteLine($"Password: {password}");
         Console.WriteLine();

         #endregion

         Console.WriteLine("Usage with custom termination behaviour");
         Console.Write("Enter ");
         Console.ForegroundColor = ConsoleColor.Red;
         Console.Write("exit");
         Console.ResetColor();
         Console.Write(" : ");
         new InputMask { MaskingCompleted = WhenInputIsExit }.Read(4, '.');
         Console.WriteLine(" ==> finished on exit");
         Console.WriteLine();

         Console.ReadLine();
      }

      private static bool WhenInputIsExit(ConsoleKeyInfo key, string input)
      {
         return input == "exit";
      }

   }
}
