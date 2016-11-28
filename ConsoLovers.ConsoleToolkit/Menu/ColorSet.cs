// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColorSet.cs" company="ConsoLovers">
//   Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Menu
{
   using System.Drawing;

   public class ColorSet
   {
      #region Public Properties

      public Color Background { get; set; } = Color.Black;

      public Color DisabledBackground { get; set; } = Color.Black;

      public Color DisabledForeground { get; set; } = Color.DarkGray;

      public Color DisabledSelectedBackground { get; set; } = Color.DarkGray;

      public Color DisabledSelectedForeground { get; set; } = Color.Gray;

      public Color Foreground { get; set; } = Color.White;

      public Color SelectedBackground { get; set; } = Color.Gray;

      public Color SelectedForeground { get; set; } = Color.Black;

      #endregion

      #region Public Methods and Operators

      public Color GetBackground(bool isSelected, bool disabled)
      {
         if (isSelected)
            return disabled ? DisabledSelectedBackground : SelectedBackground;

         return disabled ? DisabledBackground : Background;
      }

      public Color GetForeground(bool isSelected, bool disabled)
      {
         if (isSelected)
            return disabled ? DisabledSelectedForeground : SelectedForeground;

         return disabled ? DisabledForeground : Foreground;
      }

      #endregion
   }
}