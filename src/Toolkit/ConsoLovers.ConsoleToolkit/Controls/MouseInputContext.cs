// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MouseInputContext.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

using System;

using ConsoLovers.ConsoleToolkit.Core.Input;
using ConsoLovers.ConsoleToolkit.InputHandler;

using JetBrains.Annotations;

internal class MouseInputContext : IMouseInputContext
{
   public MouseEventArgs MouseEventArgs { get; }

   public MouseInputContext([NotNull] MouseEventArgs mouseEventArgs)
   {
      MouseEventArgs = mouseEventArgs ?? throw new ArgumentNullException(nameof(mouseEventArgs));
   }

   public void Cancel()
   {
      Cancel(() => throw new InputCanceledException("Selection"));
   }

   public void Cancel([NotNull] Action cancellationAction)
   {
      CancellationAction = cancellationAction ?? throw new ArgumentNullException(nameof(cancellationAction));
   }

   public void Accept()
   {
      Accepted = true;
   }

   public bool Accepted { get; private set; }

   public bool Canceled => CancellationAction != null;

   public Action CancellationAction { get; private set; }
}