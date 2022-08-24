// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MiddlewareExtensions.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable once CheckNamespace

namespace ConsoLovers.ConsoleToolkit.Core;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using ConsoLovers.ConsoleToolkit.Core.Middleware;

using Microsoft.Extensions.DependencyInjection;

[SuppressMessage("ReSharper", "UnusedMember.Global")]
public static class MiddlewareExtensions
{
   #region Public Methods and Operators

   public static IApplicationBuilder<T> AddMiddleware<T>([JetBrains.Annotations.NotNull] this IApplicationBuilder<T> builder,
      [JetBrains.Annotations.NotNull] IMiddleware<T> middleware)
      where T : class
   {
      if (builder == null)
         throw new ArgumentNullException(nameof(builder));
      if (middleware == null)
         throw new ArgumentNullException(nameof(middleware));

      return builder.AddService(x => x.AddSingleton(middleware));
   }

   public static IApplicationBuilder<T> AddMiddleware<T>([JetBrains.Annotations.NotNull] this IApplicationBuilder<T> builder,
      [JetBrains.Annotations.NotNull] Type middlewareType)
      where T : class
   {
      if (builder == null)
         throw new ArgumentNullException(nameof(builder));
      if (middlewareType == null)
         throw new ArgumentNullException(nameof(middlewareType));

      return builder.AddService(x => x.AddTransient(typeof(IMiddleware<T>), middlewareType));
   }

   public static IApplicationBuilder<T> AddMiddleware<T, TMiddleware>([JetBrains.Annotations.NotNull] this IApplicationBuilder<T> builder)
      where T : class
      where TMiddleware : IMiddleware<T>
   {
      if (builder == null)
         throw new ArgumentNullException(nameof(builder));

      return builder.AddMiddleware(typeof(TMiddleware));
   }

   public static IApplicationBuilder<T> RemoveMiddleware<T>([JetBrains.Annotations.NotNull] this IApplicationBuilder<T> builder, Type middlewareType)
      where T : class
   {
      if (builder == null)
         throw new ArgumentNullException(nameof(builder));

      return builder.RemoveService(x => RemoveImplementation(x, middlewareType));
   }

   #endregion

   #region Methods

   internal static void RemoveImplementation([JetBrains.Annotations.NotNull] this IServiceCollection serviceCollection,
      [JetBrains.Annotations.NotNull] Type implementationType)
   {
      if (serviceCollection == null)
         throw new ArgumentNullException(nameof(serviceCollection));
      if (implementationType == null)
         throw new ArgumentNullException(nameof(implementationType));

      var serviceDescriptors = serviceCollection.Where(x => x.ImplementationType == implementationType).ToArray();
      foreach (var serviceDescriptor in serviceDescriptors)
         serviceCollection.Remove(serviceDescriptor);
   }

   #endregion
}