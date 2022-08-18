﻿namespace ConsoLovers.ConsoleToolkit.Console
{
   using System;
   using System.Collections.Generic;

   /// <summary>
    /// Exposes methods and properties representing a pattern.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Pattern<T> : IEquatable<Pattern<T>>
    {
        /// <summary>
        /// The value, or definition, of the pattern.
        /// </summary>
        public T Value { get; }

        /// <summary>
        /// Exposes methods and properties representing a pattern.
        /// </summary>
        /// <param name="pattern">The value, or definition, of the pattern.</param>
        protected Pattern(T pattern)
        {
            Value = pattern;
        }

        /// <summary>
        /// Finds matches between the Pattern instance and a given object.
        /// </summary>
        /// <param name="input">The object to which the Pattern instance should be compared.</param>
        /// <returns>Returns a collection of the locations in the object under comparison that
        /// match the Pattern instance.</returns>
        public abstract IEnumerable<MatchLocation> GetMatches(T input);

        public bool Equals(Pattern<T> other)
        {
            if (other == null)
            {
                return false;
            }

            return Value.Equals(other.Value);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Pattern<T>);
        }

        public override int GetHashCode()
        {
            int hash = 163;

            hash *= 79 + Value.GetHashCode();

            return hash;
        }
    }
}
