// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleInputHandler.cs" company="KUKA Roboter GmbH">
//   Copyright (c) KUKA Roboter GmbH 2006 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Timers;

   public class ConsoleInputHandler
   {
      private readonly IConsole console;

      public ConsoleInputHandler(IConsole console)
      {
         if (console == null)
            throw new ArgumentNullException(nameof(console));

         this.console = console;
      }

      #region Constants and Fields

      private readonly Queue<ConsoleKeyInfo> pressedKeys = new Queue<ConsoleKeyInfo>();

      private bool stopped;

      private Timer timer;

      #endregion

      #region Public Events

      public event EventHandler<ConsoleInputEventArgs> InputChanged;

      #endregion

      #region Public Methods and Operators

      public void Start()
      {
         timer = new Timer(1000);
         timer.AutoReset = false;
         timer.Elapsed += OnElapsed;

         while (!stopped)
         {
            var pressedKey = console.ReadKey();
            timer.Stop();

            if (char.IsLetterOrDigit(pressedKey.KeyChar))
            {
               pressedKeys.Enqueue(pressedKey);

               StringBuilder builder = new StringBuilder();
               foreach (var key in pressedKeys)
                  builder.Append(key.KeyChar);

               InputChanged?.Invoke(this, new ConsoleInputEventArgs(pressedKey, builder.ToString()));
            }
            else
            {
               InputChanged?.Invoke(this, new ConsoleInputEventArgs(pressedKey));
               pressedKeys.Clear();
            }

            timer.Start();
         }
      }

      public void Stop()
      {
         stopped = true;
      }

      #endregion

      #region Methods

      private void OnElapsed(object sender, ElapsedEventArgs e)
      {
         pressedKeys.Clear();
      }

      #endregion
   }
}