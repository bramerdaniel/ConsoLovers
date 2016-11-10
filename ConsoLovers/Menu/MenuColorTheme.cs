// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MenuColorTheme.cs" company="ConsoLovers">
//   Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Menu
{
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

      /// <summary>Gets or sets the <see cref="ColorSet"/> for the disabled hint.</summary>
      public ColorSet Hint { get; set; } = new ColorSet
      {
         DisabledSelectedForeground = Color.White,
         DisabledSelectedBackground = Color.Red
      };

      public Color MouseOverBackground { get; set; } = Color.LightGray;

      public Color MouseOverForeground { get; set; } = Color.Black;

      #endregion
   }
}