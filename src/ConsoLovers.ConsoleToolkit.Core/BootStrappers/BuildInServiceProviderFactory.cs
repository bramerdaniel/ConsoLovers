// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BuildInServiceProviderFactory.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.BootStrappers;

using System;

using Microsoft.Extensions.DependencyInjection;

internal class BuildInServiceProviderFactory : IServiceProviderFactory<IServiceCollection>
{
   #region IServiceProviderFactory<IServiceCollection> Members

   public IServiceCollection CreateBuilder(IServiceCollection services)
   {
      return services;
   }

   public IServiceProvider CreateServiceProvider(IServiceCollection containerBuilder)
   {
      return new DefaultServiceProvider(containerBuilder);
   }

   #endregion
}