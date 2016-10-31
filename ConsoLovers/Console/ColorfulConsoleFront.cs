// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColorfulConsoleFront.cs" company="ConsoLovers">
//   Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.Console
{
   using System;
   using System.Collections.Generic;
   using System.Drawing;
   using System.IO;
   using System.Text;

   using ConsoLovers.Contracts;

   /// <summary>Wraps around the System.ColoredConsole class, adding enhanced styling functionality.</summary>
   public partial class ColoredConsole : IColoredConsole
   {
      private static IColoredConsole instance;

      #region Constructors and Destructors

      static ColoredConsole()
      {
         colorStore = GetColorStore();
         colorManagerFactory = new ColorManagerFactory();
         colorManager = colorManagerFactory.GetManager(colorStore, MAX_COLOR_CHANGES, INITIAL_COLOR_CHANGE_COUNT_VALUE);

         Console.CancelKeyPress += Console_CancelKeyPress;
      }

      #endregion

      #region Public Events

      public static event ConsoleCancelEventHandler CancelKeyPress = delegate { };

      #endregion

      #region Public Properties

      public Color BackgroundColor
      {
         get
         {
            return colorManager.GetColor(Console.BackgroundColor);
         }
         set
         {
            Console.BackgroundColor = colorManager.GetConsoleColor(value);
         }
      }

      public int BufferHeight
      {
         get
         {
            return Console.BufferHeight;
         }
         set
         {
            Console.BufferHeight = value;
         }
      }

      public static int BufferWidth
      {
         get
         {
            return Console.BufferWidth;
         }
         set
         {
            Console.BufferWidth = value;
         }
      }

      public static bool CapsLock => Console.CapsLock;

      public int CursorLeft
      {
         get
         {
            return Console.CursorLeft;
         }
         set
         {
            Console.CursorLeft = value;
         }
      }

      public int CursorSize
      {
         get
         {
            return Console.CursorSize;
         }
         set
         {
            Console.CursorSize = value;
         }
      }

      public int CursorTop
      {
         get
         {
            return Console.CursorTop;
         }
         set
         {
            Console.CursorTop = value;
         }
      }

      public bool CursorVisible
      {
         get
         {
            return Console.CursorVisible;
         }
         set
         {
            Console.CursorVisible = value;
         }
      }

      public static TextWriter Error => Console.Error;

      public Color ForegroundColor
      {
         get
         {
            return colorManager.GetColor(Console.ForegroundColor);
         }
         set
         {
            Console.ForegroundColor = colorManager.GetConsoleColor(value);
         }
      }

      public TextReader In => Console.In;

      public static Encoding InputEncoding
      {
         get
         {
            return Console.InputEncoding;
         }
         set
         {
            Console.InputEncoding = value;
         }
      }

      public static bool KeyAvailable => Console.KeyAvailable;

      public static int LargestWindowHeight => Console.LargestWindowHeight;

      public static int LargestWindowWidth => Console.LargestWindowWidth;

      public static bool NumberLock => Console.NumberLock;

      public static TextWriter Out => Console.Out;

      public static Encoding OutputEncoding
      {
         get
         {
            return Console.OutputEncoding;
         }
         set
         {
            Console.OutputEncoding = value;
         }
      }

      public static string Title
      {
         get
         {
            return Console.Title;
         }
         set
         {
            Console.Title = value;
         }
      }

      public static bool TreatControlCAsInput
      {
         get
         {
            return Console.TreatControlCAsInput;
         }
         set
         {
            Console.TreatControlCAsInput = value;
         }
      }

      public static int WindowHeight
      {
         get
         {
            return Console.WindowHeight;
         }
         set
         {
            Console.WindowHeight = value;
         }
      }

      public static int WindowLeft
      {
         get
         {
            return Console.WindowLeft;
         }
         set
         {
            Console.WindowLeft = value;
         }
      }

      public int WindowTop
      {
         get
         {
            return Console.WindowTop;
         }
         set
         {
            Console.WindowTop = value;
         }
      }

      public int WindowWidth
      {
         get
         {
            return Console.WindowWidth;
         }
         set
         {
            Console.WindowWidth = value;
         }
      }

      #endregion

      #region Public Methods and Operators

      public void Beep(int frequency, int duration)
      {
         Console.Beep(frequency, duration);
      }

      public void Clear()
      {
         Console.Clear();
      }

      public static void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop)
      {
         Console.MoveBufferArea(sourceLeft, sourceTop, sourceWidth, sourceHeight, targetLeft, targetTop);
      }

      public static void MoveBufferArea
         (int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop, char sourceChar, ConsoleColor sourceForeColor,
            ConsoleColor sourceBackColor)
      {
         Console.MoveBufferArea(sourceLeft, sourceTop, sourceWidth, sourceHeight, targetLeft, targetTop, sourceChar, sourceForeColor, sourceBackColor);
      }

      public static Stream OpenStandardError()
      {
         return Console.OpenStandardError();
      }

#if !NETSTANDARD1_3
      public static Stream OpenStandardError(int bufferSize)
      {
         return Console.OpenStandardError(bufferSize);
      }
#endif

      public static Stream OpenStandardInput()
      {
         return Console.OpenStandardInput();
      }

#if !NETSTANDARD1_3
      public static Stream OpenStandardInput(int bufferSize)
      {
         return Console.OpenStandardInput(bufferSize);
      }
#endif

      public static Stream OpenStandardOutput()
      {
         return Console.OpenStandardOutput();
      }

#if !NETSTANDARD1_3
      public static Stream OpenStandardOutput(int bufferSize)
      {
         return Console.OpenStandardOutput(bufferSize);
      }
#endif

      public static int Read()
      {
         return Console.Read();
      }

      public ConsoleKeyInfo ReadKey()
      {
         return Console.ReadKey();
      }

      public static ConsoleKeyInfo ReadKey(bool intercept)
      {
         return Console.ReadKey(intercept);
      }

      public static string ReadLine()
      {
         return Console.ReadLine();
      }

      public void ResetColor()
      {
         Console.ResetColor();
      }

      public void SetBufferSize(int width, int height)
      {
         Console.SetBufferSize(width, height);
      }

      public void SetCursorPosition(int left, int top)
      {
         Console.SetCursorPosition(left, top);
      }

      public void SetError(TextWriter newError)
      {
         Console.SetError(newError);
      }

      public void SetIn(TextReader newIn)
      {
         Console.SetIn(newIn);
      }

      public static void SetOut(TextWriter newOut)
      {
         Console.SetOut(newOut);
      }

      public static void SetWindowPosition(int left, int top)
      {
         Console.SetWindowPosition(left, top);
      }

      public static void SetWindowSize(int width, int height)
      {
         Console.SetWindowSize(width, height);
      }

      public static void Write(bool value)
      {
         Console.Write(value);
      }

      public static void Write(bool value, Color color)
      {
         WriteInColor(Console.Write, value, color);
      }

      public static void Write(char value)
      {
         Console.Write(value);
      }

      public static void Write(char value, Color color)
      {
         WriteInColor(Console.Write, value, color);
      }

      public static void Write(char[] value)
      {
         Console.Write(value);
      }

      public static void Write(char[] value, Color color)
      {
         WriteInColor(Console.Write, value, color);
      }

      public static void Write(decimal value)
      {
         Console.Write(value);
      }

      public static void Write(decimal value, Color color)
      {
         WriteInColor(Console.Write, value, color);
      }

      public static void Write(double value)
      {
         Console.Write(value);
      }

      public static void Write(double value, Color color)
      {
         WriteInColor(Console.Write, value, color);
      }

      public static void Write(float value)
      {
         Console.Write(value);
      }

      public static void Write(float value, Color color)
      {
         WriteInColor(Console.Write, value, color);
      }

      public static void Write(int value)
      {
         Console.Write(value);
      }

      public static void Write(int value, Color color)
      {
         WriteInColor(Console.Write, value, color);
      }

      public static void Write(long value)
      {
         Console.Write(value);
      }

      public static void Write(long value, Color color)
      {
         WriteInColor(Console.Write, value, color);
      }

      public static void Write(object value)
      {
         Console.Write(value);
      }

      public static void Write(object value, Color color)
      {
         WriteInColor(Console.Write, value, color);
      }

      public void Write(string value)
      {
         Console.Write(value);
      }

      public void Clear(Color clearingColor)
      {
         Console.BackgroundColor = colorManager.GetConsoleColor(clearingColor);
         Console.Clear();
         Console.ResetColor();
      }

      public void Write(string value, Color color)
      {
         WriteInColor(Console.Write, value, color);
      }

      public void Write(uint value)
      {
         Console.Write(value);
      }

      public static void Write(uint value, Color color)
      {
         WriteInColor(Console.Write, value, color);
      }

      public static void Write(ulong value)
      {
         Console.Write(value);
      }

      public static void Write(ulong value, Color color)
      {
         WriteInColor(Console.Write, value, color);
      }

      public static void Write(string format, object arg0)
      {
         Console.WriteLine(format, arg0);
      }

      public static void Write(string format, object arg0, Color color)
      {
         WriteInColor(Console.Write, format, arg0, color);
      }

      public static void Write(string format, params object[] args)
      {
         Console.Write(format, args);
      }

      public static void Write(string format, Color color, params object[] args)
      {
         WriteInColor(Console.Write, format, args, color);
      }

      public static void Write(char[] buffer, int index, int count)
      {
         Console.Write(buffer, index, count);
      }

      public static void Write(char[] buffer, int index, int count, Color color)
      {
         WriteChunkInColor(Console.Write, buffer, index, count, color);
      }

      public static void Write(string format, object arg0, object arg1)
      {
         Console.Write(format, arg0, arg1);
      }

      public static void Write(string format, object arg0, object arg1, Color color)
      {
         WriteInColor(Console.Write, format, arg0, arg1, color);
      }

      public static void Write(string format, object arg0, object arg1, object arg2)
      {
         Console.Write(format, arg0, arg1, arg2);
      }

      public static void Write(string format, object arg0, object arg1, object arg2, Color color)
      {
         WriteInColor(Console.Write, format, arg0, arg1, arg2, color);
      }

      public static void Write(string format, object arg0, object arg1, object arg2, object arg3)
      {
         Console.Write(format, arg0, arg1, arg2, arg3);
      }

      public static void Write(string format, object arg0, object arg1, object arg2, object arg3, Color color)
      {
         // NOTE: The Intellisense for this overload of System.ColoredConsole.Write is misleading, as the C# compiler
         //       actually resolves this overload to System.ColoredConsole.Write(string format, object[] args)!

         WriteInColor(Console.Write, format, new[] { arg0, arg1, arg2, arg3 }, color);
      }

      public static void WriteAlternating(bool value, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.Write, value, alternator);
      }

      public static void WriteAlternating(char value, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.Write, value, alternator);
      }

      public static void WriteAlternating(char[] value, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.Write, value, alternator);
      }

      public static void WriteAlternating(decimal value, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.Write, value, alternator);
      }

      public static void WriteAlternating(double value, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.Write, value, alternator);
      }

      public static void WriteAlternating(float value, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.Write, value, alternator);
      }

      public static void WriteAlternating(int value, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.Write, value, alternator);
      }

      public static void WriteAlternating(long value, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.Write, value, alternator);
      }

      public static void WriteAlternating(object value, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.Write, value, alternator);
      }

      public static void WriteAlternating(string value, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.Write, value, alternator);
      }

      public static void WriteAlternating(uint value, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.Write, value, alternator);
      }

      public static void WriteAlternating(ulong value, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.Write, value, alternator);
      }

      public static void WriteAlternating(string format, object arg0, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.Write, format, arg0, alternator);
      }

      public static void WriteAlternating(string format, ColorAlternator alternator, params object[] args)
      {
         WriteInColorAlternating(Console.Write, format, args, alternator);
      }

      public static void WriteAlternating(char[] buffer, int index, int count, ColorAlternator alternator)
      {
         WriteChunkInColorAlternating(Console.Write, buffer, index, count, alternator);
      }

      public static void WriteAlternating(string format, object arg0, object arg1, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.Write, format, arg0, arg1, alternator);
      }

      public static void WriteAlternating(string format, object arg0, object arg1, object arg2, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.Write, format, arg0, arg1, arg2, alternator);
      }

      public static void WriteAlternating(string format, object arg0, object arg1, object arg2, object arg3, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.Write, format, new[] { arg0, arg1, arg2, arg3 }, alternator);
      }

      public void WriteAscii(string value)
      {
         WriteAscii(value, null);
      }

      public void WriteAscii(string value, FigletFont font)
      {
         WriteLine(GetFiglet(font).ToAscii(value).ConcreteValue);
      }

      public static void WriteAscii(string value, Color color)
      {
         WriteAscii(value, null, color);
      }

      public static void WriteAscii(string value, FigletFont font, Color color)
      {
         WriteLine(GetFiglet(font).ToAscii(value).ConcreteValue, color);
      }

      public static void WriteAsciiAlternating(string value, ColorAlternator alternator)
      {
         WriteAsciiAlternating(value, null, alternator);
      }

      public static void WriteAsciiAlternating(string value, FigletFont font, ColorAlternator alternator)
      {
         foreach (var line in GetFiglet(font).ToAscii(value).ConcreteValue.Split('\n'))
         {
            WriteLineAlternating(line, alternator);
         }
      }

      public static void WriteAsciiStyled(string value, StyleSheet styleSheet)
      {
         WriteAsciiStyled(value, null, styleSheet);
      }

      public static void WriteAsciiStyled(string value, FigletFont font, StyleSheet styleSheet)
      {
         WriteLineStyled(GetFiglet(font).ToAscii(value), styleSheet);
      }

      public static void WriteFormatted(string format, object arg0, Color styledColor, Color defaultColor)
      {
         WriteInColorFormatted(WRITE_TRAILER, format, arg0, styledColor, defaultColor);
      }

      public static void WriteFormatted(string format, Formatter arg0, Color defaultColor)
      {
         WriteInColorFormatted(WRITE_TRAILER, format, arg0, defaultColor);
      }

      public static void WriteFormatted(string format, Color styledColor, Color defaultColor, params object[] args)
      {
         WriteInColorFormatted(WRITE_TRAILER, format, args, styledColor, defaultColor);
      }

      public static void WriteFormatted(string format, Color defaultColor, params Formatter[] args)
      {
         WriteInColorFormatted(WRITE_TRAILER, format, args, defaultColor);
      }

      public static void WriteFormatted(string format, object arg0, object arg1, Color styledColor, Color defaultColor)
      {
         WriteInColorFormatted(WRITE_TRAILER, format, arg0, arg1, styledColor, defaultColor);
      }

      public static void WriteFormatted(string format, Formatter arg0, Formatter arg1, Color defaultColor)
      {
         WriteInColorFormatted(WRITE_TRAILER, format, arg0, arg1, defaultColor);
      }

      public static void WriteFormatted(string format, object arg0, object arg1, object arg2, Color styledColor, Color defaultColor)
      {
         WriteInColorFormatted(WRITE_TRAILER, format, arg0, arg1, arg2, styledColor, defaultColor);
      }

      public static void WriteFormatted(string format, Formatter arg0, Formatter arg1, Formatter arg2, Color defaultColor)
      {
         WriteInColorFormatted(WRITE_TRAILER, format, arg0, arg1, arg2, defaultColor);
      }

      public static void WriteFormatted(string format, object arg0, object arg1, object arg2, object arg3, Color styledColor, Color defaultColor)
      {
         WriteInColorFormatted(WRITE_TRAILER, format, new[] { arg0, arg1, arg2, arg3 }, styledColor, defaultColor);
      }

      public static void WriteFormatted(string format, Formatter arg0, Formatter arg1, Formatter arg2, Formatter arg3, Color defaultColor)
      {
         WriteInColorFormatted(WRITE_TRAILER, format, new[] { arg0, arg1, arg2, arg3 }, defaultColor);
      }

      public void WriteLine()
      {
         Console.WriteLine();
      }

      public static void WriteLine(bool value)
      {
         Console.WriteLine(value);
      }

      public static void WriteLine(bool value, Color color)
      {
         WriteInColor(Console.WriteLine, value, color);
      }

      public static void WriteLine(char value)
      {
         Console.WriteLine(value);
      }

      public static void WriteLine(char value, Color color)
      {
         WriteInColor(Console.WriteLine, value, color);
      }

      public static void WriteLine(char[] value)
      {
         Console.WriteLine(value);
      }

      public static void WriteLine(char[] value, Color color)
      {
         WriteInColor(Console.WriteLine, value, color);
      }

      public static void WriteLine(decimal value)
      {
         Console.WriteLine(value);
      }

      public static void WriteLine(decimal value, Color color)
      {
         WriteInColor(Console.WriteLine, value, color);
      }

      public static void WriteLine(double value)
      {
         Console.WriteLine(value);
      }

      public static void WriteLine(double value, Color color)
      {
         WriteInColor(Console.WriteLine, value, color);
      }

      public static void WriteLine(float value)
      {
         Console.WriteLine(value);
      }

      public static void WriteLine(float value, Color color)
      {
         WriteInColor(Console.WriteLine, value, color);
      }

      public static void WriteLine(int value)
      {
         Console.WriteLine(value);
      }

      public static void WriteLine(int value, Color color)
      {
         WriteInColor(Console.WriteLine, value, color);
      }

      public static void WriteLine(long value)
      {
         Console.WriteLine(value);
      }

      public static void WriteLine(long value, Color color)
      {
         WriteInColor(Console.WriteLine, value, color);
      }

      public static void WriteLine(object value)
      {
         Console.WriteLine(value);
      }

      public static void WriteLine(object value, Color color)
      {
         WriteInColor(Console.WriteLine, value, color);
      }

      public void WriteLine(string value)
      {
         Console.WriteLine(value);
      }

      public static void WriteLine(string value, Color color)
      {
         WriteInColor(Console.WriteLine, value, color);
      }

      public static void WriteLine(uint value)
      {
         Console.WriteLine(value);
      }

      public static void WriteLine(uint value, Color color)
      {
         WriteInColor(Console.WriteLine, value, color);
      }

      public static void WriteLine(ulong value)
      {
         Console.WriteLine(value);
      }

      public static void WriteLine(ulong value, Color color)
      {
         WriteInColor(Console.WriteLine, value, color);
      }

      public static void WriteLine(string format, object arg0)
      {
         Console.WriteLine(format, arg0);
      }

      public static void WriteLine(string format, object arg0, Color color)
      {
         WriteInColor(Console.WriteLine, format, arg0, color);
      }

      public static void WriteLine(string format, params object[] args)
      {
         Console.WriteLine(format, args);
      }

      public static void WriteLine(string format, Color color, params object[] args)
      {
         WriteInColor(Console.WriteLine, format, args, color);
      }

      public static void WriteLine(char[] buffer, int index, int count)
      {
         Console.WriteLine(buffer, index, count);
      }

      public static void WriteLine(char[] buffer, int index, int count, Color color)
      {
         WriteChunkInColor(Console.WriteLine, buffer, index, count, color);
      }

      public static void WriteLine(string format, object arg0, object arg1)
      {
         Console.WriteLine(format, arg0, arg1);
      }

      public static void WriteLine(string format, object arg0, object arg1, Color color)
      {
         WriteInColor(Console.WriteLine, format, arg0, arg1, color);
      }

      public static void WriteLine(string format, object arg0, object arg1, object arg2)
      {
         Console.WriteLine(format, arg0, arg1, arg2);
      }

      public static void WriteLine(string format, object arg0, object arg1, object arg2, Color color)
      {
         WriteInColor(Console.WriteLine, format, arg0, arg1, arg2, color);
      }

      public static void WriteLine(string format, object arg0, object arg1, object arg2, object arg3)
      {
         Console.WriteLine(format, arg0, arg1, arg2, arg3);
      }

      public static void WriteLine(string format, object arg0, object arg1, object arg2, object arg3, Color color)
      {
         // NOTE: The Intellisense for this overload of System.ColoredConsole.WriteLine is misleading, as the C# compiler
         //       actually resolves this overload to System.ColoredConsole.WriteLine(string format, object[] args)!

         WriteInColor(Console.WriteLine, format, new[] { arg0, arg1, arg2, arg3 }, color);
      }

      public static void WriteLineAlternating(ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.Write, WRITELINE_TRAILER, alternator);
      }

      public static void WriteLineAlternating(bool value, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.WriteLine, value, alternator);
      }

      public static void WriteLineAlternating(char value, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.WriteLine, value, alternator);
      }

      public static void WriteLineAlternating(char[] value, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.WriteLine, value, alternator);
      }

      public static void WriteLineAlternating(decimal value, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.WriteLine, value, alternator);
      }

      public static void WriteLineAlternating(double value, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.WriteLine, value, alternator);
      }

      public static void WriteLineAlternating(float value, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.WriteLine, value, alternator);
      }

      public static void WriteLineAlternating(int value, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.WriteLine, value, alternator);
      }

      public static void WriteLineAlternating(long value, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.WriteLine, value, alternator);
      }

      public static void WriteLineAlternating(object value, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.WriteLine, value, alternator);
      }

      public static void WriteLineAlternating(string value, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.WriteLine, value, alternator);
      }

      public static void WriteLineAlternating(uint value, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.WriteLine, value, alternator);
      }

      public static void WriteLineAlternating(ulong value, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.WriteLine, value, alternator);
      }

      public static void WriteLineAlternating(string format, object arg0, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.WriteLine, format, arg0, alternator);
      }

      public static void WriteLineAlternating(string format, ColorAlternator alternator, params object[] args)
      {
         WriteInColorAlternating(Console.WriteLine, format, args, alternator);
      }

      public static void WriteLineAlternating(char[] buffer, int index, int count, ColorAlternator alternator)
      {
         WriteChunkInColorAlternating(Console.WriteLine, buffer, index, count, alternator);
      }

      public static void WriteLineAlternating(string format, object arg0, object arg1, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.WriteLine, format, arg0, arg1, alternator);
      }

      public static void WriteLineAlternating(string format, object arg0, object arg1, object arg2, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.WriteLine, format, arg0, arg1, arg2, alternator);
      }

      public static void WriteLineAlternating(string format, object arg0, object arg1, object arg2, object arg3, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.WriteLine, format, new[] { arg0, arg1, arg2, arg3 }, alternator);
      }

      public static void WriteLineFormatted(string format, object arg0, Color styledColor, Color defaultColor)
      {
         WriteInColorFormatted(WRITELINE_TRAILER, format, arg0, styledColor, defaultColor);
      }

      public static void WriteLineFormatted(string format, Formatter arg0, Color defaultColor)
      {
         WriteInColorFormatted(WRITELINE_TRAILER, format, arg0, defaultColor);
      }

      public static void WriteLineFormatted(string format, Color styledColor, Color defaultColor, params object[] args)
      {
         WriteInColorFormatted(WRITELINE_TRAILER, format, args, styledColor, defaultColor);
      }

      public static void WriteLineFormatted(string format, Color defaultColor, params Formatter[] args)
      {
         WriteInColorFormatted(WRITELINE_TRAILER, format, args, defaultColor);
      }

      public static void WriteLineFormatted(string format, object arg0, object arg1, Color styledColor, Color defaultColor)
      {
         WriteInColorFormatted(WRITELINE_TRAILER, format, arg0, arg1, styledColor, defaultColor);
      }

      public static void WriteLineFormatted(string format, Formatter arg0, Formatter arg1, Color defaultColor)
      {
         WriteInColorFormatted(WRITELINE_TRAILER, format, arg0, arg1, defaultColor);
      }

      public static void WriteLineFormatted(string format, object arg0, object arg1, object arg2, Color styledColor, Color defaultColor)
      {
         WriteInColorFormatted(WRITELINE_TRAILER, format, arg0, arg1, arg2, styledColor, defaultColor);
      }

      public static void WriteLineFormatted(string format, Formatter arg0, Formatter arg1, Formatter arg2, Color defaultColor)
      {
         WriteInColorFormatted(WRITELINE_TRAILER, format, arg0, arg1, arg2, defaultColor);
      }

      public static void WriteLineFormatted(string format, object arg0, object arg1, object arg2, object arg3, Color styledColor, Color defaultColor)
      {
         WriteInColorFormatted(WRITELINE_TRAILER, format, new[] { arg0, arg1, arg2, arg3 }, styledColor, defaultColor);
      }

      public static void WriteLineFormatted(string format, Formatter arg0, Formatter arg1, Formatter arg2, Formatter arg3, Color defaultColor)
      {
         WriteInColorFormatted(WRITELINE_TRAILER, format, new[] { arg0, arg1, arg2, arg3 }, defaultColor);
      }

      public static void WriteLineStyled(StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITE_TRAILER, WRITELINE_TRAILER, styleSheet);
      }

      public static void WriteLineStyled(bool value, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITELINE_TRAILER, value, styleSheet);
      }

      public static void WriteLineStyled(char value, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITELINE_TRAILER, value, styleSheet);
      }

      public static void WriteLineStyled(char[] value, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITELINE_TRAILER, value, styleSheet);
      }

      public static void WriteLineStyled(decimal value, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITELINE_TRAILER, value, styleSheet);
      }

      public static void WriteLineStyled(double value, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITELINE_TRAILER, value, styleSheet);
      }

      public static void WriteLineStyled(float value, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITELINE_TRAILER, value, styleSheet);
      }

      public static void WriteLineStyled(int value, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITELINE_TRAILER, value, styleSheet);
      }

      public static void WriteLineStyled(long value, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITELINE_TRAILER, value, styleSheet);
      }

      public static void WriteLineStyled(StyledString value, StyleSheet styleSheet)
      {
         WriteAsciiInColorStyled(WRITELINE_TRAILER, value, styleSheet);
      }

      public static void WriteLineStyled(string value, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITELINE_TRAILER, value, styleSheet);
      }

      public static void WriteLineStyled(uint value, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITELINE_TRAILER, value, styleSheet);
      }

      public static void WriteLineStyled(ulong value, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITELINE_TRAILER, value, styleSheet);
      }

      public static void WriteLineStyled(string format, object arg0, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITELINE_TRAILER, format, arg0, styleSheet);
      }

      public static void WriteLineStyled(StyleSheet styleSheet, string format, params object[] args)
      {
         WriteInColorStyled(WRITELINE_TRAILER, format, args, styleSheet);
      }

      public static void WriteLineStyled(char[] buffer, int index, int count, StyleSheet styleSheet)
      {
         WriteChunkInColorStyled(WRITELINE_TRAILER, buffer, index, count, styleSheet);
      }

      public static void WriteLineStyled(string format, object arg0, object arg1, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITELINE_TRAILER, format, arg0, arg1, styleSheet);
      }

      public static void WriteLineStyled(string format, object arg0, object arg1, object arg2, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITELINE_TRAILER, format, arg0, arg1, arg2, styleSheet);
      }

      public static void WriteLineStyled(string format, object arg0, object arg1, object arg2, object arg3, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITELINE_TRAILER, format, new[] { arg0, arg1, arg2, arg3 }, styleSheet);
      }

      public static void WriteLineWithGradient<T>(IEnumerable<T> input, Color startColor, Color endColor, int maxColorsInGradient = MAX_COLOR_CHANGES)
      {
         DoWithGradient(WriteLine, input, startColor, endColor, maxColorsInGradient);
      }

      public static void WriteStyled(bool value, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITE_TRAILER, value, styleSheet);
      }

      public static void WriteStyled(char value, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITE_TRAILER, value, styleSheet);
      }

      public static void WriteStyled(char[] value, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITE_TRAILER, value, styleSheet);
      }

      public static void WriteStyled(decimal value, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITE_TRAILER, value, styleSheet);
      }

      public static void WriteStyled(double value, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITE_TRAILER, value, styleSheet);
      }

      public static void WriteStyled(float value, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITE_TRAILER, value, styleSheet);
      }

      public static void WriteStyled(int value, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITE_TRAILER, value, styleSheet);
      }

      public static void WriteStyled(long value, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITE_TRAILER, value, styleSheet);
      }

      public static void WriteStyled(object value, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITE_TRAILER, value, styleSheet);
      }

      public static void WriteStyled(string value, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITE_TRAILER, value, styleSheet);
      }

      public static void WriteStyled(uint value, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITE_TRAILER, value, styleSheet);
      }

      public static void WriteStyled(ulong value, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITE_TRAILER, value, styleSheet);
      }

      public static void WriteStyled(string format, object arg0, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITE_TRAILER, format, arg0, styleSheet);
      }

      public static void WriteStyled(StyleSheet styleSheet, string format, params object[] args)
      {
         WriteInColorStyled(WRITE_TRAILER, format, args, styleSheet);
      }

      public static void WriteStyled(char[] buffer, int index, int count, StyleSheet styleSheet)
      {
         WriteChunkInColorStyled(WRITE_TRAILER, buffer, index, count, styleSheet);
      }

      public static void WriteStyled(string format, object arg0, object arg1, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITE_TRAILER, format, arg0, arg1, styleSheet);
      }

      public static void WriteStyled(string format, object arg0, object arg1, object arg2, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITE_TRAILER, format, arg0, arg1, arg2, styleSheet);
      }

      public static void WriteStyled(string format, object arg0, object arg1, object arg2, object arg3, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITE_TRAILER, format, new[] { arg0, arg1, arg2, arg3 }, styleSheet);
      }

      public static void WriteWithGradient<T>(IEnumerable<T> input, Color startColor, Color endColor, int maxColorsInGradient = MAX_COLOR_CHANGES)
      {
         DoWithGradient(Write, input, startColor, endColor, maxColorsInGradient);
      }

      #endregion

      #region Methods

      private static void Console_CancelKeyPress(object sender, ConsoleCancelEventArgs e)
      {
         CancelKeyPress.Invoke(sender, e);
      }

      #endregion

#if !NET40
      public static bool IsErrorRedirected => Console.IsErrorRedirected;

      public static bool IsInputRedirected => Console.IsInputRedirected;

      public static bool IsOutputRedirected => Console.IsOutputRedirected;

      public static IColoredConsole Instance
      {
         get
         {
            return instance ?? (instance = new ColoredConsole());
         }
      }
#endif
   }
}