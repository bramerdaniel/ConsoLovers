// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExitCodeHandlerExtensions.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core;

using System;

using Microsoft.Extensions.DependencyInjection;

public static class ExitCodeHandlerExtensions
{
   #region Public Methods and Operators


   /// <summary>Registers the specified <see cref="exitCodeHandlerType"/> as <see cref="IExitCodeHandler"/> for the application.</summary>
   /// <typeparam name="T">The type of the application builder</typeparam>
   /// <param name="builder">The builder.</param>
   /// <param name="exitCodeHandlerType">Type of the exit code handler.</param>
   /// <returns>The current <see cref="IApplicationBuilder{T}"/> for more fluent configuration</returns>
   /// <exception cref="System.ArgumentNullException">builder</exception>
   public static IApplicationBuilder<T> UseExitCodeHandler<T>([JetBrains.Annotations.NotNull] this IApplicationBuilder<T> builder,
      Type exitCodeHandlerType)
      where T : class
   {
      if (builder == null)
         throw new ArgumentNullException(nameof(builder));

      return builder.AddService(x => x.AddSingleton(typeof(IExitCodeHandler), exitCodeHandlerType));
   }

   /// <summary>Registers the specified <see cref="exitCodeHandler"/> as <see cref="IExitCodeHandler"/> for the application.</summary>
   /// <typeparam name="T">The type of the application builder</typeparam>
   /// <param name="builder">The builder.</param>
   /// <param name="exitCodeHandler">The <see cref="IExitCodeHandler"/>.</param>
   /// <returns>The current <see cref="IApplicationBuilder{T}"/> for more fluent configuration</returns>
   /// <exception cref="System.ArgumentNullException">builder</exception>
   public static IApplicationBuilder<T> UseExitCodeHandler<T>([JetBrains.Annotations.NotNull] this IApplicationBuilder<T> builder,
      IExitCodeHandler exitCodeHandler)
      where T : class
   {
      if (builder == null)
         throw new ArgumentNullException(nameof(builder));

      return builder.AddService(x => x.AddSingleton(exitCodeHandler));
   }

   #endregion
}