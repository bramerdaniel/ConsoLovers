// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Lifetime.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.DIContainer
{
    /// <summary>Possible values for the lifetime of an object</summary>
    public enum Lifetime
    {
        /// <summary>Lifetime is not controlled by the container. New instance </summary>
        None,

        /// <summary>Only one instance is created by the <see cref="IContainer"/> this is returned for every request</summary>
        Singleton,
    }
}