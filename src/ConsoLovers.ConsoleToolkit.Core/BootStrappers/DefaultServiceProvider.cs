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

   private void RegisterSingleton(ServiceDescriptor service)
   {
      if (service.ImplementationType != null)
      {
         container.Register(service.ServiceType, service.ImplementationType).WithLifetime(Lifetime.Singleton);
      }
      else if(service.ImplementationInstance != null)
      {
         container.Register(service.ServiceType, service.ImplementationInstance).WithLifetime(Lifetime.Singleton);
      }
      else
      {
         
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