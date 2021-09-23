// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExceptionFormatter.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
    using JetBrains.Annotations;
    using System;

    /// <summary>Helper class for printing <see cref="Exception"/> details to the console</summary>
    public class ExceptionFormatter
    {
        #region Constants and Fields

        private readonly IConsole console;

        private readonly string headerFormatString = "##### {0} ####";

        #endregion Constants and Fields

        #region Constructors and Destructors

        /// <summary>Initializes a new instance of the <see cref="ExceptionFormatter"/> class.</summary>
        /// <param name="console">The console.</param>
        private ExceptionFormatter([NotNull] IConsole console)
        {
            this.console = console ?? throw new ArgumentNullException(nameof(console));
        }

        /// <summary>Initializes a new instance of the <see cref="ExceptionFormatter"/> class.</summary>
        public ExceptionFormatter()
         : this(new ConsoleProxy())
        {
        }

        #endregion Constructors and Destructors

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

        #endregion Public Methods and Operators

        #region Methods

        private void PrintLine(string header, string text)
        {
            var consoleWidth = Console.WindowWidth;
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

        #endregion Methods
    }
}