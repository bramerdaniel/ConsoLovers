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

   using FluentAssertions;

   using Microsoft.VisualStudio.TestTools.UnitTesting;

   [TestClass]
   [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
   public class Create
   {
      [TestMethod]
      public void CreateSimpleObjectWithoutDependenciesByType()
      {
         var simple = CreateTarget().Create(typeof(Simple));
         simple.Should().NotBeNull();

         var demo = CreateTarget().Create<Demo>();
         demo.Should().NotBeNull();
         demo.GetId().Should().Be(0);
      }

      [TestMethod]
      public void CreateSimpleObjectWithoutDependenciesByGenericParameter()
      {
         var simple = CreateTarget().Create<Simple>();
         simple.Should().NotBeNull();

         var demo = (Demo)CreateTarget().Create(typeof(Demo));
         demo.Should().NotBeNull();
      }

      [TestMethod]
      public void CreateSimpleObjectWithDependencies()
      {
         var container = CreateTarget();
         var dependancies = container.Create<HaveDependancies>();
         dependancies.Should().NotBeNull();
         dependancies.Demo.Should().BeNull();

         container.Register<IDemo, Demo>();
         dependancies = container.Create<HaveDependancies>();
         dependancies.Should().NotBeNull();
         dependancies.Demo.Should().NotBeNull();
      }

      [TestMethod]
      public void CreateSimpleObjectWithOptions()
      {
         Container container = new Container();

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

      [TestMethod]
      public void CreateObjectWithNamedDependencies()
      {
         var container = CreateTarget();
         container.Register<IDemo, Demo>();

         var dependancies = container.Create<HaveNamedDependancies>();
         dependancies.Should().NotBeNull();
         dependancies.Demo.Should().BeNull();

         container.RegisterNamed<IDemo, Demo>("Name");

         dependancies = container.Create<HaveNamedDependancies>();
         dependancies.Should().NotBeNull();
         dependancies.Demo.Should().NotBeNull();
      }

      private static Container CreateTarget()
      {
         return new Container();
      }
   }
}