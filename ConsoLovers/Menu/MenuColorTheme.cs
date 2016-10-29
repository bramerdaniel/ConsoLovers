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

   public class ConsoleMenuThemes
   {
      #region Public Properties

      public static MenuColorTheme Blue
      {
         get
         {
            var blueTheme = new MenuColorTheme();
            blueTheme.Selector.SelectedForeground = ConsoleColor.White;
            blueTheme.Selector.SelectedBackground = ConsoleColor.Blue;
            blueTheme.Selector.DisabledSelectedBackground = ConsoleColor.DarkBlue;
            blueTheme.Selector.DisabledSelectedForeground = ConsoleColor.DarkGray;
            blueTheme.MenuItem.Foreground = ConsoleColor.Blue;
            blueTheme.MenuItem.DisabledForeground = ConsoleColor.DarkBlue;
            blueTheme.MenuItem.SelectedForeground = ConsoleColor.DarkBlue;
            blueTheme.MenuItem.SelectedBackground = ConsoleColor.Blue;
            blueTheme.MenuItem.DisabledSelectedForeground = ConsoleColor.Blue;
            blueTheme.MenuItem.DisabledSelectedBackground = ConsoleColor.DarkBlue;
            blueTheme.Expander.SelectedBackground = ConsoleColor.Blue;
            blueTheme.Expander.SelectedForeground = ConsoleColor.White;
            blueTheme.HeaderForeground = ConsoleColor.Blue;
            blueTheme.HeaderBackground = ConsoleColor.Black;
            blueTheme.FooterForeground = ConsoleColor.Blue;
            blueTheme.FooterBackground = ConsoleColor.Black;

            return blueTheme;
         }
      }

      public static MenuColorTheme Red
      {
         get
         {
            var redTheme = new MenuColorTheme
            {
               ConsoleBackground = ConsoleColor.Red,
               HeaderForeground = ConsoleColor.Red,
               HeaderBackground = ConsoleColor.DarkRed,
               MenuItem =
                  new ColorSet
                  {
                     Foreground = ConsoleColor.Black,
                     Background = ConsoleColor.Red,
                     SelectedForeground = ConsoleColor.White,
                     SelectedBackground = ConsoleColor.DarkRed,
                     DisabledForeground = ConsoleColor.Gray,
                     DisabledBackground = ConsoleColor.Red,
                     DisabledSelectedBackground = ConsoleColor.DarkRed,
                     DisabledSelectedForeground = ConsoleColor.Red
                  },
               Selector =
                  new ColorSet
                  {
                     Background = ConsoleColor.Red,
                     DisabledBackground = ConsoleColor.Red,
                     DisabledSelectedBackground = ConsoleColor.DarkRed,
                     SelectedForeground = ConsoleColor.White,
                     SelectedBackground = ConsoleColor.DarkRed
                  },
               Expander =
                  new ColorSet
                  {
                     Foreground = ConsoleColor.Black,
                     Background = ConsoleColor.Red,
                     SelectedForeground = ConsoleColor.White,
                     SelectedBackground = ConsoleColor.DarkRed
                  }
            };

            return redTheme;
         }
      }

      #endregion
   }
}