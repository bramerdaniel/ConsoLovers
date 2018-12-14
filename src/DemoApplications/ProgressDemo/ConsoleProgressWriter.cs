namespace ProgressDemo
{
   using System;

   using ConsoLovers.ConsoleToolkit.Core;

   internal class ConsoleProgressWriter : IProgressWriter
   {
      private readonly int originalTop;

      private readonly int originalLeft;

      public ConsoleProgressWriter(int originalTop, int originalLeft)
      {
         this.originalTop = originalTop;
         this.originalLeft = originalLeft;
      }

      public IConsole Console { get; } = new ConsoleProxy();

      public void DrawValue(ProgressInfo progressInfo)
      {
         var valueString = $"{progressInfo.ProgressValue} % ".PadLeft(7);
         
         var leftMargin = Console.CursorLeft;
         Console.CursorLeft = leftMargin + valueString.Length;
         Console.WriteLine(progressInfo.Text, ConsoleColor.Green);
         Console.CursorLeft = leftMargin;

         Console.Write(valueString, ConsoleColor.Green);

         var rest = progressInfo.AvailableWidth - valueString.Length;

         var width = rest  * progressInfo.ProgressValue / 100;
         Console.Write(string.Empty.PadRight((int)width, '█'), ConsoleColor.Green);
         //Console.Write(string.Empty.PadRight((int)width, '▄'));
      }
   }
}