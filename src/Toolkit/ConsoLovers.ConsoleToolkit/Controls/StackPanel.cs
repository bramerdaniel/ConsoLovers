// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StackPanel.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

using System;
using System.Collections.Generic;
using System.Linq;

using JetBrains.Annotations;

public class StackPanel : Renderable, IHaveAlignment
{

   private MeasuredSize size;

   public StackPanel()
   : this(RenderingStyle.Default)
   {

   }

   public StackPanel(RenderingStyle style)
   : base(style)
   {
      Children = new List<IRenderable>(5);
      Measurements = new Dictionary<IRenderable, MeasuredSize>();
   }

   private List<IRenderable> Children { get; }

   private Dictionary<IRenderable, MeasuredSize> Measurements { get; }

   public Alignment Alignment { get; set; }

   public Thickness Padding { get; set; }

   public override MeasuredSize Measure(int availableWidth)
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
            totalWidth += childSize.MinWidth;
         }
      }

      size = new MeasuredSize
      {
         Height = totalHeight,
         MinWidth = totalWidth,
      };

      return size;
   }

   public override IEnumerable<Segment> RenderLine(IRenderContext context, int lineIndex)
   {
      foreach (var child in Children)
      {
         if (Measurements.TryGetValue(child, out size))
         {
            if (size.Height > lineIndex)
            {
               var childSegments = child.RenderLine(context, lineIndex).ToArray();
               foreach (var segment in childSegments)
                  yield return segment;

               var required = size.MinWidth - childSegments.Sum(x => x.Width);
               if (required > 0)
                  yield return new Segment(this, string.Empty.PadRight(required), child.Style);
            }
            else
            {
               yield return new Segment(this, string.Empty.PadRight(size.MinWidth), child.Style);
            }
         }
      }
   }


   public StackPanel Add([NotNull] IRenderable renderable)
   {
      if (renderable == null)
         throw new ArgumentNullException(nameof(renderable));

      Children.Add(renderable);
      return this;
   }

   public StackPanel Remove([NotNull] IRenderable renderable)
   {
      if (renderable == null)
         throw new ArgumentNullException(nameof(renderable));

      Children.Remove(renderable);
      return this;
   }
}