// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandGroup.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

using System;
using System.Threading;
using System.Threading.Tasks;

using JetBrains.Annotations;

/// <summary>
///    Command that serves a group for nested commands, or as base class for commands that want to execute logic before or after the child command
///    execution
/// </summary>
/// <typeparam name="T">Arguments class containing the child commands</typeparam>
/// <seealso cref="ConsoLovers.ConsoleToolkit.Core.CommandLineArguments.IAsyncCommand&lt;T&gt;"/>
public class CommandGroup<T> : IAsyncCommand<T>
{
   private readonly ICommandExecutor commandExecutor;

   public CommandGroup([NotNull] ICommandExecutor commandExecutor)
   {
      this.commandExecutor = commandExecutor ?? throw new ArgumentNullException(nameof(commandExecutor));
   }

   #region IAsyncCommand<T> Members

   /// <summary>Gets or sets the arguments that were specified for the command.</summary>
   public T Arguments { get; set; }

   /// <summary>Executes the asynchronous.</summary>
   /// <param name="cancellationToken">The cancellation token.</param>
   public virtual async Task ExecuteAsync(CancellationToken cancellationToken)
   {
      await BeforeChildExecutionAsync();
      await commandExecutor.ExecuteCommandAsync(Arguments, cancellationToken);
      await AfterChildExecutionAsync();
   }

   #endregion

   #region Methods

   /// <summary>Called after the child command was executed.</summary>
   protected virtual Task AfterChildExecutionAsync()
   {
      return Task.CompletedTask;
   }

   /// <summary>Called before the child command is executed.</summary>
   protected virtual Task BeforeChildExecutionAsync()
   {
      return Task.CompletedTask;
   }

   #endregion
}