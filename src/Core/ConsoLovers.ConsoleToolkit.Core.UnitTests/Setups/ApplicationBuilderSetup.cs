// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationBuilderSetup.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.Setups;

using ConsoLovers.ConsoleToolkit.Core.Builders;

using Microsoft.Extensions.DependencyInjection;

internal class ApplicationBuilderSetup<T> : SetupBase<ApplicationBuilder<T>>
   where T : class
{
   private IServiceCollection serviceCollection;

   protected override ApplicationBuilder<T> CreateInstance()
   {
      var builder = new ApplicationBuilder<T>();
      if (serviceCollection != null)
         builder.UseServiceCollection(serviceCollection);

      return builder;
   }

   public ApplicationBuilderSetup<T> UseServiceCollection(IServiceCollection collection)
   {
      serviceCollection = collection;
      return this;
   }
}