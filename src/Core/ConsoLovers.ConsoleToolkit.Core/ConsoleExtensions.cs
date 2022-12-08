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

   public static IFixedSegment CreateFixedSegment([NotNull] this IConsole console, int width)
   {
      if (console == null)
         throw new ArgumentNullException(nameof(console));

      return new FixedSegment(console, console.CursorLeft, console.CursorTop, width);
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