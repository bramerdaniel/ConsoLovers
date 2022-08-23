// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MiddlewareExtensions.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

// ReSharper disable once CheckNamespace
namespace ConsoLovers.ConsoleToolkit.Core;

using System;

using ConsoLovers.ConsoleToolkit.Core.Builders;
using ConsoLovers.ConsoleToolkit.Core.Middleware;

using Microsoft.Extensions.DependencyInjection;

public static class MiddlewareExtensions
{
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
}