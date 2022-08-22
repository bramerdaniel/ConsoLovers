// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestHelperExtensions.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests;

using System;
using System.Threading.Tasks;

using ConsoLovers.ConsoleToolkit.Core.Builders;

using Microsoft.VisualStudio.TestTools.UnitTesting;

public static class TestHelperExtensions
{
   internal static IServiceProvider CreateServiceProvider<T>(this IApplicationBuilder<T> builder)
      where T : class
   {
      if (builder is ApplicationBuilder<T> applicationBuilder)
         return applicationBuilder.GetOrCreateServiceProvider();

      throw new AssertFailedException($"The builder was not a {nameof(ApplicationBuilder<T>)}");
   }

   internal static IConsoleApplication<T> RunTest<T>([JetBrains.Annotations.NotNull] this IApplicationBuilder<T> builder, 
      Action<T> applicationLogic, string args, out IServiceProvider serviceProvider)
      where T : class
   {
      builder.UseApplicationLogic((t, _) =>
      {
         applicationLogic(t);
         return Task.CompletedTask;
      });

      if (builder is ApplicationBuilder<T> applicationBuilder)
      {
         serviceProvider =  applicationBuilder.GetOrCreateServiceProvider();
      }
      else
      {
         serviceProvider = null;
      }
      
      return builder.Run(applicationLogic);
   }


   internal static IConsoleApplication<T> RunTest<T>([JetBrains.Annotations.NotNull] this IApplicationBuilder<T> builder,
      Action<T> applicationLogic, out IServiceProvider serviceProvider)
      where T : class
   {
      builder.UseApplicationLogic((t, _) =>
      {
         applicationLogic(t);
         return Task.CompletedTask;
      });

      if (builder is ApplicationBuilder<T> applicationBuilder)
      {
         serviceProvider = applicationBuilder.GetOrCreateServiceProvider();
      }
      else
      {
         serviceProvider = null;
      }

      return builder.Run();
   }
}