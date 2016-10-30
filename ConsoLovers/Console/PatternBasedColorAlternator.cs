// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PatternBasedColorAlternator.cs" company="ConsoLovers">
//   Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.Console
{
   using System;
   using System.Drawing;
   using System.Linq;

   /// <summary>Exposes methods and properties used for alternating over a set of colors according to the occurrences of patterns.</summary>
   /// <typeparam name="T"></typeparam>
   public sealed class PatternBasedColorAlternator<T> : ColorAlternator, IPrototypable<PatternBasedColorAlternator<T>>
   {
      #region Constants and Fields

      private readonly PatternCollection<T> patternMatcher;

      private bool isFirstRun = true;

      #endregion

      #region Constructors and Destructors

      /// <summary>Exposes methods and properties used for alternating over a set of colors according to the occurrences of patterns.</summary>
      /// <param name="patternMatcher">The PatternMatcher instance which will dictate what will need to happen in order for the color to alternate.</param>
      /// <param name="colors">The set of colors over which to alternate.</param>
      public PatternBasedColorAlternator(PatternCollection<T> patternMatcher, params Color[] colors)
         : base(colors)
      {
         this.patternMatcher = patternMatcher;
      }

      #endregion

      #region IPrototypable<PatternBasedColorAlternator<T>> Members

      public new PatternBasedColorAlternator<T> Prototype()
      {
         return new PatternBasedColorAlternator<T>(patternMatcher.Prototype(), Colors.DeepCopy().ToArray());
      }

      #endregion

      #region Public Methods and Operators

      /// <summary>Alternates colors based on patterns matched in the input string.</summary>
      /// <param name="input">The string to be styled.</param>
      /// <returns>The current color of the ColorAlternator.</returns>
      public override Color GetNextColor(string input)
      {
         if (Colors.Length == 0)
         {
            throw new InvalidOperationException("No colors have been supplied over which to alternate!");
         }

         if (isFirstRun)
         {
            isFirstRun = false;
            return Colors[nextColorIndex];
         }

         if (patternMatcher.MatchFound(input))
         {
            TryIncrementColorIndex();
         }

         Color nextColor = Colors[nextColorIndex];

         return nextColor;
      }

      #endregion

      #region Methods

      protected override ColorAlternator PrototypeCore()
      {
         return Prototype();
      }

      protected override void TryIncrementColorIndex()
      {
         if (nextColorIndex >= Colors.Length - 1)
         {
            nextColorIndex = 0;
         }
         else
         {
            nextColorIndex++;
         }
      }

      #endregion
   }
}