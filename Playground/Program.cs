// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Playground
{
   using System;
   using System.Runtime.InteropServices;

   using Microsoft.Win32.SafeHandles;

   class Program
   {
      #region Constants and Fields

      private const UInt32 STD_OUTPUT_HANDLE = unchecked((UInt32)(-11));

      private static SafeFileHandle _safeFileHandle;

      #endregion

      #region Methods

      [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
      private static extern IntPtr GetStdHandle(UInt32 type);

      static void Main()
      {
      }

      [DllImport("kernel32.dll", SetLastError = true)]
      private static extern bool WriteConsoleOutput
      (SafeFileHandle hConsoleOutput, InputConsoleBox.CharInfo[] lpBuffer, InputConsoleBox.Coord dwBufferSize, InputConsoleBox.Coord dwBufferCoord,
         ref InputConsoleBox.SmallRect lpWriteRegion);

      #endregion
   }
}