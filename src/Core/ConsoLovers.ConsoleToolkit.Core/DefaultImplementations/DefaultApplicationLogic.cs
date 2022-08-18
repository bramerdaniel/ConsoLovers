// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Class1.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.DefaultImplementations;

using System;
using System.Threading;
using System.Threading.Tasks;

using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

using JetBrains.Annotations;

public class DefaultApplicationLogic : IApplicationLogic
{
   private readonly IConsole console;

   public DefaultApplicationLogic([NotNull] IConsole console)
   {
      this.console = console ?? throw new ArgumentNullException(nameof(console));
   }

   public Task ExecuteAsync<T>(T arguments, CancellationToken cancellationToken)
   {
      console.WriteLine("What now ?", ConsoleColor.Cyan);
      console.WriteLine("There is not logic provided for this application");
      return Task.CompletedTask;
   }
}