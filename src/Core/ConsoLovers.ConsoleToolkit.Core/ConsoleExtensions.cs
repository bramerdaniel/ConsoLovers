// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleExtensions.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core;

using System;

using JetBrains.Annotations;

/// <summary>Helper class for creating <see cref="IFixedSegment"/>s</summary>
public static class FixedSectionExtensions
{
   #region Public Methods and Operators

   /// <summary>Creates a fixed segment for the cursor left to the line end.</summary>
   /// <param name="console">The console.</param>
   /// <returns>The create segment</returns>
   /// <exception cref="System.ArgumentNullException">console</exception>
   public static IFixedSegment CreateFixedSegment([NotNull] this IConsole console)
   {
      if (console == null)
         throw new ArgumentNullException(nameof(console));

      var width = console.WindowWidth - console.CursorLeft;
      return new FixedSegment(console, console.CursorLeft, console.CursorTop, width);
   }

   /// <summary>Creates a fixed segment for the cursor left with the specified width.</summary>
   /// <param name="console">The console.</param>
   /// <param name="width">The width of the segment.</param>
   /// <returns>The create segment</returns>
   /// <exception cref="System.ArgumentNullException">console</exception>
   public static IFixedSegment CreateFixedSegment([NotNull] this IConsole console, int width)
   {
      if (console == null)
         throw new ArgumentNullException(nameof(console));

      return new FixedSegment(console, console.CursorLeft, console.CursorTop, width);
   }

   /// <summary>Creates a fixed segment for the cursor left with the specified width and sets the specified <see cref="initialText"/>.</summary>
   /// <param name="console">The console.</param>
   /// <param name="width">The width.</param>
   /// <param name="initialText">The initial text.</param>
   /// <returns></returns>
   /// <exception cref="System.ArgumentNullException">console</exception>
   public static IFixedSegment CreateFixedSegment([NotNull] this IConsole console, int width, string initialText)
   {
      if (console == null)
         throw new ArgumentNullException(nameof(console));

      return new FixedSegment(console, console.CursorLeft, console.CursorTop, width)
         .Update(initialText);
   }

   public static IFixedSegment CreateFixedSegment([NotNull] this IConsole console, int width, string initialText, ConsoleColor foreground)
   {
      if (console == null)
         throw new ArgumentNullException(nameof(console));

      return new FixedSegment(console, console.CursorLeft, console.CursorTop, width)
         .Update(initialText, foreground);
   }

   public static IFixedSegment CreateFixedSegment([NotNull] this IConsole console, int left, int top, int width)
   {
      if (console == null)
         throw new ArgumentNullException(nameof(console));

      return new FixedSegment(console, left, top, width);
   }

   public static IFixedSegment CreateFixedSegment([NotNull] this IConsole console, int left, int top, int width, string initialText)
   {
      if (console == null)
         throw new ArgumentNullException(nameof(console));

      return new FixedSegment(console, left, top, width).Update(initialText);
   }

   #endregion
}