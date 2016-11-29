// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleBuffer.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Console
{
   using System;
   using System.Runtime.InteropServices;

   /// <summary>Class providing direct write access to the console buffer, without the need to set the cursor before.</summary>
   public class ConsoleBuffer : IConsoleBuffer
   {
      #region Constants and Fields

      private readonly IntPtr outputHandle;

      private readonly bool[,] readonlySections;

      #endregion

      #region Constructors and Destructors

      /// <summary>Initializes a new instance of the <see cref="ConsoleBuffer"/> class.</summary>
      public ConsoleBuffer()
      {
         readonlySections = new bool[Console.BufferWidth, Console.BufferHeight];
         outputHandle = Win32Console.GetStdHandle(Win32Console.STD_OUTPUT_HANDLE);
      }

      public bool[,] ReadonlySections => readonlySections;

      #endregion

      #region Public Methods and Operators

      /// <summary>Draws the specified text.</summary>
      /// <param name="left">The column.</param>
      /// <param name="top">The line.</param>
      /// <param name="text">The text.</param>
      public void WriteLine(int left, int top, string text)
      {
         WriteLine(left, top, text, Console.ForegroundColor, Console.BackgroundColor, true);
      }

      /// <summary>Draws the specified text.</summary>
      /// <param name="left">The column.</param>
      /// <param name="top">The line.</param>
      /// <param name="text">The text.</param>
      /// <param name="clearLine">if set to <c>true</c> line is cleared before drawing.</param>
      public void WriteLine(int left, int top, string text, bool clearLine)
      {
         WriteLine(left, top, text, Console.ForegroundColor, Console.BackgroundColor, clearLine);
      }

      /// <summary>Draws the specified text.</summary>
      /// <param name="left">The column.</param>
      /// <param name="top">The line.</param>
      /// <param name="text">The text.</param>
      /// <param name="foreground">The foreground.</param>
      /// <param name="clearLine">if set to <c>true</c> line is cleared before drawing.</param>
      public void WriteLine(int left, int top, string text, ConsoleColor foreground, bool clearLine)
      {
         WriteLine(left, top, text, foreground, Console.BackgroundColor, clearLine);
      }

      public void WriteLine(int left, int top, string text, ConsoleColor foreground)
      {
         WriteLine(left, top, text, foreground, true);
      }

      /// <summary>Draws the specified text.</summary>
      /// <param name="left">The column.</param>
      /// <param name="top">The line.</param>
      /// <param name="text">The text.</param>
      /// <param name="foreground">The foreground.</param>
      /// <param name="background">The background.</param>
      /// <param name="clearLine">if set to <c>true</c> line is cleared before drawing.</param>
      public void WriteLine(int left, int top, string text, ConsoleColor foreground, ConsoleColor background, bool clearLine)
      {
         Win32Console.CHAR_INFO[] bufferToWrite;

         if (clearLine)
         {
            Win32Console.CONSOLE_SCREEN_BUFFER_INFO bufferInfo;
            Win32Console.GetConsoleScreenBufferInfo(outputHandle, out bufferInfo);
            bufferToWrite = CreateBuffer(bufferInfo.dwSize.X, foreground, background);
            InsertInBuffer(bufferToWrite, (short)left, text, foreground, background);
            left = 0;
         }
         else
         {
            bufferToWrite = CreateBuffer(text, foreground, background);
         }

         var rect = new Win32Console.SMALL_RECT { Left = (short)left, Top = (short)top, Right = (short)(left + bufferToWrite.Length), Bottom = (short)(top + 1) };
         Win32Console.WriteConsoleOutput(
            outputHandle,
            bufferToWrite,
            new Win32Console.COORD { X = (short)bufferToWrite.Length, Y = 1 },
            new Win32Console.COORD { X = 0, Y = 0 },
            ref rect);
      }

      public void WriteLine(int left, int top, char character, ConsoleColor foreground, ConsoleColor background)
      {
         if (readonlySections[left, top])
            return;

         short attribute = ConsoleColorToColorAttribute(foreground, background);
         var bufferToWrite = new[] { new Win32Console.CHAR_INFO { attributes = attribute, charData = character } };

         var rect = new Win32Console.SMALL_RECT { Left = (short)left, Top = (short)top, Right = (short)(left + bufferToWrite.Length), Bottom = (short)(top + 1) };
         Win32Console.WriteConsoleOutput(
            outputHandle,
            bufferToWrite,
            new Win32Console.COORD { X = (short)bufferToWrite.Length, Y = 1 },
            new Win32Console.COORD { X = 0, Y = 0 },
            ref rect);
      }


      #endregion

      #region Methods

      private static short ConsoleColorToColorAttribute(ConsoleColor foreground, ConsoleColor background)
      {
         var f = (ushort)foreground;
         var b = (ushort)background;
         return (short)(f | b << 4);
      }

      private static Win32Console.CHAR_INFO[] CreateBuffer(short length, ConsoleColor foreground, ConsoleColor background)
      {
         short attribute = ConsoleColorToColorAttribute(foreground, background);
         var buffer = new Win32Console.CHAR_INFO[length];
         for (int i = 0; i < buffer.Length; i++)
            buffer[i].attributes = attribute;

         return buffer;
      }

      private static Win32Console.CHAR_INFO[] CreateBuffer(string text, ConsoleColor foreground, ConsoleColor background)
      {
         short attribute = ConsoleColorToColorAttribute(foreground, background);
         var buffer = new Win32Console.CHAR_INFO[text.Length];
         var index = 0;
         foreach (var c in text)
         {
            buffer[index].charData = c;
            buffer[index].attributes = attribute;
            index++;
         }

         return buffer;
      }

      private static void InsertInBuffer(Win32Console.CHAR_INFO[] buffer, short startIndex, string text, ConsoleColor foreground, ConsoleColor background)
      {
         short attribute = ConsoleColorToColorAttribute(foreground, background);
         var index = startIndex;
         foreach (var c in text)
         {
            buffer[index].charData = c;
            buffer[index].attributes = attribute;
            index++;
         }
      }

      #endregion
   }
}