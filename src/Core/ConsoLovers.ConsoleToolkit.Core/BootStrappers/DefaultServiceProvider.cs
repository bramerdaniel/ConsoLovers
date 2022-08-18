// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultServiceProvider.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.BootStrappers;

using System;

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
      container.Register<IServiceProvider>(this)
         .WithLifetime(Lifetime.Singleton);

      foreach (var service in serviceCollection)
      {
         switch (service.Lifetime)
         {
            case ServiceLifetime.Singleton:
               RegisterSingleton(service);
               break;
            case ServiceLifetime.Scoped:
            case ServiceLifetime.Transient:
               RegisterTransient(service);
               break;
            default:
               throw new ArgumentOutOfRangeException();
         }
      }
   }

   private void RegisterTransient(ServiceDescriptor service)
   {
      container.Register(service.ServiceType, service.ImplementationType)
         .WithLifetime(Lifetime.None);
   }

   private void RegisterSingleton(ServiceDescriptor serviceDescriptor)
   {
      if (serviceDescriptor.ImplementationType != null)
      {
         container.Register(serviceDescriptor.ServiceType, serviceDescriptor.ImplementationType).WithLifetime(Lifetime.Singleton);
      }
      else if (serviceDescriptor.ImplementationInstance != null)
      {
         container.Register(serviceDescriptor.ServiceType, serviceDescriptor.ImplementationInstance).WithLifetime(Lifetime.Singleton);
      }
      else if (serviceDescriptor.ImplementationFactory != null)
      {
         container.Register(serviceDescriptor.ServiceType, _ => serviceDescriptor.ImplementationFactory(this)).WithLifetime(Lifetime.Singleton);
      }
   }

   #endregion

   #region IServiceProvider Members

   public object GetService(Type serviceType)
   {
      return container.Resolve(serviceType);
   }

   #endregion
}