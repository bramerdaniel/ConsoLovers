// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ElementColors.cs" company="KUKA Roboter GmbH">
//   Copyright (c) KUKA Roboter GmbH 2006 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers
{
   using System;

   public class ElementColors
   {
      #region Public Properties

      public ConsoleColor Background { get; set; } = ConsoleColor.Black;

      public ConsoleColor DisabledBackground { get; set; } = ConsoleColor.Black;

      public ConsoleColor DisabledForeground { get; set; } = ConsoleColor.DarkGray;

      public ConsoleColor Foreground { get; set; } = ConsoleColor.White;

      public ConsoleColor SelectedForeground { get; set; } = ConsoleColor.Black;

      public ConsoleColor SelectedBackground{ get; set; } = ConsoleColor.Gray;

      public ConsoleColor DisabledSelectedForeground { get; set; } = ConsoleColor.Gray;

      public ConsoleColor DisabledSelectedBackground{ get; set; } = ConsoleColor.DarkGray;

      public ConsoleColor GetForeground(bool isSelected, bool disabled)
      {
         if (isSelected)
            return disabled ? DisabledSelectedForeground : SelectedForeground;

         return disabled ? DisabledForeground : Foreground;
      }

      public ConsoleColor GetBackground(bool isSelected, bool disabled)
      {
         if (isSelected)
            return disabled ? DisabledSelectedBackground: SelectedBackground;

         return disabled ? DisabledBackground : Background;
      }

      #endregion
   }
}