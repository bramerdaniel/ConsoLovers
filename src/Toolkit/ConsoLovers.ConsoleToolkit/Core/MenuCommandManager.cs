// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MenuCommandManager.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   using System;
   using System.Collections.Generic;
   using System.Linq;
   using System.Text;
   using System.Threading;
   using System.Threading.Tasks;

   using ConsoLovers.ConsoleToolkit.Core.MenuBuilding;
   using ConsoLovers.ConsoleToolkit.Menu;

   using JetBrains.Annotations;

   using Microsoft.Extensions.DependencyInjection;

   public class MenuCommandManager : IMenuCommandManager
   {
      #region Constants and Fields

      private readonly IServiceProvider serviceProvider;

      private ICommandMenuOptions commandMenuOptions = new CommandMenuOptions();

      private CancellationToken? currentCancellationToken;

      #endregion

      #region Constructors and Destructors

      public MenuCommandManager([NotNull] IServiceProvider serviceProvider, [NotNull] IMenuArgumentManager argumentManager)
      {
         ArgumentManager = argumentManager ?? throw new ArgumentNullException(nameof(argumentManager));
         this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
      }

      #endregion

      #region IMenuCommandManager Members

      public void Show<T>(CancellationToken cancellationToken)
      {
         ShowAsync<T>(cancellationToken).GetAwaiter().GetResult();
      }

      public Task ShowAsync<T>(CancellationToken cancellationToken)
      {
         var menu = CreateConsoleMenu<T>();

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

      public void UseOptions([NotNull] ICommandMenuOptions options)
      {
         commandMenuOptions = options ?? throw new ArgumentNullException(nameof(options));
      }

      #endregion

      #region Public Properties

      public IMenuArgumentManager ArgumentManager { get; }

      #endregion

      #region Properties

      private IConsoleMenuOptions ConsoleMenuOptions => commandMenuOptions.MenuOptions;

      #endregion

      #region Methods

      private string ComputeDisplayName(IArgumentNode argumentNode)
      {
         var argumentType = argumentNode.Parent?.ArgumentType;
         if (argumentType == null)
            return argumentNode.DisplayName;

         var argument = ArgumentManager.GetOrCreate(argumentType);
         if (argument == null)
            return argumentNode.DisplayName;

         return ComputeDisplayName(argumentNode, argumentNode.PropertyInfo.GetValue(argument));
      }

      private string ComputeDisplayName(IArgumentNode argumentNode, object value)
      {
         if (value == null)
            return $"{argumentNode.DisplayName}=<not specified>";

         if (argumentNode.IsPassword)
         {
            var password = "*".PadRight(value.ToString()?.Length ?? 1, '*');
            return $"{argumentNode.DisplayName}={password}";
         }

         return $"{argumentNode.DisplayName}={value}";
      }

      private PrintableItem CreateArgumentNode(IArgumentNode argumentNode)
      {
         var displayName = ComputeDisplayName(argumentNode);

         if (argumentNode.Type == typeof(bool))
            return new ConsoleMenuItem(displayName, x => ToggleParameter(x, argumentNode));
         return new ConsoleMenuItem(displayName, x => SetParameter(x, argumentNode));

         void SetParameter(ConsoleMenuItem menuItem, IArgumentNode node)
         {
            var context = new MenuExecutionContext(ArgumentManager, node.Parent)
            {
               MenuItem = menuItem,
               MenuBuilderOptions = commandMenuOptions.BuilderOptions,
               InputReader = serviceProvider.GetRequiredService<IInputReader>()
            };

            var value = context.InitializeArgument(node);
            menuItem.Text = ComputeDisplayName(argumentNode, value);
         }

         void ToggleParameter(ConsoleMenuItem menuItem, IArgumentNode argument)
         {
            //var value = (bool)argument.PropertyInfo.GetValue(argumentInstance);
            //value = !value;

            //argument.PropertyInfo.SetValue(argumentInstance, value, null);
            //menuItem.Text = $"{argument.ParameterName}={value}";
         }
      }

      private ConsoleMenuItem CreateCommandMenuItem(ICommandNode commandNode)
      {
         var subCommands = commandNode.Nodes.OfType<ICommandNode>().ToArray();
         if (subCommands.Any())
         {
            var children = subCommands
               .Select(CreateMenuItem).Where(child => child != null)
               .ToArray();

            return new ConsoleMenuItem(commandNode.DisplayName, children);
         }

         if (commandNode.InitializationMode == ArgumentInitializationModes.AsMenu)
            return CreateMenuItemWithArgumentMenu(commandNode);

         return new ConsoleMenuItem(commandNode.DisplayName, x => ExecuteNode(commandNode, x)) { HandleException = OnExecuteException };
      }

      private ConsoleMenu CreateConsoleMenu<T>()
      {
         var menu = new ConsoleMenu(ConsoleMenuOptions);
         foreach (var itemNode in CreateNodes<T>())
         {
            var menuItem = CreateMenuItem(itemNode);
            if (menuItem != null)
               menu.Add(menuItem);
         }

         InitializeMenu(menu);
         return menu;
      }

      private void InitializeMenu(ConsoleMenu menu)
      {
         var initializer = serviceProvider.GetService<IMenuInitializer>();
         if (initializer == null)
            return;

         initializer.Initialize(new MenuInitializationContext(menu, serviceProvider));
      }

      internal IEnumerable<IMenuNode> CreateNodes<T>()
      {
         var menuBuilder = new MenuBuilder(commandMenuOptions.BuilderOptions);
         return menuBuilder.Build<T>();
      }

      private PrintableItem CreateMenuItem(IMenuNode node)
      {
         if (node is ICommandNode command && command.IsVisible)
            return CreateCommandMenuItem(command);

         if (node is IArgumentNode argumentNode)
            return CreateArgumentNode(argumentNode);

         // TODO support option nodes
         return null;
      }

      private ConsoleMenuItem CreateMenuItemWithArgumentMenu(ICommandNode commandNode)
      {
         var children = commandNode.Nodes.OfType<IArgumentNode>()
            .Where(x => x.ShowInMenu)
            .Select(CreateMenuItem)
            .Where(child => child != null)
            .ToList();

         children.Add(new ConsoleMenuItem("Execute", x => ExecuteNode(commandNode, x)) { HandleException = OnExecuteException });
         return new ConsoleMenuItem(commandNode.DisplayName, children.ToArray());
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

      internal MenuExecutionContext ExecuteNode(ICommandNode node, ConsoleMenuItem menuItem)
      {
         var context = InitializeArguments(node, menuItem);
         ExecuteInternal(context);
         return context;
      }

      internal MenuExecutionContext InitializeArguments(ICommandNode node, ConsoleMenuItem menuItem)
      {
         var executionContext = new MenuExecutionContext(ArgumentManager, node)
         {
            Command = serviceProvider.GetService(node.Type) ?? ActivatorUtilities.CreateInstance(serviceProvider, node.Type),
            MenuItem = menuItem,
            MenuBuilderOptions = commandMenuOptions.BuilderOptions,
            InputReader = serviceProvider.GetRequiredService<IInputReader>()
         };

         InitializeArguments(node, executionContext);
         return executionContext;
      }

      private void InitializeArguments(ICommandNode node, MenuExecutionContext executionContext)
      {
         if (node.InitializationMode == ArgumentInitializationModes.None)
            return;

         // Unless the user has specified that he wants to have no argument initialization, 
         // we create the argument class for the command
         executionContext.GetOrCreateArguments();

         if (node.InitializationMode == ArgumentInitializationModes.AsMenu)
            return;
         
         if (node.InitializationMode == ArgumentInitializationModes.WhileExecution)
         {
            executionContext.InitializeArguments();
            return;
         }

         if (node.InitializationMode == ArgumentInitializationModes.Custom)
         {
            var command = executionContext.Command;
            if (command is IMenuArgumentInitializer initializer)
            {
               initializer.InitializeArguments(executionContext);
               return;
            }

            var message = new StringBuilder();
            message.AppendLine($"The command {command?.GetType().Name} does not implement the {nameof(IMenuArgumentInitializer)} interface.");
            message.AppendLine($"Either implement the interface in the command, or change the {nameof(MenuCommandAttribute.ArgumentInitialization)} of the command to e.g. {nameof(ArgumentInitializationModes.None)}.");
            throw new InvalidOperationException(message.ToString());
         }
      }

      private bool OnExecuteException(Exception exception)
      {
         var exceptionHandler = serviceProvider.GetService<IMenuExceptionHandler>();
         return exceptionHandler != null && exceptionHandler.Handle(exception);
      }

      #endregion

      // TODO support for ignoring groups
      // TODO support for additional description
      // TODO support for additional localization
      // TODO Use HelpText.Priority as default sort order
      // TODO Add support for shared initialization for argument base classes (Shared address and username for all commands)
      // TODO Call argument validators during argument initialization
   }
}