// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MatrixEffect.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Utils
{
   using System;
   using System.Collections.Generic;
   using System.Threading;

   using ConsoLovers.ConsoleToolkit.Console;

   public class MatrixEffect
   {
      #region Constants and Fields

      static readonly Random random = new Random();

      static bool thistime;

      private ConsoleColor initialBackground;

      private ConsoleColor initialForeground;

      private string[] lines;

      private string text;

      private Thread thread;

      private int? xPosition;

      private int? yPosition;

      #endregion

      #region Public Properties

      public ConsoleColor AdditionalColor { get; set; } = ConsoleColor.White;

      public ConsoleColor MatrixColor { get; set; } = ConsoleColor.DarkGreen;

      public ConsoleColor MatrixDrawingColor { get; set; } = ConsoleColor.Green;

      public string Text
      {
         get
         {
            return text;
         }
         set
         {
            if (text == value)
               return;

            text = value;
            lines = null;
         }
      }

      public ConsoleColor TextForeground { get; set; } = ConsoleColor.Cyan;

      public int TextXPosition
      {
         get
         {
            if (xPosition.HasValue)
               return xPosition.Value;

            xPosition = ComputeXPosition();
            return xPosition.Value;
         }
         set
         {
            xPosition = value;
         }
      }

      public int TextYPosition
      {
         get
         {
            if (!yPosition.HasValue)
               yPosition = ComputeYPosition();

            return yPosition.Value;
         }

         set
         {
            yPosition = value;
         }
      }

      #endregion

      #region Properties

      internal IConsoleBuffer Buffer { get; } = new ConsoleBuffer();

      #endregion

      #region Public Methods and Operators

      public void Start()
      {
         DrawMatrix();
         CleanUp();
      }

      public void StartAsync()
      {
         thread = new Thread(DrawMatrix);
         thread.Start();
      }

      public void Wait()
      {
         thread?.Join();

         CleanUp();
      }

      #endregion

      #region Methods

      private static IEnumerable<string> CreateTextOutput()
      {
         yield return "                                                                              ";
         yield return "                                                                              ";
         yield return "    XX        XX XXXXXXXX  XX     XX         XX        XX XXXXXX XX     XX    ";
         yield return "     XX      XX  XX    XX  XX     XX         XX        XX   XX   XXX    XX    ";
         yield return "      XX    XX   XX    XX  XX     XX         XX        XX   XX   XXXX   XX    ";
         yield return "       XX  XX    XX    XX  XX     XX         XX        XX   XX   XX XX  XX    ";
         yield return "         XX      XX    XX  XX     XX         XX   XX   XX   XX   XX  XX XX    ";
         yield return "         XX      XX    XX  XX     XX         XX   XX   XX   XX   XX   XXXX    ";
         yield return "         XX      XX    XX  XX     XX         XX   XX   XX   XX   XX    XXX    ";
         yield return "         XX      XXXXXXXX  XXXXXXXXX         XXXXXXXXXXXX XXXXXX XX     XX    ";
         yield return "                                                                              ";
         yield return "                                                                              ";
      }

      private static char GetRandomChar()
      {
         int t = random.Next(10);
         if (t <= 2)
            return (char)('0' + random.Next(10));
         if (t <= 4)
            return (char)('a' + random.Next(27));
         if (t <= 6)
            return (char)('A' + random.Next(27));

         return (char)random.Next(32, 255);
      }

      private static int InBoxY(int n, int height)
      {
         n = n % height;
         if (n < 0)
            return n + height;
         return n;
      }

      private void CleanUp()
      {
         Console.ForegroundColor = initialForeground;
         Console.BackgroundColor = initialBackground;
         Console.Clear();
      }

      private int ComputeXPosition()
      {
         int longestLine = LongestLine();

         return Console.WindowWidth / 2 - longestLine / 2 - 1;
      }

      private int ComputeYPosition()
      {
         return Console.WindowHeight / 2 - 1 - LineCount() / 2;
      }

      private void Draw_Text(int xPos, int yPos, IList<string> texts)
      {
         var oldColor = Console.ForegroundColor;
         Console.ForegroundColor = ConsoleColor.Magenta;
         for (int i = 0; i < texts.Count; i++)
         {
            Console.SetCursorPosition(xPos, yPos + i);
            Console.Write(texts[i]);
         }
         Console.ForegroundColor = oldColor;
      }

      private void DrawMatrix()
      {
         initialForeground = Console.ForegroundColor;
         initialBackground = Console.BackgroundColor;
         Console.WindowLeft = Console.WindowTop = 0;
         Console.WindowHeight = Console.BufferHeight = Math.Max(50, Console.WindowHeight);
         Console.WindowWidth = Console.BufferWidth = Math.Max(120, Console.WindowWidth);

#if readkey
			Console.WriteLine("H1T 7NY K3Y T0 C0NT1NU3 =/");
			Console.ReadKey();
#endif

         Console.CursorVisible = false;
         int width, height;
         int[] y;
         int[] l;
         Initialize(out width, out height, out y, out l);

         while (true)
         {
            DateTime t1 = DateTime.Now;
            MatrixStep(width, height, y, l);
            var ms = 10 - (int)(DateTime.Now - t1).TotalMilliseconds;

            if (ms > 0)
               Thread.Sleep(ms);

            if (Console.KeyAvailable)
            {
               var key = Console.ReadKey().Key;
               switch (key)
               {
                  case ConsoleKey.Escape:
                  case ConsoleKey.Enter:
                     return;
                  case ConsoleKey.F5:
                     Initialize(out width, out height, out y, out l);
                     break;
               }
            }
         }
      }

      private void DrawText()
      {
         var x = TextXPosition;
         var y = TextYPosition;
         var longestLine = LongestLine();

         //var oldColor = Console.ForegroundColor;
         //Console.ForegroundColor = TextForeground;

         foreach (var line in GetLines())
         {
            var xx = x + (longestLine - line.Length) / 2;
            if (!string.IsNullOrWhiteSpace(line))
            {
               Buffer.WriteLine(xx, y, line, TextForeground, false);
               //Console.SetCursorPosition(xx, y);
               //Console.Write(line);
            }

            y++;
         }

         //Console.ForegroundColor = oldColor;
      }

      private string[] GetLines()
      {
         return lines ?? (lines = Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None));
      }

      private void Initialize(out int width, out int height, out int[] y, out int[] l)
      {
         int h1;
         int h2 = (h1 = (height = Console.WindowHeight) / 2) / 2;
         width = Console.WindowWidth - 1;
         y = new int[width];
         l = new int[width];
         int x;
         Console.Clear();
         for (x = 0; x < width; ++x)
         {
            y[x] = random.Next(height);
            l[x] = random.Next(h2 * (x % 11 != 10 ? 2 : 1), h1 * (x % 11 != 10 ? 2 : 1));
         }
      }

      private int LineCount()
      {
         return GetLines().Length;
      }

      private int LongestLine()
      {
         int longestLine = 0;
         foreach (var line in Text.Split(new[] { Environment.NewLine }, StringSplitOptions.None))
            longestLine = Math.Max(longestLine, line.Length);

         return longestLine;
      }

      private void MatrixStep(int width, int height, int[] y, int[] l)
      {
         int x;
         thistime = !thistime;

         for (x = 0; x < width; ++x)
         {
            ConsoleColor color;
            if (x % 11 == 10)
            {
               if (!thistime)
                  continue;

               color = AdditionalColor;
            }
            else
            {
               // Console.ForegroundColor = MatrixColor;
               var top = InBoxY(y[x] - 2 - l[x] / 40 * 2, height);
               var left = x;
               Write(left, top, GetRandomChar().ToString(), MatrixColor);
               //Console.SetCursorPosition(left, top);
               //Console.Write(GetRandomChar());

               // Thread.SpinWait(100000);
               if (Text != null)
                  DrawText();

               // Draw_Text(17, 19, CreateTextOutput().ToList());

               color = MatrixDrawingColor;
            }

            Write(x, y[x], GetRandomChar().ToString(), color);
            //Console.SetCursorPosition(x, y[x]);
            //Console.Write(GetRandomChar());

            y[x] = InBoxY(y[x] + 1, height);

            Write(x, InBoxY(y[x] - l[x], height), " ", color);
            //Console.SetCursorPosition(x, InBoxY(y[x] - l[x], height));
            //Console.Write(" ");
         }
      }

      private void Write(int left, int top, string character, ConsoleColor foreground)
      {
         Buffer.WriteLine(left, top, character, foreground, false);
      }

      #endregion
   }
}