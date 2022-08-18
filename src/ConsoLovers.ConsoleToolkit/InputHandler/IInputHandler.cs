// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IInputHandler.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.InputHandler
{
   using System;

   public interface IInputHandler
   {
      event EventHandler<MouseEventArgs> MouseDoubleClicked;

      event EventHandler<MouseEventArgs> MouseClicked;

      event EventHandler<MouseEventArgs> MouseMoved;

      event EventHandler<MouseEventArgs> MouseWheelChanged;

      event EventHandler<KeyEventArgs> KeyDown;

      event WindowsConsoleInputHandler.ConsoleWindowBufferSizeEvent WindowBufferSizeEvent;

      void Start();

      void Stop();

      void Wait();
   }
}