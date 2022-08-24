// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Create.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.DIContainer
{
   using System.Diagnostics.CodeAnalysis;

   using ConsoLovers.ConsoleToolkit.Core.DIContainer;
   using ConsoLovers.ConsoleToolkit.Core.DIContainer.Strategies;
   using ConsoLovers.ConsoleToolkit.Core.UnitTests.DIContainer.Testclasses;
   using ConsoLovers.ConsoleToolkit.Core.UnitTests.Setups;

   using FluentAssertions;

   using Microsoft.VisualStudio.TestTools.UnitTesting;

   [TestClass]
   [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
   public class Create
   {
      [TestMethod]
      public void CreateSimpleObjectWithoutDependenciesByType()
      {
         var container = Setup.Container().Done();

         var simple = container.Create(typeof(Simple));
         simple.Should().NotBeNull();

         var demo = container.Create<Demo>();
         demo.Should().NotBeNull();
         demo.GetId().Should().Be(0);
      }

      [TestMethod]
      public void CreateSimpleObjectWithoutDependenciesByGenericParameter()
      {
         var container = Setup.Container().Done();
         var simple = container.Create<Simple>();
         simple.Should().NotBeNull();

         var demo = (Demo)container.Create(typeof(Demo));
         demo.Should().NotBeNull();
      }

      [TestMethod]
      public void CreateSimpleObjectWithDependencies()
      {
         var container = Setup.Container().Done();
         var dependencies = container.Create<HaveDependancies>();
         dependencies.Should().NotBeNull();
         dependencies.Demo.Should().BeNull();

         container.Register<IDemo, Demo>();
         dependencies = container.Create<HaveDependancies>();
         dependencies.Should().NotBeNull();
         dependencies.Demo.Should().NotBeNull();
      }

      [TestMethod]
      public void CreateSimpleObjectWithOptions()
      {
         var container = Setup.Container().Done();

         // Normal method
         var simple = container.Create(typeof(Simple), new ContainerOptions { ConstructorSelectionStrategy = ConstructorSelectionStrategies.WithInjectionConstructorAttribute });
         simple.Should().BeNull();

         var noAtt = container.Create(typeof(NoAttributes), new ContainerOptions { ConstructorSelectionStrategy = ConstructorSelectionStrategies.WithInjectionConstructorAttribute });
         noAtt.Should().BeNull();

         var one = (OneAttribute)container.Create(
            typeof(OneAttribute),
            new ContainerOptions { ConstructorSelectionStrategy = ConstructorSelectionStrategies.WithInjectionConstructorAttribute });
         one.Should().NotBeNull();
         one.Id.Should().Be(1);

         one = container.Create<OneAttribute>(new ContainerOptions { ConstructorSelectionStrategy = ConstructorSelectionStrategies.WithMostParameters });
         one.Should().NotBeNull();
         one.Id.Should().Be(0);

         var multiple = container.Create<MultipleConstructorAttributes>(new ContainerOptions { ConstructorSelectionStrategy = ConstructorSelectionStrategies.WithMostParameters });
         multiple.Should().NotBeNull();
         multiple.Id.Should().Be(3);

         var combinedSimple = container.Create<Simple>(new ContainerOptions { ConstructorSelectionStrategy = ConstructorSelectionStrategies.WithCombinedLogic });
         combinedSimple.Should().NotBeNull();

         var combined = container.Create<MultipleConstructorAttributes>(new ContainerOptions { ConstructorSelectionStrategy = ConstructorSelectionStrategies.WithCombinedLogic });
         combined.Should().NotBeNull();
         combined.Id.Should().Be(2);
      }

   }
}