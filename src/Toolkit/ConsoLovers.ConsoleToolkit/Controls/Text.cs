﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CText.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

using System;
using System.Collections.Generic;
using System.Linq;

public class Text : Renderable, IHaveAlignment
{
   private string[] lines;

   #region Constructors and Destructors

   public Text(string value, RenderingStyle style)
      : base(style)
   {
      Value = value ?? String.Empty;
   }

   public Text(string value)
   : base(RenderingStyle.Default)
   {
      Value = value;
   }

   public static implicit operator Text(string text) => new(text);

   public Alignment Alignment { get; set; }

   #endregion

   #region Public Properties

   public string Value { get; set; }

   private static string[] Split(string value)
   {
#if NETFRAMEWORK

      return value.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
#else
      return value.Split(Environment.NewLine);
#endif
   }

   internal int Length => Value.Length;

   #endregion

   #region Public Methods and Operators

   public override RenderSize MeasureOverride(IRenderContext context, int availableWidth)
   {
      lines = WrapIfRequired(Value, availableWidth).ToArray();
      var longestLine = lines.Max(x => x.Length);

      return new RenderSize
      {
         Height = lines.Length,
         Width = longestLine
      };
   }

   private IEnumerable<string> WrapIfRequired(string value, int availableWidth)
   {
      foreach (var line in Split(value))
      {
         if (line.Length > availableWidth)
         {
            foreach (var wrappedLine in line.Wrap(availableWidth))
               yield return wrappedLine;
         }
         else
         {
            yield return line;
         }
      }
   }

   public override IEnumerable<Segment> RenderLine(IRenderContext context, int line)
   {
      if (line >= lines.Length)
         throw new ArgumentOutOfRangeException(nameof(line), $"Only can render lines between {0} and {lines.Length - 1}");
      
      yield return new Segment(this, lines[line], Style);
   }

   #endregion

   public static IRenderable FromString(string value)
   {
      return (Text)value;
   }
}