// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DelegateLogic.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Services;

using System;
using System.Threading;
using System.Threading.Tasks;

using JetBrains.Annotations;

public class DelegateLogic<TLogic> : IApplicationLogic
   where TLogic : class
{
   #region Constants and Fields

   private readonly Func<TLogic, CancellationToken, Task> logic;

   #endregion

   #region Constructors and Destructors

   public DelegateLogic([NotNull] Func<TLogic, CancellationToken, Task> logic)
   {
      this.logic = logic ?? throw new ArgumentNullException(nameof(logic));
   }

   #endregion

   #region IApplicationLogic Members

   public Task ExecuteAsync<T>(T arguments, CancellationToken cancellationToken)
   {
      return logic(arguments as TLogic, cancellationToken);
   }

   #endregion
}