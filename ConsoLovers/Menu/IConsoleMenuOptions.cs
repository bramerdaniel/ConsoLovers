namespace ConsoLovers.ConsoleToolkit.Menu
{
   using System;

   using ConsoLovers.ConsoleToolkit.Contracts;

   public interface IConsoleMenuOptions
   {
      /// <summary>Gets or sets a value indicating whether the circular selection is enabled or not.</summary>
      bool CircularSelection { get; set; }

      bool ClearOnExecution { get; set; }

      bool ExecuteOnIndexSelection { get; set; }

      ExpanderDescription Expander { get; set; }

      /// <summary>Gets or sets the size of the indent that is used to indent child menu items.</summary>
      int IndentSize { get; set; }

      /// <summary>Gets or sets a value indicating whether the <see cref="ConsoleMenuItem"/>s should be displayed and be accessible with an index.</summary>
      bool IndexMenuItems { get; set; }

      /// <summary>Gets or sets the selection strech mode that is used for displaying the selection.</summary>
      SelectionStrech SelectionStrech { get; set; }

      /// <summary>Gets or sets the selector that is used for displaying the selection.</summary>
      string Selector { get; set; }

      /// <summary>Gets or sets the footer that is displayed below the menu.</summary>
      object Footer { get; set; }

      /// <summary>Gets or sets the header that is displayed.</summary>
      object Header { get; set; }

      ConsoleKey[] CloseKeys { get; set; }
   }
}