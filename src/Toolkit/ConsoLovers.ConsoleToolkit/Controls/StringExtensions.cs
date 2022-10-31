// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StringExtensions.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

using Enumerable = System.Linq.Enumerable;

public static class StringExtensions
{
   // Cache whether or not internally normalized line endings
   // already are normalized. No reason to do yet another replace if it is.
   private static readonly bool _alreadyNormalized = Environment.NewLine.Equals("\n", StringComparison.OrdinalIgnoreCase);
   
   internal static string CapitalizeFirstLetter(this string? text, CultureInfo? culture = null)
   {
      if (text == null)
      {
         return string.Empty;
      }

      culture ??= CultureInfo.InvariantCulture;

      if (text.Length > 0 && char.IsLower(text[0]))
      {
         text = string.Format(culture, "{0}{1}", char.ToUpper(text[0], culture), text.Substring(1));
      }

      return text;
   }

   internal static string? RemoveNewLines(this string? text)
   {
      return text?.ReplaceExact("\r\n", string.Empty)
          ?.ReplaceExact("\n", string.Empty);
   }

   internal static string NormalizeNewLines(this string? text, bool native = false)
   {
      text = text?.ReplaceExact("\r\n", "\n");
      text ??= string.Empty;

      if (native && !_alreadyNormalized)
      {
         text = text.ReplaceExact("\n", Environment.NewLine);
      }

      return text;
   }

   internal static string[] SplitLines(this string text)
   {
      var result = text?.NormalizeNewLines()?.Split(new[] { '\n' }, StringSplitOptions.None);
      return result ?? Array.Empty<string>();
   }


   internal static string Repeat(this string text, int count)
   {
      if (text is null)
      {
         throw new ArgumentNullException(nameof(text));
      }

      if (count <= 0)
      {
         return string.Empty;
      }

      if (count == 1)
      {
         return text;
      }

      return string.Concat(Enumerable.Repeat(text, count));
   }

   internal static string ReplaceExact(this string text, string oldValue, string? newValue)
   {
      return text.Replace(oldValue, newValue);

   }
}