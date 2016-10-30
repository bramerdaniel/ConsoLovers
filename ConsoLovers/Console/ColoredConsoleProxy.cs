namespace ConsoLovers.Console
{
   using System;

   using ConsoLovers.Contracts;

   public class ColoredConsoleProxy : ConsoleProxy, IColoredConsole
   {
      public void Clear(ConsoleColor clearingColor)
      {
         System.Console.BackgroundColor = clearingColor;
         System.Console.Clear();
         System.Console.ResetColor();
      }

      private static IColoredConsole instance;

      public new static IColoredConsole Instance => instance ?? (instance = new ColoredConsoleProxy());
   }
}