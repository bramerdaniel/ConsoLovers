// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Padding.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

using System.Collections.Generic;

public class Padding : InteractiveRenderable
{
   private readonly IRenderable content;

   private Thickness value;

   public Padding(IRenderable content)
   : this(content, new Thickness(1))
   {
      this.content = content;
   }

   public Padding(IRenderable content, Thickness value)
   {
      this.content = content;
      Value = value;
   }

   public Thickness Value
   {
      get => value;
      set
      {
         if(Equals(this.value, value))
            return;

         this.value = value;
         Invalidate(InvalidationScope.All);
      }
   }

   public override IEnumerable<IRenderable> GetChildren()
   {
      if (content != null)
         yield return content;
   }

   public override IEnumerable<Segment> RenderLine(IRenderContext context, int line)
   {
      var contentSize = context.GetMeasuredSize(content);
      if (line < Value.Top)
      {
         yield return new Segment(this, string.Empty.PadRight(MeasuredSize.Width), Style);
      }
      else if (line >= Value.Top && line < Value.Top + contentSize.Height)
      {
         if (Value.Left > 0)
            yield return new Segment(this, string.Empty.PadRight(Value.Left), Style);

         foreach (var segment in content.RenderLine(context, line - Value.Top))
            yield return segment;

         if (Value.Right > 0)
            yield return new Segment(this, string.Empty.PadRight(Value.Right), Style);
      }
      else if (line >= Value.Top + contentSize.Height)
      {
         yield return new Segment(this, string.Empty.PadRight(MeasuredSize.Width), Style);
      }
   }

   public override RenderSize MeasureOverride(IRenderContext context, int availableWidth)
   {
      var childSize = context.Measure(content, availableWidth);
      return new RenderSize
      {
         Height = childSize.Height + Value.Top + Value.Bottom,
         Width = childSize.Width + Value.Left + Value.Right
      };
   }
}