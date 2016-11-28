namespace ConsoLovers.ConsoleToolkit.Contracts
{
   using System;

   /// <summary>The most simple implementation of the <see cref="IConsole"/> interface that is forwarding all calls directly to the <see cref="Console"/>, and provides some extra features
   /// </summary>
   /// <seealso cref="ConsoLovers.ConsoleToolkit.Contracts.IConsole" />
   public class ConsoleProxy : IConsole
   {
      public ConsoleColor BackgroundColor
      {
         get
         {
            return Console.BackgroundColor;
         }
         set
         {
            Console.BackgroundColor = value;
         }
      }

      public int CursorLeft
      {
         get
         {
            return Console.CursorLeft;
         }
         set
         {
            Console.CursorLeft = value;
         }
      }

      public int CursorTop
      {
         get
         {
            return Console.CursorTop;
         }
         set
         {
            Console.CursorTop = value;
         }
      }

      public ConsoleColor ForegroundColor
      {
         get
         {
            return Console.ForegroundColor;
         }
         set
         {
            Console.ForegroundColor = value;
         }
      }

      public int WindowWidth
      {
         get
         {
            return Console.WindowWidth;
         }
         set
         {
            Console.WindowWidth = value;
         }
      }

      public void Clear()
      {
         Console.Clear();
      }

      public void Clear(ConsoleColor color)
      {
         Console.BackgroundColor = color;
         Console.Clear();
         Console.ResetColor();
      }

      public ConsoleKeyInfo ReadKey()
      {
         return Console.ReadKey();
      }

      public ConsoleKeyInfo ReadKey(bool intercept)
      {
         return Console.ReadKey(intercept);
      }

      public string ReadLine()
      {
         return Console.ReadLine();
      }

      public void ResetColor()
      {
         Console.ResetColor();
      }

      public void SetCursorPosition(int left, int top)
      {
         Console.SetCursorPosition(left, top);
      }

      public void Write(string value)
      {
         Console.Write(value);
      }

      public void Write(string value, ConsoleColor foreground)
      {
         var original = Console.ForegroundColor;
         Console.ForegroundColor = foreground;

         Console.Write(value);

         Console.ForegroundColor = original;
      }

      public void Write(string value, ConsoleColor foreground, ConsoleColor background)
      {
         var foregroundColor = Console.ForegroundColor;
         var backgroundColor = Console.BackgroundColor;
         Console.ForegroundColor = foreground;
         Console.BackgroundColor = background;

         Console.Write(value);

         Console.ForegroundColor = foregroundColor;
         Console.BackgroundColor = backgroundColor;
      }

      public void Write(char value)
      {
         Console.Write(value);
      }

      public void WriteLine()
      {
         Console.WriteLine();
      }

      public void WriteLine(string value)
      {
         Console.WriteLine(value);
      }

      public void WriteLine(string value, ConsoleColor foreground)
      {
         var foregroundColor = Console.ForegroundColor;
         Console.ForegroundColor = foreground;

         Console.WriteLine(value);

         Console.ForegroundColor = foregroundColor;
      }

      public void WriteLine(string value, ConsoleColor foreground, ConsoleColor background)
      {
         var foregroundColor = Console.ForegroundColor;
         var backgroundColor = Console.BackgroundColor;
         Console.ForegroundColor = foreground;
         Console.BackgroundColor = background;

         Console.WriteLine(value);

         Console.ForegroundColor = foregroundColor;
         Console.BackgroundColor = backgroundColor;
      }

      public void Beep()
      {
         Console.Beep();
      }

      public void WriteLine(bool value)
      {
         Console.WriteLine(value);
      }

      public void WriteLine(char value)
      {
         Console.WriteLine(value);
      }

      public void WriteLine(char[] value)
      {
         Console.WriteLine(value);
      }

      public void WriteLine(decimal value)
      {
         Console.WriteLine(value);
      }

      public void WriteLine(double value)
      {
         Console.WriteLine(value);
      }

      public void WriteLine(float value)
      {
         Console.WriteLine(value);
      }

      public void WriteLine(int value)
      {
         Console.WriteLine(value);
      }

      public void WriteLine(long value)
      {
         Console.WriteLine(value);
      }

      public void WriteLine(object value)
      {
         Console.WriteLine(value);
      }

      public void WriteLine(uint value)
      {
         Console.WriteLine(value);
      }

      public void WriteLine(ulong value)
      {
         Console.WriteLine(value);
      }

      public void WriteLine(string format, object arg0)
      {
         Console.WriteLine(format, arg0);
      }

      public void WriteLine(string format, params object[] args)
      {
         Console.WriteLine(format, args);
      }

      public void WriteLine(char[] buffer, int index, int count)
      {
         Console.WriteLine(buffer, index, count);
      }

      public void WriteLine(string format, object arg0, object arg1)
      {
         Console.WriteLine(format, arg0, arg1);
      }

      public void WriteLine(string format, object arg0, object arg1, object arg2)
      {

         Console.WriteLine(format, arg0, arg1, arg2);
      }

      public void WriteLine(string format, object arg0, object arg1, object arg2, object arg3)
      {
         Console.WriteLine(format, arg0, arg1, arg2, arg3);
      }
   }
}