﻿namespace ConsoLovers.ConsoleToolkit.Console
{
   /// <summary>
    /// Exposes methods used for creating (potentially inexact) copies of objects.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IPrototypable<T>
    {
        /// <summary>
        /// Returns a potentially inexact copy of the target object.
        /// </summary>
        /// <returns></returns>
        T Prototype();
    }
}
