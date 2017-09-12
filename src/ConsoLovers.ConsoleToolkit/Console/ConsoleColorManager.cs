// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleColorManager.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Console
{
   using System;
   using System.Drawing;
   using System.Linq;

   /// <summary>Manages the number of different colors that the Windows console is able to display in a given session.</summary>
   public sealed class ConsoleColorManager : IDisposable
   {
      #region Constants and Fields

      // Note that if you set ConsoleColor.Black to a different color, then the background of the
      // console will change as a side-effect!  The index of Black (in the ConsoleColor definition) is 0,
      // so avoid that index.
      private const int INITIAL_COLOR_CHANGE_COUNT_VALUE = 1;

      // Limitation of the Windows console window.
      private const int MAX_COLOR_CHANGES = 16;

      private readonly ColorMapper colorMapper;

      private readonly ColorStore colorStore;

      private readonly int maxColorChanges;

      private int colorChangeCount;

      #endregion

      #region Constructors and Destructors

      /// <summary>Manages the number of different colors that the Windows console is able to display in a given session.</summary>
      public ConsoleColorManager()
      {
         colorStore = new ColorStore();
         colorMapper = new ColorMapper();

         colorChangeCount = INITIAL_COLOR_CHANGE_COUNT_VALUE;
         maxColorChanges = MAX_COLOR_CHANGES;
      }

      #endregion

      #region IDisposable Members

      public void Dispose()
      {
         foreach (ConsoleColor consoleColor in Enum.GetValues(typeof(ConsoleColor)))
            colorMapper.Map(consoleColor).To(ConsoleColorEquivalents.GetEquivalet(consoleColor));
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
            return GetConsoleColorInternal(color);
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

      private ConsoleColor GetConsoleColorInternal(Color color)
      {
         if (!CanChangeColor())
         {
            return colorStore.LastConsoleColor();
         }

         if (!colorStore.ContainsColor(color))
         {
            ConsoleColor consoleColor = (ConsoleColor)colorChangeCount;
            colorMapper.Map(consoleColor).To(color);
            colorStore.Update(color, consoleColor);

            colorChangeCount++;
         }

         return colorStore[color];
      }

      #endregion
   }
}