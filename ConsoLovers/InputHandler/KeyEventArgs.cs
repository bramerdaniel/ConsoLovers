// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KeyEventArgs.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.InputHandler
{
   using System;

   public class KeyEventArgs : EventArgs
   {
      #region Public Properties

      public ConsoleKey Key { get; set; }

      public char KeyChar { get; set; }

      public int VirtualKeyCode { get; set; }

      public ControlKeyState ControlKeys { get; set; }

      #endregion
   }

   [Flags]
   public enum ControlKeyState
   {
      RightAltPressed = 0x0001,
      LeftAltPressed = 0x0002,
      RightCtrlPressed = 0x0004,
      LeftCtrlPressed = 0x0008,
      ShiftPressed = 0x0010,
      NumLockOn = 0x0020,
      ScrollLockOn = 0x0040,
      CapsLockOn = 0x0080,
      EnhancedKey = 0x0100
   }
}