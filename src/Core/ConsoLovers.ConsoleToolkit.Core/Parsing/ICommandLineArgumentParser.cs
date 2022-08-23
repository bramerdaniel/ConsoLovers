// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICommandLineArgumentParser.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   /// <summary>Interface that creates a command line dictionary from the given command line arguments</summary>
   public interface ICommandLineArgumentParser
   {
      #region Public Properties

      /// <summary>Gets or sets the <see cref="ICommandLineOptions"/>.</summary>
      ICommandLineOptions Options { get; set; }

      #endregion

      #region Public Methods and Operators

      /// <summary>Parses the given arguments into a dictionary.</summary>
      /// <param name="args">The command line arguments as string.</param>
      /// <returns>The <see cref="ICommandLineArguments"/></returns>
      ICommandLineArguments ParseArguments(string args);

      /// <summary>Parses the given arguments into a dictionary.</summary>
      /// <param name="args">The command line arguments.</param>
      /// <returns>The <see cref="ICommandLineArguments"/></returns>
      ICommandLineArguments ParseArguments(string[] args);

      #endregion
   }
}