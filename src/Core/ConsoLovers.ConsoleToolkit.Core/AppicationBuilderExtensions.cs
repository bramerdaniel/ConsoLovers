// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AppicationBuilderExtensions.cs" company="KUKA Deutschland GmbH">
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

using Microsoft.Extensions.DependencyInjection;

public static class ApplicationBuilderExtensions
{
   #region Public Methods and Operators

   public static IApplicationBuilder<T> AddResourceManager<T>(this IApplicationBuilder<T> builder, ResourceManager resourceManager)
      where T : class
   {
      if (builder is not IServiceConfigurationHandler configurationHandler)
         throw new InvalidOperationException("The builder does not support service configuration");

      configurationHandler.ConfigureRequiredService<DefaultLocalizationService>(localizationService =>
      {
         localizationService.AddResourceManager(resourceManager);
      });
      return builder;
   }

   /// <summary>
   ///    Adds a scoped service of the type specified in <paramref name="serviceType"/> with an implementation of the type specified in
   ///    <paramref name="implementationType"/> to the specified <see cref="IServiceCollection"/>.
   /// </summary>
   /// <param name="builder">The <see cref="IApplicationBuilder{T}"/> to add the service to.</param>
   /// <param name="serviceType">The type of the service to register.</param>
   /// <param name="implementationType">The implementation type of the service.</param>
   /// <returns>A reference to this instance after the operation has completed.</returns>
   /// <seealso cref="ServiceLifetime.Scoped"/>
   public static IApplicationBuilder<T> AddScoped<T>(this IApplicationBuilder<T> builder,
      Type serviceType, Type implementationType)
      where T : class
   {
      return builder.ConfigureServices(s => s.AddScoped(serviceType, implementationType));
   }

   /// <summary>Adds a singleton service of the type specified in <paramref name="serviceType"/> to the specified <see cref="IServiceCollection"/>.</summary>
   /// <param name="builder">The <see cref="IApplicationBuilder{T}"/> to add the service to.</param>
   /// <param name="serviceType">The type of the service to register and the implementation to use.</param>
   /// <returns>A reference to this instance after the operation has completed.</returns>
   /// <seealso cref="ServiceLifetime.Singleton"/>
   public static IApplicationBuilder<T> AddSingleton<T>(this IApplicationBuilder<T> builder, Type serviceType)
      where T : class
   {
      return builder.ConfigureServices(s => s.AddSingleton(serviceType));
   }

   public static IApplicationBuilder<T> AddSingleton<T>(this IApplicationBuilder<T> builder, Type serviceType, Type implementationType)
      where T : class
   {
      return builder.ConfigureServices(s => s.AddSingleton(serviceType, implementationType));
   }

   public static IApplicationBuilder<T> AddSingleton<T>(this IApplicationBuilder<T> builder, Type serviceType, object implementation)
      where T : class
   {
      return builder.ConfigureServices(s => s.AddSingleton(serviceType, implementation));
   }

   public static IExecutable<T> Run<T>([JetBrains.Annotations.NotNull] this IApplicationBuilder<T> builder)
      where T : class
   {
      return builder.Run(Environment.CommandLine);
   }

   public static IExecutable<T> Run<T>([JetBrains.Annotations.NotNull] this IApplicationBuilder<T> builder,
      [JetBrains.Annotations.NotNull] string[] args)
      where T : class
   {
      if (builder == null)
         throw new ArgumentNullException(nameof(builder));

      if (args == null)
         throw new ArgumentNullException(nameof(args));

      return builder.Build().RunAsync(args, CancellationToken.None)
         .GetAwaiter()
         .GetResult();
   }

   public static IExecutable<T> Run<T>([JetBrains.Annotations.NotNull] this IApplicationBuilder<T> builder, string args)
      where T : class
   {
      if (builder == null)
         throw new ArgumentNullException(nameof(builder));

      return builder.Build().RunAsync(args, CancellationToken.None)
         .GetAwaiter()
         .GetResult();
   }

   public static Task<IExecutable<T>> RunAsync<T>([JetBrains.Annotations.NotNull] this IApplicationBuilder<T> builder,
      CancellationToken cancellationToken)
      where T : class
   {
      if (builder == null)
         throw new ArgumentNullException(nameof(builder));

      return builder.RunAsync(Environment.CommandLine, cancellationToken);
   }

   public static Task<IExecutable<T>> RunAsync<T>([JetBrains.Annotations.NotNull] this IApplicationBuilder<T> builder, string args,
      CancellationToken cancellationToken)
      where T : class
   {
      if (builder == null)
         throw new ArgumentNullException(nameof(builder));

      return builder.Build().RunAsync(args, cancellationToken);
   }

   public static Task<IExecutable<T>> RunAsync<T>([JetBrains.Annotations.NotNull] this IApplicationBuilder<T> builder, string[] args,
      CancellationToken cancellationToken)
      where T : class
   {
      if (builder == null)
         throw new ArgumentNullException(nameof(builder));

      return builder.Build().RunAsync(args, cancellationToken);
   }

   public static Task<IExecutable<T>> RunAsync<T>([JetBrains.Annotations.NotNull] this IApplicationBuilder<T> builder)
      where T : class
   {
      return builder.RunAsync(CancellationToken.None);
   }

   public static IApplicationBuilder<T> ShowHelpWithoutArguments<T>([JetBrains.Annotations.NotNull] this IApplicationBuilder<T> builder)
      where T : class
   {
      if (builder == null)
         throw new ArgumentNullException(nameof(builder));

      return builder.ConfigureServices(x => x.AddTransient<IApplicationLogic, ShowHelpLogic>());
   }

   public static IApplicationBuilder<T> UseApplicationLogic<T>([JetBrains.Annotations.NotNull] this IApplicationBuilder<T> builder,
      [JetBrains.Annotations.NotNull] IApplicationLogic applicationLogic)
      where T : class
   {
      if (builder == null)
         throw new ArgumentNullException(nameof(builder));
      if (applicationLogic == null)
         throw new ArgumentNullException(nameof(applicationLogic));

      return builder.ConfigureServices(x => x.AddSingleton(applicationLogic));
   }

   public static IApplicationBuilder<T> UseApplicationLogic<T>([JetBrains.Annotations.NotNull] this IApplicationBuilder<T> builder,
      [JetBrains.Annotations.NotNull] IApplicationLogic<T> applicationLogic)
      where T : class
   {
      if (builder == null)
         throw new ArgumentNullException(nameof(builder));
      if (applicationLogic == null)
         throw new ArgumentNullException(nameof(applicationLogic));

      return builder.ConfigureServices(x => x.AddSingleton(applicationLogic));
   }

   public static IApplicationBuilder<T> UseApplicationLogic<T>([JetBrains.Annotations.NotNull] this IApplicationBuilder<T> builder,
      [JetBrains.Annotations.NotNull] Func<T, CancellationToken, Task> applicationLogic)
      where T : class
   {
      if (builder == null)
         throw new ArgumentNullException(nameof(builder));
      if (applicationLogic == null)
         throw new ArgumentNullException(nameof(applicationLogic));

      return builder.ConfigureServices(x => x.AddSingleton<IApplicationLogic>(new DelegateLogic<T>(applicationLogic)));
   }

   public static IApplicationBuilder<T> UseApplicationLogic<T, TLogic>([JetBrains.Annotations.NotNull] this IApplicationBuilder<T> builder)
      where T : class
      where TLogic : class, IApplicationLogic

   {
      if (builder == null)
         throw new ArgumentNullException(nameof(builder));

      return builder.ConfigureServices(x => x.AddTransient<IApplicationLogic, TLogic>());
   }

   #endregion
}