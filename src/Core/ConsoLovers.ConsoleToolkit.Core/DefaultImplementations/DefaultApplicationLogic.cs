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
      console.WriteLine();
      console.WriteLine("And now what ?", ConsoleColor.Cyan);
      console.WriteLine();
      console.WriteLine("There is not logic provided for this application when it is called without parameters !");
      console.WriteLine();
      console.WriteLine("If you are a developer of this app you should provide a logic");
      console.WriteLine("if none of the commands are executed");
      console.WriteLine();
      console.WriteLine("An easy way is to call the builder method ShowHelpWithoutArguments()");
      console.WriteLine();
      return Task.CompletedTask;
   }
}