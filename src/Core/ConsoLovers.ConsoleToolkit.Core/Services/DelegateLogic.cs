// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DelegateLogic.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Services;

using System;
using System.Threading;
using System.Threading.Tasks;

using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

using JetBrains.Annotations;

public class DelegateLogic<TLogic> : IApplicationLogic
   where TLogic : class
{
   private readonly Func<TLogic, CancellationToken, Task> logic;

   public DelegateLogic([NotNull] Func<TLogic, CancellationToken, Task> logic)
   {
      this.logic = logic ?? throw new ArgumentNullException(nameof(logic));
   }

   public Task ExecuteAsync<T>(T arguments, CancellationToken cancellationToken)
   {
      return logic(arguments as TLogic, cancellationToken);

   }
}