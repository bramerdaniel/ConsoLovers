// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleInputHandler.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.InputHandler
{
   using System;
   using System.Runtime.InteropServices;
   using System.Threading;

   public class WindowsConsoleInputHandler : IInputHandler
   {
      #region Constants and Fields

      public const uint STD_ERROR_HANDLE = unchecked((uint)-12);

      public const uint STD_INPUT_HANDLE = unchecked((uint)-10);

      public const uint STD_OUTPUT_HANDLE = unchecked((uint)-11);

      private bool isRunning;

      private Thread thread;

      #endregion

      #region Delegates

      public delegate void ConsoleKeyEvent(KEY_EVENT_RECORD r);
      
      public delegate void ConsoleWindowBufferSizeEvent(WINDOW_BUFFER_SIZE_RECORD r);

      #endregion

      #region Public Events

      public event EventHandler<MouseEventArgs> MouseDoubleClicked;

      public event EventHandler<MouseEventArgs> MouseClicked;
      
      public event EventHandler<MouseEventArgs> MouseMoved;

      public event EventHandler<MouseEventArgs> MouseWheelChanged;

      public event EventHandler<KeyEventArgs> KeyDown;

      public event ConsoleWindowBufferSizeEvent WindowBufferSizeEvent;

      #endregion

      #region Public Methods and Operators

      [DllImport("kernel32.dll")]
      public static extern bool GetConsoleMode(IntPtr hConsoleInput, ref uint lpMode);

      [DllImport("kernel32.dll")]
      public static extern IntPtr GetStdHandle(uint nStdHandle);

      [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
      public static extern bool ReadConsoleInput(IntPtr hConsoleInput, [Out] INPUT_RECORD[] lpBuffer, uint nLength, ref uint lpNumberOfEventsRead);

      [DllImport("kernel32.dll")]
      public static extern bool SetConsoleMode(IntPtr hConsoleInput, uint dwMode);

      [DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
      public static extern bool WriteConsoleInput(IntPtr hConsoleInput, INPUT_RECORD[] lpBuffer, uint nLength, ref uint lpNumberOfEventsWritten);

      protected void Prepare()
      {
         IntPtr inHandle = GetStdHandle(STD_INPUT_HANDLE);
         uint mode = 0;
         GetConsoleMode(inHandle, ref mode);
         mode &= ~ConsoleModes.ENABLE_QUICK_EDIT_MODE; //disable
         mode |= ConsoleModes.ENABLE_WINDOW_INPUT; //enable (if you want)
         mode |= ConsoleModes.ENABLE_MOUSE_INPUT; //enable
         SetConsoleMode(inHandle, mode);
      }

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

      private KeyEventArgs CreateEventArgs(KEY_EVENT_RECORD keyEvent)
      {
         return new KeyEventArgs
         {
            Key = (ConsoleKey)keyEvent.wVirtualKeyCode,
            KeyChar = keyEvent.UnicodeChar,
            VirtualKeyCode = keyEvent.wVirtualKeyCode,
            ControlKeys = (ControlKeyState)keyEvent.dwControlKeyState
      };
      }

      public void Stop() => isRunning = false;

      public void Wait() => thread.Join();

      #endregion

      #region Methods

      private MouseEventArgs CreateEventArgs(MOUSE_EVENT_RECORD mouseEvent)
      {
         return new MouseEventArgs { ButtonState = (ButtonStates)mouseEvent.dwButtonState, WindowLeft = mouseEvent.dwMousePosition.X, WindowTop = mouseEvent.dwMousePosition.Y };
      }

      #endregion
   }
}