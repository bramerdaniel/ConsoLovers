// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestHelperExtensions.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests;

using System;
using ConsoLovers.ConsoleToolkit.Core.Builders;

using Microsoft.VisualStudio.TestTools.UnitTesting;

public static class TestHelperExtensions
{
   internal static IServiceProvider CreateServiceProvider<T>(this IApplicationBuilder<T> builder)
      where T : class
   {
      if (builder is ApplicationBuilder<T> applicationBuilder)
         return applicationBuilder.CreateServiceProvider();

      throw new AssertFailedException($"The builder was not a {nameof(ApplicationBuilder<T>)}");
   }
}