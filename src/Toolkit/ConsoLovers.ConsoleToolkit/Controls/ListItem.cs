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
public class ListItem<T> : Renderable
{
   #region Constructors and Destructors

   public ListItem(T value)
      : this(value, CreateTemplate(value))
   {
   }

   public ListItem(T value, [NotNull] IRenderable template)
   {
      Value = value;
      Template = template ?? throw new ArgumentNullException(nameof(template));
   }

   #endregion

   #region Public Properties

   public IRenderable Template { get; }

   public T Value { get; }

   #endregion

   #region Public Methods and Operators

   public override MeasuredSize Measure(int availableWidth)
   {
      return Template.Measure(availableWidth);
   }

   public override IEnumerable<Segment> RenderLine(IRenderContext context, int line)
   {
      return Template.RenderLine(context, line);
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
}