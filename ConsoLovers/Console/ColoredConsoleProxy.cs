namespace ConsoLovers.Console
{
   using System;

   using ConsoLovers.Contracts;

   public class ColoredConsoleProxy : ConsoleProxy, IColoredConsole
   {
      public void Clear(ConsoleColor clearingColor)
      {
         Console.BackgroundColor = clearingColor;
         Console.Clear();
         Console.ResetColor();
      }

      private static IColoredConsole instance;

      public new static IColoredConsole Instance => instance ?? (instance = new ColoredConsoleProxy());
   }
}