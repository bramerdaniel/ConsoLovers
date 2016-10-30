// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MenuColorTheme.cs" company="ConsoLovers">
//   Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.Menu
{
   using System;
   using System.Drawing;

   public class MenuColorTheme
   {
      #region Public Properties

      public Color ConsoleBackground { get; set; } = Color.Black;

      public ColorSet Expander { get; set; } = new ColorSet
      {
         Foreground = Color.White,
         Background = Color.Black,
         DisabledBackground = Color.Black,
         DisabledForeground = Color.DarkGray,
         SelectedForeground = Color.Black,
         SelectedBackground = Color.Gray,
         DisabledSelectedForeground = Color.Gray,
         DisabledSelectedBackground = Color.DarkGray
      };

      public Color FooterBackground { get; set; } = Color.Black;

      public Color FooterForeground { get; set; } = Color.White;

      public Color HeaderBackground { get; set; } = Color.Black;

      public Color HeaderForeground { get; set; } = Color.White;

      /// <summary>Gets or sets the <see cref="ColorSet"/> of a normal, not seleted menu item.</summary>
      public ColorSet MenuItem { get; set; } = new ColorSet
      {
         Foreground = Color.White,
         Background = Color.Black,
         DisabledBackground = Color.Black,
         DisabledForeground = Color.DarkGray,
         SelectedForeground = Color.Black,
         SelectedBackground = Color.Gray,
         DisabledSelectedForeground = Color.Gray,
         DisabledSelectedBackground = Color.DarkGray
      };

      /// <summary>Gets or sets the <see cref="ColorSet"/> of the selector.</summary>
      public ColorSet Selector { get; set; } = new ColorSet
      {
         Foreground = Color.White,
         Background = Color.Black,
         DisabledBackground = Color.Black,
         DisabledForeground = Color.DarkGray
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
            blueTheme.Selector.SelectedForeground = Color.White;
            blueTheme.Selector.SelectedBackground = Color.Blue;
            blueTheme.Selector.DisabledSelectedBackground = Color.DarkBlue;
            blueTheme.Selector.DisabledSelectedForeground = Color.DarkGray;
            blueTheme.MenuItem.Foreground = Color.Blue;
            blueTheme.MenuItem.DisabledForeground = Color.DarkBlue;
            blueTheme.MenuItem.SelectedForeground = Color.DarkBlue;
            blueTheme.MenuItem.SelectedBackground = Color.Blue;
            blueTheme.MenuItem.DisabledSelectedForeground = Color.Blue;
            blueTheme.MenuItem.DisabledSelectedBackground = Color.DarkBlue;
            blueTheme.Expander.SelectedBackground = Color.Blue;
            blueTheme.Expander.SelectedForeground = Color.White;
            blueTheme.HeaderForeground = Color.Blue;
            blueTheme.HeaderBackground = Color.Black;
            blueTheme.FooterForeground = Color.Blue;
            blueTheme.FooterBackground = Color.Black;

            return blueTheme;
         }
      }

      public static MenuColorTheme Red
      {
         get
         {
            var redTheme = new MenuColorTheme
            {
               ConsoleBackground = Color.Red,
               HeaderForeground = Color.Red,
               HeaderBackground = Color.DarkRed,
               MenuItem =
                  new ColorSet
                  {
                     Foreground = Color.Black,
                     Background = Color.Red,
                     SelectedForeground = Color.White,
                     SelectedBackground = Color.DarkRed,
                     DisabledForeground = Color.Gray,
                     DisabledBackground = Color.Red,
                     DisabledSelectedBackground = Color.DarkRed,
                     DisabledSelectedForeground = Color.Red
                  },
               Selector =
                  new ColorSet
                  {
                     Background = Color.Red,
                     DisabledBackground = Color.Red,
                     DisabledSelectedBackground = Color.DarkRed,
                     SelectedForeground = Color.White,
                     SelectedBackground = Color.DarkRed
                  },
               Expander =
                  new ColorSet
                  {
                     Foreground = Color.Black,
                     Background = Color.Red,
                     SelectedForeground = Color.White,
                     SelectedBackground = Color.DarkRed
                  }
            };

            return redTheme;
         }
      }

      public static MenuColorTheme Bahama
      {
         get
         {
            var theme = new MenuColorTheme
            {
               MenuItem = new ColorSet
               {
                  SelectedForeground = Color.Blue,
                  Background = Color.Black,
                  SelectedBackground = Color.Azure,
                  DisabledForeground = Color.Orange,
                  DisabledBackground = Color.Black,
                  DisabledSelectedForeground = Color.Orange,
                  DisabledSelectedBackground = Color.Azure
               },

               Selector = new ColorSet
               {
                  SelectedForeground = Color.Blue,
                  SelectedBackground = Color.Azure,
                  DisabledSelectedForeground = Color.Orange,
                  DisabledSelectedBackground= Color.Azure
               },

               Expander = new ColorSet
               {
                  SelectedForeground = Color.Blue,
                  SelectedBackground = Color.Azure
               }
            };

            return theme;
         }
      }


      #endregion
   }
}