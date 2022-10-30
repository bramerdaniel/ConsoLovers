// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KeyInputContext.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

using System;

using ConsoLovers.ConsoleToolkit.Core.Input;
using ConsoLovers.ConsoleToolkit.InputHandler;

using JetBrains.Annotations;

internal class KeyInputContext : IKeyInputContext
{
   #region Constructors and Destructors

   public KeyInputContext([NotNull] KeyEventArgs args)
   {
      KeyEventArgs = args ?? throw new ArgumentNullException(nameof(args));
   }

   #endregion

   #region IKeyInputContext Members

   public KeyEventArgs KeyEventArgs { get; }

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

   #endregion

   #region Public Properties

   public bool Accepted { get; private set; }

   public bool Canceled => CancellationAction != null;

   public Action CancellationAction { get; private set; }

   #endregion
}