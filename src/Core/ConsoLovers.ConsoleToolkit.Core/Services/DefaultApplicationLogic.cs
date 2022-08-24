// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultApplicationLogic.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Services;

using System;
using System.Threading;
using System.Threading.Tasks;

using JetBrains.Annotations;

public class DefaultApplicationLogic : IApplicationLogic
{
   #region Constants and Fields

   private readonly IConsole console;

   #endregion

   #region Constructors and Destructors

   public DefaultApplicationLogic([NotNull] IConsole console)
   {
      this.console = console ?? throw new ArgumentNullException(nameof(console));
   }

   #endregion

   #region IApplicationLogic Members

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

   #endregion
}