// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CSelector.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

using System;
using System.Collections.Generic;

using JetBrains.Annotations;

public class CSelector<T> : InteractiveRenderable, IKeyInputHandler, IHaveAlignment
{
   #region Constants and Fields

   private Orientation orientation;

   private ISelectorRenderer renderer;

   private int selectedIndex;

   private string selector;

   #endregion

   #region Constructors and Destructors

   public CSelector()
   {
      Items = new List<ListItem<T>>(5);
      Orientation = Orientation.Vertical;
      renderer = new VerticalSelectorRenderer<T>(this);
   }

   #endregion

   #region IHaveAlignment Members

   public Alignment Alignment { get; set; }

   #endregion

   #region IKeyInputHandler Members

   public void HandleKeyInput(IKeyInputContext context)
   {
      if(context.KeyEventArgs.Key == ConsoleKey.Escape && !AllowCancellation)
         return;

      Renderer.HandleKeyInput(context);
   }

   #endregion

   #region Public Properties

   /// <summary>Gets or sets a value indicating whether user is allowed  to cancel the selection or not.</summary>
   public bool AllowCancellation { get; set; } = true;

   public IList<ListItem<T>> Items { get; }

   public Orientation Orientation
   {
      get => orientation;
      set
      {
         if (orientation == value)
            return;

         orientation = value;
         switch (orientation)
         {
            case Orientation.Vertical:
               Renderer = new VerticalSelectorRenderer<T>(this);
               break;
            case Orientation.Horizontal:
               Renderer = new HorizontalSelectorRenderer<T>(this);
               break;
            default:
               throw new ArgumentOutOfRangeException(nameof(value), value, null);
         }
      }
   }

   public int SelectedIndex
   {
      get => selectedIndex;
      set
      {
         if (selectedIndex == value)
            return;

         selectedIndex = value;
         Invalidate();
      }
   }

   /// <summary>Gets the selected item.</summary>
   public ListItem<T> SelectedItem
   {
      get
      {
         if (SelectedIndex >= 0 && SelectedIndex < Items.Count)
            return Items[SelectedIndex];
         return null;
      }
      internal set => SelectedIndex = Items.IndexOf(value);
   }

   /// <summary>Gets the selected item.</summary>
   public T SelectedValue => SelectedItem == null ? default : SelectedItem.Value;

   public RenderingStyle SelectionStyle { get; set; } = RenderingStyle.Selection;

   public string Selector
   {
      get => selector ?? UseDefaultSelector(Orientation);
      set => selector = value;
   }

   private string UseDefaultSelector(Orientation ori)
   {
      return ori switch
      {
         Orientation.Vertical => "> ",
         Orientation.Horizontal => "^",
         _ => throw new ArgumentOutOfRangeException(nameof(ori))
      };
   }

   #endregion

   #region Properties

   private ISelectorRenderer Renderer
   {
      get => renderer;
      set
      {
         if (renderer == value)
            return;

         renderer = value;
         Invalidate();
      }
   }

   #endregion

   #region Public Methods and Operators

   public void Add(T value, [NotNull] string displayText)
   {
      if (displayText == null)
         throw new ArgumentNullException(nameof(displayText));

      Items.Add(new ListItem<T>(this, value, new CText(displayText)));
   }

   public void Add(T value, [NotNull] IRenderable template)
   {
      if (template == null)
         throw new ArgumentNullException(nameof(template));

      Items.Add(new ListItem<T>(this, value, template));
   }

   public void Add(T value)
   {
      Items.Add(new ListItem<T>(this, value));
   }

   public override MeasuredSize Measure(int availableWidth)
   {
      return Renderer.Measure(availableWidth);
   }

   public override IEnumerable<Segment> RenderLine(IRenderContext context, int line)
   {
      return Renderer.RenderLine(context, line);
   }

   #endregion
}