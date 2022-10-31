// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ListItem.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

using System;
using System.Collections.Generic;

using JetBrains.Annotations;

/// <summary>Represents one item in a <see cref="CSelector{T}"/></summary>
/// <typeparam name="T">The type of the value</typeparam>
/// <seealso cref="ConsoLovers.ConsoleToolkit.Controls.Renderable" />
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
         if(Equals(template, value))
            return;

         template = value;
         Invalidate();
      }
   }

   /// <summary>Gets the value the list item holds.</summary>
   public T Value { get; }

   #endregion

   #region Public Methods and Operators

   public override MeasuredSize Measure(int availableWidth)
   {
      return Template.Measure(availableWidth);
   }

   public RenderingStyle MouseOverStyle
   {
      get => mouseOverStyle ??= Owner.MouseOverStyle;
      set => mouseOverStyle = value;
   }

   public override IEnumerable<Segment> RenderLine(IRenderContext context, int line)
   {
      var segments = Template.RenderLine(context, line);
      foreach (var segment in segments)
      {
         var segmentStyle = isMouseOver ? MouseOverStyle : segment.Style;
         yield return new Segment(this, segment.Text, segmentStyle);
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

      return new CText(value?.ToString() ?? "null");
   }

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
         Invalidate();
      }
   }

   /// <summary>Gets the selector that hold this item</summary>
   public CSelector<T> Owner { get; }
}