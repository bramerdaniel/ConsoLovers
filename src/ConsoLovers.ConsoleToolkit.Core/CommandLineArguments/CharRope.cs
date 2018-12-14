// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CharRope.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
   using System;
   using System.Collections;
   using System.Collections.Generic;
   using System.Diagnostics;

   using JetBrains.Annotations;

   public class CharRope : IEnumerable<CharInfo>
   {
      #region Constants and Fields

      private readonly string original;

      #endregion

      #region Constructors and Destructors

      public CharRope([NotNull] string original)
      {
         this.original = original ?? throw new ArgumentNullException(nameof(original));
      }

      #endregion

      #region IEnumerable Members

      IEnumerator IEnumerable.GetEnumerator()
      {
         return GetEnumerator();
      }

      #endregion

      #region IEnumerable<CharInfo> Members

      public IEnumerator<CharInfo> GetEnumerator()
      {
         bool insideQuotes = false;
         for (int i = 0; i < original.Length; i++)
         {
            var current = original[i];
            var previous = i == 0 ? (char?)null : original[i - 1];
            var next = i + 1 == original.Length ? (char?)null : original[i + 1];
            
            if (current == '"' && previous != '\\')
               insideQuotes = !insideQuotes;

            yield return new CharInfo(current, previous, next, insideQuotes);
         }
      }

      #endregion
   }

   [DebuggerDisplay("{Previous} <- {Current} -> {Next}")]
   public class CharInfo
   {
      private readonly bool insideQuotes;

      private readonly bool isLast;

      private readonly bool isFirst;

      #region Constructors and Destructors

      public CharInfo(char current, char? previous, char? next, bool insideQuotes)
      {
         Current = current;
         Previous = previous ?? char.MinValue;
         Next = next ?? char.MinValue;
         isFirst = previous == null;
         isLast= next== null;

         this.insideQuotes = insideQuotes;
      }

      #endregion

      #region Public Properties

      public char Current { get; }

      public char Next { get; }

      public bool InsideQuotes()
      {
         return insideQuotes;
      }

      public bool IsFirst()
      {
         return isFirst;
      }

      public bool IsLast()
      {
         return isLast;
      }

      public char Previous { get; }

      #endregion

      #region Public Methods and Operators

      public bool IsQuote()
      {
         return Current == '"';
      }

      #endregion

      public bool IsWhiteSpace()
      {
         return char.IsWhiteSpace(Current);
      }

      public override string ToString()
      {
         return Current.ToString();
      }

      public bool IsEscaped()
      {
         return Previous == '\\';
      }
   }
}