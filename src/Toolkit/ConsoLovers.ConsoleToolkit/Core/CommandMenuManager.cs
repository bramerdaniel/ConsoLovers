// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandMenuManager.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit
{
   using System;
   using System.CodeDom;
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

      private ConsoleMenuItem CreateMenuItem(CommandInfo commandInfo)
      {
         var menuInfo = CreateMenuCommandInfo(commandInfo);
         if (!menuInfo.Visible)
            return null;

         if (commandInfo.ArgumentType == null)
            return new ConsoleMenuItem(menuInfo.DisplayName, x => Execute(x, menuInfo)) { HandleException = OnExecuteException };

         var settingsAttribute = (MenuCommandAttribute)commandInfo.ArgumentType
            .GetCustomAttributes(typeof(MenuCommandAttribute), true).FirstOrDefault();

         if (settingsAttribute != null && !settingsAttribute.Visible)
            return null;

         var argumentInfo = reflector.GetTypeInfo(commandInfo.ArgumentType);
         if (argumentInfo.HasCommands)
            return CreateChildMenu(argumentInfo, menuInfo);

         if (menuInfo.ArgumentInitializationMode == ArgumentInitializationModes.AsMenu && argumentInfo.Properties.Any())
            return CreateArgumentsMenu(menuInfo, argumentInfo);

         return new ConsoleMenuItem(menuInfo.DisplayName, x => Execute(x, menuInfo)) { HandleException = OnExecuteException };
      }

      private bool OnExecuteException(Exception exception)
      {
         var exceptionHandler = serviceProvider.GetService<IMenuExceptionHandler>();
         return exceptionHandler != null && exceptionHandler.Handle(exception);
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
               ArgumentInfo = argumentInfo,
               Visible = commandMenuOptions.MenuBehaviour == MenuBuilderBehaviour.ShowAllCommand,
               DisplayName = commandInfo.ParameterName,
               ArgumentInitializationMode = commandMenuOptions.DefaultArgumentInitializationMode
            };
         }

         return new MenuCommandInfo(commandInfo)
         {
            Visible = menuAttribute.Visible,
            DisplayName = GetValue(x => x.DisplayName ?? commandInfo.ParameterName, commandInfo.ParameterName),
            ArgumentInitializationMode = GetInitMode(),
            ArgumentInfo = argumentInfo
         };

         T GetValue<T>(Func<MenuCommandAttribute, T> getValue, T defaultValue)
         {
            if (menuAttribute is MenuCommandAttribute menuCommand)
               return getValue(menuCommand);
            return defaultValue;
         }

         ArgumentInitializationModes GetInitMode()
         {
            if (menuAttribute is MenuCommandAttribute menuCommand)
            {
               if (menuCommand.ArgumentInitializationMode == ArgumentInitializationModes.Default)
                  return commandMenuOptions.DefaultArgumentInitializationMode;
               return menuCommand.ArgumentInitializationMode;
            }
            return commandMenuOptions.DefaultArgumentInitializationMode;
         }
      }


      private ConsoleMenuItem CreateArgumentsMenu(MenuCommandInfo menuInfo, ArgumentClassInfo argumentInfo)
      {
         var menuAttribute = menuInfo.CommandInfo.PropertyInfo.GetAttribute<MenuCommandAttribute>();
         if (menuAttribute == null || menuAttribute.ArgumentInitializationMode == ArgumentInitializationModes.AsMenu)
            return new ConsoleMenuItem(menuInfo.DisplayName, () => CreateMenuItemsForArguments(menuInfo.CommandInfo, argumentInfo), true);
         return new ConsoleMenuItem(menuInfo.DisplayName, x => Execute(x, menuInfo)) { HandleException = OnExecuteException };
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

         // TODO sort by order
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


      private void SetArguments(object command, object arguments)
      {
         var propertyInfo = command.GetType().GetProperty("Arguments");
         if (propertyInfo != null)
            propertyInfo.SetValue(command, arguments);
      }

      private void Execute(ConsoleMenuItem menuItem, MenuCommandInfo menuInfo)
      {
         var command = serviceProvider.GetRequiredService(menuInfo.CommandInfo.ParameterType);

         var executionContext = new MenuExecutionContext(ArgumentManager, menuInfo) { MenuItem = menuItem, Command = command };
         if (menuInfo.ArgumentInitializationMode == ArgumentInitializationModes.WhileExecution)
            executionContext.InitializeArguments();

         ExecuteInternal(executionContext);
      }
      
      private void Execute(ConsoleMenuItem menuItem, CommandInfo commandInfo, object arguments)
      {
         var command = serviceProvider.GetService(commandInfo.ParameterType);
         SetArguments(command, arguments);
         ExecuteInternal(new MenuExecutionContext(ArgumentManager) { MenuItem = menuItem, Command = command });
      }

      private void ExecuteInternal(MenuExecutionContext executionContext)
      {
         if (executionContext.Command is IMenuCommand menuCommand)
         {
            menuCommand.Execute(executionContext);
         }
         else if (executionContext.Command is IAsyncMenuCommand asyncMenuCommand)
         {
            asyncMenuCommand.ExecuteAsync(executionContext, currentCancellationToken.GetValueOrDefault(CancellationToken.None))
               .GetAwaiter().GetResult();
         }
         else if (executionContext.Command is ICommandBase commandBase)
         {
            var executionEngine = serviceProvider.GetRequiredService<IExecutionEngine>();
            executionEngine.ExecuteCommand(commandBase, CancellationToken.None);
         }
      }

      #endregion
   }
}