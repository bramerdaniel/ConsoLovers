// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HelperExtensions.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests;

using System;
using ConsoLovers.ConsoleToolkit.Core.Bootstrappers;
using ConsoLovers.ConsoleToolkit.Core.Builders;

using Microsoft.VisualStudio.TestTools.UnitTesting;

public static class HelperExtensions
{
   internal static T BuildApplication<T>(this IBootstrapper<T> bootstrapper)
      where T : class, IApplication
   {
      if (bootstrapper is GenericBootstrapper<T> genericBuilder)
      {
         var applicationManager = genericBuilder.CreateApplicationManager();
         return applicationManager.CreateApplication();
      }

      throw new AssertFailedException($"The builder was not a {nameof(GenericBootstrapper<T>)}");
   }

   internal static IServiceProvider CreateServiceProvider<T>(this IApplicationBuilder<T> builder)
      where T : class
   {
      if (builder is ApplicationBuilder<T> applicationBuilder)
         return applicationBuilder.CreateServiceProvider();

      throw new AssertFailedException($"The builder was not a {nameof(ApplicationBuilder<T>)}");
   }
}