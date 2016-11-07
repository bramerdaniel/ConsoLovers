namespace ConsoLovers.ConsoleToolkit.InputHandler
{
   using System;

   [Flags]
   public enum ButtonStates
   {
      Left = 0x0001,

      Right = 0x0002,

      Second = 0x0004,

      Third = 0x0008,

      Fourth = 0x00010
   }
}