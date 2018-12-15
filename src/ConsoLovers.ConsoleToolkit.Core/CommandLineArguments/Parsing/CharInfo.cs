namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments.Parsing
{
   using System.Diagnostics;

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