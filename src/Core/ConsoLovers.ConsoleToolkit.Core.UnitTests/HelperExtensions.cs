// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HelperExtensions.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests;

using System;

using ConsoLovers.ConsoleToolkit.Core.Builders;

using Microsoft.VisualStudio.TestTools.UnitTesting;

public static class HelperExtensions
{
   internal static T BuildApplication<T>(this IApplicationBuilder<T> applicationBuilder)
      where T : class, IApplication
   {
      if (applicationBuilder is GenericApplicationBuilder<T> genericBuilder)
      {
         var applicationManager = genericBuilder.CreateApplicationManager();
         return applicationManager.CreateApplication();
      }

      throw new AssertFailedException($"The applicationBuilder was not a {nameof(GenericApplicationBuilder<T>)}");
   }

   internal static IServiceProvider CreateServiceProvider<T>(this IApplicationBuilder<T> applicationBuilder)
      where T : class, IApplication
   {
      if (applicationBuilder is GenericApplicationBuilder<T> genericBuilder)
         return genericBuilder.CreateServiceProvider();

      throw new AssertFailedException($"The applicationBuilder was not a {nameof(GenericApplicationBuilder<T>)}");
   }
}