// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomCommand.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.UnitTests;

using ConsoLovers.ConsoleToolkit.Core;

public class InitializerCommand<T> : ICommand<T>, IMenuCommand, IMenuArgumentInitializer
{
   #region IMenuArgumentInitializer Members

   public void InitializeArguments(IArgumentInitializationContext context)
   {
      Initialized = true;
   }

   #endregion

   #region ICommand<T> Members

   public void Execute()
   {
      WasExecuted = true;
   }

   public T Arguments { get; set; }

   #endregion

   #region IMenuCommand Members

   public void Execute(IMenuExecutionContext context)
   {
      WasExecutedFromMenu = true;
      Execute();
   }

   #endregion

   #region Public Properties

   public bool WasExecuted { get; private set; }

   public bool WasExecutedFromMenu { get; private set; }

   public bool Initialized { get; private set; }

   #endregion
}