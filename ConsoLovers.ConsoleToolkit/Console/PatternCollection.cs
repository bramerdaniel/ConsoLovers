// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PatternCollection.cs" company="ConsoLovers">
//   Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Console
{
   using System.Collections.Generic;

   /// <summary>Represents a collection of Pattern objects.</summary>
   /// <typeparam name="T"></typeparam>
   public abstract class PatternCollection<T> : IPrototypable<PatternCollection<T>>
   {
      #region Constants and Fields

      protected List<Pattern<T>> patterns = new List<Pattern<T>>();

      #endregion

      #region IPrototypable<PatternCollection<T>> Members

      public PatternCollection<T> Prototype()
      {
         return PrototypeCore();
      }

      #endregion

      #region Public Methods and Operators

      /// <summary>Attempts to match any of the PatternCollection's member Patterns against a string input.</summary>
      /// <param name="input">The input against which Patterns will potentially be matched.</param>
      /// <returns>Returns 'true' if any of the PatternCollection's member Patterns matches against the input string.</returns>
      public abstract bool MatchFound(string input);

      #endregion

      #region Methods

      protected abstract PatternCollection<T> PrototypeCore();

      #endregion
   }
}