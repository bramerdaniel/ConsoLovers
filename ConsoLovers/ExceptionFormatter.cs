// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExceptionFormatter.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace ConsoLovers.ConsoleToolkit
{
   using System;

   using ConsoLovers.ConsoleToolkit.Contracts;

   using JetBrains.Annotations;

   public class ExceptionFormatter
   {
      private readonly IConsole console;

      public ExceptionFormatter()
         : this(new ConsoleProxy())
      {
      }

      private ExceptionFormatter([NotNull] IConsole console)
      {
         if (console == null)
            throw new ArgumentNullException(nameof(console));

         this.console = console;
      }

      public void Print([NotNull] Exception exception)
      {
         if (exception == null)
            throw new ArgumentNullException(nameof(exception));

         console.WriteLine(exception.GetType().FullName, ConsoleColor.Red);
         console.WriteLine(exception.ToString(), ConsoleColor.Red);
      }
   }
}