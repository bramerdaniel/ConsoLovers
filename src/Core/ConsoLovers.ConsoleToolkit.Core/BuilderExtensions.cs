// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BuilderExtensions.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using ConsoLovers.ConsoleToolkit.Core.Localization;

namespace ConsoLovers.ConsoleToolkit.Core;

using System;
using System.Resources;

using ConsoLovers.ConsoleToolkit.Core.BootStrappers;
using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
using ConsoLovers.ConsoleToolkit.Core.DefaultImplementations;

using JetBrains.Annotations;

using Microsoft.Extensions.DependencyInjection;

public static class BuilderExtensions
{
   public static IBootstrapper<T> AddResourceManager<T>(this IBootstrapper<T> bootstrapper, ResourceManager resourceManager)
      where T : class, IApplication
   {
      var configurationHandler = bootstrapper as IServiceConfigurationHandler;
      if (configurationHandler == null)
         throw new InvalidOperationException("The bootstrapper does not support service configuration");

      configurationHandler.ConfigureRequiredService<DefaultLocalizationService>(service => service.AddResourceManager(resourceManager));
      return bootstrapper;
   }

   public static IBootstrapper<T> UseApplicationLogic<T>([NotNull] this IBootstrapper<T> bootstrapper, [NotNull] IApplicationLogic applicationLogic)
      where T : class, IApplication
   {
      if (bootstrapper == null)
         throw new ArgumentNullException(nameof(bootstrapper));
      if (applicationLogic == null)
         throw new ArgumentNullException(nameof(applicationLogic));

      return bootstrapper.ConfigureServices(x => x.AddSingleton(applicationLogic));
   }

   public static IBootstrapper<T> UseApplicationLogic<T, TLogic>([NotNull] this IBootstrapper<T> bootstrapper)
      where T : class, IApplication
      where TLogic : class, IApplicationLogic

   {
      if (bootstrapper == null)
         throw new ArgumentNullException(nameof(bootstrapper));

      return bootstrapper.ConfigureServices(x => x.AddTransient<IApplicationLogic, TLogic>());
   }

   public static IBootstrapper<T> ShowHelpWithoutArguments<T>([NotNull] this IBootstrapper<T> bootstrapper)
      where T : class, IApplication
   {
      if (bootstrapper == null)
         throw new ArgumentNullException(nameof(bootstrapper));

      return bootstrapper.ConfigureServices(x => x.AddTransient<IApplicationLogic, ShowHelpLogic>());
   }
}