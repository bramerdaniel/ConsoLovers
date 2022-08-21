// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExceptionHandler.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Services;

using System;

using JetBrains.Annotations;

public class ExceptionHandler : IExceptionHandler
{
   private readonly IConsole console;

   public ExceptionHandler([NotNull] IConsole console)
   {
      this.console = console ?? throw new ArgumentNullException(nameof(console));
   }

   public bool Handle(Exception exception)
   {
      console.WriteLine("AN UNHANDLED EXCEPTION OCCURRED", ConsoleColor.Red);
      console.WriteLine();
      console.WriteLine(exception.Message, ConsoleColor.White);
      console.WriteLine();
      console.WriteLine(exception.ToString(), ConsoleColor.DarkGray);
      
      return true;
   }
}