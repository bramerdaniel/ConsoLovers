// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NativeMethods.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace ConsoLovers.ConsoleToolkit.PInvoke
{
   using System;
   using System.Runtime.InteropServices;

   public class NativeMethods
   {
      public const int MF_ENABLED = 0x00000000;     //enabled button status
      public const int MF_GRAYED = 0x1;             //disabled button status (enabled = false)
      public const int MF_DISABLED = 0x00000002;    //disabled button status
      public const int MF_BYCOMMAND = 0x00000000;
      public const int SC_CLOSE = 0xF060;
      public const int SC_MINIMIZE = 0xF020;
      public const int SC_MAXIMIZE = 0xF030;

      [DllImport("user32.dll")]
      public static extern int DeleteMenu(IntPtr hMenu, int nPosition, int wFlags);

      [DllImport("user32.dll")]
      public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);

      [DllImport("kernel32.dll", ExactSpelling = true)]
      public static extern IntPtr GetConsoleWindow();

      [DllImport("user32.dll")]
      public extern static int SetWindowLong(IntPtr hwnd, int index, int value);

      [DllImport("user32.dll")]
      public extern static int GetWindowLong(IntPtr hwnd, int index);



   }
}