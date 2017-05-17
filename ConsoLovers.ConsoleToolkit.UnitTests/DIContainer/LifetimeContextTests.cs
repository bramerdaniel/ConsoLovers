// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LifetimeContextTests.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.UnitTests.DIContainer
{
   using System;

   using ConsoLovers.ConsoleToolkit.DIContainer;
   using ConsoLovers.UnitTests.DIContainer.Testclasses;

   using FluentAssertions;

   using Microsoft.VisualStudio.TestTools.UnitTesting;

   /// <summary>Tests for <see cref="LifetimeContext"/></summary>
   [TestClass]
   public class LifetimeContextTests
   {
      #region Public Methods and Operators

      /// <summary>Tests, if multiple registering of the same type in the <see cref="LifetimeContext"/> returns in throwing a <see cref="RegistrationException"/>.</summary>
      [TestMethod]
      public void DoubleRegisteringInContext()
      {
         var container = new Container();
         using (var lifetimeContext = new LifetimeContext(container))
         {
            var registerAction = new Action(() => lifetimeContext.Register<IDemo, Demo>());
            registerAction.ShouldNotThrow<RegistrationException>("first registering should succeed");
            registerAction.ShouldThrow<RegistrationException>("second registering should fail with an exception");
         }
      }

      /// <summary>Tests, if a component that was already registered in the <see cref="Container"/> and is again registered in <see cref="LifetimeContext"/>
      ///    leads to an <see cref="RegistrationException"/>.</summary>
      [TestMethod]
      public void DoubleRegisteringOfContainerComponent()
      {
         var container = new Container();
         container.Register<IDemo, Demo>();
         using (var lifetimeContext = new LifetimeContext(container))
         {
            var registerAction = new Action(() => lifetimeContext.Register<IDemo, Demo>());
            registerAction.ShouldThrow<RegistrationException>("registration of a component that was already registered in the container should fail with an exception");
         }
      }

      /// <summary>Tests, if a multiple resolve in the <see cref="LifetimeContext"/> results in returning the same instances.</summary>
      [TestMethod]
      public void MultipleResolveInContext()
      {
         var container = new Container();
         using (var lifetimeContext = new LifetimeContext(container))
         {
            lifetimeContext.Register<IDemo, Demo>();
            var demo1 = container.Resolve<IDemo>();
            var demo2 = container.Resolve<IDemo>();

            demo1.Should().Be(demo2, "objects should have the same instance");
         }
      }

      /// <summary>Tests, if a resolve in different <see cref="LifetimeContext"/>s results in returning different instances.</summary>
      [TestMethod]
      public void ResolveInDifferentContexts()
      {
         var container = new Container();
         IDemo demo1, demo2;
         using (var lifetimeContext = new LifetimeContext(container))
         {
            lifetimeContext.Register<IDemo, Demo>();
            demo1 = container.Resolve<IDemo>();
         }

         using (var lifetimeContext = new LifetimeContext(container))
         {
            lifetimeContext.Register<IDemo, Demo>();
            demo2 = container.Resolve<Demo>();
         }

         demo1.Should().NotBe(demo2, "objects should have different instances.");
      }

      /// <summary>Tests, if a resolve outside of the <see cref="LifetimeContext"/> results in returning <c>null</c>.</summary>
      [TestMethod]
      public void ResolvingOutsideOfContext()
      {
         var container = new Container();
         using (var lifetimeContext = new LifetimeContext(container))
         {
            lifetimeContext.Register<IDemo, Demo>();
            var demo1 = container.Resolve<IDemo>();

            demo1.Should().NotBeNull("in the context the resolve should return an instance");
         }

         var demo2 = container.Resolve<IDemo>();
         demo2.Should().BeNull("outside the context the resolve should return null");
      }

      /// <summary>Tests, if a resolving of components with dependencies return the same instance as dependency as the resolved component.</summary>
      [TestMethod]
      public void ResolvingWithDependencies()
      {
         var container = new Container();
         using (var lifetimeContext = new LifetimeContext(container))
         {
            lifetimeContext.Register<IDemo, Demo>();
            lifetimeContext.Register<IHaveDependencies, HaveDependancies>();

            var demo = container.Resolve<IDemo>();
            var dependency = container.Resolve<IHaveDependencies>() as HaveDependancies;

            dependency.Demo.Should().Be(demo, "dependent instance should be the same as the resolved one");
         }
      }

      #endregion
   }
}