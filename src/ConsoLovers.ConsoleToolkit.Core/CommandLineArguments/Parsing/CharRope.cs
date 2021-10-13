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
        #region Private Fields

        private readonly string original;

        #endregion Private Fields

        #region Public Constructors

        public CharRope([NotNull] string original)
        {
            this.original = original ?? throw new ArgumentNullException(nameof(original));
        }

        #endregion Public Constructors

        #region Public Methods

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

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion Public Methods
    }
}