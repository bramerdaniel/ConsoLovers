// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExceptionHandler.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Services;

using System;

using JetBrains.Annotations;

/// <summary>The default implementation of the <see cref="IExceptionHandler"/> interface</summary>
/// <seealso cref="ConsoLovers.ConsoleToolkit.Core.IExceptionHandler"/>
public class ExceptionHandler : IExceptionHandler
{
   #region Constants and Fields

   private readonly IConsole console;

   #endregion

   #region Constructors and Destructors

   public ExceptionHandler([NotNull] IConsole console)
   {
      this.console = console ?? throw new ArgumentNullException(nameof(console));
   }

   #endregion

   #region IExceptionHandler Members

   public bool Handle(Exception exception)
   {
      console.WriteLine(@"  _   _       _                     _ _          _   _____                    _   _             ", ConsoleColor.Red);
      console.WriteLine(@" | | | |_ __ | |__   __ _ _ __   __| | | ___  __| | | ____|_  _____ ___ _ __ | |_(_) ___  _ __  ", ConsoleColor.Red);
      console.WriteLine(@" | | | | '_ \| '_ \ / _` | '_ \ / _` | |/ _ \/ _` | |  _| \ \/ / __/ _ \ '_ \| __| |/ _ \| '_ \ ", ConsoleColor.Red);
      console.WriteLine(@" | |_| | | | | | | | (_| | | | | (_| | |  __/ (_| | | |___ >  < (_|  __/ |_) | |_| | (_) | | | |", ConsoleColor.Red);
      console.WriteLine(@"  \___/|_| |_|_| |_|\__,_|_| |_|\__,_|_|\___|\__,_| |_____/_/\_\___\___| .__/ \__|_|\___/|_| |_|", ConsoleColor.Red);
      console.WriteLine(@"                                                                       |_|                      ", ConsoleColor.Red);
      console.WriteLine();
      console.WriteLine($" Type:     {exception.GetType()}");
      console.WriteLine();
      console.WriteLine($" Message:  {exception.Message}");
      console.WriteLine();
      console.WriteLine(" StackTrace");
      console.WriteLine();
      console.WriteLine(exception.StackTrace);

      return true;
   }

   #endregion
}