// --------------------------------------------------------------------------------------------------------------------
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

   using ConsoLovers.ConsoleToolkit.Core.Input;
   using ConsoLovers.ConsoleToolkit.Core.MenuBuilding;
   using ConsoLovers.ConsoleToolkit.Menu;

   using JetBrains.Annotations;

   using Microsoft.Extensions.DependencyInjection;

   public class MenuCommandManager : IMenuCommandManager
   {
      #region Constants and Fields

      private readonly IArgumentReflector reflector;

      private readonly IServiceProvider serviceProvider;

      private ICommandMenuOptions commandMenuOptions = new CommandMenuOptions();

      private CancellationToken? currentCancellationToken;

      #endregion

      #region Constructors and Destructors

      public MenuCommandManager([NotNull] IServiceProvider serviceProvider, [NotNull] IArgumentReflector reflector,
         [NotNull] IMenuArgumentManager argumentManager)
      {
         ArgumentManager = argumentManager ?? throw new ArgumentNullException(nameof(argumentManager));
         this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
         this.reflector = reflector ?? throw new ArgumentNullException(nameof(reflector));
      }

      #endregion

      #region IMenuCommandManager Members

      public void Show<T>(CancellationToken cancellationToken)
      {
         ShowAsync<T>(cancellationToken).GetAwaiter().GetResult();
      }

      public Task ShowAsync<T>(CancellationToken cancellationToken)
      {
         var menu = new ConsoleMenu(ConsoleMenuOptions);
         foreach (var itemNode in new MenuBuilder(commandMenuOptions.BuilderOptions).Build<T>())
         {
            if (itemNode.IsVisible)
            {
               var menuItem = CreateMenuItem(itemNode);
               if (menuItem != null)
                  menu.Add(menuItem);
            }
         }

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

      private PrintableItem CreateMenuItem(IMenuNode node)
      {
         if (node is ICommandNode command)
            return CreateCommandNode(command);

         if (node is IArgumentNode argumentNode)
            return CreateArgumentNode(argumentNode);

         // TODO support option nodes
         return null;
      }

      private PrintableItem CreateArgumentNode(IArgumentNode argumentNode)
      {
         if (argumentNode.Type == typeof(bool))
         {
            return new ConsoleMenuItem(argumentNode.DisplayName, x => ToggleParameter(x, argumentNode));
         }

         return new ConsoleMenuItem(argumentNode.DisplayName, x => SetParameter(x, argumentNode));

         void SetParameter(ConsoleMenuItem menuItem, IArgumentNode node)
         {
            // TODO specify some handler that can handle this 

            try
            {
               var value = new InputBox<string>($"{node.DisplayName}: ").ReadLine();
               // node.PropertyInfo.SetValue(argumentInstance, value, null);
               menuItem.Text = $"{node.DisplayName}={value}";
            }
            catch (InputCanceledException)
            {
               // The user did not want to specify a value
            }
         }

         void ToggleParameter(ConsoleMenuItem menuItem, IArgumentNode argument)
         {
            //var value = (bool)argument.PropertyInfo.GetValue(argumentInstance);
            //value = !value;

            //argument.PropertyInfo.SetValue(argumentInstance, value, null);
            //menuItem.Text = $"{argument.ParameterName}={value}";
         }
      }

      private ConsoleMenuItem CreateCommandNode(ICommandNode node)
      {
         if (node.Nodes.Where(n => n.IsVisible).Any())
         {
            var children = node.Nodes.Where(n => n.IsVisible)
               .Select(CreateMenuItem).Where(child => child != null)
               .ToArray();

            return new ConsoleMenuItem(node.DisplayName, children);
         }

         return new ConsoleMenuItem(node.DisplayName, x => ExecuteNode(node, x)) { HandleException = OnExecuteException };
      }

      private void ExecuteNode(ICommandNode node, ConsoleMenuItem menuItem)
      {
         var executionContext = new MenuExecutionContext(ArgumentManager, node)
         {
            Command = serviceProvider.GetService(node.Type) ?? ActivatorUtilities.CreateInstance(serviceProvider, node.Type),
            MenuItem = menuItem
         };

         if (node.InitializationMode == ArgumentInitializationModes.WhileExecution)
            executionContext.InitializeArguments();

         ExecuteInternal(executionContext);
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

      private bool OnExecuteException(Exception exception)
      {
         var exceptionHandler = serviceProvider.GetService<IMenuExceptionHandler>();
         return exceptionHandler != null && exceptionHandler.Handle(exception);
      }

      #endregion

      // TODO sort by menu attribute
      // TODO support for ignoring groups
      // TODO support for additional menu items
      // TODO support for additional description
      // TODO support for additional localization
      // TODO support for Arguments and ConsoleMenuOptions
   }
}