// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IExecutable.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Builders;

using System.Threading;
using System.Threading.Tasks;

public interface IExecutable<T>
where T : class
{
   /// <summary>Gets the arguments.</summary>
   T Arguments { get; }

   Task<IExecutable<T>> RunAsync(string args, CancellationToken cancellation);
   Task<IExecutable<T>> RunAsync(string[] args, CancellationToken cancellation);
}