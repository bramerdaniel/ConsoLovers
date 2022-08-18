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
         isRunning = true;

         thread = new Thread(
            () =>
            {
               while (true)
               {
                  if (isRunning)
                  {
                     var keyInfo = Console.ReadKey();
                     KeyDown?.Invoke(this, new KeyEventArgs { Key = keyInfo.Key });
                  }
                  else
                  {
                     return;
                  }
               }
            });

         thread.Start();
      }

      public void Stop()
      {
         isRunning = false;
      }

      public void Wait()
      {
         thread.Join();
      }

      #endregion
   }
}