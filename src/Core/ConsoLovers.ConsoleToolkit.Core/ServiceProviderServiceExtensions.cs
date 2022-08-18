﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Class1.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core;

using System;
using System.Collections.Generic;

/// <summary>
/// Extension methods for getting services from an <see cref="IServiceProvider" />.
/// </summary>
internal static class ServiceProviderServiceExtensions
{
   /// <summary>
   /// Get service of type <typeparamref name="T"/> from the <see cref="IServiceProvider"/>.
   /// </summary>
   /// <typeparam name="T">The type of service object to get.</typeparam>
   /// <param name="provider">The <see cref="IServiceProvider"/> to retrieve the service object from.</param>
   /// <returns>A service object of type <typeparamref name="T"/> or null if there is no such service.</returns>
   public static T? GetService<T>(this IServiceProvider provider)
   {
      if (provider == null)
      {
         throw new ArgumentNullException(nameof(provider));
      }

      return (T?)provider.GetService(typeof(T));
   }

   /// <summary>
   /// Get service of type <paramref name="serviceType"/> from the <see cref="IServiceProvider"/>.
   /// </summary>
   /// <param name="provider">The <see cref="IServiceProvider"/> to retrieve the service object from.</param>
   /// <param name="serviceType">An object that specifies the type of service object to get.</param>
   /// <returns>A service object of type <paramref name="serviceType"/>.</returns>
   /// <exception cref="System.InvalidOperationException">There is no service of type <paramref name="serviceType"/>.</exception>
   public static object GetRequiredService(this IServiceProvider provider, Type serviceType)
   {
      if (provider == null)
      {
         throw new ArgumentNullException(nameof(provider));
      }

      if (serviceType == null)
      {
         throw new ArgumentNullException(nameof(serviceType));
      }


      object? service = provider.GetService(serviceType);
      if (service == null)
      {
         throw new InvalidOperationException($"The service {serviceType.FullName} could not be resolved");
      }

      return service;
   }

   /// <summary>
   /// Get service of type <typeparamref name="T"/> from the <see cref="IServiceProvider"/>.
   /// </summary>
   /// <typeparam name="T">The type of service object to get.</typeparam>
   /// <param name="provider">The <see cref="IServiceProvider"/> to retrieve the service object from.</param>
   /// <returns>A service object of type <typeparamref name="T"/>.</returns>
   /// <exception cref="System.InvalidOperationException">There is no service of type <typeparamref name="T"/>.</exception>
   public static T GetRequiredService<T>(this IServiceProvider provider) where T : notnull
   {
      if (provider == null)
      {
         throw new ArgumentNullException(nameof(provider));
      }

      return (T)provider.GetRequiredService(typeof(T));
   }

   /// <summary>
   /// Get an enumeration of services of type <typeparamref name="T"/> from the <see cref="IServiceProvider"/>.
   /// </summary>
   /// <typeparam name="T">The type of service object to get.</typeparam>
   /// <param name="provider">The <see cref="IServiceProvider"/> to retrieve the services from.</param>
   /// <returns>An enumeration of services of type <typeparamref name="T"/>.</returns>
   public static IEnumerable<T> GetServices<T>(this IServiceProvider provider)
   {
      if (provider == null)
      {
         throw new ArgumentNullException(nameof(provider));
      }

      return provider.GetRequiredService<IEnumerable<T>>();
   }

   /// <summary>
   /// Get an enumeration of services of type <paramref name="serviceType"/> from the <see cref="IServiceProvider"/>.
   /// </summary>
   /// <param name="provider">The <see cref="IServiceProvider"/> to retrieve the services from.</param>
   /// <param name="serviceType">An object that specifies the type of service object to get.</param>
   /// <returns>An enumeration of services of type <paramref name="serviceType"/>.</returns>
   public static IEnumerable<object?> GetServices(this IServiceProvider provider, Type serviceType)
   {
      if (provider == null)
      {
         throw new ArgumentNullException(nameof(provider));
      }

      if (serviceType == null)
      {
         throw new ArgumentNullException(nameof(serviceType));
      }

      Type? genericEnumerable = typeof(IEnumerable<>).MakeGenericType(serviceType);
      return (IEnumerable<object>)provider.GetRequiredService(genericEnumerable);
   }
}