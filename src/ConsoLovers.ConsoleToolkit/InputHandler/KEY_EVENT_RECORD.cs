// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KEY_EVENT_RECORD.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.InputHandler
{
   using System.Runtime.InteropServices;

   [StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
   public struct KEY_EVENT_RECORD
   {
      [FieldOffset(0)]
      public bool bKeyDown;

      [FieldOffset(4)]
      public ushort wRepeatCount;

      [FieldOffset(6)]
      public ushort wVirtualKeyCode;

      [FieldOffset(8)]
      public ushort wVirtualScanCode;

      [FieldOffset(10)]
      public char UnicodeChar;

      [FieldOffset(10)]
      public byte AsciiChar;

      public const int CAPSLOCK_ON = 0x0080,
         ENHANCED_KEY = 0x0100,
         LEFT_ALT_PRESSED = 0x0002,
         LEFT_CTRL_PRESSED = 0x0008,
         NUMLOCK_ON = 0x0020,
         RIGHT_ALT_PRESSED = 0x0001,
         RIGHT_CTRL_PRESSED = 0x0004,
         SCROLLLOCK_ON = 0x0040,
         SHIFT_PRESSED = 0x0010;

      [FieldOffset(12)]
      public uint dwControlKeyState;
   }
}