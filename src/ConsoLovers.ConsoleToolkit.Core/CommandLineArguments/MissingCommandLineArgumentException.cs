// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MissingCommandLineArgumentException.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
   /// <summary>Exception that is thrown when a command line argument is missing</summary>
   public class MissingCommandLineArgumentException : CommandLineArgumentException
   {
      #region Constructors and Destructors

      /// <summary>Initializes a new instance of the <see cref="MissingCommandLineArgumentException"/> class. Initializes a new instance of the <see cref="T:System.Exception"/> class.</summary>
      /// <param name="argument">The message that describes the error.</param>
      public MissingCommandLineArgumentException(string argument)
         : base($"The command line argument '{argument}' is missing but is specified as required.")
      {
         Argument = argument;
      }

      #endregion

      #region Public Properties

      /// <summary>Gets the missing command line argument name .</summary>
      public string Argument { get; private set; }

      #endregion
   }
}