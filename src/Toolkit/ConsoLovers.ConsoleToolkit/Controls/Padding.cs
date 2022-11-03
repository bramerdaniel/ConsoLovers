// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Padding.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

using System;
using System.Collections.Generic;

public class Padding : InteractiveRenderable
{
   #region Constants and Fields

   private readonly IRenderable content;

   private Thickness value;

   #endregion

   #region Constructors and Destructors

   public Padding(IRenderable content)
      : this(content, new Thickness(1))
   {
      this.content = content;
   }

   public Padding(IRenderable content, Thickness value)
   {
      this.content = content;
      Value = value;
   }

   #endregion

   #region Public Properties

   public Thickness Value
   {
      get => value;
      set
      {
         if (Equals(this.value, value))
            return;

         this.value = value;
         Invalidate(InvalidationScope.All);
      }
   }

   #endregion

   #region Properties

   public int Bottom
   {
      get => Math.Max(Value.Bottom, 0);
      set => Value = new Thickness(Left, Top, Right, Math.Max(value, 0));
   }

   public int Left
   {
      get => Math.Max(Value.Left, 0);
      set => Value = new Thickness(Math.Max(value, 0), Top, Right, Bottom);
   }

   public int Right
   {
      get => Math.Max(Value.Right, 0);
      set => Value = new Thickness(Left, Top, Math.Max(value, 0), Bottom);
   }

   public int Top
   {
      get => Math.Max(Value.Top, 0);
      set => Value = new Thickness(Left, Math.Max(value, 0), Right, Bottom);
   }

   #endregion

   #region Public Methods and Operators

   public override IEnumerable<IRenderable> GetChildren()
   {
      if (content != null)
         yield return content;
   }

   public override RenderSize MeasureOverride(IRenderContext context, int availableWidth)
   {
      var childSize = context.Measure(content, availableWidth);
      return new RenderSize
      {
         Height = childSize.Height + Top + Bottom,
         Width = childSize.Width + Left + Right
      };
   }

   public override IEnumerable<Segment> RenderLine(IRenderContext context, int line)
   {
      var contentSize = context.GetMeasuredSize(content);
      if (line < Top)
      {
         yield return new Segment(this, string.Empty.PadRight(MeasuredSize.Width), Style);
      }
      else if (line >= Top && line < Top + contentSize.Height)
      {
         if (Left > 0)
            yield return new Segment(this, string.Empty.PadRight(Left), Style);

         foreach (var segment in content.RenderLine(context, line - Top))
            yield return segment;

         if (Right > 0)
            yield return new Segment(this, string.Empty.PadRight(Right), Style);
      }
      else if (line >= Top + contentSize.Height)
      {
         yield return new Segment(this, string.Empty.PadRight(MeasuredSize.Width), Style);
      }
   }

   #endregion
}