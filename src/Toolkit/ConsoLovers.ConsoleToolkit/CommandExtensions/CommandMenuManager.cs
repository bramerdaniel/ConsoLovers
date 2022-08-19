// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandMenuManager.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.CommandExtensions
{
   using System;
   using System.Collections.Generic;
   using System.Threading.Tasks;

   using ConsoLovers.ConsoleToolkit.Core;
   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
   using ConsoLovers.ConsoleToolkit.Menu;

   using JetBrains.Annotations;

   public class CommandMenuManager : ICommandMenuManager
   {
      #region Constants and Fields

      private readonly IArgumentReflector reflector;

      private readonly IServiceProvider serviceProvider;

      private IConsoleMenuOptions options;

      #endregion

      #region Constructors and Destructors

      public CommandMenuManager([NotNull] IServiceProvider serviceProvider, [NotNull] IArgumentReflector reflector)
      {
         this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
         this.reflector = reflector ?? throw new ArgumentNullException(nameof(reflector));
      }

      #endregion

      #region ICommandMenuManager Members

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
            ExecuteOnIndexSelection = options.ExecuteOnIndexSelection,
            Expander = options.Expander,
            IndentSize = options.IndentSize,
            IndexMenuItems = options.IndexMenuItems,
            CloseKeys = options.CloseKeys
         };

         var classInfo = reflector.GetTypeInfo<T>();
         foreach (var info in classInfo.CommandInfos)
         {
            var menuItem = CreateMenuItem(info);
            if (menuItem != null)
               menu.Add(menuItem);
         }

         menu.Show();
      }

      public void UseOptions(IConsoleMenuOptions options)
      {
         this.options = options;
      }

      #endregion

      #region Public Methods and Operators

      public Task ShowAsync<T>()
      {
         Show<T>();
         return Task.CompletedTask;
      }

      #endregion

      #region Methods

      private ConsoleMenuItem CreateMenuItem(CommandInfo info)
      {
         // TODO sort by menu attribute
         // TODO support for ignoring groups
         // TODO support for additional menu items
         // TODO support for additional description
         // TODO support for additional localization
         // TODO support for Arguments and options

         var menuAttribute = GetOrCreateMenuAttribute(info);
         if (menuAttribute.Hide)
            return null;

         if (info.ArgumentType == null)
            return new ConsoleMenuItem(menuAttribute.DisplayName, x => Execute(x, info));

         var argumentInfo = reflector.GetTypeInfo(info.ArgumentType);
         if (argumentInfo.HasCommands)
         {
            var items = new List<PrintableItem>();
            foreach (var childCommand in argumentInfo.CommandInfos)
            {
               var childMenuItem = CreateMenuItem(childCommand);
               if (childMenuItem != null)
                  items.Add(childMenuItem);
            }

            return new ConsoleMenuItem(menuAttribute.DisplayName, items.ToArray());
         }

         return new ConsoleMenuItem(menuAttribute.DisplayName, x => Execute(x, info));
      }

      private static ConsoleMenuAttribute GetOrCreateMenuAttribute(CommandInfo info)
      {
         var menuAttribute = info.PropertyInfo.GetAttribute<ConsoleMenuAttribute>();
         if (menuAttribute == null)
         {
            menuAttribute = new ConsoleMenuAttribute(info.ParameterName);
         }

         return menuAttribute;
      }

      private void Execute(ConsoleMenuItem menuItem, CommandInfo commandInfo)
      {
         var command = serviceProvider.GetService(commandInfo.ParameterType);
         if (command is IMenuCommand menuCommand)
         {
            var executionContext = new MenuExecutionContext{ MenuItem = menuItem};
            menuCommand.ExecuteFromMenu(executionContext);
         }
      }

      #endregion
   }
}