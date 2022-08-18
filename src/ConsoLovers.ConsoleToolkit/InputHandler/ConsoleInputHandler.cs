// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleInputHandler.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.InputHandler
{
   using System;
   using System.Threading;

   public class ConsoleInputHandler : IInputHandler
   {
      private Thread thread;

      private bool isRunning;

      #region Public Events

      public event EventHandler<KeyEventArgs> KeyDown;

      public event EventHandler<MouseEventArgs> MouseClicked;

      public event EventHandler<MouseEventArgs> MouseDoubleClicked;

      public event EventHandler<MouseEventArgs> MouseMoved;

      public event EventHandler<MouseEventArgs> MouseWheelChanged;

      public event WindowsConsoleInputHandler.ConsoleWindowBufferSizeEvent WindowBufferSizeEvent;

      #endregion

      #region IInputHandler Members

      public void Start()
      {
         ////thread = new Thread(
         ////   () =>
         ////   {
         ////      while (true)
         ////      {
         ////         if (isRunning)

         ////            var keyEvent = record[0].KeyEvent;

         ////         if (keyEvent.bKeyDown)
         ////            KeyDown?.Invoke(this, CreateEventArgs(keyEvent));
         ////      }
         ////         else
         ////      {
         ////         uint numWritten = 0;
         ////         WriteConsoleInput(handleIn, record, 1, ref numWritten);
         ////         return;
         ////      }
         ////   }
         ////   });

         ////thread.Start();
      }

   public void Stop()
   {
   }

   public void Wait()
   {
   }

   #endregion
}
}