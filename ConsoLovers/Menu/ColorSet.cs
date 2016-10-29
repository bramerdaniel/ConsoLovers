// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColorSet.cs" company="ConsoLovers">
//   Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.Menu
{
   using System;

   public class ColorSet
   {
      #region Public Properties

      public ConsoleColor Background { get; set; } = ConsoleColor.Black;

      public ConsoleColor DisabledBackground { get; set; } = ConsoleColor.Black;

      public ConsoleColor DisabledForeground { get; set; } = ConsoleColor.DarkGray;

      public ConsoleColor DisabledSelectedBackground { get; set; } = ConsoleColor.DarkGray;

      public ConsoleColor DisabledSelectedForeground { get; set; } = ConsoleColor.Gray;

      public ConsoleColor Foreground { get; set; } = ConsoleColor.White;

      public ConsoleColor SelectedBackground { get; set; } = ConsoleColor.Gray;

      public ConsoleColor SelectedForeground { get; set; } = ConsoleColor.Black;

      #endregion

      #region Public Methods and Operators

      public ConsoleColor GetBackground(bool isSelected, bool disabled)
      {
         if (isSelected)
            return disabled ? DisabledSelectedBackground : SelectedBackground;

         return disabled ? DisabledBackground : Background;
      }

      public ConsoleColor GetForeground(bool isSelected, bool disabled)
      {
         if (isSelected)
            return disabled ? DisabledSelectedForeground : SelectedForeground;

         return disabled ? DisabledForeground : Foreground;
      }

      #endregion
   }
}