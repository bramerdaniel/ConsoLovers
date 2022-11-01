// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CPanel.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

using System;
using System.Collections.Generic;
using System.Linq;

using JetBrains.Annotations;

public class Panel : Renderable, IHaveAlignment
{
   public Panel()
   : this(RenderingStyle.Default)
   {
   }

   public Panel(RenderingStyle style)
   : base(style)
   {
      Children = new List<IRenderable>(5);
      Measurements = new Dictionary<IRenderable, RenderSize>();
   }

   private List<IRenderable> Children { get; }

   private Dictionary<IRenderable, RenderSize> Measurements { get; }

   public Alignment Alignment { get; set; }

   public Thickness Padding { get; set; }

   public override RenderSize MeasureOverride(int availableWidth)
   {
      int totalHeight = 1;
      int totalWidth = 0;

      foreach (var child in Children)
      {
         var childSize = child.Measure(availableWidth);
         Measurements[child] = childSize;

         if (childSize.Height > totalHeight)
         {
            totalHeight = childSize.Height;
            totalWidth += childSize.Width;
         }
      }

      return new RenderSize
      {
         Height = totalHeight,
         Width = totalWidth,
      };
   }

   public override IEnumerable<Segment> RenderLine(IRenderContext context, int line)
   {
      foreach (var child in Children)
      {
         if (Measurements.TryGetValue(child, out var size))
         {
            if (size.Height > line)
            {
               var childSegments = child.RenderLine(context, line).ToArray();
               foreach (var segment in childSegments)
                  yield return segment;

               var required = size.Width - childSegments.Sum(x => x.Width);
               if (required > 0)
                  yield return new Segment(this, string.Empty.PadRight(required), child.Style);
            }
            else
            {
               yield return new Segment(this, string.Empty.PadRight(size.Width), child.Style);
            }
         }
      }
   }


   public Panel Add([NotNull] IRenderable renderable)
   {
      if (renderable == null)
         throw new ArgumentNullException(nameof(renderable));

      Children.Add(renderable);
      return this;
   }

   public Panel Remove([NotNull] IRenderable renderable)
   {
      if (renderable == null)
         throw new ArgumentNullException(nameof(renderable));

      Children.Remove(renderable);
      return this;
   }
}