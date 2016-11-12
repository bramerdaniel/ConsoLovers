//jmedved.com

using Microsoft.Win32.SafeHandles;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace ConsoleMouseSample
{
   using System.Drawing;
   using System.Reflection;
   using System.Threading;

   using ConsoLovers.ConsoleToolkit.Console;
   using ConsoLovers.ConsoleToolkit.InputHandler;

   class App
   {
      private static void ColorSimulation()
      {
         foreach (PropertyInfo property in typeof(Color).GetProperties(BindingFlags.Static | BindingFlags.Public))
            if (property.PropertyType == typeof(Color))
            {
               var value = (Color)property.GetValue(null);
               Write(property.Name, value);
               Thread.Sleep(1000);
            }

      }

      private static void Write(string text, Color value)
      {
         new ColorMapper().Write(text, value);
      }

      static void Main(string[] args)
      {
         ColorSimulation();

         var listener = new ConsoleInputHandler();
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

   public sealed class ColorMapper
   {
      [StructLayout(LayoutKind.Sequential)]
      private struct COORD
      {
         internal short X;
         internal short Y;
      }

      [StructLayout(LayoutKind.Sequential)]
      private struct SMALL_RECT
      {
         internal short Left;
         internal short Top;
         internal short Right;
         internal short Bottom;
      }

      [StructLayout(LayoutKind.Sequential)]
      private struct COLORREF
      {
         private uint ColorDWORD;

         internal COLORREF(Color color)
         {
            ColorDWORD = (uint)color.R + (((uint)color.G) << 8) + (((uint)color.B) << 16);
         }

         internal COLORREF(uint r, uint g, uint b)
         {
            ColorDWORD = r + (g << 8) + (b << 16);
         }
      }

      [StructLayout(LayoutKind.Sequential)]
      private struct CONSOLE_SCREEN_BUFFER_INFO_EX
      {
         internal int cbSize;
         internal COORD dwSize;
         internal COORD dwCursorPosition;
         internal ushort wAttributes;
         internal SMALL_RECT srWindow;
         internal COORD dwMaximumWindowSize;
         internal ushort wPopupAttributes;
         internal bool bFullscreenSupported;
         internal COLORREF black;
         internal COLORREF darkBlue;
         internal COLORREF darkGreen;
         internal COLORREF darkCyan;
         internal COLORREF darkRed;
         internal COLORREF darkMagenta;
         internal COLORREF darkYellow;
         internal COLORREF gray;
         internal COLORREF darkGray;
         internal COLORREF blue;
         internal COLORREF green;
         internal COLORREF cyan;
         internal COLORREF red;
         internal COLORREF magenta;
         internal COLORREF yellow;
         internal COLORREF white;
      }

      const int STD_OUTPUT_HANDLE = -11;                                       // per WinBase.h
      private static readonly IntPtr INVALID_HANDLE_VALUE = new IntPtr(-1);    // per WinBase.h

      [DllImport("kernel32.dll", SetLastError = true)]
      private static extern IntPtr GetStdHandle(int nStdHandle);

      [DllImport("kernel32.dll", SetLastError = true)]
      private static extern bool GetConsoleScreenBufferInfoEx(IntPtr hConsoleOutput, ref CONSOLE_SCREEN_BUFFER_INFO_EX csbe);

      [DllImport("kernel32.dll", SetLastError = true)]
      private static extern bool SetConsoleScreenBufferInfoEx(IntPtr hConsoleOutput, ref CONSOLE_SCREEN_BUFFER_INFO_EX csbe);

      /// <summary>
      /// Maps a System.Drawing.Color to a System.ConsoleColor.
      /// </summary>
      /// <param name="oldColor">The color to be replaced.</param>
      /// <param name="newColor">The color to be mapped.</param>
      public void MapColor(ConsoleColor oldColor, Color newColor)
      {
         // NOTE: The default console colors used are gray (foreground) and black (background).
         MapColor(oldColor, newColor.R, newColor.G, newColor.B);
      }

      private void MapColor(ConsoleColor color, uint r, uint g, uint b)
      {
         CONSOLE_SCREEN_BUFFER_INFO_EX csbe = new CONSOLE_SCREEN_BUFFER_INFO_EX();
         csbe.cbSize = (int)Marshal.SizeOf(csbe);                    // 96 = 0x60

         IntPtr hConsoleOutput = GetStdHandle(STD_OUTPUT_HANDLE);    // 7
         if (hConsoleOutput == INVALID_HANDLE_VALUE)
         {
            throw new ColorMappingException(Marshal.GetLastWin32Error());
         }

         bool brc = GetConsoleScreenBufferInfoEx(hConsoleOutput, ref csbe);
         if (!brc)
         {
            throw new ColorMappingException(Marshal.GetLastWin32Error());
         }

         switch (color)
         {
            case ConsoleColor.Black:
               csbe.black = new COLORREF(r, g, b);
               break;
            case ConsoleColor.DarkBlue:
               csbe.darkBlue = new COLORREF(r, g, b);
               break;
            case ConsoleColor.DarkGreen:
               csbe.darkGreen = new COLORREF(r, g, b);
               break;
            case ConsoleColor.DarkCyan:
               csbe.darkCyan = new COLORREF(r, g, b);
               break;
            case ConsoleColor.DarkRed:
               csbe.darkRed = new COLORREF(r, g, b);
               break;
            case ConsoleColor.DarkMagenta:
               csbe.darkMagenta = new COLORREF(r, g, b);
               break;
            case ConsoleColor.DarkYellow:
               csbe.darkYellow = new COLORREF(r, g, b);
               break;
            case ConsoleColor.Gray:
               csbe.gray = new COLORREF(r, g, b);
               break;
            case ConsoleColor.DarkGray:
               csbe.darkGray = new COLORREF(r, g, b);
               break;
            case ConsoleColor.Blue:
               csbe.blue = new COLORREF(r, g, b);
               break;
            case ConsoleColor.Green:
               csbe.green = new COLORREF(r, g, b);
               break;
            case ConsoleColor.Cyan:
               csbe.cyan = new COLORREF(r, g, b);
               break;
            case ConsoleColor.Red:
               csbe.red = new COLORREF(r, g, b);
               break;
            case ConsoleColor.Magenta:
               csbe.magenta = new COLORREF(r, g, b);
               break;
            case ConsoleColor.Yellow:
               csbe.yellow = new COLORREF(r, g, b);
               break;
            case ConsoleColor.White:
               csbe.white = new COLORREF(r, g, b);
               break;
         }

         csbe.srWindow.Bottom++;
         csbe.srWindow.Right++;

         brc = SetConsoleScreenBufferInfoEx(hConsoleOutput, ref csbe);
         if (!brc)
         {
            throw new ColorMappingException(Marshal.GetLastWin32Error());
         }
      }

      public void Write(string text, Color value)
      {
         MapColor(ConsoleColor.Black, value);
         Console.ForegroundColor = ConsoleColor.Cyan;
         Console.WriteLine(text);
      }
   }
}