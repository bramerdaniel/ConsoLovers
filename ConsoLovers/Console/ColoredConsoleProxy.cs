namespace ConsoLovers.Console
{
   using System;
   using System.Drawing;

   using ConsoLovers.Contracts;

   public class ColoredConsoleProxy : ConsoleProxy, IColoredConsole
   {
      public void Clear(Color clearingColor)
      {
        ColoredConsole.BackgroundColor = clearingColor;
        ColoredConsole.Clear();
        ColoredConsole.ResetColor();
      }

      private static IColoredConsole instance;

      public new static IColoredConsole Instance => instance ?? (instance = new ColoredConsoleProxy());
   }
}