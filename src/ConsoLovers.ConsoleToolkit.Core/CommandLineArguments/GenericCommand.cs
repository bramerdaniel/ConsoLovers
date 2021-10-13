// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Command.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
    public class GenericCommand<T> : ICommand<T>
    {
        #region Public Properties

        public T Arguments { get; set; }

        #endregion Public Properties

        #region Public Methods

        public virtual void Execute()
        {
        }

        #endregion Public Methods
    }
}