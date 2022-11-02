// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RenderingRun.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using ConsoLovers.ConsoleToolkit.Core;
using ConsoLovers.ConsoleToolkit.InputHandler;

using JetBrains.Annotations;

internal class RenderingRun : IDisposable, IRenderContext
{
   #region Constants and Fields

   private readonly IConsole console;

   private readonly List<Action> disposeActions = new();

   private IInputHandler inputHandler;

   private readonly Dictionary<int, List<RenderInfo>> renderInfos = new();

   private readonly IRenderable root;

   private Action cancellationAction;


   private IRenderable hoveredRenderable;

   #endregion

   #region Constructors and Destructors

   public RenderingRun([NotNull] IConsole console, [NotNull] IRenderable root)
   {
      this.console = console ?? throw new ArgumentNullException(nameof(console));
      this.root = root ?? throw new ArgumentNullException(nameof(root));
   }

   class ConsolePosition
   {
      public ConsolePosition(int cursorTop, int cursorLeft)
      {
         CursorLeft = cursorLeft;
         CursorTop = cursorTop;
      }

      public int CursorLeft { get; }

      public int CursorTop { get; }
   }

   #endregion

   #region IDisposable Members

   private ConsolePosition InitialPosition { get; set; }

   public void Dispose()
   {
      if (inputHandler != null)
      {
         inputHandler.KeyDown -= OnKeyDown;
         inputHandler.MouseClicked -= OnMouseClicked;
         inputHandler.MouseMoved -= OnMouseMoved;
      }

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
      inputHandler = InputHandlerFactory.GetInputHandler();
      inputHandler.KeyDown += OnKeyDown;
      inputHandler.MouseClicked += OnMouseClicked;
      inputHandler.MouseMoved += OnMouseMoved;
      inputHandler.Start();

      InitialPosition = new ConsolePosition(console.CursorTop, console.CursorLeft);
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
      // Trace.WriteLine(segment.Text + " = " + console.CursorLeft);
      lineRenderInfos.Add(renderInfo);
   }

   public void Wait()
   {
      inputHandler.Wait();

      if (cancellationAction != null)
         cancellationAction();
   }

   #endregion

   #region Methods

   private void AttachToInteractiveEvents(IRenderable renderable)
   {
      if (renderable is IInteractiveRenderable interactiveRenderable)
      {
         // TODO cache registration and do only once
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

   private void ComputeContext(IRenderable renderable, RenderSize measuredSize)
   {
      if (renderable is IHaveAlignment hasAlignment)
      {
         if (hasAlignment.Alignment == Alignment.Right)
         {
            var left = console.WindowWidth - measuredSize.Width;
            console.CursorLeft = left;
         }

         if (hasAlignment.Alignment == Alignment.Center)
         {
            var remaining = console.WindowWidth - measuredSize.Width;
            console.CursorLeft = remaining / 2;
         }
      }
      
   }

   private IMouseInputHandler FindInputHandler(int line, int column)
   {
      return FindRenderable(line, column) as IMouseInputHandler;
   }

   private IRenderable FindRenderable(int line, int column)
   {
      if (renderInfos.TryGetValue(line, out var lineInfos))
      {
         foreach (var renderInfo in lineInfos)
         {
            if (renderInfo.Column <= column && column <= renderInfo.EndColumn)
            {
               // Trace.WriteLine(renderInfo.Segment.Text  + " => " + column);
               return renderInfo.Segment.Renderable;
            }
         }
      }

      return null;
   }


   private void NotifyKeyHandlers(KeyEventArgs args)
   {
      var context = new KeyInputContext(args);
      foreach (var renderable in GetHandlers().ToArray())
         Notify(renderable);

      CheckForExit(context);

      void Notify(IRenderable toNotify)
      {
         if (toNotify is IKeyInputHandler handler)
            handler.HandleKeyInput(context);
      }
   }

   private IEnumerable<IRenderable> GetHandlers()
   {
      return GetAllHandlers()
         .Distinct();

      IEnumerable<IRenderable> GetAllHandlers()
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

   private void CheckForExit(KeyInputContext context)
   {
      if (context.Accepted)
      {
         inputHandler.Stop();
         return;
      }

      if (context.Canceled)
      {
         cancellationAction = context.CancellationAction;
         inputHandler.Stop();
      }
   }

   private void OnKeyDown(object sender, KeyEventArgs e)
   {
      NotifyKeyHandlers(e);
   }

   private void OnMouseClicked(object sender, MouseEventArgs e)
   {
      NotifyMouseHandlers(e);
   }

   private void OnMouseMoved(object sender, MouseEventArgs e)
   {
      var underMouse = FindRenderable(e.WindowTop, e.WindowLeft + 1);
      HoveredRenderable = underMouse;
   }

   internal IRenderable HoveredRenderable
   {
      get => hoveredRenderable;
      set
      {
         if (Equals(hoveredRenderable, value))
            return;

         var previous = hoveredRenderable;
         hoveredRenderable = value;

         UpdateMouseOver(previous, false);
         UpdateMouseOver(hoveredRenderable, true);
      }
   }

   private void UpdateMouseOver(IRenderable renderable, bool mouseOver)
   {
      if (renderable is IMouseAware mouseAware)
      {
         mouseAware.IsMouseOver = mouseOver;
      }
   }

   private void NotifyMouseHandlers(MouseEventArgs e)
   {
      var mouseHandler = FindInputHandler(e.WindowTop, e.WindowLeft + 1);
      if (mouseHandler != null)
      {
         var context = new MouseInputContext(e);
         mouseHandler.HandleMouseInput(context);

         if (context.Accepted)
            inputHandler.Stop();

         if (context.Canceled)
         {
            inputHandler.Stop();
            cancellationAction = context.CancellationAction;
         }
      }
   }

   private void OnRenderableInvalidated(object sender, EventArgs e)
   {
      // TODO I assume this could cause fragments when previous run produced longer lines!!!
      console.CursorTop = InitialPosition.CursorTop;
      console.CursorLeft = InitialPosition.CursorLeft;

      RenderInternal(false);
   }

   private void RenderInternal(bool attachToEvents)
   {
      renderInfos.Clear();
      var availableSize = console.WindowWidth;
      var measuredSize = root.Measure(availableSize);

      var attached = new HashSet<IRenderable>();
      if (attachToEvents)
         AttachToInteractiveEvents(root, attached);

      for (int line = 0; line < measuredSize.Height; line++)
      {
         ComputeContext(root, measuredSize);
         foreach (var segment in root.RenderLine(this, line))
         {
            UpdateRenderInfo(segment);
            availableSize = WriteSegment(segment, availableSize);

            if (attachToEvents)
               AttachToInteractiveEvents(segment.Renderable, attached);
         }

         console.WriteLine();
         availableSize = console.WindowWidth;
      }
   }

   private int WriteSegment(Segment segment, int availableSize)
   {
      console.Write(segment.Text, segment.Style.GetForeground(console.ForegroundColor), segment.Style.GetBackground(console.BackgroundColor));
      return availableSize - segment.Width;
   }

   #endregion

   public void RegisterInteractive(IInteractiveRenderable renderable)
   {
      AttachToInteractiveEvents(renderable);
   }
}