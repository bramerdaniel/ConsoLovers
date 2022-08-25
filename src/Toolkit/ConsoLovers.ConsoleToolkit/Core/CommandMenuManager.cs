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

      private CancellationToken? currentCancellationToken;

      #endregion

      // TODO sort by menu attribute
      // TODO support for ignoring groups
      // TODO support for additional menu items
      // TODO support for additional description
      // TODO support for additional localization
      // TODO support for Arguments and ConsoleMenuOptions

      #region Constructors and Destructors

      public CommandMenuManager([NotNull] IServiceProvider serviceProvider, [NotNull] IArgumentReflector reflector, [NotNull] IMenuArgumentManager argumentManager)
      {
         ArgumentManager = argumentManager ?? throw new ArgumentNullException(nameof(argumentManager));
         this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
         this.reflector = reflector ?? throw new ArgumentNullException(nameof(reflector));
      }

      #endregion

      #region ICommandMenuManager Members

      public void Show<T>(CancellationToken cancellationToken)
      {
         ShowAsync<T>(cancellationToken).GetAwaiter().GetResult();
      }

      public Task ShowAsync<T>(CancellationToken cancellationToken)
      {
         var menu = new ConsoleMenu(ConsoleMenuOptions);

         foreach (var item in CreateMenuItems<T>())
            menu.Add(item);

         try
         {
            currentCancellationToken = cancellationToken;
            menu.Show();
         }
         finally
         {
            currentCancellationToken = null;
         }

         return Task.CompletedTask;
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

      private ConsoleMenuItem CreateMenuItem(CommandInfo commandInfo)
      {
         var menuInfo = CreateMenuCommandInfo(commandInfo);
         if (!menuInfo.Visible)
            return null;

         if (commandInfo.ArgumentType == null)
            return new ConsoleMenuItem(menuInfo.DisplayName, x => Execute(x, menuInfo));

         var settingsAttribute = (MenuCommandAttribute)commandInfo.ArgumentType
            .GetCustomAttributes(typeof(MenuCommandAttribute), true).FirstOrDefault();

         if (settingsAttribute != null && !settingsAttribute.Visible)
            return null;

         var argumentInfo = reflector.GetTypeInfo(commandInfo.ArgumentType);
         if (argumentInfo.HasCommands)
            return CreateChildMenu(argumentInfo, menuInfo);

         if (argumentInfo.Properties.Any())
            return CreateArgumentsMenu(menuInfo, argumentInfo);

         return new ConsoleMenuItem(menuInfo.DisplayName, x => Execute(x, menuInfo));
      }

      private MenuCommandInfo CreateMenuCommandInfo(CommandInfo commandInfo)
      {
         var menuAttribute = commandInfo.PropertyInfo.GetAttribute<MenuAttribute>();
         var argumentInfo = commandInfo.ArgumentType != null
            ? reflector.GetTypeInfo(commandInfo.ArgumentType)
            : null;

         if (menuAttribute == null)
         {
            return new MenuCommandInfo(commandInfo)
            {
               Visible = commandMenuOptions.MenuBehaviour == MenuBuilderBehaviour.ShowAllCommand,
               DisplayName = commandInfo.ParameterName,
               ArgumentInfo = argumentInfo
            };
         }

         return new MenuCommandInfo(commandInfo)
         {
            Visible = menuAttribute.Visible,
            DisplayName = GetValue(x => x.DisplayName ?? commandInfo.ParameterName, commandInfo.ParameterName),
            ArgumentInitializationMode = GetValue(x => x.ArgumentInitializationMode, ArgumentInitializationModes.AsMenu),
            ArgumentInfo = argumentInfo
         };

         T GetValue<T>(Func<MenuCommandAttribute, T> getValue, T defaultValue)
         {
            if (menuAttribute is MenuCommandAttribute menuCommand)
               return getValue(menuCommand);
            return defaultValue;
         }
      }


      private ConsoleMenuItem CreateArgumentsMenu(MenuCommandInfo menuInfo, ArgumentClassInfo argumentInfo)
      {
         var menuAttribute = menuInfo.CommandInfo.PropertyInfo.GetAttribute<MenuCommandAttribute>();
         if (menuAttribute == null || menuAttribute.ArgumentInitializationMode == ArgumentInitializationModes.AsMenu)
            return new ConsoleMenuItem(menuInfo.DisplayName, () => CreateMenuItemsForArguments(menuInfo.CommandInfo, argumentInfo), true);
         return new ConsoleMenuItem(menuInfo.DisplayName, x => Execute(x, menuInfo));
      }

      private ConsoleMenuItem CreateChildMenu(ArgumentClassInfo argumentInfo, MenuCommandInfo menuCommandAttribute)
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

      private IEnumerable<ConsoleMenuItem> CreateMenuItemsForArguments(CommandInfo commandInfo, ArgumentClassInfo argumentInfo)
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

            try
            {
               var value = new InputBox<string>(argument.ParameterName + ": ").ReadLine();
               argument.PropertyInfo.SetValue(argumentInstance, value, null);
               menuItem.Text = $"{argument.ParameterName}={value}";
            }
            catch (InputCanceledException)
            {
               // The user did not want to specify a value
            }
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
         if (command is IAsyncMenuCommand asyncMenuCommand)
         {
            var executionContext = new MenuExecutionContext { MenuItem = menuItem };
            asyncMenuCommand.ExecuteAsync(executionContext, currentCancellationToken.GetValueOrDefault(CancellationToken.None));
         }
         else if (command is ICommandBase commandBase)
         {
            var executionEngine = serviceProvider.GetRequiredService<IExecutionEngine>();
            executionEngine.ExecuteCommand(commandBase, CancellationToken.None);
         }
      }

      private void SetArguments(object command, object arguments)
      {
         var propertyInfo = command.GetType().GetProperty("Arguments");
         if (propertyInfo != null)
            propertyInfo.SetValue(command, arguments);
      }

      private void Execute(ConsoleMenuItem menuItem, MenuCommandInfo menuInfo)
      {
         var command = CreateCommand(menuInfo);
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

      private object CreateCommand(MenuCommandInfo menuInfo)
      {
         var command = serviceProvider.GetRequiredService(menuInfo.CommandInfo.ParameterType);

         if (menuInfo.ArgumentInitializationMode != ArgumentInitializationModes.WhileExecution)
            return command;

         if (InitializeCommand(menuInfo, command))
            return null;
         return command;
      }

      private bool InitializeCommand(MenuCommandInfo menuInfo, object command)
      {
         var argumentsProperty = command.GetType().GetProperty(nameof(ICommandArguments<Type>.Arguments));
         if (argumentsProperty == null)
            return true;

         var args = ArgumentManager.GetOrCreate(menuInfo.ArgumentInfo.ArgumentType);
         foreach (var argumentInfo in menuInfo.GetArgumentInfos())
         {
            var initialValue = argumentInfo.GetValue(args);
            var parameterValue = new InputBox<object>($"{argumentInfo.DisplayName}: ", initialValue).ReadLine();
            argumentInfo.SetValue(args, parameterValue);
         }

         argumentsProperty.SetValue(command, args);
         return false;
      }

      #endregion
   }
}