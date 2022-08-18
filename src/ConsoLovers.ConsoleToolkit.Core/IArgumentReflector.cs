// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IArgumentReflector.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core;

using System;

using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

/// <summary>Helper service for analyzing argument types for their mapping info</summary>
public interface IArgumentReflector
{
   #region Public Methods and Operators

   /// <summary>Gets the argument type information.</summary>
   /// <param name="argumentType">Type of the argument.</param>
   /// <returns>The <see cref="ArgumentClassInfo"/> for the type</returns>
   ArgumentClassInfo GetTypeInfo(Type argumentType);

   /// <summary>Gets the argument type information.</summary>
   /// <typeparam name="T">The type of the argument</typeparam>
   /// <returns>The <see cref="ArgumentClassInfo"/> for the type</returns>
   ArgumentClassInfo GetTypeInfo<T>();

   #endregion
}