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

      private IConsoleMenuOptions options;

      public CommandMenuManager([NotNull] IServiceProvider serviceProvider, [NotNull] IArgumentReflector reflector)
      {
         this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
         this.reflector = reflector ?? throw new ArgumentNullException(nameof(reflector));
      }
      public void Show<T>()
      {
         var menu = new ConsoleMenu
         {
            Header = options.Header,
            Footer = options.Footer,
            SelectionMode = options.SelectionMode,
            CircularSelection = options.CircularSelection,
            Selector = options.Selector,
            ClearOnExecution = options.ClearOnExecution,
            ExecuteOnIndexSelection= options.ExecuteOnIndexSelection,
            Expander = options.Expander,
            IndentSize = options.IndentSize,
            IndexMenuItems = options.IndexMenuItems,
            CloseKeys = options.CloseKeys
         };

         var classInfo = reflector.GetTypeInfo<T>();
         foreach (var info in classInfo.CommandInfos)
            menu.Add(CreateMenuItem(info));

         menu.Show();
      }

      public void UseOptions(IConsoleMenuOptions options)
      {
         this.options = options;
      }

      public Task ShowAsync<T>()
      {
         Show<T>();
         return Task.CompletedTask;
      }

      private ConsoleMenuItem CreateMenuItem(CommandInfo info)
      {
         if (info.ArgumentType == null)
            return new ConsoleMenuItem(info.ParameterName, x => Execute(x, info));

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
            return new ConsoleMenuItem(info.ParameterName, x => Execute(x, info));
         }


      }

      private void Execute(ConsoleMenuItem menuItem, CommandInfo commandInfo)
      {
         var command = serviceProvider.GetService(commandInfo.ParameterType);
         if (command is IMenuCommand menuCommand)
         {
            menuCommand.ExecuteFromMenu();
         }
      }
   }
}