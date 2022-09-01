// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NoInitializerCommand.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MenusAndCommands.Commands.Permission;

using System;
using System.Threading;
using System.Threading.Tasks;

using ConsoLovers.ConsoleToolkit.Core;

public class NoInitializerCommand : IAsyncCommand
{
   #region IAsyncCommand Members

   public Task ExecuteAsync(CancellationToken cancellationToken)
   {
      throw new InvalidOperationException("This should never get called");
   }

   #endregion
}