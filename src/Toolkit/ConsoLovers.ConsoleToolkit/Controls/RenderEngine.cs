// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RenderEngine.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

using System;

using ConsoLovers.ConsoleToolkit.Core;

using JetBrains.Annotations;

public class RenderEngine : IRenderEngine
{
   #region Constants and Fields

   private readonly IConsole console;

   #endregion

   #region Constructors and Destructors

   public RenderEngine([NotNull] IConsole console)
   {
      this.console = console ?? throw new ArgumentNullException(nameof(console));
      RenderingStyle.InitializeDefaultStyle(console);
   }

   #endregion

   #region Public Methods and Operators

   public void Render([NotNull] IRenderable renderable, bool interactive)
   {
      if (renderable == null)
         throw new ArgumentNullException(nameof(renderable));

      if (interactive)
      {
         RenderInteractive(renderable);
      }
      else
      {
         RenderNonInteractive(renderable);
      }
   }

   public void Render([NotNull] IRenderable renderable)
   {
      Render(renderable, false);
   }

   private void RenderInteractive(IRenderable renderable)
   {
      if (renderable == null)
         throw new ArgumentNullException(nameof(renderable));

      using var renderingRun = new RenderingRun(console, renderable);
      renderingRun.Start();
      renderingRun.Wait();
   }

   private void RenderNonInteractive(IRenderable renderable)
   {
      var availableSize = console.WindowWidth;
      var size = renderable.Measure(availableSize);

      for (int line = 0; line < size.Height; line++)
      {
         var context = ComputeContext(renderable, size);
         foreach (var segment in renderable.RenderLine(context, line))
            availableSize = WriteSegment(segment, availableSize);

         console.WriteLine();
         availableSize = console.WindowWidth;
      }
   }

   private RenderContext ComputeContext(IRenderable renderable, MeasuredSize measuredSize)
   {
      if (renderable is IHaveAlignment hasAlignment)
      {
         if (hasAlignment.Alignment == Alignment.Right)
         {
            var left = console.WindowWidth - measuredSize.MinWidth;
            console.CursorLeft = left;
            return new RenderContext { AvailableWidth = measuredSize.MinWidth, Size = measuredSize };
         }

         if (hasAlignment.Alignment == Alignment.Center)
         {
            var remaining = console.WindowWidth - measuredSize.MinWidth;
            console.CursorLeft = remaining / 2;
            return new RenderContext { AvailableWidth = measuredSize.MinWidth, Size = measuredSize };
         }
      }

      return new RenderContext { AvailableWidth = measuredSize.MinWidth, Size = measuredSize };
   }

   private int WriteSegment(Segment segment, int availableSize)
   {
      console.Write(segment.Text, segment.Style.Foreground, segment.Style.Background);
      return availableSize - segment.Width;
   }

   #endregion
}