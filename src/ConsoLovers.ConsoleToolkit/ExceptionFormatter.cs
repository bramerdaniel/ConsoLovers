// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExceptionFormatter.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit
{
   using System;

   using ConsoLovers.ConsoleToolkit.Contracts;
   using ConsoLovers.ConsoleToolkit.Core.Contracts;

   using JetBrains.Annotations;

   /// <summary>Helper class for printing <see cref="Exception"/> details to the console</summary>
   public class ExceptionFormatter
   {
      #region Constants and Fields

      private readonly IConsole console;

      string headerFormatString = "##### {0} ####";

      #endregion

      #region Constructors and Destructors

      /// <summary>Initializes a new instance of the <see cref="ExceptionFormatter"/> class.</summary>
      public ExceptionFormatter()
         : this(new ConsoleProxy())
      {
      }

      /// <summary>Initializes a new instance of the <see cref="ExceptionFormatter"/> class.</summary>
      /// <param name="console">The console.</param>
      private ExceptionFormatter([NotNull] IConsole console)
      {
         if (console == null)
            throw new ArgumentNullException(nameof(console));

         this.console = console;
      }

      #endregion

      #region Public Methods and Operators

      /// <summary>Prints the given <see cref="Exception"/> to the console.</summary>
      /// <param name="exception">The exception to print.</param>
      public void Print([NotNull] Exception exception)
      {
         if (exception == null)
            throw new ArgumentNullException(nameof(exception));

         console.WriteLine(string.Format(headerFormatString, exception.GetType().FullName), ConsoleColor.Red);
         console.WriteLine();
         PrintLine("Message:    ", exception.Message);
         console.WriteLine();
         PrintLine("StackTrace: ", exception.StackTrace.TrimStart());
      }

      #endregion

      #region Methods

      private void PrintLine(string header, string text)
      {
         var consoleWidth = System.Console.WindowWidth;
         var headerIndent = string.Empty.PadRight(header.Length, ' ');

         var rest = header + text;
         if (rest.Length < consoleWidth)
         {
            console.WriteLine(rest, ConsoleColor.Red);
            return;
         }

         do
         {
            var line = rest.Substring(0, consoleWidth);
            console.Write(line, ConsoleColor.Red);
            rest = headerIndent + rest.Substring(line.Length);
         }
         while (rest.Length > consoleWidth);

         if (rest.Length > 0)
            console.WriteLine(rest, ConsoleColor.Red);
      }

      #endregion
   }
}