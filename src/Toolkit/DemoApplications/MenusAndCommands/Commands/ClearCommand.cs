// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ClearCommand.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MenusAndCommands.Commands;

using System;
using System.Threading;
using System.Threading.Tasks;

using ConsoLovers.ConsoleToolkit.Core;
using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

public class ClearCommand : IAsyncCommand
{
   #region Constants and Fields

   private readonly IConsole console;

   private readonly IMenuArgumentManager argumentManager;

   #endregion

   #region Constructors and Destructors

   public ClearCommand(IConsole console, IMenuArgumentManager argumentManager)
   {
      this.console = console ?? throw new ArgumentNullException(nameof(console));
      this.argumentManager = argumentManager ?? throw new ArgumentNullException(nameof(argumentManager));
   }

   #endregion

   #region IAsyncCommand Members

   public Task ExecuteAsync(CancellationToken cancellationToken)
   {
      argumentManager.Clear();
      console.WriteLine("Cleared all argument values");
      console.ReadLine();
      return Task.CompletedTask;
   }

   #endregion
}