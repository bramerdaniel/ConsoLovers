// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IApplicationLogic.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core;

using System.Threading;
using System.Threading.Tasks;

/// <summary>The logic that will be executed when the application is started without commands</summary>
public interface IApplicationLogic
{
   #region Public Methods and Operators

   Task ExecuteAsync<T>(T arguments, CancellationToken cancellationToken);

   #endregion
}

/// <summary>The logic that will be executed when the application is started without commands</summary>
/// <typeparam name="T">The type of the arguments your logic will handle</typeparam>
public interface IApplicationLogic<in T>
{
   #region Public Methods and Operators

   Task ExecuteAsync(T arguments, CancellationToken cancellationToken);

   #endregion
}