// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColoredConsoleMenu.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Menu
{
   using System;

   using ConsoLovers.ConsoleToolkit.Console;

   public class ColoredConsoleMenu : ConsoleMenuBase
   {
      #region Constants and Fields

      private ConsoleColorManager colorManager;

      private MenuColorTheme theme = new MenuColorTheme();

      #endregion

      #region Constructors and Destructors

      public ColoredConsoleMenu()
      {
         colorManager = new ConsoleColorManager();
      }

      #endregion

      #region Public Properties

      /// <summary>Gets or sets the <see cref="MenuColorTheme"/> the <see cref="ColoredConsoleMenu"/> uses.</summary>
      public MenuColorTheme Theme
      {
         get
         {
            return theme;
         }
         set
         {
            if (theme == value)
               return;

            theme = value;
            colorManager.Dispose();
            colorManager = new ConsoleColorManager();
            Invalidate();
         }
      }

      #endregion

      #region Methods

      protected override void OnMenuClosed()
      {
         colorManager.Dispose();
      }

      protected override ConsoleColor GetConsoleBackground()
      {
         return colorManager.GetConsoleColor(Theme.ConsoleBackground);
      }

      protected override ConsoleColor GetExpanderBackground(bool isSelected, bool disabled, bool mouseOver)
      {
         if (mouseOver)
            return GetMouseOverBackground();

         return colorManager.GetConsoleColor(Theme.Expander.GetBackground(isSelected, disabled));
      }

      protected override ConsoleColor GetExpanderForeground(bool isSelected, bool disabled, bool mouseOver)
      {
         if (mouseOver)
            return GetMouseOverForeground();

         return colorManager.GetConsoleColor(Theme.Expander.GetForeground(isSelected, disabled));
      }

      protected override ConsoleColor GetFooterBackground()
      {
         return colorManager.GetConsoleColor(Theme.FooterBackground);
      }

      protected override ConsoleColor GetFooterForeground()
      {
         return colorManager.GetConsoleColor(Theme.FooterForeground);
      }

      protected override ConsoleColor GetHeaderBackground()
      {
         return colorManager.GetConsoleColor(Theme.HeaderBackground);
      }

      protected override ConsoleColor GetHeaderForeground()
      {
         return colorManager.GetConsoleColor(Theme.HeaderForeground);
      }

      protected override ConsoleColor GetHintBackground(bool isSelected, bool disabled)
      {
         return colorManager.GetConsoleColor(Theme.Hint.GetBackground(isSelected, disabled));
      }

      protected override ConsoleColor GetHintForeground(bool isSelected, bool disabled)
      {
         return colorManager.GetConsoleColor(Theme.Hint.GetForeground(isSelected, disabled));
      }

      protected override ConsoleColor GetMenuItemBackground(bool isSelected, bool disabled, bool mouseOver, ConsoleColor? elementBackground)
      {
         if (mouseOver)
            return GetMouseOverBackground();

         if (elementBackground.HasValue)
            return elementBackground.Value;

         return colorManager.GetConsoleColor(Theme.MenuItem.GetBackground(isSelected, disabled));
      }

      protected override ConsoleColor GetMenuItemForeground(bool isSelected, bool disabled, bool mouseOver, ConsoleColor? elementForeground)
      {
         if (mouseOver)
            return GetMouseOverForeground();

         if (elementForeground.HasValue)
            return elementForeground.Value;

         return colorManager.GetConsoleColor(Theme.MenuItem.GetForeground(isSelected, disabled));
      }

      protected override ConsoleColor GetMouseOverBackground()
      {
         return colorManager.GetConsoleColor(Theme.MouseOverBackground);
      }

      protected override ConsoleColor GetMouseOverForeground()
      {
         return colorManager.GetConsoleColor(Theme.MouseOverForeground);
      }

      protected override ConsoleColor GetSelectorBackground(bool isSelected, bool disabled, bool mouseOver)
      {
         return colorManager.GetConsoleColor(Theme.Selector.GetBackground(isSelected, disabled));
      }

      protected override ConsoleColor GetSelectorForeground(bool isSelected, bool disabled, bool mouseOver)
      {
         return colorManager.GetConsoleColor(Theme.Selector.GetForeground(isSelected, disabled));
      }

      #endregion
   }
}