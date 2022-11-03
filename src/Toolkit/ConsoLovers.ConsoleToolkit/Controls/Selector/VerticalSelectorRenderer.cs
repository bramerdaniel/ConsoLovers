// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Renderer.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

using System;
using System.Collections.Generic;
using System.Linq;

using JetBrains.Annotations;

internal class VerticalSelectorRenderer<T> : ISelectorRenderer
{
   private readonly CSelector<T> selector;

   public VerticalSelectorRenderer([NotNull] CSelector<T> selector)
   {
      this.selector = selector ?? throw new ArgumentNullException(nameof(selector));
   }

   struct ItemRenderInfo
   {
      #region Public Properties

      public bool AppendSelector { get; set; }

      public IRenderable Item { get; set; }

      public int ItemIndex { get; set; }

      public int ItemLine { get; set; }

      public int ItemWidth => ItemSize.Width;

      public RenderSize ItemSize { get; set; }

      #endregion
   }

   private Queue<ItemRenderInfo> renderQueue;

   public RenderSize Measure(IRenderContext context, int availableWidth)
   {
      var height = 0;
      var width = 0;

      renderQueue = new Queue<ItemRenderInfo>();

      int itemIndex = 0;
      var availableItemLength = availableWidth - selector.Selector.Length;

      foreach (var item in selector.Items)
      {
         var itemSize = context.Measure(item, availableItemLength);

         height += itemSize.Height;
         width = Math.Max(width, itemSize.Width);

         for (var i = 0; i < itemSize.Height; i++)
         {
            var data = new ItemRenderInfo
            {
               Item = item,
               ItemLine = i,
               ItemIndex = itemIndex,
               ItemSize = itemSize,
               AppendSelector = AppendSelector(i, itemSize.Height),
            };

            renderQueue.Enqueue(data);
         }

         itemIndex++;
      }

      width += selector.Selector.Length;
      return new RenderSize { Height = height, Width = width };
   }

   private static bool AppendSelector(int line, int height)
   {
      if (height == 1 || height == 2)
         return line == 0;

      var halfHeight = (height - 1) / 2;
      return line == halfHeight;
   }

   public IEnumerable<Segment> RenderLine(IRenderContext context, int line)
   {
      var data = renderQueue.Dequeue();

      if (data.ItemIndex == selector.SelectedIndex && data.AppendSelector)
      {
         yield return new Segment(selector, $"{selector.Selector}", selector.SelectionStyle);
      }
      else
      {
         yield return new Segment(selector, string.Empty.PadRight(selector.Selector.Length), data.Item.Style);
      }
      
      var segments = data.Item.RenderLine(context, data.ItemLine).ToArray();
      foreach (var segment in segments)
      {
         yield return data.ItemIndex == selector.SelectedIndex
            ? segment.OverrideStyle(selector.SelectionStyle)
            : segment;
      }
      
      // This extends the selection but not the mouse over effect
      ////var width = context.AvailableWidth - data.ItemWidth;
      ////if (width > 0)
      ////{
      ////   var style = data.ItemIndex == selector.SelectedIndex ? selector.SelectionStyle : selector.Style;
      ////   yield return new Segment(selector, string.Empty.PadRight(width), style);
      ////}
   }

   private void DecreaseSelectedIndex()
   {
      var nextIndex = selector.SelectedIndex - 1;
      if (nextIndex < 0)
         nextIndex = selector.Items.Count - 1;

      selector.SelectedIndex = nextIndex;
   }

   private void IncreaseSelectedIndex()
   {
      var nextIndex = selector.SelectedIndex + 1;
      if (nextIndex >= selector.Items.Count)
         nextIndex = 0;

      selector.SelectedIndex = nextIndex;
   }

   public void HandleKeyInput(IKeyInputContext context)
   {
      switch (context.KeyEventArgs.Key)
      {
         case ConsoleKey.UpArrow:
            DecreaseSelectedIndex();
            break;
         case ConsoleKey.DownArrow:
            IncreaseSelectedIndex();
            break;
         case ConsoleKey.End:
            selector.SelectedIndex = selector.Items.Count - 1;
            break;
         case ConsoleKey.Home:
            selector.SelectedIndex = 0;
            break;
         case ConsoleKey.Enter:
            context.Accept();
            break;
         case ConsoleKey.Escape:
            context.Cancel();
            break;
      }
   }
}