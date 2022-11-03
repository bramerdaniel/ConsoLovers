// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRenderable.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls
{
   using System.Collections.Generic;

   public interface IRenderable
   {
      #region Public Methods and Operators

      /// <summary>Measures the renderable object.</summary>
      /// <param name="availableWidth">The maximum allowed width.</param>
      /// <returns>The minimum and maximum width of the object.</returns>
      RenderSize Measure(int availableWidth);

      /// <summary>Renders the specified <see cref="line"/> of the renderable.</summary>
      /// <param name="context">The context.</param>
      /// <param name="line">The line.</param>
      /// <returns>The segments for this line</returns>
      IEnumerable<Segment> RenderLine(IRenderContext context, int line);
      
      /// <summary>Gets or sets the style the renderable will use.</summary>
      RenderingStyle Style { get; set; }
      
      /// <summary>Gets the children of the <see cref="IRenderable"/>.</summary>
      /// <returns>All children</returns>
      IEnumerable<IRenderable> GetChildren();

      #endregion

   }
}