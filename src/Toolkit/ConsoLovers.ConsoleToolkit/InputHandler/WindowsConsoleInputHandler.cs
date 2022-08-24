// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WindowsConsoleInputHandler.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.InputHandler
{
   using System;
   using System.Runtime.InteropServices;
   using System.Threading;

   public sealed class WindowsConsoleInputHandler : IInputHandler
   {
      #region Constants and Fields

      private const uint STD_INPUT_HANDLE = unchecked((uint) -10);

      private bool isRunning;

      private Thread thread;

      #endregion

      #region Public Events

      /// <summary>Occurs when a keyboard key was pressed.</summary>
      public event EventHandler<KeyEventArgs> KeyDown;

      /// <summary>Occurs when a mouse button was clicked.</summary>
      public event EventHandler<MouseEventArgs> MouseClicked;

      /// <summary>Occurs when a mouse button was double clicked.</summary>
      public event EventHandler<MouseEventArgs> MouseDoubleClicked;

      /// <summary>Occurs when the mouse was moved.</summary>
      public event EventHandler<MouseEventArgs> MouseMoved;

      /// <summary>Occurs when the mouse wheel position changed.</summary>
      public event EventHandler<MouseEventArgs> MouseWheelChanged;

      #endregion

      #region IInputHandler Members

      /// <summary>Starts the input observation.</summary>
      public void Start()
      {
         if (isRunning)
            return;

         Prepare();
         isRunning = true;

         IntPtr handleIn = GetStdHandle(STD_INPUT_HANDLE);
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

      /// <summary>Stops the input observation.</summary>
      public void Stop() => isRunning = false;

      /// <summary>Joins the calling and the input handler thread.</summary>
      public void Wait() => thread.Join();

      #endregion

      #region Methods

      [DllImport("kernel32.dll")]
      private static extern bool GetConsoleMode(IntPtr hConsoleInput, ref uint lpMode);

      [DllImport("kernel32.dll")]
      private static extern IntPtr GetStdHandle(uint nStdHandle);

      [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
      private static extern bool ReadConsoleInput(IntPtr hConsoleInput, [Out] INPUT_RECORD[] lpBuffer, uint nLength, ref uint lpNumberOfEventsRead);

      [DllImport("kernel32.dll")]
      private static extern bool SetConsoleMode(IntPtr hConsoleInput, uint dwMode);

      [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
      private static extern bool WriteConsoleInput(IntPtr hConsoleInput, INPUT_RECORD[] lpBuffer, uint nLength, ref uint lpNumberOfEventsWritten);

      private KeyEventArgs CreateEventArgs(KEY_EVENT_RECORD keyEvent)
      {
         return new KeyEventArgs
         {
            Key = (ConsoleKey) keyEvent.wVirtualKeyCode,
            KeyChar = keyEvent.UnicodeChar,
            VirtualKeyCode = keyEvent.wVirtualKeyCode,
            ControlKeys = (ControlKeyState) keyEvent.dwControlKeyState
         };
      }

      private MouseEventArgs CreateEventArgs(MOUSE_EVENT_RECORD mouseEvent) =>
         new MouseEventArgs {ButtonState = (ButtonStates) mouseEvent.dwButtonState, WindowLeft = mouseEvent.dwMousePosition.X, WindowTop = mouseEvent.dwMousePosition.Y};

      private void Prepare()
      {
         var inHandle = GetStdHandle(STD_INPUT_HANDLE);

         uint mode = 0;
         GetConsoleMode(inHandle, ref mode);

         mode &= ~ConsoleModes.ENABLE_QUICK_EDIT_MODE; //disable
         mode |= ConsoleModes.ENABLE_WINDOW_INPUT; //enable (if you want)
         mode |= ConsoleModes.ENABLE_MOUSE_INPUT; //enable

         SetConsoleMode(inHandle, mode);
      }

      #endregion
   }
}