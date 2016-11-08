//jmedved.com

using Microsoft.Win32.SafeHandles;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ConsoleMouseSample
{
   using ConsoLovers.ConsoleToolkit.InputHandler;

   class App
   {
      static void Main(string[] args)
      {
         var listener = new ConsoleInputHandler();
         listener.Prepare();
         listener.MouseMoved += OnMouseMoved;
         listener.MouseDoubleClicked += OnMouseDoubleClicked;
         listener.MouseWheelChanged += OnMouseWheelChanged;
         listener.KeyEvent += OnKeyEvent;
         listener.KeyDown += OnKeyDown;
         listener.Start();
         listener.Wait();

         //var menu = new ConsoleMenu { Header = "Mouse demo", CircularSelection = false, Selector = "» ", Theme = ConsoleMenuThemes.Bahama };

         //menu.SelectionStrech = SelectionStrech.UnifiedLength;
         //menu.Add(new ConsoleMenuItem("Disabled without command"));
         //menu.Add(
         //   new ConsoleMenuItem(
         //      "Remove until 9 remain",
         //      x =>
         //      {
         //         while (menu.Count >= 10)
         //            menu.RemoveAt(menu.Count - 1);
         //      }));
         //menu.Add(new ConsoleMenuItem("Show Progress", m => {}));
         //menu.Add(new ConsoleMenuItem("Set user name", m => { }));
         //menu.Add(new ConsoleMenuItem("Connect to server", m => { }, () => false) { DisabledHint = "Set username first" });
         //menu.Add(new ConsoleMenuItem("Register crash event handler", m => { }));
         //menu.Add(new ConsoleMenuItem("Simulate Crash", m => { }));
         //menu.Add(new ConsoleMenuItem("Close menu", x => menu.Close()));
         //menu.Add(new ConsoleMenuItem("Exit", x => Environment.Exit(0)));
         //menu.Show();


         Console.ReadKey();

         //Console.WriteLine("This is a test");
         //Console.ReadLine();
         // ProcessMouseInputs();
      }

      private static void OnMouseWheelChanged(object sender, MouseEventArgs e)
      {
         Console.Beep();
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
            var listener = sender as ConsoleInputHandler;
            listener.Stop();
         }

         Console.SetCursorPosition(0, 0);
         Console.Write($"X: {e.WindowLeft}, Y: {e.WindowTop}".PadLeft(Console.WindowWidth - 1));

      }

      private static void OnKeyEvent(KEY_EVENT_RECORD KeyEvent)
      {
         Console.SetCursorPosition(0, 6);
         Console.WriteLine("Key event  ");
         Console.WriteLine($"    bKeyDown  .......:  {KeyEvent.bKeyDown,5}  ");
         Console.WriteLine($"    wRepeatCount ....:   {KeyEvent.wRepeatCount,4:0}  ");
         Console.WriteLine($"    wVirtualKeyCode .:   {KeyEvent.wVirtualKeyCode,4:0}  ");
         Console.WriteLine($"    uChar ...........:      {KeyEvent.UnicodeChar}  ");
         Console.WriteLine($"    dwControlKeyState: 0x{KeyEvent.dwControlKeyState:X4}  ");
         var consoleKey = (ConsoleKey)KeyEvent.wVirtualKeyCode;
         Console.WriteLine($"    ConsoleKey: .....:   {consoleKey.ToString().PadRight(10)}  ");
      }

      private static void OnKeyDown(object sender, KeyEventArgs e)
      {
         Console.SetCursorPosition(0, 0);
         //Console.WriteLine($"    bKeyDown  .......:  {KeyEvent.bKeyDown,5}  ");
         //Console.WriteLine($"    wRepeatCount ....:   {KeyEvent.wRepeatCount,4:0}  ");
         //Console.WriteLine($"    wVirtualKeyCode .:   {KeyEvent.wVirtualKeyCode,4:0}  ");
         //Console.WriteLine($"    dwControlKeyState: 0x{KeyEvent.dwControlKeyState:X4}  ");
         Console.WriteLine();
         Console.WriteLine($" ConsoleKey: .....:     {e.Key.ToString().PadRight(10)}  ");
         Console.WriteLine($" KeyChar .........:     {e.KeyChar}  ");
         Console.WriteLine($" Modifiers .........:   {e.ControlKeys.ToString().PadRight(Console.WindowWidth - 10)}  ");

      }

      private static void OnMouseEvent(MOUSE_EVENT_RECORD MouseEvent)
      {
         Console.SetCursorPosition(0, 0);
         Console.WriteLine("Mouse event");
         Console.WriteLine($"    X ...............:   {MouseEvent.dwMousePosition.X,4:0}  ");
         Console.WriteLine($"    Y ...............:   {MouseEvent.dwMousePosition.Y,4:0}  ");
         Console.WriteLine($"    dwButtonState ...: 0x{MouseEvent.dwButtonState:X4}  ");
         Console.WriteLine($"    dwControlKeyState: 0x{MouseEvent.dwControlKeyState:X4}  ");
         Console.WriteLine($"    dwEventFlags ....: 0x{MouseEvent.dwEventFlags:X4}  ");

      }



   }
}