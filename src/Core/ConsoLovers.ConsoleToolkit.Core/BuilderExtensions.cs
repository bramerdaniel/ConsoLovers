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

using ConsoLovers.ConsoleToolkit.Core.Builders;
using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
using ConsoLovers.ConsoleToolkit.Core.DefaultImplementations;

using JetBrains.Annotations;

using Microsoft.Extensions.DependencyInjection;

public static class BuilderExtensions
{
   #region Public Methods and Operators

   public static IApplicationBuilder<T> AddResourceManager<T>(this IApplicationBuilder<T> applicationBuilder, ResourceManager resourceManager)
      where T : class, IApplication
   {
      var configurationHandler = applicationBuilder as IServiceConfigurationHandler;
      if (configurationHandler == null)
         throw new InvalidOperationException("The applicationBuilder does not support service configuration");

      configurationHandler.ConfigureRequiredService<DefaultLocalizationService>(service => service.AddResourceManager(resourceManager));
      return applicationBuilder;
   }

   public static T Run<T>([NotNull] this IApplicationBuilder<T> applicationBuilder)
      where T : class, IApplication
   {
      if (applicationBuilder == null)
         throw new ArgumentNullException(nameof(applicationBuilder));

      return applicationBuilder.RunAsync(CancellationToken.None)
         .GetAwaiter()
         .GetResult();
   }

   public static T Run<T>([NotNull] this IApplicationBuilder<T> applicationBuilder, string[] args)
      where T : class, IApplication
   {
      if (applicationBuilder == null)
         throw new ArgumentNullException(nameof(applicationBuilder));

      return applicationBuilder.RunAsync(args, CancellationToken.None)
         .GetAwaiter()
         .GetResult();
   }

   public static IApplication Run([NotNull] this IApplicationBuilder applicationBuilder)
   {
      if (applicationBuilder == null)
         throw new ArgumentNullException(nameof(applicationBuilder));

      return applicationBuilder.RunAsync(CancellationToken.None)
         .GetAwaiter()
         .GetResult();
   }

   public static IApplication Run([NotNull] this IApplicationBuilder applicationBuilder, string args)
   {
      if (applicationBuilder == null)
         throw new ArgumentNullException(nameof(applicationBuilder));

      return applicationBuilder.RunAsync(args, CancellationToken.None)
         .GetAwaiter()
         .GetResult();
   }

   public static IApplication Run([NotNull] this IApplicationBuilder applicationBuilder, string[] args)
   {
      if (applicationBuilder == null)
         throw new ArgumentNullException(nameof(applicationBuilder));

      return applicationBuilder.RunAsync(args, CancellationToken.None)
         .GetAwaiter()
         .GetResult();
   }

   public static Task<IApplication> RunAsync([NotNull] this IApplicationBuilder applicationBuilder)
   {
      if (applicationBuilder == null)
         throw new ArgumentNullException(nameof(applicationBuilder));

      return applicationBuilder.RunAsync(Environment.CommandLine, CancellationToken.None);
   }

   public static Task<IApplication> RunAsync([NotNull] this IApplicationBuilder applicationBuilder, CancellationToken cancellationToken)
   {
      if (applicationBuilder == null)
         throw new ArgumentNullException(nameof(applicationBuilder));

      return applicationBuilder.RunAsync(Environment.CommandLine, cancellationToken);
   }

   public static IApplicationBuilder<T> ShowHelpWithoutArguments<T>([NotNull] this IApplicationBuilder<T> applicationBuilder)
      where T : class, IApplication
   {
      if (applicationBuilder == null)
         throw new ArgumentNullException(nameof(applicationBuilder));

      return applicationBuilder.ConfigureServices(x => x.AddTransient<IApplicationLogic, ShowHelpLogic>());
   }

   public static IApplicationBuilder<T> UseApplicationLogic<T>([NotNull] this IApplicationBuilder<T> applicationBuilder, [NotNull] IApplicationLogic applicationLogic)
      where T : class, IApplication
   {
      if (applicationBuilder == null)
         throw new ArgumentNullException(nameof(applicationBuilder));
      if (applicationLogic == null)
         throw new ArgumentNullException(nameof(applicationLogic));

      return applicationBuilder.ConfigureServices(x => x.AddSingleton(applicationLogic));
   }

   public static IApplicationBuilder<T> UseApplicationLogic<T, TArguments>([NotNull] this IApplicationBuilder<T> applicationBuilder, [NotNull] Func<TArguments, CancellationToken, Task> applicationLogic)
      where T : class, IApplication where TArguments : class
   {
      if (applicationBuilder == null)
         throw new ArgumentNullException(nameof(applicationBuilder));
      if (applicationLogic == null)
         throw new ArgumentNullException(nameof(applicationLogic));

      return applicationBuilder.ConfigureServices(x => x.AddSingleton<IApplicationLogic>(new DelegateLogic<TArguments>(applicationLogic)));
   }

   public static IApplicationBuilder<T> UseApplicationLogic<T, TLogic>([NotNull] this IApplicationBuilder<T> applicationBuilder)
      where T : class, IApplication
      where TLogic : class, IApplicationLogic

   {
      if (applicationBuilder == null)
         throw new ArgumentNullException(nameof(applicationBuilder));

      return applicationBuilder.ConfigureServices(x => x.AddTransient<IApplicationLogic, TLogic>());
   }

   #endregion
}