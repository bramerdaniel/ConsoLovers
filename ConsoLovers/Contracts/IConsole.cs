namespace ConsoLovers.ConsoleToolkit.Contracts
{
   using System;
   using System.Drawing;

   public interface IConsole
   {
      ConsoleKeyInfo ReadKey();

      void Clear();

      void Clear(ConsoleColor color);
      
      int CursorLeft { get; set; }

      int WindowWidth { get; set; }

      int CursorTop { get; set; }

      ConsoleColor ForegroundColor { get; set; }

      ConsoleColor BackgroundColor { get; set; }

      void WriteLine();

      void ResetColor();
      void WriteLine(string value);

      void SetCursorPosition(int left, int top);

      void Write(string value);
   }
}