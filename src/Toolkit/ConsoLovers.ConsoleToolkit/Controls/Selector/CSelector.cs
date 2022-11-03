// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CSelector.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

using System;
using System.Collections.Generic;
using System.Linq;

using JetBrains.Annotations;

public class CSelector<T> : InteractiveRenderable, IKeyInputHandler, IHaveAlignment
{
   #region Constants and Fields

   private Orientation orientation;

   private ISelectorRenderer renderer;

   private int selectedIndex;

   private string selector;

   private SelectorStyles styles;

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
      if (context.KeyEventArgs.Key == ConsoleKey.Escape && !AllowCancellation)
         return;

      Renderer.HandleKeyInput(context);
   }

   #endregion

   #region Public Properties

   /// <summary>Gets or sets a value indicating whether user is allowed  to cancel the selection or not.</summary>
   public bool AllowCancellation { get; set; } = true;

   // TODO make items readonly
   public IList<ListItem<T>> Items { get; }

   /// <summary>Gets or sets the style the item the mouse is over will have.</summary>
   public RenderingStyle MouseOverStyle
   {
      get => Styles.MouseOverStyle;
      set => Styles.MouseOverStyle = value;
   }

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
         NotifyStyleChanged();
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

   /// <summary>Gets or sets the style the selected item will have.</summary>
   public RenderingStyle SelectionStyle
   {
      get => Styles.SelectionStyle;
      set => Styles.SelectionStyle = value;
   }

   public string Selector
   {
      get => selector ?? UseDefaultSelector(Orientation);
      set => selector = value;
   }

   /// <summary>Gets or sets all the styles this selector uses.</summary>
   public SelectorStyles Styles
   {
      get => styles ??= new SelectorStyles();
      set => styles = value;
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
         Invalidate(InvalidationScope.All);
      }
   }

   #endregion

   #region Public Methods and Operators

   /// <summary>Adds the specified value to the selector using the <see cref="displayText"/>.</summary>
   public void Add(T value, [NotNull] string displayText)
   {
      if (displayText == null)
         throw new ArgumentNullException(nameof(displayText));

      Items.Add(new ListItem<T>(this, value, new CText(displayText)));
   }

   /// <summary>Adds the specified value to the selector using the <see cref="template"/>.</summary>
   public void Add(T value, [NotNull] IRenderable template)
   {
      if (template == null)
         throw new ArgumentNullException(nameof(template));

      Items.Add(new ListItem<T>(this, value, template));
   }

   /// <summary>Adds the specified value to the selector.</summary>
   public void Add(T value)
   {
      Items.Add(new ListItem<T>(this, value));
   }

   public override IEnumerable<IRenderable> GetChildren()
   {
      return Items;
   }

   public override RenderSize MeasureOverride(int availableWidth)
   {
      return Renderer.Measure(availableWidth);
   }

   public override IEnumerable<Segment> RenderLine(IRenderContext context, int line)
   {
      return Renderer.RenderLine(context, line);
   }

   #endregion

   #region Methods

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
}