// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IApplicationLogic.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

using System.Threading;
using System.Threading.Tasks;

/// <summary>The logic that will be executed when the application is started without commands</summary>
public interface IApplicationLogic
{
   Task ExecuteAsync<T>(T arguments, CancellationToken cancellationToken);
}

/// <summary>The logic that will be executed when the application is started without commands</summary>
/// <typeparam name="T">The type of the arguments your logic will handle</typeparam>
public interface IApplicationLogic<in T>
{
   Task ExecuteAsync(T arguments, CancellationToken cancellationToken);
}