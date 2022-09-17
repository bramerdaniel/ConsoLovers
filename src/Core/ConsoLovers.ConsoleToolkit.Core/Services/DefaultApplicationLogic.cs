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
      console.WriteLine("If you are a developer of this app you should provide a logic, that is executed when the");
      console.WriteLine("application is executed without using a command");
      console.WriteLine();
      console.WriteLine("There are several ways to provide an application logic");
      console.WriteLine();
      console.WriteLine("1. Call the run method with an action delegate:");
      console.WriteLine("   ConsoleApplication.WithArguments<Args>()", ConsoleColor.Cyan);
      console.WriteLine("      .Run(args => Console.WriteLine(\"No args specified\"));", ConsoleColor.Cyan);
      console.WriteLine();
      console.WriteLine("2. Register your custom IApplicationLogic<T> implementation");
      console.WriteLine("   ConsoleApplication.WithArguments<Args>()", ConsoleColor.Cyan);
      console.WriteLine("      .UseApplicationLogic(typeof(MyCustomLogic))", ConsoleColor.Cyan);
      console.WriteLine("      .Run();", ConsoleColor.Cyan);
      console.WriteLine();
      console.WriteLine("3. Use a build in application logic implementation");
      console.WriteLine("   ConsoleApplication.WithArguments<Args>()", ConsoleColor.Cyan);
      console.WriteLine("      .ShowHelpWithoutArguments()", ConsoleColor.Cyan);
      console.WriteLine("      .Run();", ConsoleColor.Cyan);
      console.WriteLine();
      console.WriteLine("4. Use the ConsoLoversToolkit");
      console.WriteLine("   ConsoleApplication.WithArguments<Args>()", ConsoleColor.Cyan);
      console.WriteLine("      .UseMenuWithoutArguments()", ConsoleColor.Cyan);
      console.WriteLine("      .Run();", ConsoleColor.Cyan);
      console.WriteLine();
      return Task.CompletedTask;
   }

   #endregion
}