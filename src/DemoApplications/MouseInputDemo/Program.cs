// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MouseInputDemo
{
   using System;
   using System.Drawing;
   using System.Runtime.InteropServices;

   using ConsoLovers.ConsoleToolkit;
   using ConsoLovers.ConsoleToolkit.Console;
   using ConsoLovers.ConsoleToolkit.InputHandler;

   class App : ConsoleApplication
   {
      #region Methods

      static void Main(string[] args)
      {
         ConsoleApplicationManager.RunThis(args);
         Console.ReadKey();
      }

      private void OnKeyDown(object sender, KeyEventArgs e)
      {
         Console.SetCursorPosition(0, 0);
         Console.WriteLine();
         Console.WriteLine($" ConsoleKey: .....:     {e.Key.ToString().PadRight(10)}  ");
         Console.WriteLine($" KeyChar .........:     {e.KeyChar}  ");
         Console.WriteLine($" Modifiers .........:   {e.ControlKeys.ToString().PadRight(Console.WindowWidth - 10)}  ");
      }

      private static void OnMouseDoubleClicked(object sender, MouseEventArgs e)
      {
         if ((e.ButtonState & ButtonStates.Left) == ButtonStates.Left)
         {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(e.WindowLeft, e.WindowTop);
            Console.Write("#");
            Console.ResetColor();
         }
      }

      private void OnMouseEvent(MOUSE_EVENT_RECORD MouseEvent)
      {
         Console.SetCursorPosition(0, 0);
         Console.WriteLine("Mouse event");
         Console.WriteLine($"    X ...............:   {MouseEvent.dwMousePosition.X,4:0}  ");
         Console.WriteLine($"    Y ...............:   {MouseEvent.dwMousePosition.Y,4:0}  ");
         Console.WriteLine($"    dwButtonState ...: 0x{MouseEvent.dwButtonState:X4}  ");
         Console.WriteLine($"    dwControlKeyState: 0x{MouseEvent.dwControlKeyState:X4}  ");
         Console.WriteLine($"    dwEventFlags ....: 0x{MouseEvent.dwEventFlags:X4}  ");
      }

      private void OnMouseMoved(object sender, MouseEventArgs e)
      {
         if ((e.ButtonState & ButtonStates.Left) == ButtonStates.Left)
         {
            Console.SetCursorPosition(e.WindowLeft, e.WindowTop);
            Console.Write("+");
         }

         if ((e.ButtonState & ButtonStates.Right) == ButtonStates.Right)
         {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.SetCursorPosition(e.WindowLeft, e.WindowTop);
            Console.Write("*");
            Console.ResetColor();
         }

         if ((e.ButtonState & ButtonStates.Second) == ButtonStates.Second)
         {
            var listener = sender as ConsoleInputHandler;
            listener?.Stop();
         }

         Console.SetCursorPosition(0, 0);
         Console.Write($"X: {e.WindowLeft}, Y: {e.WindowTop}".PadLeft(Console.WindowWidth - 1));
      }

      private void OnMouseWheelChanged(object sender, MouseEventArgs e)
      {
         Console.Beep();
      }

      #endregion

      public override void Run()
      {
         var listener = new ConsoleInputHandler();
         listener.MouseMoved += OnMouseMoved;
         listener.MouseDoubleClicked += OnMouseDoubleClicked;
         listener.MouseWheelChanged += OnMouseWheelChanged;
         listener.KeyDown += OnKeyDown;
         listener.Start();
         listener.Wait();

      }
   }
   
}