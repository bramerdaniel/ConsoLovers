// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LocalizationServiceExtensions.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Localization;

using System;
using System.Resources;

using ConsoLovers.ConsoleToolkit.Core.BootStrappers;

public static class LocalizationServiceExtensions
{
   public static IBootstrapper<T> AddResourceManager<T>(this IBootstrapper<T> bootstrapper, ResourceManager resourceManager)
      where T : class, IApplication
   {
      var configurationHandler = bootstrapper as IServiceConfigurationHandler;
      if (configurationHandler == null)
         throw new InvalidOperationException("The bootstrapper does not support service configuration");

      configurationHandler.ConfigureRequiredService<DefaultLocalizationService>(
         localizationService => localizationService.AddResourceManager(resourceManager));

      return bootstrapper;
   }
}