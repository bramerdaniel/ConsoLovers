// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Class1.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.InputHandler
{
   using System.Runtime.InteropServices;

   [StructLayout(LayoutKind.Sequential)]
   public struct MOUSE_EVENT_RECORD
   {
      public COORD dwMousePosition;

      public uint dwButtonState;

      public uint dwControlKeyState;

      public uint dwEventFlags;
   }
}