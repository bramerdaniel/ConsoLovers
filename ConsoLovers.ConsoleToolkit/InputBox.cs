// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InputBox.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit
{
   using System;
   using System.Globalization;

   using ConsoLovers.ConsoleToolkit.Contracts;

   /// <summary>Helper class for creating a</summary>
   /// <typeparam name="T">The type of the expected input</typeparam>
   /// <seealso cref="ConsoLovers.ConsoleToolkit.IInputBox"/>
   public class InputBox<T> : IInputBox
   {
      #region Constants and Fields

      private readonly int defaultLength = 0;

      private InputRange inputRange;

      private Func<ConsoleKeyInfo, bool> isValidInput;

      private Func<ConsoleKeyInfo, string, bool> maskingCompleted;

      private ConsoleColor originalBackground;

      private ConsoleColor originalForeground;

      #endregion

      #region Constructors and Destructors

      /// <inheritdoc/>
      /// <summary>Initializes a new instance of the <see cref="T:ConsoLovers.ConsoleToolkit.InputBox`1"/> class.</summary>
      public InputBox()
         : this(new ConsoleProxy())
      {
      }

      /// <inheritdoc/>
      /// <summary>Initializes a new instance of the <see cref="T:ConsoLovers.ConsoleToolkit.InputBox`1"/> class.</summary>
      /// <param name="label">The label.</param>
      public InputBox(string label)
         : this(new InputLabel(label))
      {
      }

      public InputBox(InputLabel label)
         : this(new ConsoleProxy())
      {
         Label = label;
      }

      /// <inheritdoc/>
      /// <summary>Initializes a new instance of the <see cref="T:ConsoLovers.ConsoleToolkit.InputBox`1"/> class.</summary>
      /// <param name="label">The label.</param>
      /// <param name="initialValue">The initial value.</param>
      public InputBox(string label, string initialValue)
         : this(label)
      {
         InitialValue = initialValue;
      }

      /// <summary>Initializes a new instance of the <see cref="InputBox{T}"/> class.</summary>
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

      /// <summary>Gets or sets a value indicating whether the input is a password.</summary>
      public bool IsPassword { get; set; }

      /// <summary>Gets or sets a function that is called to check if the pressed console key is valid in this context (default is !char.IsControl(key.KeyChar)).</summary>
      public Func<ConsoleKeyInfo, bool> IsValidInput
      {
         get => isValidInput ?? NoControlCharacter;

         set => isValidInput = value;
      }

      /// <summary>Gets or sets the label that is displayed on the left of the text to input.</summary>
      public InputLabel Label { get; set; }

      /// <summary>Gets or sets a function that checks if the masking completed (default when [ENTER] is pressed).</summary>
      public Func<ConsoleKeyInfo, string, bool> MaskingCompleted
      {
         get => maskingCompleted ?? CompleteWithEnterKey;

         set => maskingCompleted = value;
      }

      /// <summary>Gets or sets the password character.</summary>
      public char PasswordChar { get; set; } = '*';

      /// <summary>Gets or sets the placeholder character.</summary>
      public char PlaceholderChar { get; set; } = ' ';

      #endregion

      #region Public Methods and Operators

      /// <summary>Reads the following input with the default mask of *.</summary>
      /// <returns>The input as string</returns>
      public string Read()
      {
         return ReadInternal(defaultLength, false);
      }

      /// <summary>Reads the following input with the given mask and inserts a newline.</summary>
      /// <param name="length">The allowed length of the masked input.</param>
      /// <returns>The input as string</returns>
      public string Read(int length)
      {
         return ReadInternal(length, false);
      }

      /// <summary>Reads the following input with the default mask of * and inserts a newline.</summary>
      /// <returns>The input as value of type <see cref="T"/></returns>
      public T ReadLine()
      {
         return ReadLine(s => (T)Convert.ChangeType(s, typeof(T), CultureInfo.InvariantCulture));
      }

      /// <summary>Reads the following input with the default mask of * and inserts a newline.</summary>
      /// <param name="converterFunction">The converter function to get a value from the input string.</param>
      /// <returns>The input as value of type <see cref="T"/></returns>
      public T ReadLine(Func<string, T> converterFunction)
      {
         return ConvertedValue(defaultLength, converterFunction);
      }

      /// <summary>Reads the following input with the given mask and inserts a newline.</summary>
      /// <param name="length">The allowed length of the masked input.</param>
      /// <param name="converterFunction">The converter function to get a value from the input string.</param>
      /// <returns>The input as value of type <see cref="T"/></returns>
      public T ReadLine(int length, Func<string, T> converterFunction)
      {
         return ConvertedValue(length, converterFunction);
      }

      /// <summary>Reads the following input with the given mask and inserts a newline.</summary>
      /// <param name="length">The allowed length of the masked input.</param>
      /// <returns>The input as value of type <see cref="T"/></returns>
      public T ReadLine(int length)
      {
         return ReadLine(length, s => (T)Convert.ChangeType(s, typeof(T)));
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

      private T ConvertedValue(int length, Func<string, T> converterFunction)
      {
         while (true)
         {
            try
            {
               var stringValue = ReadInternal(length, true);
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

      private string ReadInternal(int length, bool newline)
      {
         Label?.Print();
         SetColors();

         var initialCursorSize = Console.CursorSize;
         var initialLeft = Console.CursorLeft;

         inputRange = new InputRange(InitialValue, length);

         Console.SetCursorPosition(initialLeft, Console.CursorTop);
         WriteText(InitialValue ?? string.Empty, length);
         Console.SetCursorPosition(initialLeft + inputRange.Position, Console.CursorTop);

         while (true)
         {
            var key = Console.ReadKey(true);
            if (MaskingCompleted(key, inputRange.Text))
            {
               Console.CursorSize = initialCursorSize;
               return ReturnResult(newline);
            }

            if (key.Key == ConsoleKey.Backspace)
            {
               var lengthBeforeDelete = inputRange.Length;
               if (inputRange.Remove())
               {
                  Console.SetCursorPosition(initialLeft, Console.CursorTop);
                  WriteText(inputRange.Text, lengthBeforeDelete);
                  Console.SetCursorPosition(initialLeft + inputRange.Position, Console.CursorTop);
               }

               continue;
            }

            if (key.Key == ConsoleKey.Insert)
            {
               inputRange.Mode = inputRange.Mode == InsertionMode.Delete ? InsertionMode.Insert : InsertionMode.Delete;
               Console.CursorSize = inputRange.Mode == InsertionMode.Delete ? 100 : initialCursorSize;
               continue;
            }

            if (key.Key == ConsoleKey.Home)
            {
               inputRange.Home();
               Console.SetCursorPosition(initialLeft, Console.CursorTop);
               continue;
            }

            if (key.Key == ConsoleKey.End)
            {
               inputRange.End();
               Console.SetCursorPosition(initialLeft + inputRange.Length, Console.CursorTop);
               continue;
            }

            if (key.Key == ConsoleKey.Delete)
            {
               var lengthBeforeDelete = inputRange.Length;
               if (inputRange.Move(1) && inputRange.Remove())
               {
                  Console.SetCursorPosition(initialLeft, Console.CursorTop);
                  WriteText(inputRange.Text, lengthBeforeDelete);
                  Console.SetCursorPosition(initialLeft + inputRange.Position, Console.CursorTop);
               }

               continue;
            }

            if (key.Key == ConsoleKey.LeftArrow)
            {
               if (inputRange.Move(-1))
                  MoveCursor(-1, initialLeft);

               continue;
            }

            if (key.Key == ConsoleKey.RightArrow)
            {
               if (inputRange.Move(1))
                  MoveCursor(1, initialLeft);

               continue;
            }

            if (IsValidInput(key) && ValidForType(key))
            {
               if (!inputRange.Insert(key.KeyChar))
                  continue;

               Console.SetCursorPosition(initialLeft, Console.CursorTop);
               WriteText(inputRange.Text, 0);
               Console.SetCursorPosition(initialLeft + inputRange.Position, Console.CursorTop);

               if (maskingCompleted != null && MaskingCompleted(key, inputRange.Text))
               {
                  Console.CursorSize = initialCursorSize;
                  return ReturnResult(newline);
               }
            }
         }
      }

      private string ReturnResult(bool newline)
      {
         Console.ForegroundColor = originalForeground;
         Console.BackgroundColor = originalBackground;

         if (newline)
            Console.WriteLine();

         return inputRange.Text;
      }

      private void SetColors()
      {
         originalForeground = Console.ForegroundColor;
         originalBackground = Console.BackgroundColor;
         Console.BackgroundColor = Background;
         Console.ForegroundColor = Foreground;
      }

      private bool ValidForType(ConsoleKeyInfo key)
      {
         if (typeof(T) == typeof(int))
            return char.IsDigit(key.KeyChar);

         if (typeof(T) == typeof(double))
            return char.IsDigit(key.KeyChar) || key.KeyChar == '.';

         if (typeof(T) == typeof(bool))
         {
            var lower = char.ToLower(key.KeyChar);
            return lower == 't' || lower == 'r' || lower == 'u' || lower == 'e' || lower == 'f' || lower == 'a' || lower == 'l' || lower == 's';
         }

         return true;
      }

      private void WriteText(string text, int paddingSize)
      {
         if (IsPassword)
         {
            Console.Write(string.Empty.PadRight(text.Length, PasswordChar).PadRight(paddingSize, PlaceholderChar));
         }
         else
         {
            Console.Write(text.PadRight(paddingSize));
         }
      }

      #endregion
   }
}