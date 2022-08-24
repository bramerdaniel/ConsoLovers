// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UseServiceCollection.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.ApplicationBuilderTests;

using System;
using System.Diagnostics.CodeAnalysis;

using ConsoLovers.ConsoleToolkit.Core.UnitTests.Setups;

using FluentAssertions;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
public class UseServiceCollectionTests
{
   [TestMethod]
   public void EnsureSpecifiedServiceCollectionIsUsedCorrectly()
   {
      var serviceCollection = new ServiceCollection();

      var builder = Setup.ApplicationBuilder<Args>()
         .UseServiceCollection(serviceCollection)
         .Done();

      var serviceProvider = builder.GetOrCreateServiceProvider();

      serviceProvider.GetRequiredService<IServiceCollection>().Should().BeSameAs(serviceCollection);
      serviceProvider.GetRequiredService<Args>().ServiceCollection.Should().BeSameAs(serviceCollection);
   }

   [TestMethod]
   public void EnsureNullThrowsArgumentNullException()
   {
      var target = Setup.ApplicationBuilder<Args>().Done();

      target.Invoking(x => x.UseServiceCollection(null!))
         .Should().Throw<ArgumentNullException>();
   }

   [TestMethod]
   public void EnsurePreviousServicesAreStillAvailable()
   {
      var serviceCollection = new ServiceCollection();
      serviceCollection.AddSingleton<SecondService>();

      var builder = Setup.ApplicationBuilder<Args>().Done();
      builder.AddService(s => s.AddSingleton<FirstService>());

      builder.UseServiceCollection(serviceCollection);

      var serviceProvider = builder.GetOrCreateServiceProvider();

      serviceProvider.GetRequiredService<FirstService>().Should().NotBeNull();
      serviceProvider.GetRequiredService<SecondService>().Should().NotBeNull();
   }

   private class FirstService
   {
   }

   private class SecondService
   {
   }

   public class Args
   {
      public Args([JetBrains.Annotations.NotNull] IServiceCollection serviceCollection)
      {
         ServiceCollection = serviceCollection ?? throw new ArgumentNullException(nameof(serviceCollection));
      }

      public IServiceCollection ServiceCollection { get; }
   }
}

