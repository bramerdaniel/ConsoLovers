// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RenderContext.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Prompts;

using System;

using ConsoLovers.ConsoleToolkit.Core;

using JetBrains.Annotations;

public class RenderContext : IRenderContext
{
   public RenderContext([NotNull] IConsole console)
   {
      Console = console ?? throw new ArgumentNullException(nameof(console));
   }

   public IConsole Console { get; }
}