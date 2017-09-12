// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColorManagerFactory.cs" company="ConsoLovers">
//   Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Console
{
   using System;
   using System.Collections.Concurrent;
   using System.Drawing;

   public sealed class ColorManagerFactory
   {
      #region Public Methods and Operators

      public ColorManager GetManager(ColorStore colorStore, int maxColorChanges, int initialColorChangeCountValue)
      {
         return new ColorManager(colorStore, GetColorMapper(), maxColorChanges, initialColorChangeCountValue);
      }

      public ColorManager GetManager
         (ConcurrentDictionary<Color, ConsoleColor> colorMap, ConcurrentDictionary<ConsoleColor, Color> consoleColorMap, int maxColorChanges, int initialColorChangeCountValue)
      {
         ColorStore colorStore = GetColorStore(colorMap, consoleColorMap);
         ColorMapper colorMapper = GetColorMapper();

         return new ColorManager(colorStore, colorMapper, maxColorChanges, initialColorChangeCountValue);
      }

      #endregion

      #region Methods

      private ColorMapper GetColorMapper()
      {
         return new ColorMapper();
      }

      private ColorStore GetColorStore(ConcurrentDictionary<Color, ConsoleColor> colorMap, ConcurrentDictionary<ConsoleColor, Color> consoleColorMap)
      {
         return new ColorStore(colorMap, consoleColorMap);
      }

      #endregion
   }
}