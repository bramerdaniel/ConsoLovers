// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BuilderExtensions.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core;

using System;
using System.Resources;
using System.Threading;
using System.Threading.Tasks;

using ConsoLovers.ConsoleToolkit.Core.BootStrappers;
using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
using ConsoLovers.ConsoleToolkit.Core.DefaultImplementations;
using ConsoLovers.ConsoleToolkit.Core.Localization;

using JetBrains.Annotations;

using Microsoft.Extensions.DependencyInjection;

public static class BuilderExtensions
{
   #region Public Methods and Operators

   public static IBootstrapper<T> AddResourceManager<T>(this IBootstrapper<T> bootstrapper, ResourceManager resourceManager)
      where T : class, IApplication
   {
      var configurationHandler = bootstrapper as IServiceConfigurationHandler;
      if (configurationHandler == null)
         throw new InvalidOperationException("The bootstrapper does not support service configuration");

      configurationHandler.ConfigureRequiredService<DefaultLocalizationService>(service => service.AddResourceManager(resourceManager));
      return bootstrapper;
   }

   public static T Run<T>([NotNull] this IBootstrapper<T> bootstrapper)
      where T : class, IApplication
   {
      if (bootstrapper == null)
         throw new ArgumentNullException(nameof(bootstrapper));

      return bootstrapper.RunAsync(CancellationToken.None)
         .GetAwaiter()
         .GetResult();
   }

   public static T Run<T>([NotNull] this IBootstrapper<T> bootstrapper, string[] args)
      where T : class, IApplication
   {
      if (bootstrapper == null)
         throw new ArgumentNullException(nameof(bootstrapper));

      return bootstrapper.RunAsync(args, CancellationToken.None)
         .GetAwaiter()
         .GetResult();
   }

   public static IApplication Run([NotNull] this IBootstrapper bootstrapper)
   {
      if (bootstrapper == null)
         throw new ArgumentNullException(nameof(bootstrapper));

      return bootstrapper.RunAsync(CancellationToken.None)
         .GetAwaiter()
         .GetResult();
   }

   public static IApplication Run([NotNull] this IBootstrapper bootstrapper, string args)
   {
      if (bootstrapper == null)
         throw new ArgumentNullException(nameof(bootstrapper));

      return bootstrapper.RunAsync(args, CancellationToken.None)
         .GetAwaiter()
         .GetResult();
   }

   public static IApplication Run([NotNull] this IBootstrapper bootstrapper, string[] args)
   {
      if (bootstrapper == null)
         throw new ArgumentNullException(nameof(bootstrapper));

      return bootstrapper.RunAsync(args, CancellationToken.None)
         .GetAwaiter()
         .GetResult();
   }

   public static Task<IApplication> RunAsync([NotNull] this IBootstrapper bootstrapper)
   {
      if (bootstrapper == null)
         throw new ArgumentNullException(nameof(bootstrapper));

      return bootstrapper.RunAsync(Environment.CommandLine, CancellationToken.None);
   }

   public static Task<IApplication> RunAsync([NotNull] this IBootstrapper bootstrapper, CancellationToken cancellationToken)
   {
      if (bootstrapper == null)
         throw new ArgumentNullException(nameof(bootstrapper));

      return bootstrapper.RunAsync(Environment.CommandLine, cancellationToken);
   }

   public static IBootstrapper<T> ShowHelpWithoutArguments<T>([NotNull] this IBootstrapper<T> bootstrapper)
      where T : class, IApplication
   {
      if (bootstrapper == null)
         throw new ArgumentNullException(nameof(bootstrapper));

      return bootstrapper.ConfigureServices(x => x.AddTransient<IApplicationLogic, ShowHelpLogic>());
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

   #endregion
}