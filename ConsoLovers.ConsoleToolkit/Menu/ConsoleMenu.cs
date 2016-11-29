// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleMenu.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Menu
{
   using System;

   public class ConsoleMenu : ConsoleMenuBase
   {
      #region Constants and Fields

      ConsoleColor sharedBackground = ConsoleColor.Black;

      private ConsoleColor sharedForeground = ConsoleColor.Gray;

      #endregion

      #region Methods

      protected override ConsoleColor GetConsoleBackground()
      {
         return sharedBackground;
      }

      protected override ConsoleColor GetExpanderBackground(bool isSelected, bool disabled, bool mouseOver)
      {
         return isSelected ? ConsoleColor.White : ConsoleColor.Black;
      }

      protected override ConsoleColor GetExpanderForeground(bool isSelected, bool disabled, bool mouseOver)
      {
         return GetSharedForeground(isSelected, disabled);
      }

      protected override ConsoleColor GetFooterBackground()
      {
         return sharedBackground;
      }

      protected override ConsoleColor GetFooterForeground()
      {
         return sharedForeground;
      }

      protected override ConsoleColor GetHeaderBackground()
      {
         return sharedBackground;
      }

      protected override ConsoleColor GetHeaderForeground()
      {
         return sharedForeground;
      }

      protected override ConsoleColor GetHintBackground(bool isSelected, bool disabled)
      {
         return ConsoleColor.Red;
      }

      protected override ConsoleColor GetHintForeground(bool isSelected, bool disabled)
      {
         return ConsoleColor.White;
      }

      protected override ConsoleColor GetMenuItemBackground(bool isSelected, bool disabled, bool mouseOver)
      {
         if (mouseOver && !isSelected)
            return GetMouseOverBackground();

         return isSelected ? ConsoleColor.White : sharedBackground;
      }

      protected override ConsoleColor GetMenuItemForeground(bool isSelected, bool disabled, bool mouseOver)
      {
         if (mouseOver)
            return GetMouseOverForeground();

         return GetSharedForeground(isSelected, disabled);
      }

      protected override ConsoleColor GetMouseOverBackground()
      {
         return ConsoleColor.Gray;
      }

      protected override ConsoleColor GetMouseOverForeground()
      {
         return ConsoleColor.Black;
      }

      protected override ConsoleColor GetSelectorBackground(bool isSelected, bool disabled, bool mouseOver)
      {
         if (mouseOver && !isSelected)
            return GetMouseOverBackground();

         return isSelected ? ConsoleColor.White : ConsoleColor.Black;
      }

      protected override ConsoleColor GetSelectorForeground(bool isSelected, bool disabled, bool mouseOver)
      {
         return ConsoleColor.Black;
      }

      private ConsoleColor GetSharedForeground(bool isSelected, bool disabled)
      {
         if (disabled)
            return ConsoleColor.DarkGray;

         return isSelected ? ConsoleColor.Black : ConsoleColor.Gray;
      }

      #endregion
   }
}