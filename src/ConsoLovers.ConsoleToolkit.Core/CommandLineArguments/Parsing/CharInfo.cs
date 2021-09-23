namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments.Parsing
{
    using System.Diagnostics;

    [DebuggerDisplay("{Previous} <- {Current} -> {Next}")]
    public class CharInfo
    {
        private readonly bool insideQuotes;

        private readonly bool isFirst;
        private readonly bool isLast;

        #region Constructors and Destructors

        public CharInfo(char current, char? previous, char? next, bool insideQuotes)
        {
            Current = current;
            Previous = previous ?? char.MinValue;
            Next = next ?? char.MinValue;
            isFirst = previous == null;
            isLast = next == null;

            this.insideQuotes = insideQuotes;
        }

        #endregion Constructors and Destructors

        #region Public Properties

        public char Current { get; }

        public char Next { get; }

        public char Previous { get; }

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

        #endregion Public Properties

        #region Public Methods and Operators

        public bool IsQuote()
        {
            return Current == '"';
        }

        #endregion Public Methods and Operators

        public bool IsEscaped()
        {
            return Previous == '\\';
        }

        public bool IsWhiteSpace()
        {
            return char.IsWhiteSpace(Current);
        }

        public override string ToString()
        {
            return Current.ToString();
        }
    }
}