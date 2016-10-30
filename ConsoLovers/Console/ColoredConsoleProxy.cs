namespace ConsoLovers.Console
{
   using System;
   using System.Drawing;

   using ConsoLovers.Contracts;

   public class ColoredConsoleProxy : ConsoleProxy, IColoredConsole
   {
      public void Clear(Color clearingColor)
      {
        Console.BackgroundColor = clearingColor;
        Console.Clear();
        Console.ResetColor();
      }

      private static IColoredConsole instance;

      public new static IColoredConsole Instance => instance ?? (instance = new ColoredConsoleProxy());
   }
}