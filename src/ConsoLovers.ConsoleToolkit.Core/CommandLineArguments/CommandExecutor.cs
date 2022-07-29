// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandExecutor.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

using System;
using System.Threading.Tasks;

using JetBrains.Annotations;

public class CommandExecutor
{
   private static async Task<ICommandBase> RunWithCommandAsync(ICommandBase executable)
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
   
   public static async Task<ICommandBase> ExecuteCommandAsync<T>(T arguments)
   {
      var applicationArguments = ArgumentClassInfo.FromType<T>();
      if (!applicationArguments.HasCommands)
         return null;

      ICommandBase command = GetMappedCommand<T>(arguments);
      if (command != null)
      {
         await RunWithCommandAsync(command);
         return command;
      }

      return null;
   }

   private static ICommandBase GetMappedCommand<T>(T arguments)
   {
      if (arguments == null)
         return null;

      foreach (var propertyInfo in typeof(T).GetProperties())
      {
         if (propertyInfo.PropertyType.GetInterface(typeof(ICommandBase).FullName) != null)
         {
            if (propertyInfo.GetValue(arguments) is ICommandBase value)
               return value;
         }
      }

      return null;
   }
}