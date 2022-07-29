// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandExecutor.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

using System;
using System.Threading.Tasks;

public static class CommandExecutor
{
   #region Public Methods and Operators

   /// <summary>Executes the first mapped command of the <see cref="arguments"/>.</summary>
   /// <typeparam name="T">The type of the arguments to execute</typeparam>
   /// <param name="arguments">The arguments.</param>
   /// <returns>The command that was executed</returns>
   public static ICommandBase ExecuteCommand<T>(T arguments)
   {
      return ExecuteCommandAsync(arguments).GetAwaiter().GetResult();
   }

   /// <summary>Executes the first mapped command of the <see cref="arguments"/>.</summary>
   /// <typeparam name="T">The type of the arguments to execute</typeparam>
   /// <param name="arguments">The arguments.</param>
   /// <returns>The command that was executed</returns>
   public static async Task<ICommandBase> ExecuteCommandAsync<T>(T arguments)
   {
      var applicationArguments = ArgumentClassInfo.FromType<T>();
      if (!applicationArguments.HasCommands)
         return null;

      var command = GetMappedCommand(arguments);
      return command != null ? await ExecuteCommandAsync(command) : null;
   }

   #endregion

   #region Methods

   private static async Task<ICommandBase> ExecuteCommandAsync(ICommandBase executable)
   {
      switch (executable)
      {
         case IAsyncCommand asyncCommand:
            await asyncCommand.ExecuteAsync();
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