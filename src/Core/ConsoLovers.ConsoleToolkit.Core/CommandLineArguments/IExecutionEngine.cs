// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IExecutionEngine.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

using System.Threading;
using System.Threading.Tasks;

/// <summary>The service that is responsible for executing commands</summary>
public interface IExecutionEngine
{
   /// <summary>Executes the first mapped command of the <see cref="arguments"/>.</summary>
   /// <typeparam name="T">The type of the arguments to execute</typeparam>
   /// <param name="arguments">The arguments.</param>
   /// <returns>The command that was executed</returns>
   ICommandBase ExecuteCommand<T>(T arguments);

   /// <summary>Executes the first mapped command of the <see cref="arguments"/>.</summary>
   /// <typeparam name="T">The type of the arguments to execute</typeparam>
   /// <param name="arguments">The arguments.</param>
   /// <param name="cancellationToken">The cancellation token.</param>
   /// <returns>The command that was executed</returns>
   Task<ICommandBase> ExecuteCommandAsync<T>(T arguments, CancellationToken cancellationToken);

   /// <summary>Executes the specified command asynchronous.</summary>
   /// <param name="executable">The command to execute.</param>
   /// <param name="cancellationToken">The cancellation token.</param>
   /// <returns>The command that was executed</returns>
   Task<ICommandBase> ExecuteCommandAsync(ICommandBase executable, CancellationToken cancellationToken);

   /// <summary>Executes the specified <see cref="executable"/>.</summary>
   /// <param name="executable">The executable <see cref="ICommandBase"/>.</param>
   /// <param name="cancellationToken">The cancellation token in case the command is an <see cref="IAsyncCommand"/>.</param>
   void ExecuteCommand(ICommandBase executable, CancellationToken cancellationToken);

   /// <summary>Executes the specified <see cref="executable"/>.</summary>
   /// <param name="executable">The executable <see cref="ICommandBase"/>.</param>
   void ExecuteCommand(ICommandBase executable);

   /// <summary>Executes the <see cref="IApplicationLogic"/>.</summary>
   /// <typeparam name="T"></typeparam>
   /// <param name="arguments">The arguments.</param>
   /// <param name="cancellationToken">The cancellation token.</param>
   /// <returns></returns>
   Task ExecuteAsync<T>(T arguments, CancellationToken cancellationToken);

}