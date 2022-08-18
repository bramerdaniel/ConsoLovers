// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HelperExtensions.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests;

using System;

using ConsoLovers.ConsoleToolkit.Core.BootStrappers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

public static class HelperExtensions
{
   internal static T BuildApplication<T>(this IBootstrapper<T> bootstrapper)
      where T : class, IApplication
   {
      if (bootstrapper is GenericBootstrapper<T> genericBootstrapper)
      {
         var applicationManager = genericBootstrapper.CreateApplicationManager();
         return applicationManager.CreateApplication();
      }

      throw new AssertFailedException($"The bootstrapper was not a {nameof(GenericBootstrapper<T>)}");
   }

   internal static IServiceProvider CreateServiceProvider<T>(this IBootstrapper<T> bootstrapper)
      where T : class, IApplication
   {
      if (bootstrapper is GenericBootstrapper<T> genericBootstrapper)
         return genericBootstrapper.CreateServiceProvider();

      throw new AssertFailedException($"The bootstrapper was not a {nameof(GenericBootstrapper<T>)}");
   }
}