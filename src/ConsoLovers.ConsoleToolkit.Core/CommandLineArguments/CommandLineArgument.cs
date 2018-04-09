namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
   using System.Diagnostics;

   [DebuggerDisplay("[{Index}] {Name}={Value}")]
   public class CommandLineArgument
   {
      #region Public Properties

      /// <summary>Gets or sets the index of the argument in the command line parameter string.</summary>
      public int Index { get; set; }

      /// <summary>Gets or sets the value.</summary>
      public string Value { get; set; }

      /// <summary>Gets or sets the name.</summary>
      public string Name { get; set; }

      #endregion
   }
}