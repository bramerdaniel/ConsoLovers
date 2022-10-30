// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RenderEngine.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

using System;
using System.Collections.Generic;
using System.Linq;

using ConsoLovers.ConsoleToolkit.Core;
using ConsoLovers.ConsoleToolkit.InputHandler;

using JetBrains.Annotations;

public class RenderEngine : IRenderEngine
{
   #region Constants and Fields

   private readonly IConsole console;

   private IInputHandler inputHandler;

   #endregion

   #region Constructors and Destructors

   public RenderEngine([NotNull] IConsole console)
   {
      this.console = console ?? throw new ArgumentNullException(nameof(console));
      RenderingStyle.InitializeDefaultStyle(console);
   }

   #endregion

   #region Public Methods and Operators

   private Dictionary<int, List<RenderInfo>> renderInfos = new();

   public void Render([NotNull] IRenderable renderable)
   {
      if (renderable == null)
         throw new ArgumentNullException(nameof(renderable));
      
      if (renderable is IClickable clickable)
      {
         RenderInteactive(clickable);
      }
      else
      {
         RenderSimple(renderable);
      }
   }

   private void RenderInteactive(IClickable renderable)
   {
      if (renderable == null)
         throw new ArgumentNullException(nameof(renderable));

      var availableSize = console.WindowWidth;
      var size = renderable.Measure(availableSize);

      inputHandler = InputHandlerFactory.GetInputHandler();
      inputHandler.MouseClicked += OnMouseClicked;
      inputHandler.Start();

      for (int line = 0; line < size.Height; line++)
      {
         var context = ComputeContext(renderable, size);
         foreach (var segment in renderable.RenderLine(context, line))
         {
            UpdateRenderInfo(segment);
            availableSize = WriteSegment(segment, availableSize);
         }

         console.WriteLine();
         availableSize = console.WindowWidth;
      }

      inputHandler.Wait();
   }

   private void UpdateRenderInfo(Segment segment)
   {
      if (!renderInfos.TryGetValue(console.CursorTop, out var lineRenderInfos))
      {
         lineRenderInfos = new List<RenderInfo>();
         renderInfos.Add(console.CursorTop, lineRenderInfos);
      }

      var renderInfo = new RenderInfo(console.CursorTop, console.CursorLeft, segment);
      lineRenderInfos.Add(renderInfo);
   }

   private void RenderSimple(IRenderable renderable)
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

   private void OnMouseClicked(object sender, MouseEventArgs e)
   {
      // Console.WriteLine($"Clicked Left={e.WindowLeft}, Top={e.WindowTop}");
      var clickable = FindClickable(e.WindowTop, e.WindowLeft);
      if (clickable != null)
      {
         inputHandler.Stop();
         clickable.NotifyClicked();
      }
   }

   private IClickable FindClickable(int line, int column)
   {
      if (renderInfos.TryGetValue(line, out var lineInfos))
      {
         foreach (var renderInfo in lineInfos)
         {
            if (renderInfo.Column <= column &&  column <= renderInfo.EndColumn)
            {
               return (IClickable)renderInfo.Segment.Renderable;
            }
         }
      }

      return null;
   }

   private RenderContext ComputeContext(IRenderable renderable, MeasuredSize measuredSize)
   {
      if (renderable is IHaveAlignment hasAlignment)
      {
         if (hasAlignment.Alignment == Alignment.Right)
         {
            var left = console.WindowWidth - measuredSize.MinWidth;
            console.CursorLeft = left;
            return new RenderContext { AvailableWidth = measuredSize.MinWidth };
         }

         if (hasAlignment.Alignment == Alignment.Center)
         {
            var remaining = console.WindowWidth - measuredSize.MinWidth;
            console.CursorLeft = remaining / 2;
            return new RenderContext { AvailableWidth = measuredSize.MinWidth };
         }
      }

      return new RenderContext { AvailableWidth = measuredSize.MinWidth };
   }

   private int WriteSegment(Segment segment, int availableSize)
   {
      console.Write(segment.Text, segment.Style.Foreground, segment.Style.Background);
      return availableSize - segment.Width;
   }

   #endregion
}