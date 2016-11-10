// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleMenuInputHandler.cs" company="ConsoLovers">
//   Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Menu
{
   using System;
   using System.Collections.Generic;
   using System.Text;
   using System.Timers;

   using ConsoLovers.ConsoleToolkit.Contracts;
   using ConsoLovers.ConsoleToolkit.InputHandler;

   internal class ConsoleMenuInputHandler
   {
      #region Constants and Fields

      private readonly IConsole console;

      private readonly Queue<ConsoleKeyInfo> pressedKeys = new Queue<ConsoleKeyInfo>();

      private bool stopped;

      private Timer timer;

      private ConsoleInputHandler handler;

      #endregion

      #region Constructors and Destructors

      internal ConsoleMenuInputHandler(IConsole console)
      {
         if (console == null)
            throw new ArgumentNullException(nameof(console));

         this.console = console;

         handler = new ConsoleInputHandler();
      }

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

         handler.KeyDown += OnKeyDown;
         handler.Start();
         handler.Wait();
      }


      public event EventHandler<MouseEventArgs> MouseMoved
      {
         add
         {
            handler.MouseMoved += value;
         }
         remove
         {
            handler.MouseMoved -= value;
         }
      }


      public event EventHandler<MouseEventArgs> MouseDoubleClicked
      {
         add
         {
            handler.MouseDoubleClicked += value;
         }
         remove
         {
            handler.MouseDoubleClicked -= value;
         }
      }

      public event EventHandler<MouseEventArgs> MouseClicked
      {
         add
         {
            handler.MouseClicked += value;
         }
         remove
         {
            handler.MouseClicked -= value;
         }
      }


      private void OnKeyDown(object sender, KeyEventArgs e)
      {
         timer.Stop();
         var pressedKey = new ConsoleKeyInfo(e.KeyChar, e.Key, false, false, false);

         if (char.IsLetterOrDigit(e.KeyChar))
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

      private void OnMouseMoved(object sender, MouseEventArgs e)
      {
         Console.Beep();
      }

      public void Stop()
      {
         stopped = true;
         handler.Stop();
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