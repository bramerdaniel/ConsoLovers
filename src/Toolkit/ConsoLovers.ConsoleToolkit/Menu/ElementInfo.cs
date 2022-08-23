// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ElementInfo.cs" company="ConsoLovers">
//   Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Menu
{
   using System;

   internal class ElementInfo
   {
      #region Public Properties

      public bool Disabled { get; set; }

      public string Hint { get; set; }

      public string Identifier { get; set; }

      public string Indent { get; set; } = string.Empty;

      public string IndexString { get; set; }

      public bool? IsExpanded { get; set; }

      public bool IsSelected { get; set; }

      public bool IsMouseOver{ get; set; }

      public PrintableItem MenuItem { get; set; }

      public string Path { get; set; }

      public string Text { get; set; }

      public int Line { get; set; }

      public int Length { get; set; }

      public bool IsSelectable { get; set; }

      public ConsoleColor? Foreground { get; set; }

      public ConsoleColor? Background { get; set; }

      public ExpanderDescription Expander { get; set; }

      #endregion
   }
}