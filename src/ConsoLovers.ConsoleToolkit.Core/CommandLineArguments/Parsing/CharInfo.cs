namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments.Parsing
{
    using System.Diagnostics;

    [DebuggerDisplay("{Previous} <- {Current} -> {Next}")]
    public class CharInfo
    {
        #region Private Fields

        private readonly bool insideQuotes;

        private readonly bool isFirst;
        private readonly bool isLast;

        #endregion Private Fields

        #region Public Constructors

        public CharInfo(char current, char? previous, char? next, bool insideQuotes)
        {
            Current = current;
            Previous = previous ?? char.MinValue;
            Next = next ?? char.MinValue;
            isFirst = previous == null;
            isLast = next == null;

            this.insideQuotes = insideQuotes;
        }

        #endregion Public Constructors

        #region Public Properties

        public char Current { get; }

        public char Next { get; }

        public char Previous { get; }

        #endregion Public Properties

        #region Public Methods

        public bool InsideQuotes()
        {
            return insideQuotes;
        }

        public bool IsEscaped()
        {
            return Previous == '\\';
        }

        public bool IsFirst()
        {
            return isFirst;
        }

        public bool IsLast()
        {
            return isLast;
        }

        public bool IsQuote()
        {
            return Current == '"';
        }

        public bool IsWhiteSpace()
        {
            return char.IsWhiteSpace(Current);
        }

        public override string ToString()
        {
            return Current.ToString();
        }

        #endregion Public Methods
    }
}