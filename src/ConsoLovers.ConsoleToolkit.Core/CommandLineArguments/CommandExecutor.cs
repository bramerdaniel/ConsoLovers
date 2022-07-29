// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandExecutor.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

using System;
using System.Threading.Tasks;

public class CommandExecutor
{
   private static async Task RunWithCommandAsync(ICommandBase executable)
   {
      switch (executable)
      {
         case IAsyncCommand asyncCommand:
            await asyncCommand.ExecuteAsync();
            break;
         case ICommand command:
            command.Execute();
            break;
         default:
            throw new InvalidOperationException("Command type not supported");
      }
   }

   public static async Task<bool> ExecuteCommandAsync<T>(T arguments)
   {
      var applicationArguments = ArgumentClassInfo.FromType<T>();
      if (!applicationArguments.HasCommands)
         return false;

      ICommandBase command = GetMappedCommand<T>(arguments);
      if (command == null)
         return false;

      await RunWithCommandAsync(command);
      return true;
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