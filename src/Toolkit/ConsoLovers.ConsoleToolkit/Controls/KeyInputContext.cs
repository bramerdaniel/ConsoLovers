// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KeyInputContext.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

using System;

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
      Canceled = true;
   }

   #endregion

   #region Public Properties

   public bool Canceled { get; set; }

   #endregion
}