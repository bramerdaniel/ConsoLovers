namespace ConsoLovers.ConsoleToolkit.Menu
{
   using System.Drawing;

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

      public static MenuColorTheme Pink => new MenuColorTheme
      {
         HeaderBackground = Color.Pink,
         HeaderForeground = Color.DeepPink,
         ConsoleBackground = Color.Pink,

         MenuItem = new ColorSet
         {
            Foreground = Color.DeepPink,
            Background = Color.Pink,
            SelectedForeground = Color.White,
            SelectedBackground = Color.DeepPink,
            DisabledForeground = Color.Gray,
            DisabledBackground = Color.Pink,
            DisabledSelectedBackground = Color.HotPink,
            DisabledSelectedForeground = Color.Pink
         },

         Selector = new ColorSet
         {
            Background = Color.Pink,
            DisabledBackground = Color.Pink,
            DisabledSelectedBackground = Color.HotPink,
            SelectedForeground = Color.White,
            SelectedBackground = Color.DeepPink
         },

         Expander = new ColorSet
         {
            Foreground = Color.DeepPink,
            Background = Color.Pink,
            SelectedForeground = Color.White,
            SelectedBackground = Color.DeepPink
         },

         Hint = new ColorSet
         {
            DisabledSelectedForeground = Color.Black,
            DisabledSelectedBackground = Color.PaleVioletRed
         }
      };

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
                  DisabledSelectedBackground = Color.Azure
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