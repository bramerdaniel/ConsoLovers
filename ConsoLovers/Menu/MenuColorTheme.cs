// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MenuColorTheme.cs" company="ConsoLovers">
//   Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.Menu
{
   using System;

   public class MenuColorTheme
   {
      #region Public Properties

      public ConsoleColor ConsoleBackground { get; set; } = ConsoleColor.Black;

      public ColorSet Expander { get; set; } = new ColorSet
      {
         Foreground = ConsoleColor.White,
         Background = ConsoleColor.Black,
         DisabledBackground = ConsoleColor.Black,
         DisabledForeground = ConsoleColor.DarkGray,
         SelectedForeground = ConsoleColor.Black,
         SelectedBackground = ConsoleColor.Gray,
         DisabledSelectedForeground = ConsoleColor.Gray,
         DisabledSelectedBackground = ConsoleColor.DarkGray
      };

      public ConsoleColor FooterBackground { get; set; } = ConsoleColor.Black;

      public ConsoleColor FooterForeground { get; set; } = ConsoleColor.White;

      public ConsoleColor HeaderBackground { get; set; } = ConsoleColor.Black;

      public ConsoleColor HeaderForeground { get; set; } = ConsoleColor.White;

      /// <summary>Gets or sets the <see cref="ColorSet"/> of a normal, not seleted menu item.</summary>
      public ColorSet MenuItem { get; set; } = new ColorSet
      {
         Foreground = ConsoleColor.White,
         Background = ConsoleColor.Black,
         DisabledBackground = ConsoleColor.Black,
         DisabledForeground = ConsoleColor.DarkGray,
         SelectedForeground = ConsoleColor.Black,
         SelectedBackground = ConsoleColor.Gray,
         DisabledSelectedForeground = ConsoleColor.Gray,
         DisabledSelectedBackground = ConsoleColor.DarkGray
      };

      /// <summary>Gets or sets the <see cref="ColorSet"/> of the selector.</summary>
      public ColorSet Selector { get; set; } = new ColorSet
      {
         Foreground = ConsoleColor.White,
         Background = ConsoleColor.Black,
         DisabledBackground = ConsoleColor.Black,
         DisabledForeground = ConsoleColor.DarkGray
      };

      #endregion
   }
}