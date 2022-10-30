// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GList.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

using System;
using System.Collections.Generic;
using System.Linq;

using JetBrains.Annotations;

public class GList<T> : InteractiveRenderable, IKeyInputHandler, IHaveAlignment
{
   #region Constants and Fields

   private Queue<ItemRenderInfo> renderQueue;

   private int selectedIndex;

   #endregion

   #region Constructors and Destructors

   public GList()
   {
      Items = new List<ListItem<T>>(5);
   }

   #endregion

   #region IHaveAlignment Members

   public Alignment Alignment { get; set; }

   #endregion

   #region IKeyInputHandler Members

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
         case ConsoleKey.Enter:
            context.Cancel();
            break;
      }
   }

   #endregion

   #region Public Properties

   public IList<ListItem<T>> Items { get; }

   public int SelectedIndex
   {
      get => selectedIndex;
      set
      {
         if (selectedIndex == value)
            return;

         selectedIndex = value;
         RaiseInvalidated();
      }
   }

   /// <summary>Gets the selected item.</summary>
   public T SelectedItem
   {
      get
      {
         if (SelectedIndex >= 0 && SelectedIndex < Items.Count)
            return Items[SelectedIndex].Value;
         return default;
      }
   }

   public RenderingStyle SelectionStyle { get; set; } = RenderingStyle.Selection;

   public string Selector { get; set; } = "> ";

   #endregion

   #region Public Methods and Operators

   public void Add([NotNull] T value, string displayText)
   {
      Items.Add(new ListItem<T>(value, new CText(displayText)));
   }

   public void Add([NotNull] T value, IRenderable template)
   {
      Items.Add(new ListItem<T>(value, template));
   }

   public void Add([NotNull] T value)
   {
      Items.Add(new ListItem<T>(value));
   }

   public override MeasuredSize Measure(int availableWidth)
   {
      var height = 0;
      var width = 0;

      renderQueue = new Queue<ItemRenderInfo>();

      int itemIndex = 0;
      var availableItemLength = availableWidth - Selector.Length;

      foreach (var item in Items)
      {
         var itemSize = item.Measure(availableItemLength);

         height += itemSize.Height;
         width = Math.Max(width, itemSize.MinWidth);

         for (var i = 0; i < itemSize.Height; i++)
         {
            var data = new ItemRenderInfo
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

      width = width + Selector.Length;
      return new MeasuredSize { Height = height, MinWidth = width };
   }

   public override IEnumerable<Segment> RenderLine(IRenderContext context, int line)
   {
      var data = renderQueue.Dequeue();
      var itemIndex = data.ItemLine;

      if (data.ItemIndex == selectedIndex && data.AppendSelector)
      {
         yield return new Segment(this, $"{Selector}", SelectionStyle);
      }
      else
      {
         yield return new Segment(this, string.Empty.PadRight(Selector.Length), data.Item.Style);
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

   private static bool AppendSelector(int line, int height)
   {
      if (height == 1 || height == 2)
         return line == 0;

      var halfHeight = (height - 1) / 2;
      return line == halfHeight;
   }

   private void DecreaseSelectedIndex()
   {
      var nextIndex = SelectedIndex - 1;
      if (nextIndex < 0)
         nextIndex = Items.Count - 1;

      SelectedIndex = nextIndex;
   }

   private void IncreaseSelectedIndex()
   {
      var nextIndex = SelectedIndex + 1;
      if (nextIndex >= Items.Count)
         nextIndex = 0;

      SelectedIndex = nextIndex;
   }

   #endregion

   struct ItemRenderInfo
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

public class ListItem<T> : Renderable
{
   public T Value { get; }

   public IRenderable Template { get; }

   public ListItem(T value)
   : this(value, new CText(value.ToString()))
   {
   }

   public ListItem(T value, [NotNull] IRenderable template)
   {
      Value = value;
      Template = template ?? throw new ArgumentNullException(nameof(template));
   }

   public override MeasuredSize Measure(int availableWidth)
   {
      return Template.Measure(availableWidth);
   }

   public override IEnumerable<Segment> RenderLine(IRenderContext context, int line)
   {
      return Template.RenderLine(context, line);
   }
}