// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VerticalPanelRenderer.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

using System;
using System.Collections.Generic;

using JetBrains.Annotations;

internal class VerticalPanelRenderer : IPanelRenderer
{
   private readonly Queue<Data> renderQueue;

   public Panel Panel { get; }

   public VerticalPanelRenderer([NotNull] Panel panel)
   {
      Panel = panel ?? throw new ArgumentNullException(nameof(panel));
      Measurements = new Dictionary<IRenderable, RenderSize>();
      renderQueue = new Queue<Data>();

   }

   class Data
   {
      public IRenderable Child { get; set; }
      public int Line { get; set; }
   }

   public Dictionary<IRenderable, RenderSize> Measurements { get; set; }

   public IEnumerable<Segment> RenderLine(IRenderContext context, int line)
   {
      var data = renderQueue.Dequeue();
      foreach (var segment in data.Child.RenderLine(context, data.Line))
         yield return segment;
   }



   public RenderSize Measure(IRenderContext context, int availableWidth)
   {
      int totalHeight = 0;
      int totalWidth = 0;

      foreach (var child in Panel.Children)
      {
         var childSize = child.Measure(context, availableWidth);
         Measurements[child] = childSize;

         totalHeight += childSize.Height;
         if (childSize.Width > totalWidth)
            totalWidth = childSize.Width;

         AddDataToRenderQueue(child, childSize);
      }

      return new RenderSize
      {
         Height = totalHeight,
         Width = totalWidth,
      };

   }

   private void AddDataToRenderQueue(IRenderable child, RenderSize childSize)
   {
      for (int i = 0; i < childSize.Height; i++)
      {
         var data = new Data { Line = i, Child = child };
         renderQueue.Enqueue(data);
      }
   }
}