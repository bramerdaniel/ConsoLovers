// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDependencyInjectionAbstraction.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core;

using System;

using Microsoft.Extensions.DependencyInjection;

/// <summary> Shared interface for the microsoft dependency injection abstraction</summary>
/// <typeparam name="T">The implementing type</typeparam>
public interface IDependencyInjectionAbstraction<out T>
{
   #region Public Methods and Operators

   /// <summary>Add the one or more services to the passed <see cref="IServiceCollection"/>.</summary>
   /// <param name="serviceSetup">The function that passes the used <see cref="IServiceCollection"/>.</param>
   /// <returns>The current <see cref="T"/> for further configuration</returns>
   T AddService(Action<IServiceCollection> serviceSetup);

   /// <summary>Specifies the <see cref="IServiceProviderFactory{TContainerBuilder}"/> that should be used</summary>
   /// <typeparam name="TContainerBuilder">The type of the container builder.</typeparam>
   /// <param name="factory">The factory to use.</param>
   /// <returns>The current <see cref="T"/> for further configuration</returns>
   T UseServiceProviderFactory<TContainerBuilder>(IServiceProviderFactory<TContainerBuilder> factory);

   #endregion
}