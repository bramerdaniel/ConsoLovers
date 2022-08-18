// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IApplicationLogic.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

using System.Threading;
using System.Threading.Tasks;

public interface IApplicationLogic
{
   Task ExecuteAsync<T>(T arguments, CancellationToken cancellationToken);
}