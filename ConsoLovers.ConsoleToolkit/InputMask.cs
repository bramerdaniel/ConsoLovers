// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InputMask.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit
{
   using System;
   using System.Text;

   /// <summary>Helper class for creating a</summary>
   public class InputMask : IInputMask
   {
      #region Constants and Fields

      private int defaultLength = 0;

      private char defaultMask = '*';

      private char defaultPlaceHolder = ' ';

      private Func<ConsoleKeyInfo, bool> isValidInput;

      private Func<ConsoleKeyInfo, string, bool> maskingCompleted;

      #endregion

      #region IInputMask Members

      /// <summary>Reads the following input with the default mask of *.</summary>
      /// <returns>The input as string</returns>
      public string Read()
      {
         return Read(defaultMask);
      }

      /// <summary>Reads the following input with the default mask of * and inserts a newline.</summary>
      /// <returns>The input as string</returns>
      public string ReadLine()
      {
         return ReadLine(defaultMask);
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

      /// <summary>Reads the following input with the given mask.</summary>
      /// <param name="mask">The mask to use.</param>
      /// <returns>The input as string</returns>
      public string Read(char mask)
      {
         return ReadInternal(mask, defaultLength, defaultPlaceHolder, false);
      }

      /// <summary>Reads the following input with the given mask and inserts a newline.</summary>
      /// <param name="length">The allowed length of the masked input.</param>
      /// <param name="placeHolder">The place holder that should be used for remaining input length.</param>
      /// <returns>The input as string</returns>
      public string Read(int length, char placeHolder)
      {
         return ReadInternal(defaultMask, length, placeHolder, false);
      }

      /// <summary>Reads the following input with the given mask and inserts a newline.</summary>
      /// <param name="mask">The mask to use.</param>
      /// <param name="length">The allowed length of the masked input.</param>
      /// <param name="placeHolder">The place holder that should be used for remaining input length.</param>
      /// <returns>The input as string</returns>
      public string Read(char mask, int length, char placeHolder)
      {
         return ReadInternal(mask, length, placeHolder, false);
      }

      /// <summary>Reads the following input with the given mask and inserts a newline.</summary>
      /// <param name="mask">The mask to use.</param>
      /// <returns>The input as string</returns>
      public string ReadLine(char mask)
      {
         return ReadInternal(mask, defaultLength, defaultPlaceHolder, true);
      }

      /// <summary>Reads the following input with the given mask and inserts a newline.</summary>
      /// <param name="length">The allowed length of the masked input.</param>
      /// <param name="placeHolder">The place holder that should be used for remaining input length.</param>
      /// <returns>The input as string</returns>
      public string ReadLine(int length, char placeHolder)
      {
         return ReadInternal(defaultMask, length, placeHolder, true);
      }

      /// <summary>Reads the following input with the given mask and inserts a newline.</summary>
      /// <param name="mask">The mask to use.</param>
      /// <param name="length">The allowed length of the masked input.</param>
      /// <param name="placeHolder">The place holder that should be used for remaining input length.</param>
      /// <returns>The input as string</returns>
      public string ReadLine(char mask, int length, char placeHolder)
      {
         return ReadInternal(mask, length, placeHolder, true);
      }

      #endregion

      #region Methods

      private static void ClearCharacters(int positions, int leftPadding = 0, char clear = ' ')
      {
         var abs = Math.Abs(positions);

         MoveCursor(-abs, leftPadding);
         System.Console.Write(clear.ToString().PadRight(abs));
         MoveCursor(-abs, leftPadding);
      }

      private static bool CompleteWithEnterKey(ConsoleKeyInfo key, string input)
      {
         return key.Key == ConsoleKey.Enter;
      }

      private static void MoveCursor(int positions, int leftPadding = 0)
      {
         var expectedLeft = System.Console.CursorLeft + positions;
         var allowedMoves = Math.Max(expectedLeft, leftPadding);
         System.Console.SetCursorPosition(allowedMoves, System.Console.CursorTop);
      }

      private static bool NoControlCharacter(ConsoleKeyInfo key)
      {
         return !char.IsControl(key.KeyChar);
      }

      private static string ReturnResult(bool newline, StringBuilder resultBuilder)
      {
         if (newline)
            System.Console.WriteLine();

         return resultBuilder.ToString();
      }

      private string ReadInternal(char mask, int length, char placeHolder, bool newline)
      {
         StringBuilder resultBuilder = new StringBuilder();

         var initialLeft = System.Console.CursorLeft;

         if (length > 0)
         {
            for (int i = 0; i < length; i++)
               System.Console.Write(placeHolder);

            MoveCursor(-length);
         }

         while (true)
         {
            var key = System.Console.ReadKey(true);
            if (MaskingCompleted(key, resultBuilder.ToString()))
               return ReturnResult(newline, resultBuilder);

            if (key.Key == ConsoleKey.Backspace)
            {
               ClearCharacters(1, initialLeft, placeHolder);
               resultBuilder.Length = Math.Max(resultBuilder.Length - 1, 0);
               continue;
            }

            if (key.Key == ConsoleKey.LeftArrow)
            {
               MoveCursor(-1, initialLeft);
               continue;
            }

            if (key.Key == ConsoleKey.RightArrow)
            {
               MoveCursor(1, initialLeft);
               continue;
            }

            if (IsValidInput(key))
            {
               if (length > 0 && length <= resultBuilder.Length)
                  continue;

               System.Console.Write(mask);
               resultBuilder.Append(key.KeyChar);

               if (maskingCompleted != null && MaskingCompleted(key, resultBuilder.ToString()))
                  return ReturnResult(newline, resultBuilder);
            }
         }
      }

      #endregion
   }
}