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

      ConsoleColor background = ConsoleColor.Black;

      #endregion

      #region Methods

      protected override ConsoleColor GetConsoleBackground()
      {
         return background;
      }

      protected override ConsoleColor GetExpanderBackground(bool isSelected, bool disabled, bool mouseOver)
      {
         return isSelected ? ConsoleColor.White : ConsoleColor.Black;
         // Theme.Expander.GetBackground(element.IsSelected, element.Disabled);
      }

      protected override ConsoleColor GetExpanderForeground(bool isSelected, bool disabled, bool mouseOver)
      {
         return GetSharedForeground(isSelected, disabled);
      }

      protected override ConsoleColor GetFooterBackground()
      {
         // return Theme.FooterBackground;
         return background;
      }

      protected override ConsoleColor GetFooterForeground()
      {
         return ConsoleColor.Green;
      }

      protected override ConsoleColor GetHeaderBackground()
      {
         return background;
      }

      protected override ConsoleColor GetHeaderForeground()
      {
         return ConsoleColor.Red;
         // return Theme.FooterForeground;
      }

      protected override ConsoleColor GetHintBackground(bool isSelected, bool disabled)
      {
         return ConsoleColor.Red;
         // Theme.Hint.GetBackground(menuItem.IsSelected, menuItem.Disabled
      }

      protected override ConsoleColor GetHintForeground(bool isSelected, bool disabled)
      {
         return ConsoleColor.Green;
         // Theme.Hint.GetForeground(menuItem.IsSelected, menuItem.Disabled)
      }

      protected override ConsoleColor GetMenuItemBackground(bool isSelected, bool disabled, bool mouseOver)
      {
         if (mouseOver)
            return GetMouseOverBackground();

         return isSelected ? ConsoleColor.White : ConsoleColor.Black;
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

      protected override ConsoleColor GetSelectorBackground(bool isSelected, bool disabled)
      {
         return isSelected ? ConsoleColor.White : ConsoleColor.Black;
      }

      protected override ConsoleColor GetSelectorForeground(bool isSelected, bool disabled)
      {
         return ConsoleColor.Black;
      }

      private ConsoleColor GetSharedForeground(bool isSelected, bool disabled)
      {
         if (disabled)
            return ConsoleColor.DarkGray;

         return isSelected ? ConsoleColor.Black : ConsoleColor.White;
      }

      #endregion
   }
}