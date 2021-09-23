// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Command.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
    public class GenericCommand<T> : ICommand<T>
    {
        #region ICommand Members

        public virtual void Execute()
        {
        }

        #endregion ICommand Members

        #region ICommand<T> Members

        public T Arguments { get; set; }

        #endregion ICommand<T> Members
    }
}