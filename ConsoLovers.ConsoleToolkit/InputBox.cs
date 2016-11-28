// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InputMask.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit
{
   using System;
   using System.Text;

   using ConsoLovers.ConsoleToolkit.Console;
   using ConsoLovers.ConsoleToolkit.Contracts;

   /// <summary>Helper class for creating a</summary>
   public class InputBox : IInputBox
   {
      public InputLabel Label { get; set; }

      public IConsole Console { get; }

      #region Constants and Fields

      public InputBox()
         : this(new ConsoleProxy())
      {
      }

      public InputBox(string label)
         : this(new ConsoleProxy())
      {
         Label = new InputLabel(label);
      }

      public InputBox(string label, string initialValue)
         : this(label)
      {
         InitialValue = initialValue;
      }

      internal InputBox(IConsole console)
      {
         Console = console;
      }

      public string InitialValue { get; set; }

      private readonly int defaultLength = 0;

      private readonly char defaultPlaceHolder = ' ';

      private Func<ConsoleKeyInfo, bool> isValidInput;

      private Func<ConsoleKeyInfo, string, bool> maskingCompleted;

      private ConsoleColor originalForeground;

      private ConsoleColor originalBackground;

      #endregion

      #region IInputMask Members

      /// <summary>Reads the following input with the default mask of *.</summary>
      /// <returns>The input as string</returns>
      public string Read()
      {
         return ReadInternal(defaultLength, defaultPlaceHolder, false);
      }

      /// <summary>Reads the following input with the default mask of * and inserts a newline.</summary>
      /// <returns>The input as string</returns>
      public string ReadLine()
      {
         return ReadInternal(defaultLength, defaultPlaceHolder, true);
      }

      #endregion

      #region Public Properties

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

      public ConsoleColor Background { get; set; } = ConsoleColor.Black;
      public ConsoleColor Foreground { get; set; } = ConsoleColor.Gray;

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

      /// <summary>Reads the following input with the given mask and inserts a newline.</summary>
      /// <param name="length">The allowed length of the masked input.</param>
      /// <param name="placeHolder">The place holder that should be used for remaining input length.</param>
      /// <returns>The input as string</returns>
      public string Read(int length, char placeHolder)
      {
         return ReadInternal(length, placeHolder, false);
      }

      /// <summary>Reads the following input with the given mask and inserts a newline.</summary>
      /// <param name="length">The allowed length of the masked input.</param>
      /// <returns>The input as string</returns>
      public string Read(int length)
      {
         return ReadInternal(length, defaultPlaceHolder, false);
      }

      /// <summary>Reads the following input with the given mask and inserts a newline.</summary>
      /// <param name="length">The allowed length of the masked input.</param>
      /// <param name="placeHolder">The place holder that should be used for remaining input length.</param>
      /// <returns>The input as string</returns>
      public string ReadLine(int length, char placeHolder)
      {
         return ReadInternal(length, placeHolder, true);
      }

      /// <summary>Reads the following input with the given mask and inserts a newline.</summary>
      /// <param name="length">The allowed length of the masked input.</param>
      /// <returns>The input as string</returns>
      public string ReadLine(int length)
      {
         return ReadInternal(length, defaultPlaceHolder, true);
      }

      #endregion

      #region Methods

      private void ClearCharacters(int positions, int leftPadding = 0, char clear = ' ')
      {
         var abs = Math.Abs(positions);

         MoveCursor(-abs, leftPadding);
         Console.Write(clear.ToString().PadRight(abs));
         MoveCursor(-abs, leftPadding);
      }

      private static bool CompleteWithEnterKey(ConsoleKeyInfo key, string input)
      {
         return key.Key == ConsoleKey.Enter;
      }

      private void MoveCursor(int positions, int leftPadding = 0)
      {
         var expectedLeft = Console.CursorLeft + positions;
         var allowedMoves = Math.Max(expectedLeft, leftPadding);
         Console.SetCursorPosition(allowedMoves, Console.CursorTop);
      }

      private static bool NoControlCharacter(ConsoleKeyInfo key)
      {
         return !char.IsControl(key.KeyChar);
      }

      private string ReturnResult(bool newline, StringBuilder resultBuilder)
      {
         Console.ForegroundColor = originalForeground;
         Console.BackgroundColor = originalBackground;

         if (newline)
            Console.WriteLine();

         return resultBuilder.ToString();
      }

      private string ReadInternal(int length, char placeHolder, bool newline)
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

            // This does not work yet
            //if (key.Key == ConsoleKey.LeftArrow)
            //{
            //   MoveCursor(-1, initialLeft);
            //   continue;
            //}

            //if (key.Key == ConsoleKey.RightArrow)
            //{
            //   MoveCursor(1, initialLeft);
            //   continue;
            //}

            if (IsValidInput(key))
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

      private void SetColors()
      {
         originalForeground = Console.ForegroundColor;
         originalBackground = Console.BackgroundColor;
         Console.BackgroundColor = Background;
         Console.ForegroundColor = Foreground;
      }

      #endregion
   }

   public class InputLabel
   {
      private readonly IConsole console;

      public string Text { get; set; }

      public InputLabel(string text)
         :this(ColoredConsole.Instance, text)
      {
      }

      public InputLabel(IConsole console, string text)
      {
         Text = text;
         this.console = console;
         Foreground = console.ForegroundColor;
         Background = console.BackgroundColor;
      }

      public ConsoleColor Background { get; set; }
      public ConsoleColor Foreground { get; set; }

      public void Print()
      {
         console.Write(Text, Foreground, Background);
      }
   }
}