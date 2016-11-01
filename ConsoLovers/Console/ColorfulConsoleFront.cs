// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColorfulConsoleFront.cs" company="ConsoLovers">
//   Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Console
{
   using System;
   using System.Collections.Generic;
   using System.Drawing;
   using System.IO;
   using System.Text;

   using ConsoLovers.ConsoleToolkit.Contracts;

   /// <summary>Wraps around the System.ColoredConsole class, adding enhanced styling functionality.</summary>
   public partial class ColoredConsole : IColoredConsole
   {
      #region Constants and Fields

      private static IColoredConsole instance;

      #endregion

      #region Constructors and Destructors

      private ColoredConsole()
      {
         colorStore = GetColorStore();
         colorManagerFactory = new ColorManagerFactory();
         colorManager = colorManagerFactory.GetManager(colorStore, MAX_COLOR_CHANGES, INITIAL_COLOR_CHANGE_COUNT_VALUE);

         Console.CancelKeyPress += OnConsoleCancelKeyPress;
      }

      #endregion

      #region Public Events

      public event ConsoleCancelEventHandler CancelKeyPress = delegate { };

      #endregion

      #region IColoredConsole Members

      public void Clear(Color clearingColor)
      {
         Console.BackgroundColor = colorManager.GetConsoleColor(clearingColor);
         Console.Clear();
         Console.ResetColor();
      }

      #endregion

      #region IConsole Members

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

      public void Clear()
      {
         Console.Clear();
      }

      public ConsoleKeyInfo ReadKey()
      {
         return Console.ReadKey();
      }

      public void ResetColor()
      {
         Console.ResetColor();
      }

      public void SetCursorPosition(int left, int top)
      {
         Console.SetCursorPosition(left, top);
      }

      public void Write(string value)
      {
         Console.Write(value);
      }

      public void WriteLine()
      {
         Console.WriteLine();
      }

      public void WriteLine(string value)
      {
         Console.WriteLine(value);
      }

      #endregion

      #region Public Properties

      public static IColoredConsole Instance => instance ?? (instance = new ColoredConsole());

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

      public int BufferWidth
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

      public bool CapsLock => Console.CapsLock;

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

      public TextWriter Error => Console.Error;

      public TextReader In => Console.In;

      public Encoding InputEncoding
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

      public bool IsErrorRedirected => Console.IsErrorRedirected;

      public bool IsInputRedirected => Console.IsInputRedirected;

      public bool IsOutputRedirected => Console.IsOutputRedirected;

      public bool KeyAvailable => Console.KeyAvailable;

      public int LargestWindowHeight => Console.LargestWindowHeight;

      public int LargestWindowWidth => Console.LargestWindowWidth;

      public bool NumberLock => Console.NumberLock;

      public TextWriter Out => Console.Out;

      public Encoding OutputEncoding
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

      public string Title
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

      public bool TreatControlCAsInput
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

      public int WindowHeight
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

      public int WindowLeft
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

      #endregion

      #region Public Methods and Operators

      public void Beep(int frequency, int duration)
      {
         Console.Beep(frequency, duration);
      }

      public void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop)
      {
         Console.MoveBufferArea(sourceLeft, sourceTop, sourceWidth, sourceHeight, targetLeft, targetTop);
      }

      public void MoveBufferArea
         (int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop, char sourceChar, ConsoleColor sourceForeColor,
            ConsoleColor sourceBackColor)
      {
         Console.MoveBufferArea(sourceLeft, sourceTop, sourceWidth, sourceHeight, targetLeft, targetTop, sourceChar, sourceForeColor, sourceBackColor);
      }

      public Stream OpenStandardError()
      {
         return Console.OpenStandardError();
      }

#if !NETSTANDARD1_3
      public Stream OpenStandardError(int bufferSize)
      {
         return Console.OpenStandardError(bufferSize);
      }
#endif

      public Stream OpenStandardInput()
      {
         return Console.OpenStandardInput();
      }

#if !NETSTANDARD1_3
      public Stream OpenStandardInput(int bufferSize)
      {
         return Console.OpenStandardInput(bufferSize);
      }
#endif

      public Stream OpenStandardOutput()
      {
         return Console.OpenStandardOutput();
      }

#if !NETSTANDARD1_3
      public Stream OpenStandardOutput(int bufferSize)
      {
         return Console.OpenStandardOutput(bufferSize);
      }
#endif

      public int Read()
      {
         return Console.Read();
      }

      public ConsoleKeyInfo ReadKey(bool intercept)
      {
         return Console.ReadKey(intercept);
      }

      public string ReadLine()
      {
         return Console.ReadLine();
      }

      public void SetBufferSize(int width, int height)
      {
         Console.SetBufferSize(width, height);
      }

      public void SetError(TextWriter newError)
      {
         Console.SetError(newError);
      }

      public void SetIn(TextReader newIn)
      {
         Console.SetIn(newIn);
      }

      public void SetOut(TextWriter newOut)
      {
         Console.SetOut(newOut);
      }

      public void SetWindowPosition(int left, int top)
      {
         Console.SetWindowPosition(left, top);
      }

      public void SetWindowSize(int width, int height)
      {
         Console.SetWindowSize(width, height);
      }

      public void Write(bool value)
      {
         Console.Write(value);
      }

      public void Write(bool value, Color color)
      {
         WriteInColor(Console.Write, value, color);
      }

      public void Write(char value)
      {
         Console.Write(value);
      }

      public void Write(char value, Color color)
      {
         WriteInColor(Console.Write, value, color);
      }

      public void Write(char[] value)
      {
         Console.Write(value);
      }

      public void Write(char[] value, Color color)
      {
         WriteInColor(Console.Write, value, color);
      }

      public void Write(decimal value)
      {
         Console.Write(value);
      }

      public void Write(decimal value, Color color)
      {
         WriteInColor(Console.Write, value, color);
      }

      public void Write(double value)
      {
         Console.Write(value);
      }

      public void Write(double value, Color color)
      {
         WriteInColor(Console.Write, value, color);
      }

      public void Write(float value)
      {
         Console.Write(value);
      }

      public void Write(float value, Color color)
      {
         WriteInColor(Console.Write, value, color);
      }

      public void Write(int value)
      {
         Console.Write(value);
      }

      public void Write(int value, Color color)
      {
         WriteInColor(Console.Write, value, color);
      }

      public void Write(long value)
      {
         Console.Write(value);
      }

      public void Write(long value, Color color)
      {
         WriteInColor(Console.Write, value, color);
      }

      public void Write(object value)
      {
         Console.Write(value);
      }

      public void Write(object value, Color color)
      {
         WriteInColor(Console.Write, value, color);
      }

      public void Write(string value, Color color)
      {
         WriteInColor(Console.Write, value, color);
      }

      public void Write(uint value)
      {
         Console.Write(value);
      }

      public void Write(uint value, Color color)
      {
         WriteInColor(Console.Write, value, color);
      }

      public void Write(ulong value)
      {
         Console.Write(value);
      }

      public void Write(ulong value, Color color)
      {
         WriteInColor(Console.Write, value, color);
      }

      public void Write(string format, object arg0)
      {
         Console.WriteLine(format, arg0);
      }

      public void Write(string format, object arg0, Color color)
      {
         WriteInColor(Console.Write, format, arg0, color);
      }

      public void Write(string format, params object[] args)
      {
         Console.Write(format, args);
      }

      public void Write(string format, Color color, params object[] args)
      {
         WriteInColor(Console.Write, format, args, color);
      }

      public void Write(char[] buffer, int index, int count)
      {
         Console.Write(buffer, index, count);
      }

      public void Write(char[] buffer, int index, int count, Color color)
      {
         WriteChunkInColor(Console.Write, buffer, index, count, color);
      }

      public void Write(string format, object arg0, object arg1)
      {
         Console.Write(format, arg0, arg1);
      }

      public void Write(string format, object arg0, object arg1, Color color)
      {
         WriteInColor(Console.Write, format, arg0, arg1, color);
      }

      public void Write(string format, object arg0, object arg1, object arg2)
      {
         Console.Write(format, arg0, arg1, arg2);
      }

      public void Write(string format, object arg0, object arg1, object arg2, Color color)
      {
         WriteInColor(Console.Write, format, arg0, arg1, arg2, color);
      }

      public void Write(string format, object arg0, object arg1, object arg2, object arg3)
      {
         Console.Write(format, arg0, arg1, arg2, arg3);
      }

      public void Write(string format, object arg0, object arg1, object arg2, object arg3, Color color)
      {
         // NOTE: The Intellisense for this overload of System.ColoredConsole.Write is misleading, as the C# compiler
         //       actually resolves this overload to System.ColoredConsole.Write(string format, object[] args)!

         WriteInColor(Console.Write, format, new[] { arg0, arg1, arg2, arg3 }, color);
      }

      public void WriteAlternating(bool value, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.Write, value, alternator);
      }

      public void WriteAlternating(char value, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.Write, value, alternator);
      }

      public void WriteAlternating(char[] value, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.Write, value, alternator);
      }

      public void WriteAlternating(decimal value, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.Write, value, alternator);
      }

      public void WriteAlternating(double value, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.Write, value, alternator);
      }

      public void WriteAlternating(float value, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.Write, value, alternator);
      }

      public void WriteAlternating(int value, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.Write, value, alternator);
      }

      public void WriteAlternating(long value, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.Write, value, alternator);
      }

      public void WriteAlternating(object value, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.Write, value, alternator);
      }

      public void WriteAlternating(string value, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.Write, value, alternator);
      }

      public void WriteAlternating(uint value, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.Write, value, alternator);
      }

      public void WriteAlternating(ulong value, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.Write, value, alternator);
      }

      public void WriteAlternating(string format, object arg0, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.Write, format, arg0, alternator);
      }

      public void WriteAlternating(string format, ColorAlternator alternator, params object[] args)
      {
         WriteInColorAlternating(Console.Write, format, args, alternator);
      }

      public void WriteAlternating(char[] buffer, int index, int count, ColorAlternator alternator)
      {
         WriteChunkInColorAlternating(Console.Write, buffer, index, count, alternator);
      }

      public void WriteAlternating(string format, object arg0, object arg1, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.Write, format, arg0, arg1, alternator);
      }

      public void WriteAlternating(string format, object arg0, object arg1, object arg2, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.Write, format, arg0, arg1, arg2, alternator);
      }

      public void WriteAlternating(string format, object arg0, object arg1, object arg2, object arg3, ColorAlternator alternator)
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

      public void WriteAscii(string value, Color color)
      {
         WriteAscii(value, null, color);
      }

      public void WriteAscii(string value, FigletFont font, Color color)
      {
         WriteLine(GetFiglet(font).ToAscii(value).ConcreteValue, color);
      }

      public void WriteAsciiAlternating(string value, ColorAlternator alternator)
      {
         WriteAsciiAlternating(value, null, alternator);
      }

      public void WriteAsciiAlternating(string value, FigletFont font, ColorAlternator alternator)
      {
         foreach (var line in GetFiglet(font).ToAscii(value).ConcreteValue.Split('\n'))
         {
            WriteLineAlternating(line, alternator);
         }
      }

      public void WriteAsciiStyled(string value, StyleSheet styleSheet)
      {
         WriteAsciiStyled(value, null, styleSheet);
      }

      public void WriteAsciiStyled(string value, FigletFont font, StyleSheet styleSheet)
      {
         WriteLineStyled(GetFiglet(font).ToAscii(value), styleSheet);
      }

      public void WriteFormatted(string format, object arg0, Color styledColor, Color defaultColor)
      {
         WriteInColorFormatted(WRITE_TRAILER, format, arg0, styledColor, defaultColor);
      }

      public void WriteFormatted(string format, Formatter arg0, Color defaultColor)
      {
         WriteInColorFormatted(WRITE_TRAILER, format, arg0, defaultColor);
      }

      public void WriteFormatted(string format, Color styledColor, Color defaultColor, params object[] args)
      {
         WriteInColorFormatted(WRITE_TRAILER, format, args, styledColor, defaultColor);
      }

      public void WriteFormatted(string format, Color defaultColor, params Formatter[] args)
      {
         WriteInColorFormatted(WRITE_TRAILER, format, args, defaultColor);
      }

      public void WriteFormatted(string format, object arg0, object arg1, Color styledColor, Color defaultColor)
      {
         WriteInColorFormatted(WRITE_TRAILER, format, arg0, arg1, styledColor, defaultColor);
      }

      public void WriteFormatted(string format, Formatter arg0, Formatter arg1, Color defaultColor)
      {
         WriteInColorFormatted(WRITE_TRAILER, format, arg0, arg1, defaultColor);
      }

      public void WriteFormatted(string format, object arg0, object arg1, object arg2, Color styledColor, Color defaultColor)
      {
         WriteInColorFormatted(WRITE_TRAILER, format, arg0, arg1, arg2, styledColor, defaultColor);
      }

      public void WriteFormatted(string format, Formatter arg0, Formatter arg1, Formatter arg2, Color defaultColor)
      {
         WriteInColorFormatted(WRITE_TRAILER, format, arg0, arg1, arg2, defaultColor);
      }

      public void WriteFormatted(string format, object arg0, object arg1, object arg2, object arg3, Color styledColor, Color defaultColor)
      {
         WriteInColorFormatted(WRITE_TRAILER, format, new[] { arg0, arg1, arg2, arg3 }, styledColor, defaultColor);
      }

      public void WriteFormatted(string format, Formatter arg0, Formatter arg1, Formatter arg2, Formatter arg3, Color defaultColor)
      {
         WriteInColorFormatted(WRITE_TRAILER, format, new[] { arg0, arg1, arg2, arg3 }, defaultColor);
      }

      public void WriteLine(bool value)
      {
         Console.WriteLine(value);
      }

      public void WriteLine(bool value, Color color)
      {
         WriteInColor(Console.WriteLine, value, color);
      }

      public void WriteLine(char value)
      {
         Console.WriteLine(value);
      }

      public void WriteLine(char value, Color color)
      {
         WriteInColor(Console.WriteLine, value, color);
      }

      public void WriteLine(char[] value)
      {
         Console.WriteLine(value);
      }

      public void WriteLine(char[] value, Color color)
      {
         WriteInColor(Console.WriteLine, value, color);
      }

      public void WriteLine(decimal value)
      {
         Console.WriteLine(value);
      }

      public void WriteLine(decimal value, Color color)
      {
         WriteInColor(Console.WriteLine, value, color);
      }

      public void WriteLine(double value)
      {
         Console.WriteLine(value);
      }

      public void WriteLine(double value, Color color)
      {
         WriteInColor(Console.WriteLine, value, color);
      }

      public void WriteLine(float value)
      {
         Console.WriteLine(value);
      }

      public void WriteLine(float value, Color color)
      {
         WriteInColor(Console.WriteLine, value, color);
      }

      public void WriteLine(int value)
      {
         Console.WriteLine(value);
      }

      public void WriteLine(int value, Color color)
      {
         WriteInColor(Console.WriteLine, value, color);
      }

      public void WriteLine(long value)
      {
         Console.WriteLine(value);
      }

      public void WriteLine(long value, Color color)
      {
         WriteInColor(Console.WriteLine, value, color);
      }

      public void WriteLine(object value)
      {
         Console.WriteLine(value);
      }

      public void WriteLine(object value, Color color)
      {
         WriteInColor(Console.WriteLine, value, color);
      }

      public void WriteLine(string value, Color color)
      {
         WriteInColor(Console.WriteLine, value, color);
      }

      public void WriteLine(uint value)
      {
         Console.WriteLine(value);
      }

      public void WriteLine(uint value, Color color)
      {
         WriteInColor(Console.WriteLine, value, color);
      }

      public void WriteLine(ulong value)
      {
         Console.WriteLine(value);
      }

      public void WriteLine(ulong value, Color color)
      {
         WriteInColor(Console.WriteLine, value, color);
      }

      public void WriteLine(string format, object arg0)
      {
         Console.WriteLine(format, arg0);
      }

      public void WriteLine(string format, object arg0, Color color)
      {
         WriteInColor(Console.WriteLine, format, arg0, color);
      }

      public void WriteLine(string format, params object[] args)
      {
         Console.WriteLine(format, args);
      }

      public void WriteLine(string format, Color color, params object[] args)
      {
         WriteInColor(Console.WriteLine, format, args, color);
      }

      public void WriteLine(char[] buffer, int index, int count)
      {
         Console.WriteLine(buffer, index, count);
      }

      public void WriteLine(char[] buffer, int index, int count, Color color)
      {
         WriteChunkInColor(Console.WriteLine, buffer, index, count, color);
      }

      public void WriteLine(string format, object arg0, object arg1)
      {
         Console.WriteLine(format, arg0, arg1);
      }

      public void WriteLine(string format, object arg0, object arg1, Color color)
      {
         WriteInColor(Console.WriteLine, format, arg0, arg1, color);
      }

      public void WriteLine(string format, object arg0, object arg1, object arg2)
      {
         Console.WriteLine(format, arg0, arg1, arg2);
      }

      public void WriteLine(string format, object arg0, object arg1, object arg2, Color color)
      {
         WriteInColor(Console.WriteLine, format, arg0, arg1, arg2, color);
      }

      public void WriteLine(string format, object arg0, object arg1, object arg2, object arg3)
      {
         Console.WriteLine(format, arg0, arg1, arg2, arg3);
      }

      public void WriteLine(string format, object arg0, object arg1, object arg2, object arg3, Color color)
      {
         // NOTE: The Intellisense for this overload of System.ColoredConsole.WriteLine is misleading, as the C# compiler
         //       actually resolves this overload to System.ColoredConsole.WriteLine(string format, object[] args)!

         WriteInColor(Console.WriteLine, format, new[] { arg0, arg1, arg2, arg3 }, color);
      }

      public void WriteLineAlternating(ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.Write, WRITELINE_TRAILER, alternator);
      }

      public void WriteLineAlternating(bool value, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.WriteLine, value, alternator);
      }

      public void WriteLineAlternating(char value, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.WriteLine, value, alternator);
      }

      public void WriteLineAlternating(char[] value, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.WriteLine, value, alternator);
      }

      public void WriteLineAlternating(decimal value, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.WriteLine, value, alternator);
      }

      public void WriteLineAlternating(double value, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.WriteLine, value, alternator);
      }

      public void WriteLineAlternating(float value, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.WriteLine, value, alternator);
      }

      public void WriteLineAlternating(int value, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.WriteLine, value, alternator);
      }

      public void WriteLineAlternating(long value, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.WriteLine, value, alternator);
      }

      public void WriteLineAlternating(object value, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.WriteLine, value, alternator);
      }

      public void WriteLineAlternating(string value, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.WriteLine, value, alternator);
      }

      public void WriteLineAlternating(uint value, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.WriteLine, value, alternator);
      }

      public void WriteLineAlternating(ulong value, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.WriteLine, value, alternator);
      }

      public void WriteLineAlternating(string format, object arg0, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.WriteLine, format, arg0, alternator);
      }

      public void WriteLineAlternating(string format, ColorAlternator alternator, params object[] args)
      {
         WriteInColorAlternating(Console.WriteLine, format, args, alternator);
      }

      public void WriteLineAlternating(char[] buffer, int index, int count, ColorAlternator alternator)
      {
         WriteChunkInColorAlternating(Console.WriteLine, buffer, index, count, alternator);
      }

      public void WriteLineAlternating(string format, object arg0, object arg1, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.WriteLine, format, arg0, arg1, alternator);
      }

      public void WriteLineAlternating(string format, object arg0, object arg1, object arg2, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.WriteLine, format, arg0, arg1, arg2, alternator);
      }

      public void WriteLineAlternating(string format, object arg0, object arg1, object arg2, object arg3, ColorAlternator alternator)
      {
         WriteInColorAlternating(Console.WriteLine, format, new[] { arg0, arg1, arg2, arg3 }, alternator);
      }

      public void WriteLineFormatted(string format, object arg0, Color styledColor, Color defaultColor)
      {
         WriteInColorFormatted(WRITELINE_TRAILER, format, arg0, styledColor, defaultColor);
      }

      public void WriteLineFormatted(string format, Formatter arg0, Color defaultColor)
      {
         WriteInColorFormatted(WRITELINE_TRAILER, format, arg0, defaultColor);
      }

      public void WriteLineFormatted(string format, Color styledColor, Color defaultColor, params object[] args)
      {
         WriteInColorFormatted(WRITELINE_TRAILER, format, args, styledColor, defaultColor);
      }

      public void WriteLineFormatted(string format, Color defaultColor, params Formatter[] args)
      {
         WriteInColorFormatted(WRITELINE_TRAILER, format, args, defaultColor);
      }

      public void WriteLineFormatted(string format, object arg0, object arg1, Color styledColor, Color defaultColor)
      {
         WriteInColorFormatted(WRITELINE_TRAILER, format, arg0, arg1, styledColor, defaultColor);
      }

      public void WriteLineFormatted(string format, Formatter arg0, Formatter arg1, Color defaultColor)
      {
         WriteInColorFormatted(WRITELINE_TRAILER, format, arg0, arg1, defaultColor);
      }

      public void WriteLineFormatted(string format, object arg0, object arg1, object arg2, Color styledColor, Color defaultColor)
      {
         WriteInColorFormatted(WRITELINE_TRAILER, format, arg0, arg1, arg2, styledColor, defaultColor);
      }

      public void WriteLineFormatted(string format, Formatter arg0, Formatter arg1, Formatter arg2, Color defaultColor)
      {
         WriteInColorFormatted(WRITELINE_TRAILER, format, arg0, arg1, arg2, defaultColor);
      }

      public void WriteLineFormatted(string format, object arg0, object arg1, object arg2, object arg3, Color styledColor, Color defaultColor)
      {
         WriteInColorFormatted(WRITELINE_TRAILER, format, new[] { arg0, arg1, arg2, arg3 }, styledColor, defaultColor);
      }

      public void WriteLineFormatted(string format, Formatter arg0, Formatter arg1, Formatter arg2, Formatter arg3, Color defaultColor)
      {
         WriteInColorFormatted(WRITELINE_TRAILER, format, new[] { arg0, arg1, arg2, arg3 }, defaultColor);
      }

      public void WriteLineStyled(StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITE_TRAILER, WRITELINE_TRAILER, styleSheet);
      }

      public void WriteLineStyled(bool value, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITELINE_TRAILER, value, styleSheet);
      }

      public void WriteLineStyled(char value, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITELINE_TRAILER, value, styleSheet);
      }

      public void WriteLineStyled(char[] value, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITELINE_TRAILER, value, styleSheet);
      }

      public void WriteLineStyled(decimal value, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITELINE_TRAILER, value, styleSheet);
      }

      public void WriteLineStyled(double value, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITELINE_TRAILER, value, styleSheet);
      }

      public void WriteLineStyled(float value, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITELINE_TRAILER, value, styleSheet);
      }

      public void WriteLineStyled(int value, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITELINE_TRAILER, value, styleSheet);
      }

      public void WriteLineStyled(long value, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITELINE_TRAILER, value, styleSheet);
      }

      public void WriteLineStyled(StyledString value, StyleSheet styleSheet)
      {
         WriteAsciiInColorStyled(WRITELINE_TRAILER, value, styleSheet);
      }

      public void WriteLineStyled(string value, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITELINE_TRAILER, value, styleSheet);
      }

      public void WriteLineStyled(uint value, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITELINE_TRAILER, value, styleSheet);
      }

      public void WriteLineStyled(ulong value, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITELINE_TRAILER, value, styleSheet);
      }

      public void WriteLineStyled(string format, object arg0, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITELINE_TRAILER, format, arg0, styleSheet);
      }

      public void WriteLineStyled(StyleSheet styleSheet, string format, params object[] args)
      {
         WriteInColorStyled(WRITELINE_TRAILER, format, args, styleSheet);
      }

      public void WriteLineStyled(char[] buffer, int index, int count, StyleSheet styleSheet)
      {
         WriteChunkInColorStyled(WRITELINE_TRAILER, buffer, index, count, styleSheet);
      }

      public void WriteLineStyled(string format, object arg0, object arg1, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITELINE_TRAILER, format, arg0, arg1, styleSheet);
      }

      public void WriteLineStyled(string format, object arg0, object arg1, object arg2, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITELINE_TRAILER, format, arg0, arg1, arg2, styleSheet);
      }

      public void WriteLineStyled(string format, object arg0, object arg1, object arg2, object arg3, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITELINE_TRAILER, format, new[] { arg0, arg1, arg2, arg3 }, styleSheet);
      }

      public void WriteLineWithGradient<T>(IEnumerable<T> input, Color startColor, Color endColor, int maxColorsInGradient = MAX_COLOR_CHANGES)
      {
         DoWithGradient(WriteLine, input, startColor, endColor, maxColorsInGradient);
      }

      public void WriteStyled(bool value, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITE_TRAILER, value, styleSheet);
      }

      public void WriteStyled(char value, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITE_TRAILER, value, styleSheet);
      }

      public void WriteStyled(char[] value, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITE_TRAILER, value, styleSheet);
      }

      public void WriteStyled(decimal value, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITE_TRAILER, value, styleSheet);
      }

      public void WriteStyled(double value, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITE_TRAILER, value, styleSheet);
      }

      public void WriteStyled(float value, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITE_TRAILER, value, styleSheet);
      }

      public void WriteStyled(int value, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITE_TRAILER, value, styleSheet);
      }

      public void WriteStyled(long value, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITE_TRAILER, value, styleSheet);
      }

      public void WriteStyled(object value, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITE_TRAILER, value, styleSheet);
      }

      public void WriteStyled(string value, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITE_TRAILER, value, styleSheet);
      }

      public void WriteStyled(uint value, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITE_TRAILER, value, styleSheet);
      }

      public void WriteStyled(ulong value, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITE_TRAILER, value, styleSheet);
      }

      public void WriteStyled(string format, object arg0, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITE_TRAILER, format, arg0, styleSheet);
      }

      public void WriteStyled(StyleSheet styleSheet, string format, params object[] args)
      {
         WriteInColorStyled(WRITE_TRAILER, format, args, styleSheet);
      }

      public void WriteStyled(char[] buffer, int index, int count, StyleSheet styleSheet)
      {
         WriteChunkInColorStyled(WRITE_TRAILER, buffer, index, count, styleSheet);
      }

      public void WriteStyled(string format, object arg0, object arg1, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITE_TRAILER, format, arg0, arg1, styleSheet);
      }

      public void WriteStyled(string format, object arg0, object arg1, object arg2, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITE_TRAILER, format, arg0, arg1, arg2, styleSheet);
      }

      public void WriteStyled(string format, object arg0, object arg1, object arg2, object arg3, StyleSheet styleSheet)
      {
         WriteInColorStyled(WRITE_TRAILER, format, new[] { arg0, arg1, arg2, arg3 }, styleSheet);
      }

      public void WriteWithGradient<T>(IEnumerable<T> input, Color startColor, Color endColor, int maxColorsInGradient = MAX_COLOR_CHANGES)
      {
         DoWithGradient(Write, input, startColor, endColor, maxColorsInGradient);
      }

      #endregion

      #region Methods

      private void OnConsoleCancelKeyPress(object sender, ConsoleCancelEventArgs e)
      {
         CancelKeyPress.Invoke(sender, e);
      }

      #endregion
   }
}