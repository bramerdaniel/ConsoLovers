// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BuildUp.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.DIContainer
{
   using System;
   using System.Diagnostics.CodeAnalysis;

   using ConsoLovers.ConsoleToolkit.Core.DIContainer;
   using ConsoLovers.ConsoleToolkit.Core.DIContainer.Strategies;
   using ConsoLovers.ConsoleToolkit.Core.UnitTests.DIContainer.Testclasses;

   using FluentAssertions;

   using Microsoft.VisualStudio.TestTools.UnitTesting;

   [TestClass]
   [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
   public class BuildUp
   {
      [TestMethod]
      public void BuildUp_null_must_throw_ArgumentNullException()
      {
         var container = CreateTarget();
         container.Invoking(c => c.BuildUp(null)).Should().Throw<ArgumentNullException>();
      }

      [TestMethod]
      public void BuildUpAnExistingObjectInstance()
      {
         IContainer container = CreateTarget();

         var demoInstance = new Demo(222);
         container.Register<IDemo>(demoInstance);
         ObjectToBuild otb = new ObjectToBuild();
         container.BuildUp(otb);

         // Normal property injection
         otb.Demo.Should().NotBeNull();
         otb.Demo.Should().BeSameAs(demoInstance);

         // Property injection for container insself
         otb.Container.Should().BeSameAs(container);
      }

      [TestMethod]
      public void BuildUpAnExistingObjectAndInjectOnlyPropertiesWithAttributes()
      {
         IContainer container = CreateTarget();
         container.Register<IDemo>(new Demo(222));
         var injectionTarget = new PropertyInjection();
         container.BuildUp(injectionTarget);

         // Normal property injection
         injectionTarget.Attribute.Should().NotBeNull();
         injectionTarget.Attribute.GetId().Should().Be(222);

         injectionTarget.NoAttribute.Should().BeNull();
         injectionTarget.PrivateAttribute.Should().BeNull();

         // Again with other selection strategy
         container = new Container { Options = new ContainerOptions { PropertySelectionStrategy = PropertySelectionStrategies.AllWithDepencencyAttribute } };

         container.Register<IDemo>(new Demo(666));
         container.BuildUp(injectionTarget);

         // Normal property injection
         injectionTarget.Attribute.Should().NotBeNull();
         injectionTarget.Attribute.GetId().Should().Be(666);
         injectionTarget.NoAttribute.Should().BeNull();

         // Now also privates were injected
         injectionTarget.PrivateAttribute.Should().NotBeNull();
         injectionTarget.Attribute.GetId().Should().Be(666);
      }

      
      private static Container CreateTarget()
      {
         return new Container();
      }
   }
}