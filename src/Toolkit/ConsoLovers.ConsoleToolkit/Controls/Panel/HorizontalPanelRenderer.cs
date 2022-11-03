// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HorizontalPanelRenderer.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

using System;
using System.Collections.Generic;
using System.Linq;

using JetBrains.Annotations;

internal class HorizontalPanelRenderer : IPanelRenderer
{
   public Panel Panel { get; }

   public HorizontalPanelRenderer([NotNull] Panel panel)
   {
      Panel = panel ?? throw new ArgumentNullException(nameof(panel));
      Measurements = new Dictionary<IRenderable, RenderSize>();
   }

   public Dictionary<IRenderable, RenderSize> Measurements { get; set; }

   public IEnumerable<Segment> RenderLine(IRenderContext context, int line)
   {
      foreach (var child in Panel.Children)
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
                  yield return new Segment(Panel, string.Empty.PadRight(required), child.Style);
            }
            else
            {
               yield return new Segment(Panel, string.Empty.PadRight(size.Width), child.Style);
            }
         }
      }
   }

   public RenderSize Measure(IRenderContext context, int availableWidth)
   {
      int totalHeight = 1;
      int totalWidth = 0;

      foreach (var child in Panel.Children)
      {
         var childSize = context.Measure(child, availableWidth);
         Measurements[child] = childSize;

         totalWidth += childSize.Width;
         if (childSize.Height > totalHeight)
            totalHeight = childSize.Height;
      }

      return new RenderSize
      {
         Height = totalHeight,
         Width = totalWidth,
      };

   }
}