// --------------------------------------------------------------------------------------------------------------------
// <copyright file="List.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

using System;
using System.Collections.Generic;
using System.Linq;

using JetBrains.Annotations;

public class CList : InteractiveRenderable, IKeyInputHandler
{
   #region Constants and Fields

   private Queue<Renderdata> renderQueue;

   private int selectedIndex;

   #endregion

   #region Constructors and Destructors

   public CList(params IRenderable[] items)
   {
      Items = items.ToList();
   }

   #endregion

   #region IKeyInputHandler Members

   public void HandleKeyInput(IKeyInputContext context)
   {
      switch (context.KeyEventArgs.Key)
      {
         case ConsoleKey.UpArrow:
            DecreaseSelectedIndex();
            RaiseInvalidated();
            break;
         case ConsoleKey.DownArrow:
            IncreaseSelectedIndex();
            RaiseInvalidated();
            break;
         case ConsoleKey.Enter:
            context.Cancel();
            break;
      }
   }

   #endregion

   #region Public Properties

   public IList<IRenderable> Items { get; }

   public int SelectedIndex
   {
      get => selectedIndex;
      set => selectedIndex = value;
   }

   public RenderingStyle SelectionStyle { get; set; } = RenderingStyle.Selection;

   public string Selector { get; set; } = ">";

   #endregion

   #region Public Methods and Operators

   public void Add([NotNull] IRenderable renderable)
   {
      if (renderable == null)
         throw new ArgumentNullException(nameof(renderable));

      Items.Add(renderable);
   }

   public override MeasuredSize Measure(int availableWidth)
   {
      var height = 0;
      var width = 0;

      renderQueue = new Queue<Renderdata>();

      int itemIndex = 0;
      foreach (var item in Items)
      {
         var itemSize = item.Measure(availableWidth);

         height += itemSize.Height;
         width = Math.Max(width, itemSize.MinWidth);

         for (var i = 0; i < itemSize.Height; i++)
         {
            var data = new Renderdata
            {
               Item = item,
               ItemLine = i,
               ItemIndex = itemIndex,
               AppendSelector = AppendSelector(i, itemSize.Height),
               Width = itemSize.MinWidth
            };
            renderQueue.Enqueue(data);
         }

         itemIndex++;
      }

      width = width + 1 + Selector.Length;
      return new MeasuredSize { Height = height, MinWidth = width };
   }

   private static bool AppendSelector(int line, int height)
   {
      if (height == 1 || height == 2)
         return line == 0;

      var halfHeight = (height - 1) / 2;
      return line == halfHeight;
   }

   public override IEnumerable<Segment> RenderLine(IRenderContext context, int line)
   {
      var data = renderQueue.Dequeue();
      var itemIndex = data.ItemLine;

      if (data.ItemIndex == selectedIndex && data.AppendSelector)
      {
         yield return new Segment(this, $"{Selector} ", SelectionStyle);
      }
      else
      {
         yield return new Segment(this, string.Empty.PadRight(Selector.Length + 1), data.Item.Style);
      }

      var renderContext = new RenderContext { AvailableWidth = data.Width };
      var segments = data.Item.RenderLine(renderContext, itemIndex).ToArray();
      foreach (var segment in segments)
      {
         yield return data.ItemIndex == selectedIndex
            ? segment.WithStyle(SelectionStyle)
            : segment;
      }
   }

   #endregion

   #region Methods

   private void DecreaseSelectedIndex()
   {
      SelectedIndex -= 1;
      if (SelectedIndex < 0)
         SelectedIndex = Items.Count - 1;
   }

   private void IncreaseSelectedIndex()
   {
      SelectedIndex += 1;
      if (SelectedIndex >= Items.Count)
         SelectedIndex = 0;
   }

   #endregion

   struct Renderdata
   {
      #region Public Properties

      public bool AppendSelector { get; set; }

      public IRenderable Item { get; set; }

      public int ItemIndex { get; set; }

      public int ItemLine { get; set; }

      public int Width { get; set; }

      #endregion
   }
}