// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Win32Console.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Console
{
   using System;
   using System.Runtime.InteropServices;
   using System.Runtime.Versioning;
   using System.Text;

   // ReSharper disable InconsistentNaming

   public static class Win32Console
   {
      #region Constants and Fields

      internal const string KERNEL32 = "kernel32.dll";

      internal const int STD_ERROR_HANDLE = -12;

      internal const int STD_INPUT_HANDLE = -10;

      internal const int STD_OUTPUT_HANDLE = -11;

      #endregion

      #region Enums

      [Flags, Serializable]
      internal enum Color : short
      {
         Black = 0,

         ForegroundBlue = 0x1,

         ForegroundGreen = 0x2,

         ForegroundRed = 0x4,

         ForegroundYellow = 0x6,

         ForegroundIntensity = 0x8,

         BackgroundBlue = 0x10,

         BackgroundGreen = 0x20,

         BackgroundRed = 0x40,

         BackgroundYellow = 0x60,

         BackgroundIntensity = 0x80,

         ForegroundMask = 0xf,

         BackgroundMask = 0xf0,

         ColorMask = 0xff
      }

      #endregion

      #region Methods

      [DllImport(KERNEL32, CharSet = CharSet.Auto, SetLastError = true)]
      [ResourceExposure(ResourceScope.Process)]
      internal static extern bool FillConsoleOutputCharacter(IntPtr hConsoleOutput, char character, int nLength, COORD dwWriteCoord, out int pNumCharsWritten);

      [DllImport(KERNEL32, SetLastError = true)]
      [ResourceExposure(ResourceScope.None)]
      internal static extern bool GetConsoleCursorInfo(IntPtr hConsoleOutput, out CONSOLE_CURSOR_INFO cci);

      [DllImport(KERNEL32, SetLastError = true)]
      [ResourceExposure(ResourceScope.None)]
      internal static extern bool GetConsoleScreenBufferInfo(IntPtr hConsoleOutput, out CONSOLE_SCREEN_BUFFER_INFO lpConsoleScreenBufferInfo);

      [DllImport(KERNEL32, CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = true)]
      [ResourceExposure(ResourceScope.None)]
      internal static extern int GetConsoleTitle(StringBuilder sb, int capacity);

      [DllImport(KERNEL32, SetLastError = true)]
      [ResourceExposure(ResourceScope.Process)]
      internal static extern IntPtr GetStdHandle(int nStdHandle); // param is NOT a handle, but it returns one!

      [DllImport(KERNEL32, SetLastError = true)]
      [ResourceExposure(ResourceScope.Process)]
      internal static extern bool SetConsoleCursorInfo(IntPtr hConsoleOutput, ref CONSOLE_CURSOR_INFO cci);

      [DllImport(KERNEL32, CharSet = CharSet.Auto, SetLastError = true, BestFitMapping = true)]
      [ResourceExposure(ResourceScope.Process)]
      internal static extern bool SetConsoleTitle(string title);

      [DllImport(KERNEL32, SetLastError = true)]
      [ResourceExposure(ResourceScope.Process)]
      internal static extern bool WriteConsoleOutput(IntPtr hConsoleOutput, CHAR_INFO[] buffer, COORD bufferSize, COORD bufferCoord, ref SMALL_RECT writeRegion);

      #endregion

      [StructLayout(LayoutKind.Sequential)]
      internal struct CHAR_INFO
      {
         public ushort charData; // Union between WCHAR and ASCII char

         public short attributes;

         /// <summary>Returns a <see cref="System.String"/> that represents this instance.</summary>
         /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
         public override string ToString()
         {
            return $"'{(char)charData} ({attributes})";
         }
      }

      [StructLayout(LayoutKind.Sequential)]
      internal struct CONSOLE_CURSOR_INFO
      {
         internal int dwSize;

         internal bool bVisible;
      }

      [StructLayout(LayoutKind.Sequential)]
      internal struct CONSOLE_SCREEN_BUFFER_INFO
      {
         internal COORD dwSize;

         internal COORD dwCursorPosition;

         internal short wAttributes;

         internal SMALL_RECT srWindow;

         internal COORD dwMaximumWindowSize;
      }

      [StructLayout(LayoutKind.Sequential)]
      internal struct COORD
      {
         internal short X;

         internal short Y;
      }

      [StructLayout(LayoutKind.Sequential)]
      internal struct SMALL_RECT
      {
         internal short Left;

         internal short Top;

         internal short Right;

         internal short Bottom;

         public override string ToString()
         {
            return $"{nameof(Left)}: {Left}, {nameof(Top)}: {Top}, {nameof(Right)}: {Right}, {nameof(Bottom)}: {Bottom}";
         }
      }
   }

   // ReSharper restore InconsistentNaming
}