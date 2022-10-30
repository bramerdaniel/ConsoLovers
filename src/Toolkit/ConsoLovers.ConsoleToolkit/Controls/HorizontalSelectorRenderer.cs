// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HorizontalSelectorRenderer.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

using System;
using System.Collections.Generic;
using System.Linq;

using JetBrains.Annotations;

internal class HorizontalSelectorRenderer<T> : ISelectorRenderer
{
   #region Constants and Fields

   private readonly Dictionary<ListItem<T>, MeasuredSize> measuredItems = new();

   private readonly CSelector<T> selector;

   #endregion

   #region Constructors and Destructors

   public HorizontalSelectorRenderer([NotNull] CSelector<T> selector)
   {
      this.selector = selector ?? throw new ArgumentNullException(nameof(selector));
   }

   #endregion

   #region ISelectorRenderer Members

   public MeasuredSize Measure(int availableWidth)
   {
      var height = 0;
      var width = 0;

      foreach (var item in Items)
      {
         var itemSize = item.Measure(availableWidth);
         measuredItems[item] = itemSize;
         height = Math.Max(height, itemSize.Height);
         width += itemSize.MinWidth + 1;
      }

      var size = new MeasuredSize { Height = height + 1, MinWidth = width };
      return size;
   }

   public IEnumerable<Segment> RenderLine(IRenderContext context, int line)
   {
      if (line == context.Size.Height - 1)
         return RenderSelector();

      return RenderItems(context, line);
   }

   public void HandleKeyInput(IKeyInputContext context)
   {
      switch (context.KeyEventArgs.Key)
      {
         case ConsoleKey.LeftArrow:
            DecreaseSelectedIndex();
            break;
         case ConsoleKey.RightArrow:
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

   #endregion

   #region Public Properties

   public IList<ListItem<T>> Items => selector.Items;

   #endregion

   #region Methods

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

   private IEnumerable<Segment> RenderItems(IRenderContext context, int line)
   {
      foreach (var item in Items.Where(ShouldBeRendered))
      {
         var isSelectedItem = item == selector.SelectedItem;

         foreach (var segment in item.RenderLine(context, line))
         {
            if (isSelectedItem)
            {
               yield return segment.WithStyle(selector.SelectionStyle);
            }
            else
            {
               yield return segment;
            }
         }

         yield return new Segment(selector, " ", selector.Style);
      }

      bool ShouldBeRendered(ListItem<T> candidate)
      {
         if (measuredItems.TryGetValue(candidate, out var size) && size.Height > line)
            return true;
         return false;
      }
   }

   private IEnumerable<Segment> RenderSelector()
   {
      foreach (var item in Items)
      {
         if (measuredItems.TryGetValue(item, out var size))
         {
            if (item == selector.SelectedItem)
            {
               yield return new Segment(selector, selector.Selector, selector.Style);
            }
            else
            {
               yield return new Segment(selector, string.Empty.PadRight(size.MinWidth + 1), selector.Style);
            }
         }
      }
   }

   #endregion
}