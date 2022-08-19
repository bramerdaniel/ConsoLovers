// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandMenuManager.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.CommandExtensions
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Threading;
   using System.Threading.Tasks;

   using ConsoLovers.ConsoleToolkit.Core;
   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
   using ConsoLovers.ConsoleToolkit.Menu;

   using JetBrains.Annotations;

   using Microsoft.Extensions.DependencyInjection;

   public class CommandMenuManager : ICommandMenuManager
   {
      #region Constants and Fields

      private readonly IArgumentReflector reflector;

      private readonly IServiceProvider serviceProvider;

      private ICommandMenuOptions commandMenuOptions = new CommandMenuOptions();

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
            Header = ConsoleMenuOptions.Header,
            Footer = ConsoleMenuOptions.Footer,
            SelectionMode = ConsoleMenuOptions.SelectionMode,
            CircularSelection = ConsoleMenuOptions.CircularSelection,
            Selector = ConsoleMenuOptions.Selector,
            ClearOnExecution = ConsoleMenuOptions.ClearOnExecution,
            ExecuteOnIndexSelection = ConsoleMenuOptions.ExecuteOnIndexSelection,
            Expander = ConsoleMenuOptions.Expander,
            IndentSize = ConsoleMenuOptions.IndentSize,
            IndexMenuItems = ConsoleMenuOptions.IndexMenuItems,
            CloseKeys = ConsoleMenuOptions.CloseKeys
         };

         foreach (var item in CreateMenuItems<T>())
            menu.Add(item);

         menu.Show();
      }

      private IEnumerable<PrintableItem> CreateMenuItems<T>()
      {
         var classInfo = reflector.GetTypeInfo<T>();
         foreach (var info in classInfo.CommandInfos)
         {
            var menuItem = CreateMenuItem(info);
            if (menuItem != null)
               yield return menuItem;
         }
      }

      public void UseOptions([NotNull] ICommandMenuOptions options)
      {
         commandMenuOptions = options ?? throw new ArgumentNullException(nameof(options));
      }

      #endregion

      #region Properties

      private IConsoleMenuOptions ConsoleMenuOptions => commandMenuOptions.Menu;

      #endregion

      #region Public Methods and Operators

      public Task ShowAsync<T>()
      {
         Show<T>();
         return Task.CompletedTask;
      }

      #endregion

      #region Methods

      private  MenuSettingsAttribute GetOrCreateMenuAttribute(CommandInfo info)
      {
         var menuAttribute = info.PropertyInfo.GetAttribute<MenuSettingsAttribute>();
         if (menuAttribute == null)
         {
            if (commandMenuOptions.MenuBehaviour == MenuBuilderBehaviour.WithAttributesOnly)
               return null;

            menuAttribute = new MenuSettingsAttribute(info.ParameterName);
         }

         return menuAttribute;
      }

      private ConsoleMenuItem CreateMenuItem(CommandInfo info)
      {
         // TODO sort by menu attribute
         // TODO support for ignoring groups
         // TODO support for additional menu items
         // TODO support for additional description
         // TODO support for additional localization
         // TODO support for Arguments and ConsoleMenuOptions

         var menuAttribute = GetOrCreateMenuAttribute(info);
         if (menuAttribute == null || !menuAttribute.Visible)
            return null;

         if (info.ArgumentType == null)
            return new ConsoleMenuItem(menuAttribute.DisplayName, x => Execute(x, info));

         var settingsAttribute = (MenuSettingsAttribute)info.ArgumentType
            .GetCustomAttributes(typeof(MenuSettingsAttribute),true).FirstOrDefault();

         if (settingsAttribute != null && !settingsAttribute.Visible)
            return null;

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

      private void Execute(ConsoleMenuItem menuItem, CommandInfo commandInfo)
      {
         var command = serviceProvider.GetService(commandInfo.ParameterType);
         if (command is IMenuCommand menuCommand)
         {
            var executionContext = new MenuExecutionContext { MenuItem = menuItem };
            menuCommand.ExecuteFromMenu(executionContext);
         }
         else if(command is ICommandBase commandBase)
         {
            var executionEngine = serviceProvider.GetRequiredService<IExecutionEngine>();
            executionEngine.ExecuteCommandAsync(commandBase, CancellationToken.None)
               .GetAwaiter().GetResult();
         }
      }

      #endregion
   }
}