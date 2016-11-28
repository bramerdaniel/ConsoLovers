using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using Microsoft.Win32.SafeHandles;

public class InputConsoleBox
{
   #region Output
   #region Win32 interop
   private const UInt32 STD_OUTPUT_HANDLE = unchecked((UInt32)(-11));
   private const UInt32 STD_ERROR_HANDLE = unchecked((UInt32)(-12));

   [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
   private static extern IntPtr GetStdHandle(UInt32 type);
   [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
   private static extern SafeFileHandle CreateFile(
       string fileName,
       [MarshalAs(UnmanagedType.U4)] uint fileAccess,
       [MarshalAs(UnmanagedType.U4)] uint fileShare,
       IntPtr securityAttributes,
       [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
       [MarshalAs(UnmanagedType.U4)] int flags,
       IntPtr template);

   [DllImport("kernel32.dll", SetLastError = true)]
   private static extern bool WriteConsoleOutput(
       SafeFileHandle hConsoleOutput,
       CharInfo[] lpBuffer,
       Coord dwBufferSize,
       Coord dwBufferCoord,
       ref SmallRect lpWriteRegion);

   [StructLayout(LayoutKind.Sequential)]
   public struct Coord
   {
      public short X;
      public short Y;

      public Coord(short X, short Y)
      {
         this.X = X;
         this.Y = Y;
      }
   };

   [StructLayout(LayoutKind.Explicit)]
   public struct CharUnion
   {
      [FieldOffset(0)]
      public char UnicodeChar;
      [FieldOffset(0)]
      public byte AsciiChar;
   }

   [StructLayout(LayoutKind.Explicit)]
   public struct CharInfo
   {
      [FieldOffset(0)]
      public CharUnion Char;
      [FieldOffset(2)]
      public ushort Attributes;

      public CharInfo(char @char, ushort attributes)
      {
         this.Char = new CharUnion();
         Char.UnicodeChar = @char;
         Attributes = attributes;
      }
   }

   [StructLayout(LayoutKind.Sequential)]
   public struct SmallRect
   {
      public short Left;
      public short Top;
      public short Right;
      public short Bottom;
   }
   #endregion
   #region Colors Enum

   private const int HighIntensity = 0x0008;
   private const ushort COMMON_LVB_LEADING_BYTE = 0x0100;
   private const ushort COMMON_LVB_TRAILING_BYTE = 0x0200;
   private const ushort COMMON_LVB_GRID_HORIZONTAL = 0x0400;
   private const ushort COMMON_LVB_GRID_LVERTICAL = 0x0800;
   private const ushort COMMON_LVB_GRID_RVERTICAL = 0x1000;
   private const ushort COMMON_LVB_REVERSE_VIDEO = 0x4000;
   private const ushort COMMON_LVB_UNDERSCORE = 0x8000;
   private const ushort COMMON_LVB_SBCSDBCS = 0x0300;
   [Flags]
   public enum Colors : int
   {
      Black = 0x0000,
      DarkBlue = 0x0001,
      DarkGreen = 0x0002,
      DarkRed = 0x0004,
      Gray = DarkBlue | DarkGreen | DarkRed,
      DarkYellow = DarkRed | DarkGreen,
      DarkPurple = DarkRed | DarkBlue,
      DarkCyan = DarkGreen | DarkBlue,
      LightBlue = DarkBlue | HighIntensity,
      LightGreen = DarkGreen | HighIntensity,
      LightRed = DarkRed | HighIntensity,
      LightWhite = Gray | HighIntensity,
      LightYellow = DarkYellow | HighIntensity,
      LightPurple = DarkPurple | HighIntensity,
      LightCyan = DarkCyan | HighIntensity
   }

   #endregion // Colors Enum

   private readonly CharInfo[] _buffer;
   private readonly List<CharInfo> _tmpBuffer;
   private readonly short _left;
   private readonly short _top;
   private readonly short _width;
   private readonly short _height;
   private ushort _defaultColor;
   private int _cursorLeft;
   private int _cursorTop;
   private static SafeFileHandle _safeFileHandle;
   /// <summary>
   /// Automatically draw to console.
   /// Unset this if you want to manually control when (and what order) boxes are writen to consoles - or you want to batch some stuff.
   /// You must manually call <c>Draw()</c> to write to console.
   /// </summary>
   public bool AutoDraw = true;
   public bool IsDirty { get; private set; }

   public InputConsoleBox(short left, short top, short width, short height, Colors defaultForegroundColor = Colors.Gray, Colors defaultBackgroundColor = Colors.Black)
   {
      if (left < 0 || top < 0 || left + width > Console.BufferWidth || top + height > Console.BufferHeight)
         throw new Exception(string.Format("Attempting to create a box {0},{1}->{2},{3} that is out of buffer bounds 0,0->{4},{5}", left, top, left + width, top + height, Console.BufferWidth, Console.BufferHeight));

      _left = left;
      _top = top;
      _width = width;
      _height = height;
      _buffer = new CharInfo[_width * _height];
      _defaultColor = CombineColors(defaultForegroundColor, defaultBackgroundColor);
      _tmpBuffer = new List<CharInfo>(_width * _height); // Assumption that we won't be writing much more than a screenful (backbufferfull) in every write operation


      //SafeFileHandle h = CreateFile("CONOUT$", 0x40000000, 2, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero);
      if (_safeFileHandle == null)
      {
         var stdOutputHandle = GetStdHandle(STD_OUTPUT_HANDLE);
         _safeFileHandle = new SafeFileHandle(stdOutputHandle, false);
      }

      Clear();
      Draw();
   }

   public void Clear()
   {
      for (int y = 0; y < _height; y++)
      {
         for (int x = 0; x < _width; x++)
         {
            var i = (y * _width) + x;
            _buffer[i].Char.UnicodeChar = ' ';
            _buffer[i].Attributes = _defaultColor;
         }
      }
      IsDirty = true;
      // Update screen
      if (AutoDraw)
         Draw();
   }

   public void Draw()
   {
      IsDirty = false;
      var rect = new SmallRect() { Left = _left, Top = _top, Right = (short)(_left + _width), Bottom = (short)(_top + _height) };
      bool b = WriteConsoleOutput(_safeFileHandle, _buffer,
          new Coord(_width, _height),
          new Coord(0, 0), ref rect);
   }

   private static ushort CombineColors(Colors foreColor, Colors backColor)
   {
      return (ushort)((int)foreColor + (((int)backColor) << 4));
   }

   public void SetCursorPosition(int left, int top)
   {
      if (left >= _width || top >= _height)
         throw new Exception(string.Format("Position out of bounds attempting to set cursor at box pos {0},{1} when box size is only {2},{3}.", left, top, _width, _height));

      _cursorLeft = left;
      _cursorTop = top;
   }

   public void SetCursorBlink(int left, int top, bool state)
   {
      Console.SetCursorPosition(left, top);
      Console.CursorVisible = state;
      //// Does not work
      //var i = (top * _width) + left;
      //if (state)
      //    _buffer[i].Attributes = (ushort)((int)_buffer[i].Attributes & ~(int)COMMON_LVB_UNDERSCORE);
      //else
      //    _buffer[i].Attributes = (ushort)((int)_buffer[i].Attributes | (int)COMMON_LVB_UNDERSCORE);

      //if (AutoDraw)
      //    Draw();
   }

   public void WriteLine(string line, Colors fgColor, Colors bgColor)
   {
      var c = _defaultColor;
      _defaultColor = CombineColors(fgColor, bgColor);
      WriteLine(line);
      _defaultColor = c;
   }

   public void WriteLine(string line)
   {
      Write(line + "\n");
   }

   public void Write(string text)
   {
      Write(text.ToCharArray());
   }

   public void Write(char[] text)
   {
      IsDirty = true;
      _tmpBuffer.Clear();
      bool newLine = false;

      // Old-school! Could definitively have been done more easily with regex. :)
      var col = 0;
      var row = -1;
      for (int i = 0; i < text.Length; i++)
      {

         // Detect newline
         if (text[i] == '\n')
            newLine = true;
         if (text[i] == '\r')
         {
            newLine = true;
            // Skip following \n
            if (i + 1 < text.Length && text[i] == '\n')
               i++;
         }

         // Keep track of column and row
         col++;
         if (col == _width)
         {
            col = 0;
            row++;

            if (newLine) // Last character was newline? Skip filling the whole next line with empty
            {
               newLine = false;
               continue;
            }
         }

         // If we are newlining we need to fill the remaining with blanks
         if (newLine)
         {
            newLine = false;

            for (int i2 = col; i2 <= _width; i2++)
            {
               _tmpBuffer.Add(new CharInfo(' ', _defaultColor));
            }
            col = 0;
            row++;
            continue;
         }
         if (i >= text.Length)
            break;

         // Add character
         _tmpBuffer.Add(new CharInfo(text[i], _defaultColor));
      }

      var cursorI = (_cursorTop * _width) + _cursorLeft;

      // Get our end position
      var end = cursorI + _tmpBuffer.Count;

      // If we are overflowing (scrolling) then we need to complete our last line with spaces (align buffer with line ending)
      if (end > _buffer.Length && col != 0)
      {
         for (int i = col; i <= _width; i++)
         {
            _tmpBuffer.Add(new CharInfo(' ', _defaultColor));
         }
         col = 0;
         row++;
      }

      // Chop start of buffer to fit into destination buffer
      if (_tmpBuffer.Count > _buffer.Length)
         _tmpBuffer.RemoveRange(0, _tmpBuffer.Count - _buffer.Length);
      // Convert to array so we can batch copy
      var tmpArray = _tmpBuffer.ToArray();

      // Are we going to write outside of buffer?
      end = cursorI + _tmpBuffer.Count;
      var scrollUp = 0;
      if (end > _buffer.Length)
      {
         scrollUp = end - _buffer.Length;
      }

      // Scroll up
      if (scrollUp > 0)
      {
         Array.Copy(_buffer, scrollUp, _buffer, 0, _buffer.Length - scrollUp);
         cursorI -= scrollUp;
      }
      var lastPos = Math.Min(_buffer.Length, cursorI + tmpArray.Length);
      var firstPos = lastPos - tmpArray.Length;

      // Copy new data in
      Array.Copy(tmpArray, 0, _buffer, firstPos, tmpArray.Length);

      // Set new cursor position
      _cursorLeft = col;
      _cursorTop = Math.Min(_height, _cursorTop + row + 1);

      // Write to main buffer
      if (AutoDraw)
         Draw();
   }
   #endregion

   #region Input
   private string _currentInputBuffer = "";
   private string _inputPrompt;
   private int _inputCursorPos = 0;
   private int _inputFrameStart = 0;
   // Not used because COMMON_LVB_UNDERSCORE doesn't work
   //private bool _inputCursorState = false;
   //private int _inputCursorStateChange = 0;
   private int _cursorBlinkLeft = 0;
   private int _cursorBlinkTop = 0;

   public string InputPrompt
   {
      get { return _inputPrompt; }
      set
      {
         _inputPrompt = value;
         ResetInput();
      }
   }

   private void ResetInput()
   {
      SetCursorPosition(0, 0);
      _inputCursorPos = Math.Min(_currentInputBuffer.Length, _inputCursorPos);

      var inputPrompt = InputPrompt + "[" + _currentInputBuffer.Length + "] ";

      // What is the max length we can write?
      var maxLen = _width - inputPrompt.Length;
      if (maxLen < 0)
         return;

      if (_inputCursorPos > _inputFrameStart + maxLen)
         _inputFrameStart = _inputCursorPos - maxLen;
      if (_inputCursorPos < _inputFrameStart)
         _inputFrameStart = _inputCursorPos;

      _cursorBlinkLeft = inputPrompt.Length + _inputCursorPos - _inputFrameStart;

      //if (_currentInputBuffer.Length - _inputFrameStart < maxLen)
      //    _inputFrameStart--;


      // Write and pad the end
      var str = inputPrompt + _currentInputBuffer.Substring(_inputFrameStart, Math.Min(_currentInputBuffer.Length - _inputFrameStart, maxLen));
      var spaceLen = _width - str.Length;
      Write(str + (spaceLen > 0 ? new String(' ', spaceLen) : ""));

      UpdateCursorBlink(true);

   }
   private void UpdateCursorBlink(bool force)
   {
      // Since COMMON_LVB_UNDERSCORE doesn't work we won't be controlling blink
      //// Blink the cursor
      //if (Environment.TickCount > _inputCursorStateChange)
      //{
      //    _inputCursorStateChange = Environment.TickCount + 250;
      //    _inputCursorState = !_inputCursorState;
      //    force = true;
      //}
      //if (force)
      //    SetCursorBlink(_cursorBlinkLeft, _cursorBlinkTop, _inputCursorState);
      SetCursorBlink(_left + _cursorBlinkLeft, _top + _cursorBlinkTop, true);
   }

   public string ReadLine()
   {
      Console.CursorVisible = false;
      Clear();
      ResetInput();
      while (true)
      {
         Thread.Sleep(50);

         while (Console.KeyAvailable)
         {
            var key = Console.ReadKey(true);

            switch (key.Key)
            {
               case ConsoleKey.Enter:
                  {
                     var ret = _currentInputBuffer;
                     _inputCursorPos = 0;
                     _currentInputBuffer = "";
                     return ret;
                  }
               case ConsoleKey.LeftArrow:
                  {
                     _inputCursorPos = Math.Max(0, _inputCursorPos - 1);
                     break;
                  }
               case ConsoleKey.RightArrow:
                  {
                     _inputCursorPos = Math.Min(_currentInputBuffer.Length, _inputCursorPos + 1);
                     break;
                  }
               case ConsoleKey.Backspace:
                  {
                     if (_inputCursorPos > 0)
                     {
                        _inputCursorPos--;
                        _currentInputBuffer = _currentInputBuffer.Remove(_inputCursorPos, 1);
                     }
                     break;
                  }
               case ConsoleKey.Delete:
                  {
                     if (_inputCursorPos < _currentInputBuffer.Length - 1)
                        _currentInputBuffer = _currentInputBuffer.Remove(_inputCursorPos, 1);
                     break;
                  }

               default:
                  {
                     var pos = _inputCursorPos;
                     //if (_inputCursorPos == _currentInputBuffer.Length)
                     _inputCursorPos++;
                     _currentInputBuffer = _currentInputBuffer.Insert(pos, key.KeyChar.ToString());
                     break;
                  }
            }
            ResetInput();

         }

         // COMMON_LVB_UNDERSCORE doesn't work so we use Consoles default cursor
         //UpdateCursorBlink(false);

      }
   }

   #endregion

}