// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleApplicationManager.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
    using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
    using System;

    internal class ConsoleApplicationManagerGeneric<T> : ConsoleApplicationManager
       where T : class, IApplication
    {
        #region Internal Constructors

        internal ConsoleApplicationManagerGeneric(Func<T> createApplication)
         : base(type => createApplication())
        {
        }

        internal ConsoleApplicationManagerGeneric()
           : this(() => new DefaultFactory().CreateInstance<T>())
        {
        }

        #endregion Internal Constructors

        #region Public Methods

        public T Run(string[] args)
        {
            return (T)Run(typeof(T), args);
        }

        public T Run(string args)
        {
            return (T)Run(typeof(T), args);
        }

        #endregion Public Methods
    }
}