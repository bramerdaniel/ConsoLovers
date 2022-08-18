﻿// --------------------------------------------------------------------------------------------------------------------
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
         thread = new Thread(
            () =>
            {
               while (true)
               {
                  uint numRead = 0;
                  INPUT_RECORD[] record = new INPUT_RECORD[1];
                  record[0] = new INPUT_RECORD();
                  ReadConsoleInput(handleIn, record, 1, ref numRead);
                  if (isRunning)
                     switch (record[0].EventType)
                     {
                        case INPUT_RECORD.MOUSE_EVENT:
                           var mouseEvent = record[0].MouseEvent;
                           if (mouseEvent.dwButtonState == dwButtonStates.FROM_LEFT_1ST_BUTTON_PRESSED)
                           {
                              MouseClicked?.Invoke(this, CreateEventArgs(mouseEvent));
                           }
                           if (mouseEvent.dwEventFlags == dwEventFlags.DOUBLE_CLICK)
                           {
                              MouseDoubleClicked?.Invoke(this, CreateEventArgs(mouseEvent));
                           }
                           else if (mouseEvent.dwEventFlags == dwEventFlags.MOUSE_MOVED)
                           {
                              MouseMoved?.Invoke(this, CreateEventArgs(mouseEvent));
                           }
                           else if (mouseEvent.dwEventFlags == dwEventFlags.MOUSE_HWHEELED || mouseEvent.dwEventFlags == dwEventFlags.MOUSE_WHEELED)
                           {
                              MouseWheelChanged?.Invoke(this, CreateEventArgs(mouseEvent));
                           }

                           break;
                        case INPUT_RECORD.KEY_EVENT:
                           var keyEvent = record[0].KeyEvent;

                           if (keyEvent.bKeyDown)
                              KeyDown?.Invoke(this, CreateEventArgs(keyEvent));

                           break;
                        case INPUT_RECORD.WINDOW_BUFFER_SIZE_EVENT:
                           WindowBufferSizeEvent?.Invoke(record[0].WindowBufferSizeEvent);
                           break;
                     }
                  else
                  {
                     uint numWritten = 0;
                     WriteConsoleInput(handleIn, record, 1, ref numWritten);
                     return;
                  }
               }
            });

         thread.Start();
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