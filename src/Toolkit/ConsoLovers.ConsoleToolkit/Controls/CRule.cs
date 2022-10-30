// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CRule.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

using System.Collections.Generic;

public class CRule : Renderable
{

   private MeasuredSize measuredSize;

   public char RuleCharacter { get; set; } = '-';

   public int TextOffset { get; set; } = 5;
   
   public int? MaxWidth { get; set; }

   public string Text { get; }

   public CRule(string text)
   {
      Text = text;
   }

   public override MeasuredSize Measure(int availableWidth)
   {
      measuredSize = new MeasuredSize
      {
         Height = 1, 
         MinWidth = MaxWidth.GetValueOrDefault(availableWidth)
      };

      return measuredSize;
   }

   public override IEnumerable<Segment> RenderLine(IRenderContext context, int line)
   {
      var text = Text ?? string.Empty;
      var availableWidth = context.AvailableWidth;

      var (left, right) = ComputeOffsets(availableWidth);
      text = text.PadLeft(left, RuleCharacter).PadRight(availableWidth, RuleCharacter);
      yield return new Segment(this, text, Style);
   }

   private (int, int) ComputeOffsets(int availableWidth)
   {
      var text = Text;
      if (string.IsNullOrEmpty(Text))
         return (availableWidth, 0);

      var other = availableWidth - TextOffset;

      if (TextAlignment == Alignment.Left)
         return (TextOffset + text.Length, availableWidth);

      if (TextAlignment == Alignment.Right)
         return (other, availableWidth);

      var halfTextSize = text.Length / 2;
      return (availableWidth / 2 + halfTextSize, availableWidth);
   }

   public Alignment TextAlignment { get; set; }
}