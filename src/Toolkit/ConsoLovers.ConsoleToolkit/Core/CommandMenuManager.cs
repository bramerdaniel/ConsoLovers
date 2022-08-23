﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandMenuManager.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Threading;
   using System.Threading.Tasks;

   using ConsoLovers.ConsoleToolkit.Core;
   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
   using ConsoLovers.ConsoleToolkit.Core.Input;
   using ConsoLovers.ConsoleToolkit.Menu;

   using JetBrains.Annotations;

   using Microsoft.Extensions.DependencyInjection;

   public class CommandMenuManager : ICommandMenuManager
   {
      public IMenuArgumentManager ArgumentManager { get; }

      #region Constants and Fields

      private readonly IArgumentReflector reflector;

      private readonly IServiceProvider serviceProvider;

      private ICommandMenuOptions commandMenuOptions = new CommandMenuOptions();

      #endregion

      #region Constructors and Destructors

      public CommandMenuManager([NotNull] IServiceProvider serviceProvider, [NotNull] IArgumentReflector reflector, [NotNull] IMenuArgumentManager argumentManager)
      {
         ArgumentManager = argumentManager ?? throw new ArgumentNullException(nameof(argumentManager));
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

      private MenuCommandAttribute GetOrCreateMenuAttribute(CommandInfo commandInfo)
      {
         // TODO join these attribute queries
         var visibleAttribute = commandInfo.PropertyInfo.GetAttribute<MenuAttribute>();
         if (visibleAttribute != null && !visibleAttribute.Visible)
            return null;

         var menuAttribute = commandInfo.PropertyInfo.GetAttribute<MenuCommandAttribute>();
         if (menuAttribute == null)
         {
            if (commandMenuOptions.MenuBehaviour == MenuBuilderBehaviour.WithAttributesOnly)
               return null;

            menuAttribute = new MenuCommandAttribute(commandInfo.ParameterName);
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

         var settingsAttribute = (MenuCommandAttribute)info.ArgumentType
            .GetCustomAttributes(typeof(MenuCommandAttribute), true).FirstOrDefault();

         if (settingsAttribute != null && !settingsAttribute.Visible)
            return null;

         var argumentInfo = reflector.GetTypeInfo(info.ArgumentType);
         if (argumentInfo.HasCommands)
            return CreateChildMenu(argumentInfo, menuAttribute);

         if (argumentInfo.Properties.Any())
            return CreateArgumentsMenu(info, menuAttribute, argumentInfo);

         return new ConsoleMenuItem(menuAttribute.DisplayName, x => Execute(x, info));

      }

      private ConsoleMenuItem CreateArgumentsMenu(CommandInfo info, MenuCommandAttribute menuCommandAttribute, ArgumentClassInfo argumentInfo)
      {
         var consoleMenuItems = CreateItems(info, argumentInfo);
         return new ConsoleMenuItem(menuCommandAttribute.DisplayName, () => consoleMenuItems, true);
      }

      private ConsoleMenuItem CreateChildMenu(ArgumentClassInfo argumentInfo, MenuCommandAttribute menuCommandAttribute)
      {
         var items = new List<PrintableItem>();
         foreach (var childCommand in argumentInfo.CommandInfos)
         {
            var childMenuItem = CreateMenuItem(childCommand);
            if (childMenuItem != null)
               items.Add(childMenuItem);
         }

         return new ConsoleMenuItem(menuCommandAttribute.DisplayName, items.ToArray());
      }

      private IEnumerable<ConsoleMenuItem> CreateItems(CommandInfo commandInfo, ArgumentClassInfo argumentInfo)
      {
         var argumentInstance = ArgumentManager.GetOrCreate(argumentInfo.ArgumentType);

         foreach (var property in argumentInfo.Properties)
         {
            if (property is ArgumentInfo argument)
            {
               if (argument.PropertyInfo.PropertyType == typeof(bool))
               {
                  var parameterName = ComputeDisplayName(property, argumentInstance);
                  yield return new ConsoleMenuItem(parameterName, x => ToggleParameter(x, argument));
               }
               else
               {
                  var parameterName = ComputeDisplayName(property, argumentInstance);

                  yield return new ConsoleMenuItem(parameterName, x => SetParameter(x, argument));
               }
            }
         }

         yield return new ConsoleMenuItem("Execute", x => Execute(x, commandInfo, argumentInstance));

         void SetParameter(ConsoleMenuItem menuItem, ArgumentInfo argument)
         {
            // TODO specify some handler that can handle this 

            var value = new InputBox<string>(argument.ParameterName + ": ").ReadLine();
            argument.PropertyInfo.SetValue(argumentInstance, value, null);
            menuItem.Text = $"{argument.ParameterName}={value}";
         }

         void ToggleParameter(ConsoleMenuItem menuItem, ArgumentInfo argument)
         {
            var value = (bool)argument.PropertyInfo.GetValue(argumentInstance);
            value = !value;

            argument.PropertyInfo.SetValue(argumentInstance, value, null);
            menuItem.Text = $"{argument.ParameterName}={value}";
         }
      }

      private static string ComputeDisplayName(ParameterInfo property, object argument)
      {
         var value = property.PropertyInfo.GetValue(argument);
         if (value == null)
            return property.ParameterName;
         return $"{property.ParameterName}={value}";
      }

      private void Execute(ConsoleMenuItem menuItem, CommandInfo commandInfo, object arguments)
      {
         var command = serviceProvider.GetService(commandInfo.ParameterType);
         SetArguments(command, arguments);
         if (command is IMenuCommand menuCommand)
         {
            var executionContext = new MenuExecutionContext { MenuItem = menuItem };
            menuCommand.Execute(executionContext);
         }
         else if (command is ICommandBase commandBase)
         {
            var executionEngine = serviceProvider.GetRequiredService<IExecutionEngine>();
            executionEngine.ExecuteCommandAsync(commandBase, CancellationToken.None)
               .GetAwaiter().GetResult();
         }
      }

      private void SetArguments(object command, object arguments)
      {
         var propertyInfo = command.GetType().GetProperty("Arguments");
         if (propertyInfo != null)
            propertyInfo.SetValue(command, arguments);
      }

      private void Execute(ConsoleMenuItem menuItem, CommandInfo commandInfo)
      {
         var command = serviceProvider.GetService(commandInfo.ParameterType);
         InitializeArguments(command, commandInfo);

         if (command is IMenuCommand menuCommand)
         {
            var executionContext = new MenuExecutionContext { MenuItem = menuItem };
            menuCommand.Execute(executionContext);
         }
         else if (command is ICommandBase commandBase)
         {
            var executionEngine = serviceProvider.GetRequiredService<IExecutionEngine>();
            executionEngine.ExecuteCommandAsync(commandBase, CancellationToken.None)
               .GetAwaiter().GetResult();
         }
      }

      private void InitializeArguments(object command, CommandInfo commandInfo)
      {
         var argumentType = commandInfo.ArgumentType;
         if (argumentType == null)
            return;

         var classInfo = reflector.GetTypeInfo(argumentType);
      }

      #endregion
   }
}