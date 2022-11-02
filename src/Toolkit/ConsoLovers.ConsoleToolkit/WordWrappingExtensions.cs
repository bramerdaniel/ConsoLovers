// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WordWrappingExtensions.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit;

using System;
using System.Collections.Generic;

using JetBrains.Annotations;

public static class WordWrappingExtensions
{
   private static char[] wrappingChars = new[] { ' ', ',', '.', '?', '!', ':', ';', '-', '\n', '\r', '\t' };

   public static IList<string> Wrap([NotNull] this string text, int maxLineLength)
   {
      if (text == null)
         throw new ArgumentNullException(nameof(text));
      if(maxLineLength <= 0)
         throw new ArgumentOutOfRangeException(nameof(maxLineLength), "maxLineLength must be greater than 0");

      var list = new List<string>();

      int currentIndex;
      var lastWrap = 0;
      var whitespace = new[] { ' ', '\r', '\n', '\t' };
      do
      {
         currentIndex = lastWrap + maxLineLength > text.Length 
            ? text.Length 
            : text.LastIndexOfAny(wrappingChars, Math.Min(text.Length - 1, lastWrap + maxLineLength)) + 1;

         if (currentIndex <= lastWrap)
            currentIndex = Math.Min(lastWrap + maxLineLength, text.Length);

         list.Add(text.Substring(lastWrap, currentIndex - lastWrap).Trim(whitespace));
         lastWrap = currentIndex;
      } while (currentIndex < text.Length);

      return list;
   }
}