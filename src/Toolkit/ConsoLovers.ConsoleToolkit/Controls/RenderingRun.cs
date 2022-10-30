// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RenderingRun.cs" company="ConsoLovers">
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

internal class RenderingRun : IDisposable
{
   #region Constants and Fields

   private readonly IConsole console;

   private readonly List<Action> disposeActions = new();

   private readonly IInputHandler inputHandler;

   private readonly Dictionary<int, List<RenderInfo>> renderInfos = new();

   private readonly IRenderable root;

   #endregion

   #region Constructors and Destructors

   public RenderingRun([NotNull] IConsole console, [NotNull] IRenderable root)
   {
      this.console = console ?? throw new ArgumentNullException(nameof(console));
      this.root = root ?? throw new ArgumentNullException(nameof(root));
      inputHandler = InputHandlerFactory.GetInputHandler();
   }

   #endregion

   #region IDisposable Members

   public void Dispose()
   {
      inputHandler.MouseClicked -= OnMouseClicked;
      inputHandler.KeyDown -= OnKeyDown;

      foreach (var action in disposeActions)
         action();

      disposeActions.Clear();
   }

   #endregion

   #region Public Methods and Operators

   public void RenderOnce()
   {
      RenderInternal(false);
   }

   public void Start()
   {
      inputHandler.MouseClicked += OnMouseClicked;
      inputHandler.KeyDown += OnKeyDown;
      inputHandler.Start();

      RenderInternal(true);
   }

   public void UpdateRenderInfo(Segment segment)
   {
      if (!renderInfos.TryGetValue(console.CursorTop, out var lineRenderInfos))
      {
         lineRenderInfos = new List<RenderInfo>();
         renderInfos.Add(console.CursorTop, lineRenderInfos);
      }

      var renderInfo = new RenderInfo(console.CursorTop, console.CursorLeft, segment);
      lineRenderInfos.Add(renderInfo);
   }

   public void Wait()
   {
      inputHandler.Wait();
   }

   #endregion

   #region Methods

   private void AttachToInteractiveEvents(IRenderable renderable)
   {
      if (renderable is IInteractiveRenderable interactiveRenderable)
      {
         interactiveRenderable.Invalidated += OnRenderableInvalidated;
         disposeActions.Add(() => interactiveRenderable.Invalidated -= OnRenderableInvalidated);
      }
   }

   private void AttachToInteractiveEvents(IRenderable renderable, HashSet<IRenderable> attached)
   {
      if (attached.Contains(renderable))
         return;

      AttachToInteractiveEvents(renderable);
      attached.Add(renderable);
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

   private IClickable FindClickable(int line, int column)
   {
      if (renderInfos.TryGetValue(line, out var lineInfos))
      {
         foreach (var renderInfo in lineInfos)
         {
            if (renderInfo.Column <= column && column <= renderInfo.EndColumn)
            {
               return renderInfo.Segment.Renderable as IClickable;
            }
         }
      }

      return null;
   }

   private void NotifyKeyHandlers(KeyEventArgs args)
   {
      var context = new KeyInputContext(args);
      var candidates = GetHandlers().Distinct().ToArray();

      foreach (var renderable in candidates)
         Notify(renderable);

      if (context.Canceled)
         inputHandler.Stop();

      void Notify(IRenderable toNotify)
      {
         if (toNotify is IKeyInputHandler handler)
            handler.HandleKeyInput(context);
      }

      IEnumerable<IRenderable> GetHandlers()
      {
         yield return root;
         foreach (var lineInfo in renderInfos.Values)
         {
            foreach (var renderInfo in lineInfo)
            {
               var renderable = renderInfo.Segment.Renderable;
               yield return renderable;
            }
         }
      }
   }

   private void OnKeyDown(object sender, KeyEventArgs e)
   {
      NotifyKeyHandlers(e);
   }

   private void OnMouseClicked(object sender, MouseEventArgs e)
   {
      var clickable = FindClickable(e.WindowTop, e.WindowLeft);
      if (clickable != null)
      {
         inputHandler.Stop();
         clickable.NotifyClicked();
      }
   }

   private void OnRenderableInvalidated(object sender, EventArgs e)
   {
      Refresh();
   }

   private void Refresh()
   {
      // TODO this can be done better !!!
      console.Clear();
      RenderInternal(false);
   }

   private void RenderInternal(bool isFirstRun)
   {
      renderInfos.Clear();
      var availableSize = console.WindowWidth;
      var size = root.Measure(availableSize);

      var attached = new HashSet<IRenderable>();
      if (isFirstRun)
         AttachToInteractiveEvents(root, attached);

      for (int line = 0; line < size.Height; line++)
      {
         var context = ComputeContext(root, size);
         foreach (var segment in root.RenderLine(context, line))
         {
            UpdateRenderInfo(segment);
            availableSize = WriteSegment(segment, availableSize);

            if (isFirstRun)
               AttachToInteractiveEvents(segment.Renderable, attached);
         }

         console.WriteLine();
         availableSize = console.WindowWidth;
      }
   }

   private int WriteSegment(Segment segment, int availableSize)
   {
      console.Write(segment.Text, segment.Style.Foreground, segment.Style.Background);
      return availableSize - segment.Width;
   }

   #endregion
}