// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleProxy.cs" company="ConsoLovers">
//   Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.Console
{
   using System;
   using System.Drawing;

   using ConsoLovers.Contracts;

   public class ConsoleProxy : IConsole
   {
      #region Constants and Fields

      private static IConsole instance;

      #endregion

      #region IColoredConsole Members

      #endregion

      #region IConsole Members

      /// <summary>Obtains the next character or function key pressed by the user. The pressed key is displayed in the console window.</summary>
      /// <returns>
      ///    A <see cref="T:System.ConsoleKeyInfo"/> object that describes the <see cref="T:System.ConsoleKey"/> constant and Unicode character, if any, that correspond to the pressed
      ///    console key. The <see cref="T:System.ConsoleKeyInfo"/> object also describes, in a bitwise combination of <see cref="T:System.ConsoleModifiers"/> values, whether one or more
      ///    Shift, Alt, or Ctrl modifier keys was pressed simultaneously with the console key.
      /// </returns>
      /// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.ColoredConsole.In"/> property is redirected from some stream other than the console.</exception>
      public ConsoleKeyInfo ReadKey()
      {
         return ColoredConsole.ReadKey();
      }

      /// <summary>Clears the console buffer and corresponding console window of display information.</summary>
      /// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
      /// <filterpriority>1</filterpriority>
      public void Clear()
      {
         ColoredConsole.Clear();
      }

      /// <summary>Gets or sets the column position of the cursor within the buffer area.</summary>
      /// <returns>The current position, in columns, of the cursor.</returns>
      /// <exception cref="T:System.ArgumentOutOfRangeException">The value in a set operation is less than zero.-or- The value in a set operation is greater than or equal to
      ///    <see cref="P:System.ColoredConsole.BufferWidth"/>.</exception>
      /// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action. </exception>
      /// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
      public int CursorLeft
      {
         get
         {
            return ColoredConsole.CursorLeft;
         }
         set
         {
            ColoredConsole.CursorLeft = value;
         }
      }

      /// <summary>Gets or sets the width of the console window.</summary>
      /// <returns>The width of the console window measured in columns.</returns>
      /// <exception cref="T:System.ArgumentOutOfRangeException">
      ///    The value of the <see cref="P:System.ColoredConsole.WindowWidth"/> property or the value of the
      ///    <see cref="P:System.ColoredConsole.WindowHeight"/> property is less than or equal to 0.-or-The value of the <see cref="P:System.ColoredConsole.WindowHeight"/> property plus the value of the
      ///    <see cref="P:System.ColoredConsole.WindowTop"/> property is greater than or equal to <see cref="F:System.Int16.MaxValue"/>.-or-The value of the
      ///    <see cref="P:System.ColoredConsole.WindowWidth"/> property or the value of the <see cref="P:System.ColoredConsole.WindowHeight"/> property is greater than the largest possible window width
      ///    or height for the current screen resolution and console font.
      /// </exception>
      /// <exception cref="T:System.IO.IOException">Error reading or writing information.</exception>
      public int WindowWidth
      {
         get
         {
            return ColoredConsole.WindowWidth;
         }
         set
         {
            ColoredConsole.WindowWidth = value;
         }
      }

      /// <summary>Gets or sets the row position of the cursor within the buffer area.</summary>
      /// <returns>The current position, in rows, of the cursor.</returns>
      /// <exception cref="T:System.ArgumentOutOfRangeException">The value in a set operation is less than zero.-or- The value in a set operation is greater than or equal to
      ///    <see cref="P:System.ColoredConsole.BufferHeight"/>.</exception>
      /// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action. </exception>
      /// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
      public int CursorTop
      {
         get
         {
            return ColoredConsole.CursorTop;
         }
         set
         {
            ColoredConsole.CursorTop = value;
         }
      }

      /// <summary>Gets or sets the foreground color of the console.</summary>
      /// <returns>A <see cref="T:System.ConsoleColor"/> that specifies the foreground color of the console; that is, the color of each character that is displayed. The default is gray.</returns>
      /// <exception cref="T:System.ArgumentException">The color specified in a set operation is not a valid member of <see cref="T:System.ConsoleColor"/>. </exception>
      /// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action. </exception>
      /// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
      public Color ForegroundColor
      {
         get
         {
            return ColoredConsole.ForegroundColor;
         }
         set
         {
            ColoredConsole.ForegroundColor = value;
         }
      }

      /// <summary>Gets or sets the background color of the console.</summary>
      /// <returns>A value that specifies the background color of the console; that is, the color that appears behind each character. The default is black.</returns>
      /// <exception cref="T:System.ArgumentException">The color specified in a set operation is not a valid member of <see cref="T:System.ConsoleColor"/>. </exception>
      /// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action. </exception>
      /// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
      public Color BackgroundColor
      {
         get
         {
            return ColoredConsole.BackgroundColor;
         }
         set
         {
            ColoredConsole.BackgroundColor = value;
         }
      }

      /// <summary>Writes the current line terminator to the standard output stream.</summary>
      /// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
      public void WriteLine()
      {
         ColoredConsole.WriteLine();
      }

      /// <summary>Sets the foreground and background console colors to their defaults.</summary>
      /// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action. </exception>
      /// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
      public void ResetColor()
      {
         ColoredConsole.ResetColor();
      }

      /// <summary>Writes the specified string value, followed by the current line terminator, to the standard output stream.</summary>
      /// <param name="value">The value to write. </param>
      /// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
      /// <filterpriority>1</filterpriority>
      public void WriteLine(string value)
      {
         ColoredConsole.WriteLine(value);
      }

      /// <summary>Sets the position of the cursor.</summary>
      /// <param name="left">The column position of the cursor. Columns are numbered from left to right starting at 0. </param>
      /// <param name="top">The row position of the cursor. Rows are numbered from top to bottom starting at 0. </param>
      /// <exception cref="T:System.ArgumentOutOfRangeException">
      ///    <paramref name="left"/> or <paramref name="top"/> is less than zero.-or- <paramref name="left"/> is greater than or equal to
      ///    <see cref="P:System.ColoredConsole.BufferWidth"/>.-or- <paramref name="top"/> is greater than or equal to <see cref="P:System.ColoredConsole.BufferHeight"/>.
      /// </exception>
      /// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action. </exception>
      /// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
      public void SetCursorPosition(int left, int top)
      {
         ColoredConsole.SetCursorPosition(left, top);
      }

      /// <summary>Writes the specified string value to the standard output stream.</summary>
      /// <param name="value">The value to write. </param>
      /// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
      public void Write(string value)
      {
         ColoredConsole.Write(value);
      }

      #endregion

      #region Public Properties

      /// <summary>Gets or sets the height of the cursor within a character cell.</summary>
      /// <returns>The size of the cursor expressed as a percentage of the height of a character cell. The property value ranges from 1 to 100.</returns>
      /// <exception cref="T:System.ArgumentOutOfRangeException">The value specified in a set operation is less than 1 or greater than 100. </exception>
      /// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action. </exception>
      /// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
      public static int CursorSize
      {
         get
         {
            return ColoredConsole.CursorSize;
         }
         set
         {
            ColoredConsole.CursorSize = value;
         }
      }

      public static IConsole Instance => instance ?? (instance = new ConsoleProxy());

      #endregion
   }
}