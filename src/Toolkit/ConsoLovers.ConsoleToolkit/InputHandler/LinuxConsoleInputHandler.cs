// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LinuxConsoleInputHandler.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.InputHandler
{
   using System;
   using System.Threading;

   public sealed class LinuxConsoleInputHandler : IInputHandler
   {
      #region Constants and Fields

      private bool isRunning;

      private Thread thread;

      #endregion

      #region Public Events

      /// <summary>Occurs when a keyboard key was pressed.</summary>
      public event EventHandler<KeyEventArgs> KeyDown;

      /// <summary>Occurs when a mouse button was clicked. Mouse event is not supported!</summary>
      public event EventHandler<MouseEventArgs> MouseClicked;

      /// <summary>Occurs when a mouse button was double clicked. Mouse event is not supported!</summary>
      public event EventHandler<MouseEventArgs> MouseDoubleClicked;

      /// <summary>Occurs when the mouse was moved. Mouse event is not supported!</summary>
      public event EventHandler<MouseEventArgs> MouseMoved;

      /// <summary>Occurs when the mouse wheel position changed. Mouse event is not supported!</summary>
      public event EventHandler<MouseEventArgs> MouseWheelChanged;

      #endregion

      #region IInputHandler Members

      /// <summary>Starts the input observation.</summary>
      public void Start()
      {
         isRunning = true;

         thread = new Thread(
            () =>
            {
               while (true)
               {
                  if (isRunning)
                  {
                     var keyInfo = Console.ReadKey();
                     KeyDown?.Invoke(this, new KeyEventArgs {Key = keyInfo.Key});
                  }
                  else
                  {
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
   }
}