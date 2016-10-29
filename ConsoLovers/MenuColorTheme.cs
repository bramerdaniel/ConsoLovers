// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MenuColorTheme.cs" company="KUKA Roboter GmbH">
//   Copyright (c) KUKA Roboter GmbH 2006 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers
{
   using System;

   public class MenuColorTheme
   {
      #region Public Properties


      /// <summary>Gets or sets the <see cref="ElementColors"/> of a normal, not seleted menu item.</summary>
      public ElementColors MenuItem { get; set; } = new ElementColors
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

      /// <summary>Gets or sets the <see cref="ElementColors"/> of the selector.</summary>
      public ElementColors Selector { get; set; } = new ElementColors
      {
         Foreground = ConsoleColor.White,
         Background = ConsoleColor.Black,
         DisabledBackground = ConsoleColor.Black,
         DisabledForeground = ConsoleColor.DarkGray
      };

      public ElementColors Expander { get; set; } = new ElementColors
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

      public ConsoleColor HeaderForeground { get; set; } = ConsoleColor.White;

      public ConsoleColor HeaderBackground { get; set; } = ConsoleColor.Black;

      public ConsoleColor FooterForeground { get; set; } = ConsoleColor.White;

      public ConsoleColor FooterBackground { get; set; } = ConsoleColor.Black;

      public ConsoleColor ConsoleBackground { get; set; } = ConsoleColor.Black;

      #endregion
   }
}