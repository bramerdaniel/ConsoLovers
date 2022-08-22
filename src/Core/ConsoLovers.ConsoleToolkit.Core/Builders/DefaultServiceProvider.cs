// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultServiceProvider.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Builders;

using System;
using System.Collections;
using System.Collections.Generic;

using ConsoLovers.ConsoleToolkit.Core.DIContainer;

using Microsoft.Extensions.DependencyInjection;

internal class DefaultServiceProvider : IServiceProvider
{
   #region Constants and Fields

   private readonly Container container;

   #endregion

   #region Constructors and Destructors

   public static IServiceProvider ForType<T>()
      where T : class
   {
      var serviceCollection = new ServiceCollection();
      serviceCollection.AddApplicationTypes<T>();
      return new DefaultServiceProvider(serviceCollection);
   }

   public DefaultServiceProvider()
   : this(new ServiceCollection())
   {
   }

   public DefaultServiceProvider(IServiceCollection serviceCollection)
   {
      container = new Container();
      container.Register(ServiceDescriptor.Singleton(typeof(IServiceProvider), this));

      foreach (var service in serviceCollection)
         container.Register(service);
   }


   #endregion

   #region IServiceProvider Members

   public object GetService(Type serviceType)
   {
      return container.Resolve(serviceType);
   }

   #endregion
}