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

      using var renderingRun = new RenderingRun(console, renderable);
      if (interactive)
      {
         renderingRun.Start();
         renderingRun.Wait();
      }
      else
      {
         renderingRun.RenderOnce();
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
      using var renderingRun = new RenderingRun(console, renderable);
      renderingRun.RenderOnce();
   }

   #endregion
}