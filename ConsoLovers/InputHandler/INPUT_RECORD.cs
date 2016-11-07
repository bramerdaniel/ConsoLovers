// --------------------------------------------------------------------------------------------------------------------
// <copyright file="INPUT_RECORD.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.InputHandler
{
   using System.Runtime.InteropServices;

   [StructLayout(LayoutKind.Explicit)]
   public struct INPUT_RECORD
   {
      public const ushort KEY_EVENT = 0x0001, MOUSE_EVENT = 0x0002, WINDOW_BUFFER_SIZE_EVENT = 0x0004; //more

      [FieldOffset(0)]
      public ushort EventType;

      [FieldOffset(4)]
      public KEY_EVENT_RECORD KeyEvent;

      [FieldOffset(4)]
      public MOUSE_EVENT_RECORD MouseEvent;

      [FieldOffset(4)]
      public WINDOW_BUFFER_SIZE_RECORD WindowBufferSizeEvent;

      /*
            and:
             MENU_EVENT_RECORD MenuEvent;
             FOCUS_EVENT_RECORD FocusEvent;
             */
   }
}