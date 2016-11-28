// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColorManager.cs" company="ConsoLovers">
//   Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Console
{
   using System;
   using System.Drawing;
   using System.Linq;

   /// <summary>Manages the number of different colors that the Windows console is able to display in a given session.</summary>
   public sealed class ColorManager
   {
      #region Constants and Fields

      private readonly ColorMapper colorMapper;

      private readonly ColorStore colorStore;

      private readonly int maxColorChanges;

      private int colorChangeCount;

      #endregion

      #region Constructors and Destructors

      /// <summary>Manages the number of different colors that the Windows console is able to display in a given session.</summary>
      /// <param name="colorStore">The ColorStore instance in which the ColorManager will store colors.</param>
      /// <param name="colorMapper">The ColorMapper instance the ColorManager will use to relate different color types to one another.</param>
      /// <param name="maxColorChanges">
      ///    The maximum number of color changes allowed by the ColorManager.  It's necessary to keep track of this, because the Windows console can only display
      ///    16 different colors in a given session.
      /// </param>
      /// <param name="initialColorChangeCountValue">The number of color changes which have already occurred.</param>
      public ColorManager(ColorStore colorStore, ColorMapper colorMapper, int maxColorChanges, int initialColorChangeCountValue)
      {
         this.colorStore = colorStore;
         this.colorMapper = colorMapper;

         colorChangeCount = initialColorChangeCountValue;
         this.maxColorChanges = maxColorChanges;
      }

      #endregion

      #region Public Methods and Operators

      /// <summary>Gets the System.Drawing.Color mapped to the ConsoleColor provided as an argument.</summary>
      /// <param name="color">The ConsoleColor alias under which the desired System.Drawing.Color is stored.</param>
      /// <returns>The corresponding System.Drawing.Color.</returns>
      public Color GetColor(ConsoleColor color)
      {
         return colorStore[color];
      }

      /// <summary>Gets the ConsoleColor mapped to the System.Drawing.Color provided as an argument.</summary>
      /// <param name="color">The System.Drawing.Color whose ConsoleColor alias should be retrieved.</param>
      /// <returns>The corresponding ConsoleColor.</returns>
      public ConsoleColor GetConsoleColor(Color color)
      {
         try
         {
            return GetConsoleColorNative(color);
         }
         catch
         {
            return color.ToNearestConsoleColor();
         }
      }

      #endregion

      #region Methods

      private bool CanChangeColor()
      {
         return colorChangeCount < maxColorChanges;
      }

      private ConsoleColor GetConsoleColorNative(Color color)
      {
         if (!CanChangeColor())
         {
            return colorStore.LastConsoleColor();
         }

         if (!colorStore.ContainsColor(color))
         {
            ConsoleColor oldColor = (ConsoleColor)colorChangeCount;

            colorMapper.MapColor(oldColor, color);
            colorStore.Update(color, oldColor);

            colorChangeCount++;
         }

         return colorStore[color];
      }

      #endregion
   }
}