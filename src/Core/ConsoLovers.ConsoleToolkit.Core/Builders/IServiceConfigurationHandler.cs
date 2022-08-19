// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IServiceConfigurationHandler.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Builders;

using System;

public interface IServiceConfigurationHandler
{
   /// <summary>Configures the service of the specified type.</summary>
   /// <typeparam name="TService">The type of the service.</typeparam>
   /// <param name="configurationAction">The configuration action.</param>
   void ConfigureService<TService>(Action<TService> configurationAction);
   
   void ConfigureRequiredService<TService>(Action<TService> configurationAction);
}