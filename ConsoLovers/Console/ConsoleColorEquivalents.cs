// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleColorEquivalents.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Console
{
   using System;
   using System.Drawing;

   public static class ConsoleColorEquivalents
   {
      #region Public Properties

      /// <summary>Gets the <see cref="Color"/> that is used for the s<see cref="ConsoleColor.Black"/> when not channged.</summary>
      public static Color Black { get; } = Color.FromArgb(0, 0, 0);

      /// <summary>Gets the <see cref="Color"/> that is used for the s<see cref="ConsoleColor.Blue"/> when not channged.</summary>
      public static Color Blue { get; } = Color.FromArgb(0, 0, 255);

      /// <summary>Gets the <see cref="Color"/> that is used for the s<see cref="ConsoleColor.Cyan"/> when not channged.</summary>
      public static Color Cyan { get; } = Color.FromArgb(0, 255, 255);

      /// <summary>Gets the <see cref="Color"/> that is used for the s<see cref="ConsoleColor.DarkBlue"/> when not channged.</summary>
      public static Color DarkBlue { get; } = Color.FromArgb(0, 0, 128);

      /// <summary>Gets the <see cref="Color"/> that is used for the s<see cref="ConsoleColor.DarkCyan"/> when not channged.</summary>
      public static Color DarkCyan { get; } = Color.FromArgb(0, 128, 128);

      /// <summary>Gets the <see cref="Color"/> that is used for the s<see cref="ConsoleColor.DarkGray"/> when not channged.</summary>
      public static Color DarkGray { get; } = Color.FromArgb(128, 128, 128);

      /// <summary>Gets the <see cref="Color"/> that is used for the s<see cref="ConsoleColor.DarkGreen"/> when not channged.</summary>
      public static Color DarkGreen { get; } = Color.FromArgb(0, 128, 0);

      /// <summary>Gets the <see cref="Color"/> that is used for the s<see cref="ConsoleColor.DarkMagenta"/> when not channged.</summary>
      public static Color DarkMagenta { get; } = Color.FromArgb(128, 0, 128);

      /// <summary>Gets the <see cref="Color"/> that is used for the s<see cref="ConsoleColor.DarkRed"/> when not channged.</summary>
      public static Color DarkRed { get; } = Color.FromArgb(128, 0, 0);

      /// <summary>Gets the <see cref="Color"/> that is used for the s<see cref="ConsoleColor.DarkYellow"/> when not channged.</summary>
      public static Color DarkYellow { get; } = Color.FromArgb(128, 128, 0);

      /// <summary>Gets the <see cref="Color"/> that is used for the s<see cref="ConsoleColor.Gray"/> when not channged.</summary>
      public static Color Gray { get; } = Color.FromArgb(192, 192, 192);

      /// <summary>Gets the <see cref="Color"/> that is used for the s<see cref="ConsoleColor.Green"/> when not channged.</summary>
      public static Color Green { get; } = Color.FromArgb(0, 255, 0);

      /// <summary>Gets the <see cref="Color"/> that is used for the s<see cref="ConsoleColor.Magenta"/> when not channged.</summary>
      public static Color Magenta { get; } = Color.FromArgb(255, 0, 255);

      /// <summary>Gets the <see cref="Color"/> that is used for the s<see cref="ConsoleColor.Red"/> when not channged.</summary>
      public static Color Red { get; } = Color.FromArgb(255, 0, 0);

      /// <summary>Gets the <see cref="Color"/> that is used for the s<see cref="ConsoleColor.White"/> when not channged.</summary>
      public static Color White { get; } = Color.FromArgb(255, 255, 255);

      /// <summary>Gets the <see cref="Color"/> that is used for the s<see cref="ConsoleColor.Yellow"/> when not channged.</summary>
      public static Color Yellow { get; } = Color.FromArgb(255, 255, 0);

      #endregion

      /// <summary>Gets the equivalet <see cref="Color"/> for the given <see cref="ConsoleColor"/>.</summary>
      /// <param name="consoleColor"><see cref="ConsoleColor"/> to get the equivalent <see cref="Color"/> for.</param>
      /// <returns>The <see cref="Color "/> equivalent for the given <see cref="ConsoleColor"/></returns>
      /// <exception cref="System.ArgumentOutOfRangeException">null</exception>
      public static Color GetEquivalet(ConsoleColor consoleColor)
      {
         switch (consoleColor)
         {
            case ConsoleColor.Black:
               return Black;
            case ConsoleColor.DarkBlue:
               return DarkBlue;
            case ConsoleColor.DarkGreen:
               return DarkGreen;
            case ConsoleColor.DarkCyan:
               return DarkCyan;
            case ConsoleColor.DarkRed:
               return DarkRed;
            case ConsoleColor.DarkMagenta:
               return DarkMagenta;
            case ConsoleColor.DarkYellow:
               return DarkYellow;
            case ConsoleColor.Gray:
               return Gray;
            case ConsoleColor.DarkGray:
               return DarkGray;
            case ConsoleColor.Blue:
               return Blue;
            case ConsoleColor.Green:
               return Green;
            case ConsoleColor.Cyan:
               return Cyan;
            case ConsoleColor.Red:
               return Red;
            case ConsoleColor.Magenta:
               return Magenta;
            case ConsoleColor.Yellow:
               return Yellow;
            case ConsoleColor.White:
               return White;
            default:
               throw new ArgumentOutOfRangeException(nameof(consoleColor), consoleColor, null);
         }

      }
   }
}