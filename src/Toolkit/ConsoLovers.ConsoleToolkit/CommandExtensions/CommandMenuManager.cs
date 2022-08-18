// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandMenuManager.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.CommandExtensions
{
   using System;
   using System.Collections.Generic;
   using System.Threading.Tasks;

   using ConsoLovers.ConsoleToolkit.Contracts;
   using ConsoLovers.ConsoleToolkit.Core;
   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
   using ConsoLovers.ConsoleToolkit.Menu;

   using JetBrains.Annotations;

   public class CommandMenuManager : ICommandMenuManager
   {
      private readonly IServiceProvider serviceProvider;

      private readonly IArgumentReflector reflector;

      public CommandMenuManager([NotNull] IServiceProvider serviceProvider, [NotNull] IArgumentReflector reflector)
      {
         this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
         this.reflector = reflector ?? throw new ArgumentNullException(nameof(reflector));
      }
      public void Show<T>()
      {
         var menu = new ConsoleMenu
         {
            Header = "From commands",
            CircularSelection = true,
            Selector = "» ",
            SelectionStrech = SelectionStrech.UnifiedLength
         };

         var classInfo = reflector.GetTypeInfo<T>();
         foreach (var info in classInfo.CommandInfos)
            menu.Add(CreateMenuItem(info));

         menu.Show();
      }

      public Task ShowAsync<T>()
      {
         Show<T>();
         return Task.CompletedTask;
      }

      private ConsoleMenuItem CreateMenuItem(CommandInfo info)
      {
         if (info.ArgumentType == null)
            return new ConsoleMenuItem(info.ParameterName, x => Execute(info));

         var argumentInfo = reflector.GetTypeInfo(info.ArgumentType);
         if (argumentInfo.HasCommands)
         {
            var items = new List<PrintableItem>();
            foreach (var childCommand in argumentInfo.CommandInfos)
            {
               var item = CreateMenuItem(childCommand);
               items.Add(item);
            }

            return new ConsoleMenuItem(info.ParameterName, items.ToArray());
         }
         else
         {
            return new ConsoleMenuItem(info.ParameterName, x => Execute(info));
         }


      }

      private void Execute(CommandInfo commandInfo)
      {
         var command = serviceProvider.GetService(commandInfo.ParameterType);
         if (command is IMenuCommand menuCommand)
            menuCommand.ExecuteFromMenu();
      }
   }

   public interface ICommandMenuManager
   {
      void Show<T>();
   }
}