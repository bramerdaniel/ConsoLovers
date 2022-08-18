// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PrintableItem.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Menu
{
   using System;

   public class PrintableItem
   {
      #region Constants and Fields

      private ConsoleMenuBase menu;

      #endregion

      #region Public Properties

      /// <summary>Gets or sets the background color of the printable item. If set, the background color of the specified theme will be ignored</summary>
      public ConsoleColor? Background { get; set; }

      /// <summary>Gets or sets the foreground color of the printable item. If set, the foreground color of the specified theme will be ignored</summary>
      public ConsoleColor? Foreground { get; set; }

      /// <summary>Gets the <see cref="ColoredConsoleMenu"/> the item is part of.</summary>
      public ConsoleMenuBase Menu
      {
         get => menu ?? Parent?.Menu;

         internal set => menu = value;
      }

      /// <summary>Gets the menu the item is part of.</summary>
      public PrintableItem Parent { get; internal set; }

      #endregion
   }
}