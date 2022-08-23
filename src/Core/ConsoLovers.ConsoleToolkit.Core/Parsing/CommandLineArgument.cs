namespace ConsoLovers.ConsoleToolkit.Core
{
   using System.Diagnostics;

   /// <summary>Helper class containing information of the parsed command line arguments </summary>
   [DebuggerDisplay("{" + nameof(DebuggerString) + "}")]
   public class CommandLineArgument
   {
      #region Public Properties

      internal string DebuggerString => $"[{Index}] {Name}={Value}";

      /// <summary>Gets or sets the original string that was passed to the command line.</summary>
      public string OriginalString { get; set; }

      /// <summary>Gets or sets the index of the argument in the command line parameter string.</summary>
      public int Index { get; set; }

      /// <summary>Gets or sets the value.</summary>
      public string Value { get; set; }

      /// <summary>Gets or sets the name.</summary>
      public string Name { get; set; }

      /// <summary>Gets or sets a value indicating whether this <see cref="CommandLineArgument"/> was mapped.</summary>
      internal bool Mapped { get; set; }

      #endregion

      /// <summary>Determines whether the argument has an argument sign like / or -.</summary>
      public bool HasArgumentSign()
      {
         if (Name == null)
            return false;

         return OriginalString.Length > Name.Length;
      }
   }
}