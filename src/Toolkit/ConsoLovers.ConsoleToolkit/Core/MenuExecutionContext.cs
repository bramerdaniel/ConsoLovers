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
   using ConsoLovers.ConsoleToolkit.Menu;

   using JetBrains.Annotations;

   internal class MenuExecutionContext : IMenuExecutionContext
   {
      #region Constants and Fields

      private readonly MenuCommandInfo menuInfo;

      #endregion

      #region Constructors and Destructors

      public MenuExecutionContext([NotNull] IMenuArgumentManager argumentManager)
         : this(argumentManager, null)
      {
      }

      public MenuExecutionContext([NotNull] IMenuArgumentManager argumentManager, MenuCommandInfo menuInfo)
      {
         this.menuInfo = menuInfo;
         ArgumentManager = argumentManager ?? throw new ArgumentNullException(nameof(argumentManager));
      }

      #endregion

      #region IMenuExecutionContext Members

      public ConsoleMenuItem MenuItem { get; set; }

      public void InitializeArgument(string argumentName)
      {
         EnsureArgumentInstance();

         var argumentInfo = menuInfo.GetArgumentInfo(argumentName);
         if (argumentInfo == null)
            throw new ArgumentException($"Argument {argumentName} could not be found", nameof(argumentName));

         InitializeArgumentInternal(argumentInfo);
      }

      #endregion

      #region Public Properties

      public object Arguments { get; set; }

      public object Command { get; set; }

      #endregion

      #region Properties

      internal IMenuArgumentManager ArgumentManager { get; }

      #endregion

      #region Public Methods and Operators

      public void InitializeArguments()
      {
         if (menuInfo.ArgumentInfo == null)
            return;

         EnsureArgumentInstance();

         foreach (var argumentInfo in menuInfo.GetArgumentInfos().Where(x => x.Visible))
            InitializeArgumentInternal(argumentInfo);
      }

      #endregion

      #region Methods

      private void EnsureArgumentInstance()
      {
         if (Arguments != null)
            return;

         Arguments = ArgumentManager.GetOrCreate(menuInfo.ArgumentInfo.ArgumentType);
         SetArgumentsToCommand();
      }

      private void InitializeArgumentInternal(MenuArgumentInfo argumentInfo)
      {
         try
         {
            var initialValue = argumentInfo.GetValue(Arguments);
            var parameterValue =
               new InputBox<object>($"{argumentInfo.DisplayName}: ", initialValue) { IsPassword = argumentInfo.IsPassword }.ReadLine();

            argumentInfo.SetValue(Arguments, parameterValue);
         }
         catch (InputCanceledException)
         {
            // The user did not want to specify a value but for required parameters we can not continue !
            if (argumentInfo.Required)
               throw;
         }
      }

      private void SetArgumentsToCommand()
      {
         var argumentsProperty = Command.GetType().GetProperty(nameof(ICommandArguments<Type>.Arguments));
         if (argumentsProperty == null)
            return;

         argumentsProperty.SetValue(Command, Arguments);
      }

      #endregion
   }
}