// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InputRange.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit
{
   using System;
   using System.Diagnostics;
   using System.Text;

   [DebuggerDisplay("{Text} [Position={Position}, Length={Length}]")]
   internal class InputRange
   {
      #region Constants and Fields

      private readonly StringBuilder textBuilder;

      #endregion

      #region Constructors and Destructors

      public InputRange(string initialText)
         : this(initialText, Int32.MaxValue)
      {
      }

      public InputRange(string initialText, int maximalLength)
      {
         textBuilder = new StringBuilder(initialText);
         Position = initialText == null ? 0 : initialText.Length;

         if (maximalLength > 0)
            MaximalLength = maximalLength;
      }

      public InputRange()
         : this(string.Empty, int.MaxValue)
      {
      }

      #endregion

      #region Public Properties

      public int Length => textBuilder.Length;

      public int? MaximalLength { get; }

      public InsertionMode Mode { get; set; }

      public int Position { get; private set; }

      public string Text => textBuilder.ToString();

      #endregion

      #region Public Methods and Operators

      public bool End()
      {
         Position = Length;
         return true;
      }

      public bool Home()
      {
         Position = 0;
         return true;
      }

      public bool Insert(char character)
      {
         if (Mode == InsertionMode.Insert)
         {
            if (MaximalLength.HasValue && Length + 1 > MaximalLength)
               return false;

            textBuilder.Insert(Position, character);
            Position++;
            return true;
         }

         if (textBuilder.Length == Position)
         {
            if (MaximalLength.HasValue && Length + 1 > MaximalLength)
               return false;

            textBuilder.Append(character);
            Position++;
            return true;
         }

         textBuilder.Remove(Position, 1);
         textBuilder.Insert(Position, character);
         Position++;
         return true;
      }

      public bool Move(int offset)
      {
         var newPosition = Position + offset;
         if (newPosition < 0)
         {
            Position = 0;
            return false;
         }

         if (MaximalLength.HasValue && newPosition > MaximalLength)
            return false;

         if (newPosition > textBuilder.Length)
            return false;

         Position += offset;
         return true;
      }

      public bool Remove()
      {
         return RemoveInternal(1);
      }

      #endregion

      #region Methods

      private bool RemoveInternal(int length)
      {
         if (length < 0)
            throw new ArgumentOutOfRangeException(nameof(length), "Length to delete must be greater than zero");

         if (length == 0)
            return false;

         if (Mode == InsertionMode.Insert)
         {
            var positionAfterRemove = Position - length;
            if (positionAfterRemove < 0)
               return false;

            Position = positionAfterRemove;
            textBuilder.Remove(Position, length);

            return true;
         }

         if (textBuilder.Length <= Position)
            return false;

         textBuilder.Remove(Position, length);
         return true;
      }

      #endregion
   }
}