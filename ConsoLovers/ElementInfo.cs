// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ElementInfo.cs" company="KUKA Roboter GmbH">
//   Copyright (c) KUKA Roboter GmbH 2006 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers
{
   internal class ElementInfo
   {
      #region Public Properties

      public bool Disabled { get; set; }

      public string Hint { get; set; }

      public string Indent { get; set; } = string.Empty;

      public string Identifier { get; set; }

      public string IndexString { get; set; }

      public bool? IsExpanded { get; set; }

      public bool IsSelected { get; set; }

      public ConsoleMenuItem MenuItem { get; set; }

      public string Text { get; set; }

      public string Path { get; set; }

      #endregion
   }
}