﻿namespace ConsoLovers.ConsoleToolkit.Console
{
   using System;
   using System.Drawing;
   using System.Linq;

   /// <summary>
    /// Exposes methods and properties used for alternating over a set of colors according to
    /// frequency of use.
    /// </summary>
    public sealed class FrequencyBasedColorAlternator : ColorAlternator, IPrototypable<FrequencyBasedColorAlternator>
    {
        private readonly int alternationFrequency;
        private int writeCount;

        /// <summary>
        /// Exposes methods and properties used for alternating over a set of colors according to
        /// frequency of use.
        /// </summary>
        /// <param name="alternationFrequency">The number of times GetNextColor must be called in order for
        /// the color to alternate.</param>
        /// <param name="colors">The set of colors over which to alternate.</param>
        public FrequencyBasedColorAlternator(int alternationFrequency, params Color[] colors)
            : base(colors)
        {
            this.alternationFrequency = alternationFrequency;
        }

        public new FrequencyBasedColorAlternator Prototype()
        {
            return new FrequencyBasedColorAlternator(alternationFrequency, Colors.DeepCopy().ToArray());
        }

        protected override ColorAlternator PrototypeCore()
        {
            return Prototype();
        }

        /// <summary>
        /// Alternates colors based on the number of times GetNextColor has been called.
        /// </summary>
        /// <param name="input">The string to be styled.</param>
        /// <returns>The current color of the ColorAlternator.</returns>
        public override Color GetNextColor(string input)
        {
            if (Colors.Length == 0)
            {
                throw new InvalidOperationException("No colors have been supplied over which to alternate!");
            }

            Color nextColor = Colors[nextColorIndex];
            TryIncrementColorIndex();

            return nextColor;
        }

        protected override void TryIncrementColorIndex()
        {
            if (writeCount >= (Colors.Length * alternationFrequency) - 1)
            {
                nextColorIndex = 0;
                writeCount = 0;
            }
            else
            {
                writeCount++;
                nextColorIndex = (int)Math.Floor(writeCount / (double)alternationFrequency);
            }
        }
    }
}
