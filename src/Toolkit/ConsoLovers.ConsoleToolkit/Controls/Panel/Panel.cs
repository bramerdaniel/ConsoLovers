// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Panel.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

using System;
using System.Collections.Generic;

using JetBrains.Annotations;

public class Panel : InteractiveRenderable, IHaveAlignment
{
   #region Constants and Fields

   private Orientation orientation;

   #endregion

   #region Constructors and Destructors

   public Panel()
      : this(RenderingStyle.Default)
   {
   }

   public Panel(RenderingStyle style)
      : base(style)
   {
      Children = new List<IRenderable>(5);

      orientation = Orientation.Horizontal;
      Renderer = new HorizontalPanelRenderer(this);
   }

   #endregion

   #region IHaveAlignment Members

   public Alignment Alignment { get; set; }

   #endregion

   #region Public Properties

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
               Renderer = new VerticalPanelRenderer(this);
               break;
            case Orientation.Horizontal:
               Renderer = new HorizontalPanelRenderer(this);
               break;
            default:
               throw new ArgumentOutOfRangeException(nameof(value), value, null);
         }
      }
   }

   public Thickness Padding { get; set; }

   #endregion

   #region Properties

   internal List<IRenderable> Children { get; }

   private IPanelRenderer Renderer { get; set; }

   #endregion

   #region Public Methods and Operators

   public Panel Add([NotNull] IRenderable renderable)
   {
      if (renderable == null)
         throw new ArgumentNullException(nameof(renderable));

      Children.Add(renderable);
      Invalidate();
      return this;
   }

   public override RenderSize MeasureOverride(int availableWidth)
   {
      return Renderer.Measure(availableWidth);
   }

   public Panel Remove([NotNull] IRenderable renderable)
   {
      if (renderable == null)
         throw new ArgumentNullException(nameof(renderable));

      if (Children.Remove(renderable))
         Invalidate();

      return this;
   }

   public override IEnumerable<Segment> RenderLine(IRenderContext context, int line)
   {
      // context.RegisterInteractive(this);
      return Renderer.RenderLine(context, line);
   }

   #endregion
}