// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IArgumentInitializer.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
    /// <summary>Interface that can be implemented by an <see cref="IApplication"/> implementer to initialize the command line arguments</summary>
    /// <typeparam name="T">The type of the arguments</typeparam>
    public interface IArgumentInitializer<T>
       where T : class
    {
        #region Public Methods and Operators

        /// <summary>This method is responsible for creating the required default arguments.
        /// This could e.g. be a empty instance or an instance filled with data from the app.config...</summary>
        /// <returns>The created arguments instance</returns>
        T CreateArguments();

        /// <summary>Initializes the given instance with the command line arguments.</summary>
        /// <param name="instance">The instance of the arguments class to fill.</param>
        /// <param name="args">The arguments as string array that should be used.</param>
        void InitializeFromArray(T instance, string[] args);

        /// <summary>Initializes the given instance with the command line arguments.</summary>
        /// <param name="instance">The instance of the arguments class to fill.</param>
        /// <param name="args">The arguments as string that should be used.</param>
        void InitializeFromString(T instance, string args);

        #endregion Public Methods and Operators
    }
}