// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MenuCommandTestSetup.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.UnitTests.Setups;

using System;

using ConsoLovers.ConsoleToolkit.Core;

using JetBrains.Annotations;

internal class MenuCommandTestSetup<T> : SetupBase<MenuCommandTest<T>>
   where T : class
{
   private readonly TestInputReader inputReader = new TestInputReader();

   #region Public Methods and Operators

   public MenuCommandTestSetup<T> WithArgumentInitialization(ArgumentInitializationModes argumentInitialization)
   {
      Set(nameof(ArgumentInitializationModes), argumentInitialization);
      return this;
   }

   #endregion

   #region Methods

   internal MenuExecutionContext Execute(string command)
   {
      var test = Done();
      return test.Execute(command);
   }

   internal TCommand ExecuteCommand<TCommand>(string command)
      where TCommand : ICommandBase
   {
      var test = Done();
      return test.ExecuteCommand<TCommand>(command);
   }

   protected override MenuCommandTest<T> CreateInstance()
   {
      var argumentInitialization = Get(nameof(ArgumentInitializationModes), ArgumentInitializationModes.WhileExecution);
      return new MenuCommandTest<T>(argumentInitialization, inputReader);
   }

   #endregion

   public MenuCommandTestSetup<T> ConfigureInput<TValue>([NotNull] string argument, TValue value)
   {
      if (argument == null)
         throw new ArgumentNullException(nameof(argument));

      inputReader.AddInput(argument, value);
      return this;
   }
}