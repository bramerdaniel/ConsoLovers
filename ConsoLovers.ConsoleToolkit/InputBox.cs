// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InputBox.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit
{
   using System;
   using System.Text;

   using ConsoLovers.ConsoleToolkit.Contracts;

   /// <summary>Helper class for creating a</summary>
   public class InputBox : IInputBox
   {
      #region Constants and Fields

      private readonly int defaultLength = 0;

      private readonly char defaultPlaceHolder = ' ';

      private Func<ConsoleKeyInfo, bool> isValidInput;

      private Func<ConsoleKeyInfo, string, bool> maskingCompleted;

      private ConsoleColor originalBackground;

      private ConsoleColor originalForeground;

      #endregion

      #region Constructors and Destructors

      /// <summary>Initializes a new instance of the <see cref="InputBox"/> class.</summary>
      public InputBox()
         : this(new ConsoleProxy())
      {
      }

      /// <summary>Initializes a new instance of the <see cref="InputBox"/> class.</summary>
      /// <param name="label">The label.</param>
      public InputBox(string label)
         : this(new ConsoleProxy())
      {
         Label = new InputLabel(label);
      }

      /// <summary>Initializes a new instance of the <see cref="InputBox"/> class.</summary>
      /// <param name="label">The label.</param>
      /// <param name="initialValue">The initial value.</param>
      public InputBox(string label, string initialValue)
         : this(label)
      {
         InitialValue = initialValue;
      }

      /// <summary>Initializes a new instance of the <see cref="InputBox"/> class.</summary>
      /// <param name="console">The console.</param>
      internal InputBox(IConsole console)
      {
         Console = console;
      }

      #endregion

      #region Public Properties

      /// <summary>Gets or sets the background of the input text.</summary>
      public ConsoleColor Background { get; set; } = ConsoleColor.Black;

      /// <summary>Gets the console that is used.</summary>
      public IConsole Console { get; }

      /// <summary>Gets or sets the foreground of the input text.</summary>
      public ConsoleColor Foreground { get; set; } = ConsoleColor.Gray;

      /// <summary>Gets or sets the initial value.</summary>
      public string InitialValue { get; set; }

      /// <summary>Gets or sets a function that is called to check if the pressed console key is valid in this context (default is !char.IsControl(key.KeyChar)).</summary>
      public Func<ConsoleKeyInfo, bool> IsValidInput
      {
         get
         {
            return isValidInput ?? NoControlCharacter;
         }

         set
         {
            isValidInput = value;
         }
      }

      /// <summary>Gets or sets the label that is displayed on the left of the text to input.</summary>
      public InputLabel Label { get; set; }

      /// <summary>Gets or sets a function that checks if the masking completed (default when [ENTER] is pressed).</summary>
      public Func<ConsoleKeyInfo, string, bool> MaskingCompleted
      {
         get
         {
            return maskingCompleted ?? CompleteWithEnterKey;
         }

         set
         {
            maskingCompleted = value;
         }
      }

      #endregion

      #region Public Methods and Operators

      /// <summary>Reads the following input with the default mask of *.</summary>
      /// <returns>The input as string</returns>
      public string Read()
      {
         return ReadInternal<string>(defaultLength, defaultPlaceHolder, false);
      }

      /// <summary>Reads the following input with the given mask and inserts a newline.</summary>
      /// <param name="length">The allowed length of the masked input.</param>
      /// <param name="placeHolder">The place holder that should be used for remaining input length.</param>
      /// <returns>The input as string</returns>
      public string Read(int length, char placeHolder)
      {
         return ReadInternal<string>(length, placeHolder, false);
      }

      /// <summary>Reads the following input with the given mask and inserts a newline.</summary>
      /// <param name="length">The allowed length of the masked input.</param>
      /// <returns>The input as string</returns>
      public string Read(int length)
      {
         return ReadInternal<string>(length, defaultPlaceHolder, false);
      }

      /// <summary>Reads the following input with the default mask of * and inserts a newline.</summary>
      /// <typeparam name="T">The type of the expected return value</typeparam>
      /// <returns>The input as value of type <see cref="T"/></returns>
      public T ReadLine<T>()
      {
         return ReadLine(s => (T)Convert.ChangeType(s, typeof(T)));
      }

      /// <summary>Reads the following input with the default mask of * and inserts a newline.</summary>
      /// <typeparam name="T">The type of the expected return value</typeparam>
      /// <param name="converterFunction">The converter function to get a value from the input string.</param>
      /// <returns>The input as value of type <see cref="T"/></returns>
      public T ReadLine<T>(Func<string, T> converterFunction)
      {
         return ConvertedValue(defaultLength, defaultPlaceHolder, converterFunction);
      }

      /// <summary>Reads the following input with the default mask of * and inserts a newline.</summary>
      /// <returns>The input as string</returns>
      public string ReadLine()
      {
         return ReadInternal<string>(defaultLength, defaultPlaceHolder, true);
      }

      /// <summary>Reads the following input with the given mask and inserts a newline.</summary>
      /// <typeparam name="T">The type of the expected return value</typeparam>
      /// <param name="length">The allowed length of the masked input.</param>
      /// <param name="placeHolder">The place holder that should be used for remaining input length.</param>
      /// <returns>The input as value of type <see cref="T"/></returns>
      public T ReadLine<T>(int length, char placeHolder)
      {
         return ReadLine(length, placeHolder, s => (T)Convert.ChangeType(s, typeof(T)));
      }

      /// <summary>Reads the following input with the given mask and inserts a newline.</summary>
      /// <typeparam name="T">The type of the expected return value</typeparam>
      /// <param name="length">The allowed length of the masked input.</param>
      /// <param name="placeHolder">The place holder that should be used for remaining input length.</param>
      /// <param name="converterFunction">The converter function to get a value from the input string.</param>
      /// <returns>The input as value of type <see cref="T"/></returns>
      public T ReadLine<T>(int length, char placeHolder, Func<string, T> converterFunction)
      {
         return ConvertedValue(length, placeHolder, converterFunction);
      }

      /// <summary>Reads the following input with the given mask and inserts a newline.</summary>
      /// <param name="length">The allowed length of the masked input.</param>
      /// <param name="placeHolder">The place holder that should be used for remaining input length.</param>
      /// <returns>The input as string</returns>
      public string ReadLine(int length, char placeHolder)
      {
         return ReadInternal<string>(length, placeHolder, true);
      }

      /// <summary>Reads the following input with the given mask and inserts a newline.</summary>
      /// <typeparam name="T">The type of the expected return value</typeparam>
      /// <param name="length">The allowed length of the masked input.</param>
      /// <returns>The input as value of type <see cref="T"/></returns>
      public T ReadLine<T>(int length)
      {
         return ReadLine(length, s => (T)Convert.ChangeType(s, typeof(T)));
      }

      /// <summary>Reads the following input with the given mask and inserts a newline.</summary>
      /// <typeparam name="T">The type of the expected return value</typeparam>
      /// <param name="length">The allowed length of the masked input.</param>
      /// <param name="converterFunction">The converter function to get a value from the input string.</param>
      /// <returns>The input as value of type <see cref="T"/></returns>
      public T ReadLine<T>(int length, Func<string, T> converterFunction)
      {
         return ConvertedValue(length, defaultPlaceHolder, converterFunction);
      }

      #endregion

      #region Methods

      private static bool CompleteWithEnterKey(ConsoleKeyInfo key, string input)
      {
         return key.Key == ConsoleKey.Enter;
      }

      private static bool NoControlCharacter(ConsoleKeyInfo key)
      {
         return !char.IsControl(key.KeyChar);
      }

      private void ClearCharacters(int positions, int leftPadding = 0, char clear = ' ')
      {
         var abs = Math.Abs(positions);

         MoveCursor(-abs, leftPadding);
         Console.Write(clear.ToString().PadRight(abs));
         MoveCursor(-abs, leftPadding);
      }

      private T ConvertedValue<T>(int length, char placeHolder, Func<string, T> converterFunction)
      {
         while (true)
         {
            try
            {
               var stringValue = ReadInternal<T>(length, placeHolder, true);
               var value = converterFunction(stringValue);

               return value;
            }
            catch
            {
               Console.Clear();
            }
         }
      }

      private void MoveCursor(int positions, int leftPadding = 0)
      {
         var expectedLeft = Console.CursorLeft + positions;
         var allowedMoves = Math.Max(expectedLeft, leftPadding);
         Console.SetCursorPosition(allowedMoves, Console.CursorTop);
      }

      private string ReadInternal<T>(int length, char placeHolder, bool newline)
      {
         Label?.Print();

         SetColors();
         StringBuilder resultBuilder = new StringBuilder(InitialValue ?? string.Empty);

         var initialLeft = Console.CursorLeft;

         if (length > 0)
         {
            for (int i = 0; i < length; i++)
               Console.Write(placeHolder);

            MoveCursor(-length);
         }

         if (!string.IsNullOrEmpty(InitialValue))
            Console.Write(InitialValue);

         while (true)
         {
            var key = Console.ReadKey(true);
            if (MaskingCompleted(key, resultBuilder.ToString()))
               return ReturnResult(newline, resultBuilder);

            if (key.Key == ConsoleKey.Backspace)
            {
               ClearCharacters(1, initialLeft, placeHolder);
               resultBuilder.Length = Math.Max(resultBuilder.Length - 1, 0);
               continue;
            }

            if (IsValidInput(key) && ValidForType<T>(key))
            {
               if (length > 0 && length <= resultBuilder.Length)
                  continue;

               Console.Write(key.KeyChar);
               resultBuilder.Append(key.KeyChar);

               if (maskingCompleted != null && MaskingCompleted(key, resultBuilder.ToString()))
                  return ReturnResult(newline, resultBuilder);
            }
         }
      }

      private bool ValidForType<T>(ConsoleKeyInfo key)
      {
         if (typeof(T) == typeof(int))
            return char.IsDigit(key.KeyChar);

         return true;
      }

      private string ReturnResult(bool newline, StringBuilder resultBuilder)
      {
         Console.ForegroundColor = originalForeground;
         Console.BackgroundColor = originalBackground;

         if (newline)
            Console.WriteLine();

         return resultBuilder.ToString();
      }

      private void SetColors()
      {
         originalForeground = Console.ForegroundColor;
         originalBackground = Console.BackgroundColor;
         Console.BackgroundColor = Background;
         Console.ForegroundColor = Foreground;
      }

      #endregion
   }
}