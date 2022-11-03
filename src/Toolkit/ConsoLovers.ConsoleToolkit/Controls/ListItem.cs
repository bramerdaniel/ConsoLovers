// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ListItem.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using JetBrains.Annotations;

/// <summary>Represents one item in a <see cref="CSelector{T}"/></summary>
/// <typeparam name="T">The type of the value</typeparam>
/// <seealso cref="ConsoLovers.ConsoleToolkit.Controls.Renderable" />
[DebuggerDisplay("{Value} : {Template.GetType().Name}")]
public class ListItem<T> : InteractiveRenderable, IMouseInputHandler, IMouseAware
{
   private bool isMouseOver;

   private IRenderable template;

   private RenderingStyle mouseOverStyle;
   
   #region Constructors and Destructors

   public ListItem(CSelector<T> owner, T value)
      : this(owner, value, CreateTemplate(value))
   {
   }

   public ListItem([NotNull] CSelector<T> owner, T value, [NotNull] IRenderable template)
   {
      Owner = owner ?? throw new ArgumentNullException(nameof(owner));
      Template = template ?? throw new ArgumentNullException(nameof(template));
      Value = value;
   }

   #endregion

   #region Public Properties

   /// <summary>Gets the <see cref="IRenderable"/> that is displayed when rendered.</summary>
   public IRenderable Template
   {
      get => template;
      set
      {
         if (Equals(template, value))
            return;

         template = value;
         Invalidate(InvalidationScope.All);
      }
   }

   /// <summary>Gets the value the list item holds.</summary>
   public T Value { get; }

   #endregion

   #region Public Methods and Operators

   public override IEnumerable<IRenderable> GetChildren()
   {
      yield return Template;
   }

   public override RenderSize MeasureOverride(IRenderContext context, int availableWidth)
   {
      return context.Measure(Template, availableWidth);
   }

   public RenderingStyle MouseOverStyle
   {
      get => mouseOverStyle ??= Owner.MouseOverStyle;
      set => mouseOverStyle = value;
   }

   public override IEnumerable<Segment> RenderLine(IRenderContext context, int line)
   {
      var segments = context.RenderLine(Template, line).ToList();
      foreach (var segment in segments)
      {
         var result = segment;
         if (IsSelected)
            result = result.OverrideStyle(Owner.SelectionStyle);
         if (isMouseOver)
            result = result.OverrideStyle(new RenderingStyle(null, ConsoleColor.DarkGray));
         
         yield return new Segment(this, result.Text, result.Style);
      }
   }

   public void HandleMouseInput(IMouseInputContext context)
   {
      Owner.SelectedItem = this;
      context.Accept();
   }

   #endregion

   #region Methods

   private static IRenderable CreateTemplate(T value)
   {
      if (value is IRenderable renderable)
         return renderable;

      return new Text(value?.ToString() ?? "null");
   }

   public bool IsSelected => Owner.SelectedItem == this;

   #endregion

   /// <summary>Gets or sets a value indicating whether this instance is mouse over.</summary>
   /// <value>
   ///   <c>true</c> if this instance is mouse over; otherwise, <c>false</c>.
   /// </value>
   bool IMouseAware.IsMouseOver
   {
      get => isMouseOver;
      set
      {
         if (isMouseOver == value)
            return;

         isMouseOver = value;
         Invalidate(InvalidationScope.Style);
      }
   }

   /// <summary>Gets the selector that hold this item</summary>
   public CSelector<T> Owner { get; }
}