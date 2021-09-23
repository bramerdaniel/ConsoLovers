// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICommand.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
    /// <summary>Basic interface for commands without arguments</summary>
    public interface ICommand
    {
        #region Public Methods and Operators

        /// <summary>Should be called for executing the command</summary>
        void Execute();

        #endregion Public Methods and Operators
    }

    /// <summary>Basic interface for commands that required arguments for execution</summary>
    /// <typeparam name="T">The type of the arguments the command required</typeparam>
    public interface ICommand<T> : ICommand
    {
        #region Public Properties

        /// <summary>Gets or sets the arguments that were specified for the command.</summary>
        T Arguments { get; set; }

        #endregion Public Properties
    }
}