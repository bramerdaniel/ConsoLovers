// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExceptionHandlerExtensions.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core;

using System;

using ConsoLovers.ConsoleToolkit.Core.Builders;

using Microsoft.Extensions.DependencyInjection;

public static class ExceptionHandlerExtensions
{
   #region Public Methods and Operators

   /// <summary>Registers the specified <see cref="exceptionHandler"/> as <see cref="IExceptionHandler"/> for the application.</summary>
   /// <typeparam name="T">The type of the application builder</typeparam>
   /// <param name="builder">The builder.</param>
   /// <param name="exceptionHandler">The exception handler.</param>
   /// <returns></returns>
   /// <exception cref="System.ArgumentNullException">builder</exception>
   public static IApplicationBuilder<T> UseExceptionHandler<T>([JetBrains.Annotations.NotNull] this IApplicationBuilder<T> builder,
      IExceptionHandler exceptionHandler)
      where T : class
   {
      if (builder == null)
         throw new ArgumentNullException(nameof(builder));

      return builder.AddService(x => x.AddSingleton(exceptionHandler));
   }

   /// <summary>Registers the specified <see cref="exceptionHandlerType"/> as <see cref="IExceptionHandler"/> for the application.</summary>
   /// <typeparam name="T"></typeparam>
   /// <param name="builder">The builder.</param>
   /// <param name="exceptionHandlerType">Type of the exception handler.</param>
   /// <returns></returns>
   /// <exception cref="System.ArgumentNullException">builder</exception>
   public static IApplicationBuilder<T> UseExceptionHandler<T>([JetBrains.Annotations.NotNull] this IApplicationBuilder<T> builder,
      Type exceptionHandlerType)
      where T : class
   {
      if (builder == null)
         throw new ArgumentNullException(nameof(builder));

      return builder.AddService(x => x.AddSingleton(typeof(IExceptionHandler), exceptionHandlerType));
   }

   #endregion
}