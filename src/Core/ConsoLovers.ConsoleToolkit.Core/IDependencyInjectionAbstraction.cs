﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDependencyInjectionAbstraction.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core;

using System;

using Microsoft.Extensions.DependencyInjection;

/// <summary> Shared interface for the microsoft dependency injection abstraction</summary>
/// <typeparam name="T">The implementing type</typeparam>
public interface IDependencyInjectionAbstraction<out T>
{
   /// <summary>Configures the one or more services .</summary>
   /// <param name="serviceSetup">The function that passes the used <see cref="IServiceCollection"/>.</param>
   /// <returns>The current <see cref="T"/> for further configuration</returns>
   T ConfigureServices(Action<IServiceCollection> serviceSetup);

   /// <summary>Specifies the <see cref="IServiceProviderFactory{TContainerBuilder}"/> that should be used</summary>
   /// <typeparam name="TContainerBuilder">The type of the container builder.</typeparam>
   /// <param name="factory">The factory to use.</param>
   /// <returns>The current <see cref="T"/> for further configuration</returns>
   T UseServiceProviderFactory<TContainerBuilder>(IServiceProviderFactory<TContainerBuilder> factory);
}