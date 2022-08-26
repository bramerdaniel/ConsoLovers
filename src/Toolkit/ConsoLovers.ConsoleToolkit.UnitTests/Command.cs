// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Command.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.UnitTests;

using ConsoLovers.ConsoleToolkit.Core;

public class Command<T> : ICommand<T>
{
   #region ICommand<T> Members

   public void Execute()
   {
   }

   public T Arguments { get; set; }

   #endregion
}

public class Command : ICommand
{
   #region ICommand Members

   public void Execute()
   {
   }

   #endregion
}

public class MenuCommand : IMenuCommand
{
   #region IMenuCommand Members

   public void Execute(IMenuExecutionContext context)
   {
   }

   #endregion
}