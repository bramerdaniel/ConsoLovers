// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CharRope.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments.Parsing
{
    using JetBrains.Annotations;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class CharRope : IEnumerable<CharInfo>
    {
        #region Constants and Fields

        private readonly string original;

        #endregion Constants and Fields

        #region Constructors and Destructors

        public CharRope([NotNull] string original)
        {
            this.original = original ?? throw new ArgumentNullException(nameof(original));
        }

        #endregion Constructors and Destructors

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion IEnumerable Members

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

        #endregion IEnumerable<CharInfo> Members
    }
}