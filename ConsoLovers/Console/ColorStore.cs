// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ColorStore.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Console
{
   using System;
   using System.Collections.Concurrent;
   using System.Drawing;
   using System.Linq;

   /// <summary>Stores and manages the assignment of System.Drawing.Color objects to ConsoleColor objects.</summary>
   public sealed class ColorStore
   {
      #region Constants and Fields

      /// <summary>A map from System.Drawing.Color to ConsoleColor.</summary>
      private readonly ConcurrentDictionary<Color, ConsoleColor> colorToConsoleColor;

      /// <summary>A map from ConsoleColor to System.Drawing.Color.</summary>
      private readonly ConcurrentDictionary<ConsoleColor, Color> consoleColorToColor;

      #endregion

      #region Constructors and Destructors

      /// <summary>Manages the assignment of System.Drawing.Color objects to ConsoleColor objects.</summary>
      /// <param name="colorMap">The Dictionary the ColorStore should use to key System.Drawing.Color objects to ConsoleColor objects.</param>
      /// <param name="consoleColorMap">The Dictionary the ColorStore should use to key ConsoleColor objects to System.Drawing.Color objects.</param>
      public ColorStore(ConcurrentDictionary<Color, ConsoleColor> colorMap, ConcurrentDictionary<ConsoleColor, Color> consoleColorMap)
      {
         colorToConsoleColor = colorMap;
         consoleColorToColor = consoleColorMap;
      }

      public ColorStore()
      {
         colorToConsoleColor = new ConcurrentDictionary<Color, ConsoleColor>();

         consoleColorToColor = new ConcurrentDictionary<ConsoleColor, Color>();
         consoleColorToColor.TryAdd(ConsoleColor.Black, ConsoleColorEquivalents.Black);
         consoleColorToColor.TryAdd(ConsoleColor.Blue, ConsoleColorEquivalents.Blue);
         consoleColorToColor.TryAdd(ConsoleColor.Cyan, ConsoleColorEquivalents.Cyan);
         consoleColorToColor.TryAdd(ConsoleColor.DarkBlue, ConsoleColorEquivalents.DarkBlue);
         consoleColorToColor.TryAdd(ConsoleColor.DarkCyan, ConsoleColorEquivalents.DarkCyan);
         consoleColorToColor.TryAdd(ConsoleColor.DarkGray, ConsoleColorEquivalents.DarkGray);
         consoleColorToColor.TryAdd(ConsoleColor.DarkGreen, ConsoleColorEquivalents.DarkGreen);
         consoleColorToColor.TryAdd(ConsoleColor.DarkMagenta, ConsoleColorEquivalents.DarkMagenta);
         consoleColorToColor.TryAdd(ConsoleColor.DarkRed, ConsoleColorEquivalents.DarkRed);
         consoleColorToColor.TryAdd(ConsoleColor.DarkYellow, ConsoleColorEquivalents.DarkYellow);
         consoleColorToColor.TryAdd(ConsoleColor.Gray, ConsoleColorEquivalents.Gray);
         consoleColorToColor.TryAdd(ConsoleColor.Green, ConsoleColorEquivalents.Green);
         consoleColorToColor.TryAdd(ConsoleColor.Magenta, ConsoleColorEquivalents.Magenta);
         consoleColorToColor.TryAdd(ConsoleColor.Red, ConsoleColorEquivalents.Red);
         consoleColorToColor.TryAdd(ConsoleColor.White, ConsoleColorEquivalents.White);
         consoleColorToColor.TryAdd(ConsoleColor.Yellow, ConsoleColorEquivalents.Yellow);
      }

      #endregion

      #region Public Indexers

      /// <summary>Gets the <see cref="ConsoleColor"/> for the given <see cref="Color"/>.</summary>
      /// <param name="color">The <see cref="Color"/> to get the <see cref="ConsoleColor"/> for.</param>
      /// <returns>The <see cref="ConsoleColor"/> for the given <see cref="Color"/></returns>
      public ConsoleColor this[Color color] => colorToConsoleColor[color];

      /// <summary>Gets the <see cref="Color"/> for the given <see cref="ConsoleColor"/>.</summary>
      /// <param name="color">The <see cref="ConsoleColor"/> to get the <see cref="Color"/> for.</param>
      /// <returns>The <see cref="Color"/> for the given <see cref="ConsoleColor"/></returns>
      public Color this[ConsoleColor color] => consoleColorToColor[color];

      #endregion

      #region Public Methods and Operators

      public ConsoleColor LastConsoleColor()
      {
         return colorToConsoleColor.Values.LastOrDefault();
      }

      /// <summary>Notifies the caller as to whether or not the specified System.Drawing.Color needs to be added to the ColorStore.</summary>
      /// <param name="color">The System.Drawing.Color to be checked for membership.</param>
      /// <returns>Returns 'true' if the ColorStore already contains the specified System.Drawing.Color.</returns>
      public bool ContainsColor(Color color)
      {
         return colorToConsoleColor.ContainsKey(color);
      }

      /// <summary>Adds a new System.Drawing.Color to the ColorStore.</summary>
      /// <param name="color">The System.Drawing.Color to be added to the ColorStore.</param>
      /// <param name="consoleColor">The ConsoleColor to be replaced by the new System.Drawing.Color.</param>
      public void Update(Color color, ConsoleColor consoleColor)
      {
         colorToConsoleColor.TryAdd(color, consoleColor);
         consoleColorToColor[consoleColor] = color;
      }

      #endregion
   }
}