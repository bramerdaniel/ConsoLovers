// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICommandLineArgumentParser.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
    using System.Collections.Generic;

    /// <summary>Interface that creates a command line dictionary from the given command line arguments</summary>
    public interface ICommandLineArgumentParser
    {
        #region Public Methods

        /// <summary>Parses the given arguments into a dictionary.</summary>
        /// <param name="args">The command line arguments.</param>
        /// <param name="caseSensitive">if set to <c>true</c> the arguments should be treated case sensitive.</param>
        /// <returns>The created dictionary</returns>
        IDictionary<string, CommandLineArgument> ParseArguments(string[] args, bool caseSensitive);

        /// <summary>Parses the given arguments into a dictionary.</summary>
        /// <param name="args">The command line arguments as string.</param>
        /// <param name="caseSensitive">if set to <c>true</c> the arguments should be treated case sensitive.</param>
        /// <returns>The created dictionary</returns>
        IDictionary<string, CommandLineArgument> ParseArguments(string args, bool caseSensitive);

        /// <summary>Parses the given arguments into a dictionary.</summary>
        /// <param name="args">The command line arguments as string.</param>
        /// <returns>The created dictionary</returns>
        IDictionary<string, CommandLineArgument> ParseArguments(string args);

        #endregion Public Methods
    }
}