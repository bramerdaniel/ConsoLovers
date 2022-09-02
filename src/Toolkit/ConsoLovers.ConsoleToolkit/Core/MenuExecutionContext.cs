// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MenuExecutionContext.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   using System;
   using System.Linq;

   using ConsoLovers.ConsoleToolkit.Core.Input;
   using ConsoLovers.ConsoleToolkit.Core.MenuBuilding;
   using ConsoLovers.ConsoleToolkit.Menu;

   using JetBrains.Annotations;

   internal class MenuExecutionContext : IArgumentInitializationContext, IMenuExecutionContext
   {
      #region Constants and Fields

      private readonly ICommandNode commandNode;

      #endregion

      #region Constructors and Destructors

      public MenuExecutionContext([NotNull] IMenuArgumentManager argumentManager, ICommandNode node)
      {
         commandNode = node;
         ArgumentManager = argumentManager ?? throw new ArgumentNullException(nameof(argumentManager));
      }

      #endregion

      #region IMenuExecutionContext Members

      public ConsoleMenuItem MenuItem { get; set; }



      public object InitializeArgument(string argumentName)
      {
         if (!HasMenuInfo())
            return null;

         GetOrCreateArguments();

         var argumentNode = commandNode.FindArgument(argumentName);
         if (argumentNode == null)
            throw new ArgumentException($"Argument {argumentName} could not be found", nameof(argumentName));

         return InitializeArgumentInternal(argumentNode);
      }

      public object InitializeArgument([NotNull] IArgumentNode argumentNode)
      {
         if (argumentNode == null)
            throw new ArgumentNullException(nameof(argumentNode));

         return InitializeArgumentInternal(argumentNode);
      }


      private bool HasMenuInfo()
      {
         return commandNode != null && commandNode.ArgumentType != null;
      }

      #endregion

      #region Public Properties

      public object Arguments { get; set; }

      public object Command { get; set; }

      #endregion

      #region Properties

      internal IMenuArgumentManager ArgumentManager { get; }

      public IInputReader InputReader { get; internal set; }

      #endregion

      #region Public Methods and Operators

      public void InitializeArguments()
      {
         if (!HasMenuInfo())
            return;

         var argumentsToInitialize = commandNode.Nodes
            .OfType<IArgumentNode>()
            .Where(x => x.ShowInInitialization)
            .OrderBy(x => x.DisplayOrder);

         foreach (var argumentInfo in argumentsToInitialize)
            InitializeArgumentInternal(argumentInfo);
      }


      #endregion

      #region Methods

      public object GetOrCreateArguments()
      {
         if (Arguments == null && commandNode?.ArgumentType != null)
         {
            Arguments = ArgumentManager.GetOrCreate(commandNode.ArgumentType);
            SetArgumentsToCommand();
         }

         return Arguments;
      }

      private object InitializeArgumentInternal(IArgumentNode argumentNode)
      {
         object initialValue = null;
         try
         {
            GetOrCreateArguments();

            initialValue = GetInitialValue(argumentNode);
            var parameterValue = InputReader.ReadValue(argumentNode, initialValue);//  ReadValueFromConsole(argumentNode, initialValue);
            SetValue(argumentNode, parameterValue);

            return parameterValue;
         }
         catch (InputCanceledException)
         {
            // The user did not want to specify a value but for required parameters we can not continue !
            if (argumentNode.Required)
               throw;

            return initialValue;
         }
      }

      private void SetValue(IArgumentNode argumentNode, object parameterValue)
      {
         var propertyInfo = argumentNode.PropertyInfo;
         if (propertyInfo.ReflectedType != propertyInfo.DeclaringType)
         {
            if (UsesSharedInitialization(propertyInfo.DeclaringType))
            {
               var baseInstance = ArgumentManager.GetOrCreate(propertyInfo.DeclaringType);
               propertyInfo.SetValue(baseInstance, parameterValue);
            }
         }

         argumentNode.PropertyInfo.SetValue(Arguments, parameterValue);
      }

      private static bool UsesSharedInitialization(Type type)
      {
         return type.GetAttribute<ArgumentInitializationScopeAttribute>()?.Shared ?? true;
      }

      private object GetInitialValue(IArgumentNode argumentNode)
      {
         var propertyInfo = argumentNode.PropertyInfo;
         if (propertyInfo.ReflectedType != propertyInfo.DeclaringType)
         {
            if (UsesSharedInitialization(propertyInfo.DeclaringType))
            {
               var baseInstance = ArgumentManager.GetOrCreate(propertyInfo.DeclaringType);
               return propertyInfo.GetValue(baseInstance);
            }
         }

         return propertyInfo.GetValue(Arguments);
      }

      private static object ReadValueFromConsole(IArgumentNode argumentNode, object initialValue)
      {
         if (!string.IsNullOrWhiteSpace(argumentNode.Description))
            Console.WriteLine(argumentNode.Description);

         if (argumentNode.Type == typeof(int))
         {
            if (initialValue is int intValue)
               return new InputBox<int>($"{argumentNode.DisplayName}: ", intValue).ReadLine();
            return new InputBox<int>($"{argumentNode.DisplayName}: ").ReadLine();
         }

         if (argumentNode.Type == typeof(bool))
            return new InputBox<bool>($"{argumentNode.DisplayName}: ", initialValue is bool boolValue && boolValue).ReadLine();

         if (argumentNode.Type == typeof(string))
         {
            var stringValue = initialValue as string ?? string.Empty;
            return new InputBox<string>($"{argumentNode.DisplayName}: ", stringValue) { IsPassword = argumentNode.IsPassword }.ReadLine();
         }

         return new InputBox<object>($"{argumentNode.DisplayName}: ", initialValue) { IsPassword = argumentNode.IsPassword }.ReadLine();
      }

      private void SetArgumentsToCommand()
      {
         if (Command == null)
            return;

         var argumentsProperty = Command.GetType().GetProperty(nameof(ICommandArguments<Type>.Arguments));
         if (argumentsProperty == null)
            return;

         argumentsProperty.SetValue(Command, Arguments);
      }

      #endregion

      public T GetOrCreate<T>()
      {
         return ArgumentManager.GetOrCreate<T>();
      }
   }
}