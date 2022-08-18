// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MouseInputDemo
{
   using System;

   using ConsoLovers.ConsoleToolkit.InputHandler;

   class App 
   {
      #region Public Methods and Operators

      public static void Run()
      {
         var listener = new WindowsConsoleInputHandler();
         listener.MouseMoved += OnMouseMoved;
         listener.MouseDoubleClicked += OnMouseDoubleClicked;
         listener.MouseWheelChanged += OnMouseWheelChanged;
         listener.KeyDown += OnKeyDown;
         listener.Start();
         listener.Wait();
      }

      #endregion

      #region Methods

      static void Main(string[] args)
      {
         Run();
         Console.ReadKey();
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

      private static void OnKeyDown(object sender, KeyEventArgs e)
      {
         Console.SetCursorPosition(0, 0);
         Console.WriteLine();
         Console.WriteLine($" ConsoleKey: .....:     {e.Key.ToString().PadRight(10)}  ");
         Console.WriteLine($" KeyChar .........:     {e.KeyChar}  ");
         Console.WriteLine($" Modifiers .........:   {e.ControlKeys.ToString().PadRight(Console.WindowWidth - 10)}  ");
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

      private static void OnMouseMoved(object sender, MouseEventArgs e)
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
            var listener = sender as WindowsConsoleInputHandler;
            listener?.Stop();
         }

         Console.SetCursorPosition(0, 0);
         Console.Write($"X: {e.WindowLeft}, Y: {e.WindowTop}".PadLeft(Console.WindowWidth - 1));
      }

      private static void OnMouseWheelChanged(object sender, MouseEventArgs e)
      {
         Console.Beep();
      }

      #endregion
   }
}