// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationBuilderExtensions.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core;

using System;
using System.Resources;
using System.Threading;
using System.Threading.Tasks;

using ConsoLovers.ConsoleToolkit.Core.Builders;
using ConsoLovers.ConsoleToolkit.Core.Services;

using Microsoft.Extensions.DependencyInjection;

public static class ApplicationBuilderExtensions
{
   #region Public Methods and Operators

   public static IApplicationBuilder<T> AddResourceManager<T>(this IApplicationBuilder<T> builder, ResourceManager resourceManager)
      where T : class
   {
      builder.ConfigureRequiredService<T, DefaultLocalizationService>(service => service.AddResourceManager(resourceManager));
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
      return builder.AddService(s => s.AddScoped(serviceType, implementationType));
   }

   /// <summary>Adds a singleton service of the type specified in <paramref name="serviceType"/> to the specified <see cref="IServiceCollection"/>.</summary>
   /// <param name="builder">The <see cref="IApplicationBuilder{T}"/> to add the service to.</param>
   /// <param name="serviceType">The type of the service to register and the implementation to use.</param>
   /// <returns>A reference to this instance after the operation has completed.</returns>
   /// <seealso cref="ServiceLifetime.Singleton"/>
   public static IApplicationBuilder<T> AddSingleton<T>(this IApplicationBuilder<T> builder, Type serviceType)
      where T : class
   {
      return builder.AddService(s => s.AddSingleton(serviceType));
   }

   public static IApplicationBuilder<T> AddSingleton<T>(this IApplicationBuilder<T> builder, Type serviceType, Type implementationType)
      where T : class
   {
      return builder.AddService(s => s.AddSingleton(serviceType, implementationType));
   }

   public static IApplicationBuilder<T> AddSingleton<T>(this IApplicationBuilder<T> builder, Type serviceType, object implementation)
      where T : class
   {
      return builder.AddService(s => s.AddSingleton(serviceType, implementation));
   }

   public static IApplicationBuilder<T> ConfigureCommandLineParser<T>(this IApplicationBuilder<T> builder,
      Action<ICommandLineOptions> configurationAction)
      where T : class
   {
      if (builder is not IServiceConfigurationHandler configurationHandler)
         throw new InvalidOperationException("The builder does not support service configuration");

      var defaultOptions = new CommandLineOptions();
      configurationAction(defaultOptions);

      configurationHandler.ConfigureRequiredService<ICommandLineArgumentParser>(p => p.Options = defaultOptions);
      return builder;
   }

   /// <summary>
   ///    Configures the service of the specified <see cref="TService"/> after it was created by the dependency injection framework. If the service
   ///    can not be resolved, this method throws an <see cref="InvalidOperationException"/>
   /// </summary>
   /// <typeparam name="T">The type of the application arguments</typeparam>
   /// <typeparam name="TService">The type of the service to configure.</typeparam>
   /// <param name="builder">The builder that is creating the application.</param>
   /// <param name="configurationAction">The service to configure.</param>
   /// <returns></returns>
   /// <exception cref="System.InvalidOperationException">The builder does not support service configuration</exception>
   public static IApplicationBuilder<T> ConfigureRequiredService<T, TService>(this IApplicationBuilder<T> builder,
      Action<TService> configurationAction)
      where T : class
   {
      if (builder is not IServiceConfigurationHandler configurationHandler)
         throw new InvalidOperationException("The builder does not support service configuration");

      configurationHandler.ConfigureRequiredService(configurationAction);
      return builder;
   }

   /// <summary>Configures the service of the specified <see cref="TService"/> after it was created by the dependency injection framework.</summary>
   /// <typeparam name="T">The type of the application arguments</typeparam>
   /// <typeparam name="TService">The type of the service to configure.</typeparam>
   /// <param name="builder">The builder that is creating the application.</param>
   /// <param name="configurationAction">The service to configure.</param>
   /// <returns></returns>
   /// <exception cref="System.InvalidOperationException">The builder does not support service configuration</exception>
   public static IApplicationBuilder<T> ConfigureService<T, TService>(this IApplicationBuilder<T> builder, Action<TService> configurationAction)
      where T : class
   {
      if (builder is not IServiceConfigurationHandler configurationHandler)
         throw new InvalidOperationException("The builder does not support service configuration");

      configurationHandler.ConfigureService(configurationAction);
      return builder;
   }

   public static IConsoleApplication<T> Run<T>([JetBrains.Annotations.NotNull] this IApplicationBuilder<T> builder)
      where T : class
   {
      return builder.Run(Environment.CommandLine);
   }

   public static IConsoleApplication<T> Run<T>([JetBrains.Annotations.NotNull] this IApplicationBuilder<T> builder, Action<T> applicationLogic)
      where T : class
   {
      return builder.Run(applicationLogic, Environment.CommandLine);
   }

   public static IConsoleApplication<T> Run<T>([JetBrains.Annotations.NotNull] this IApplicationBuilder<T> builder, Action<T> applicationLogic,
      string args)
      where T : class
   {
      builder.UseApplicationLogic((t, _) =>
      {
         applicationLogic(t);
         return Task.CompletedTask;
      });

      return builder.Run(args);
   }

   public static IConsoleApplication<T> Run<T>([JetBrains.Annotations.NotNull] this IApplicationBuilder<T> builder,
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

   public static IConsoleApplication<T> Run<T>([JetBrains.Annotations.NotNull] this IApplicationBuilder<T> builder, string args)
      where T : class
   {
      if (builder == null)
         throw new ArgumentNullException(nameof(builder));

      return builder.Build().RunAsync(args, CancellationToken.None)
         .GetAwaiter()
         .GetResult();
   }

   public static Task<IConsoleApplication<T>> RunAsync<T>([JetBrains.Annotations.NotNull] this IApplicationBuilder<T> builder,
      CancellationToken cancellationToken)
      where T : class
   {
      if (builder == null)
         throw new ArgumentNullException(nameof(builder));

      return builder.RunAsync(Environment.CommandLine, cancellationToken);
   }

   public static Task<IConsoleApplication<T>> RunAsync<T>([JetBrains.Annotations.NotNull] this IApplicationBuilder<T> builder, string args,
      CancellationToken cancellationToken)
      where T : class
   {
      if (builder == null)
         throw new ArgumentNullException(nameof(builder));

      return builder.Build().RunAsync(args, cancellationToken);
   }

   public static Task<IConsoleApplication<T>> RunAsync<T>([JetBrains.Annotations.NotNull] this IApplicationBuilder<T> builder, string[] args,
      CancellationToken cancellationToken)
      where T : class
   {
      if (builder == null)
         throw new ArgumentNullException(nameof(builder));

      return builder.Build().RunAsync(args, cancellationToken);
   }

   public static Task<IConsoleApplication<T>> RunAsync<T>([JetBrains.Annotations.NotNull] this IApplicationBuilder<T> builder)
      where T : class
   {
      return builder.RunAsync(CancellationToken.None);
   }

   public static IApplicationBuilder<T> ShowHelpWithoutArguments<T>([JetBrains.Annotations.NotNull] this IApplicationBuilder<T> builder)
      where T : class
   {
      if (builder == null)
         throw new ArgumentNullException(nameof(builder));

      return builder.AddService(x => x.AddTransient<IApplicationLogic, ShowHelpLogic>());
   }

   public static IApplicationBuilder<T> UseApplicationLogic<T>([JetBrains.Annotations.NotNull] this IApplicationBuilder<T> builder,
      [JetBrains.Annotations.NotNull] IApplicationLogic applicationLogic)
      where T : class
   {
      if (builder == null)
         throw new ArgumentNullException(nameof(builder));
      if (applicationLogic == null)
         throw new ArgumentNullException(nameof(applicationLogic));

      return builder.AddService(x => x.AddSingleton(applicationLogic));
   }

   public static IApplicationBuilder<T> UseApplicationLogic<T>([JetBrains.Annotations.NotNull] this IApplicationBuilder<T> builder,
      [JetBrains.Annotations.NotNull] IApplicationLogic<T> applicationLogic)
      where T : class
   {
      if (builder == null)
         throw new ArgumentNullException(nameof(builder));
      if (applicationLogic == null)
         throw new ArgumentNullException(nameof(applicationLogic));

      return builder.AddService(x => x.AddSingleton(applicationLogic));
   }

   public static IApplicationBuilder<T> UseApplicationLogic<T>([JetBrains.Annotations.NotNull] this IApplicationBuilder<T> builder,
      [JetBrains.Annotations.NotNull] Func<T, CancellationToken, Task> applicationLogic)
      where T : class
   {
      if (builder == null)
         throw new ArgumentNullException(nameof(builder));
      if (applicationLogic == null)
         throw new ArgumentNullException(nameof(applicationLogic));

      return builder.AddService(x => x.AddSingleton<IApplicationLogic>(new DelegateLogic<T>(applicationLogic)));
   }

   public static IApplicationBuilder<T> UseApplicationLogic<T, TLogic>([JetBrains.Annotations.NotNull] this IApplicationBuilder<T> builder)
      where T : class
      where TLogic : class, IApplicationLogic

   {
      if (builder == null)
         throw new ArgumentNullException(nameof(builder));

      return builder.AddService(x => x.AddTransient<IApplicationLogic, TLogic>());
   }

   #endregion
}