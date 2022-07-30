// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandExecutor.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

using System;
using System.Threading;
using System.Threading.Tasks;

public class CommandExecutor : ICommandExecutor
{
   public CommandExecutor()
   {
      
   }

   #region Public Methods and Operators

   /// <summary>Executes the first mapped command of the <see cref="arguments"/>.</summary>
   /// <typeparam name="T">The type of the arguments to execute</typeparam>
   /// <param name="arguments">The arguments.</param>
   /// <returns>The command that was executed</returns>
   public ICommandBase ExecuteCommand<T>(T arguments)
   {
      return ExecuteCommandAsync(arguments, CancellationToken.None).GetAwaiter().GetResult();
   }

   /// <summary>Executes the first mapped command of the <see cref="arguments"/>.</summary>
   /// <typeparam name="T">The type of the arguments to execute</typeparam>
   /// <param name="arguments">The arguments.</param>
   /// <param name="cancellationToken">The cancellation token.</param>
   /// <returns>The command that was executed</returns>
   public async Task<ICommandBase> ExecuteCommandAsync<T>(T arguments, CancellationToken cancellationToken)
   {
      var applicationArguments = ArgumentClassInfo.FromType<T>();
      if (!applicationArguments.HasCommands)
         return null;

      var command = GetMappedCommand(arguments);
      return command != null ? await ExecuteCommandAsync(command, cancellationToken) : null;
   }

   #endregion

   #region Methods

   public async Task<ICommandBase> ExecuteCommandAsync(ICommandBase executable, CancellationToken cancellationToken)
   {
      switch (executable)
      {
         case IAsyncCommand asyncCommand:
            await asyncCommand.ExecuteAsync(cancellationToken);
            return executable;
         case ICommand command:
            command.Execute();
            return executable;
         default:
            throw new InvalidOperationException("Command type not supported");
      }
   }

   private static ICommandBase GetMappedCommand<T>(T arguments)
   {
      if (arguments == null)
         return null;

      foreach (var propertyInfo in typeof(T).GetProperties())
      {
         if (propertyInfo.PropertyType.GetInterface(typeof(ICommandBase).FullName!) != null)
         {
            if (propertyInfo.GetValue(arguments) is ICommandBase value)
               return value;
         }
      }

      return null;
   }

   #endregion
}