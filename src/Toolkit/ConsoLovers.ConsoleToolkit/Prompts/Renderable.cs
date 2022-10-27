// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Renderable.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Prompts
{
   using System;

   using JetBrains.Annotations;

   public abstract class Renderable : IRenderable
   {
      #region Constants and Fields

      private RenderingStyle style;

      #endregion

      #region Constructors and Destructors

      protected Renderable()
         : this(RenderingStyle.Default)
      {
      }

      protected Renderable([NotNull] RenderingStyle style)
      {
         this.style = style ?? throw new ArgumentNullException(nameof(style));
      }

      #endregion

      #region IRenderable Members

      public abstract Measurement Measure(int maxWidth);

      public abstract void Render(IRenderContext context);

      public RenderingStyle Style
      {
         get => style;
         set => style = value;
      }

      #endregion
   }
}