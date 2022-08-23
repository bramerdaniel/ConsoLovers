// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BuildInServiceProviderFactory.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Builders;

using System;

using ConsoLovers.ConsoleToolkit.Core.DIContainer;

using Microsoft.Extensions.DependencyInjection;

internal class BuildInServiceProviderFactory : IServiceProviderFactory<IServiceCollection>
{
   #region IServiceProviderFactory<IServiceCollection> Members

   /// <summary>Creates the builder.</summary>
   /// <param name="services">The services.</param>
   /// <returns></returns>
   public IServiceCollection CreateBuilder(IServiceCollection services)
   {
      return services;
   }

   /// <summary>Creates the service provider.</summary>
   /// <param name="containerBuilder">The container builder.</param>
   /// <returns></returns>
   public IServiceProvider CreateServiceProvider(IServiceCollection containerBuilder)
   {
      return new Container(containerBuilder);
   }

   #endregion
}