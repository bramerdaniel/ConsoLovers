// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Text.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Prompts;

using System;
using System.Collections.Generic;
using System.Linq;

public class Text : Renderable
{
   private int? minWidth;

   private string value;

   private string[] lines;

   #region Constructors and Destructors

   public Text(string value, RenderingStyle style)
      : base(style)
   {
      Value = value ?? String.Empty;
   }

   public Text(string value)
   {
      Value = value;
   }

   public int MinWidth
   {
      get => minWidth.GetValueOrDefault(Value.Length);
      set => minWidth = value;
   }

   public Alignment Alignment { get; set; }

   #endregion

   #region Public Properties

   public string Value
   {
      get => value;
      set
      {
         this.value = value;
         lines = Split(value);
      }
   }

   private static string[] Split(string value)
   {
#if NETFRAMEWORK

      return value.Split(new string[]{ Environment.NewLine }, StringSplitOptions.None);
#else
      return value.Split(Environment.NewLine, StringSplitOptions.None);
#endif
   }

   internal int Length => Value.Length;

   #endregion

   #region Public Methods and Operators

   public override MeasuredSize Measure(int availableWidth)
   {
      return new MeasuredSize
      {
         Height = lines.Length,
         MinWidth = lines.Max(x => x.Length),
      };
   }

   public override IEnumerable<Segment> RenderLine(IRenderContext context, int lineIndex)
   {
      if (lineIndex >= lines.Length)
         throw new ArgumentOutOfRangeException(nameof(lineIndex), $"Only can render lines between {0} and {lines.Length - 1}");

      var textWithPadding = PadLine(lineIndex, context.AvailableWidth);
      yield return new Segment(textWithPadding, Style);
   }

   private string PadLine(int line, int available)
   {
      var lineValue = lines[line];

      if (Alignment == Alignment.Left)
         return lineValue.PadRight(available);
      if (Alignment == Alignment.Right)
         return lineValue.PadLeft(available);

      var missing = available - lineValue.Length;
      var left = missing / 2;
      return lineValue.PadLeft(left + Length).PadRight(available);
   }

   #endregion
}