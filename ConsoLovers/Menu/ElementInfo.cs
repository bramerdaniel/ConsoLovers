// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ElementInfo.cs" company="ConsoLovers">
//   Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Menu
{
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

      public ConsoleMenuItem MenuItem { get; set; }

      public string Path { get; set; }

      public string Text { get; set; }

      #endregion
   }
}