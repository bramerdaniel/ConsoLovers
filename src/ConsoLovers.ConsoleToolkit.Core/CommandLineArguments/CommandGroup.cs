// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandGroup.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

using System.Threading.Tasks;

/// <summary>
///    Command that serves a group for nested commands, or as base class for commands that want to execute logic before or after the child command
///    execution
/// </summary>
/// <typeparam name="T">Arguments class containing the child commands</typeparam>
/// <seealso cref="ConsoLovers.ConsoleToolkit.Core.CommandLineArguments.IAsyncCommand&lt;T&gt;"/>
public class CommandGroup<T> : IAsyncCommand<T>
{
   #region IAsyncCommand<T> Members

   /// <summary>Gets or sets the arguments that were specified for the command.</summary>
   public T Arguments { get; set; }

   /// <summary>Executes the asynchronous.</summary>
   public virtual async Task ExecuteAsync()
   {
      await BeforeChildExecutionAsync();
      await CommandExecutor.ExecuteCommandAsync<T>(Arguments);
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