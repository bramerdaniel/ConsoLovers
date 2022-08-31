﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MenuCommandManager.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   using System;
   using System.Linq;
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
            var context = new MenuExecutionContext(ArgumentManager, node.Parent) { MenuItem = menuItem };

            var value = context.InitializeArgument(node);
            menuItem.Text = ComputeDisplayName(argumentNode, value);

            //try
            //{
            //   var value = new InputBox<string>($"{node.DisplayName}: ").ReadLine();

            //   var argumentInstance = ArgumentManager.GetOrCreate(argumentNode.Parent.ArgumentType);
            //   argumentNode.PropertyInfo.SetValue(argumentInstance, value, null);
            //   menuItem.Text = $"{node.DisplayName}={value}";
            //}
            //catch (InputCanceledException)
            //{
            //   // The user did not want to specify a value
            //}
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
         var menuBuilder = new MenuBuilder(commandMenuOptions.BuilderOptions);

         foreach (var itemNode in menuBuilder.Build<T>())
         {
            var menuItem = CreateMenuItem(itemNode);
            if (menuItem != null)
               menu.Add(menuItem);
         }

         return menu;
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

      private void ExecuteNode(ICommandNode node, ConsoleMenuItem menuItem)
      {
         var executionContext = new MenuExecutionContext(ArgumentManager, node)
         {
            Command = serviceProvider.GetService(node.Type) ?? ActivatorUtilities.CreateInstance(serviceProvider, node.Type), MenuItem = menuItem
         };

         // Unless the use has specified that he wants to have custom argument initialization, 
         // we initialize the argument class for the command
         if (node.InitializationMode != ArgumentInitializationModes.Custom)
            executionContext.GetOrCreateArguments();

         if (node.InitializationMode == ArgumentInitializationModes.WhileExecution)
            executionContext.InitializeArguments();

         ExecuteInternal(executionContext);
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
   }
}