// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContainerTests.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.UnitTests.DIContainer
{
   #region

   using System;
   using System.Collections.Generic;
   using System.Diagnostics.CodeAnalysis;
   using System.Linq;

   using ConsoLovers.ConsoleToolkit.DIContainer;
   using ConsoLovers.ConsoleToolkit.DIContainer.Strategies;
   using ConsoLovers.UnitTests.DIContainer.Testclasses;

   using FluentAssertions;

   using Microsoft.VisualStudio.TestTools.UnitTesting;

   using Moq;

   #endregion

   /// <summary>Test-class for the container</summary>
   [TestClass]
   // ReSharper disable InconsistentNaming
   [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
   public class ContainerTests
   {
      #region Public Methods and Operators




      [TestMethod]
      public void Check_simple_generic_handler_registration()
      {
         Container container = new Container();
         container.Register<IDemo>(c => new Demo());
         var instance = container.Resolve(typeof(IDemo));
         instance.Should().BeOfType<Demo>();
      }

      [TestMethod]
      public void Resolve_func_that_gets_the_registered_interface()
      {
         Container container = new Container();
         container.Register<IDemo>(c => new Demo(77));
         var factoryMethod = container.Resolve<Func<IDemo>>();
         factoryMethod.Should().NotBeNull();
         var demo = factoryMethod();
         demo.Should().NotBeNull();
         demo.GetId().Should().Be(77);
      }

      [TestMethod]
      public void ResolveAnIEnumerableOfRegisteredTypes()
      {
         Container container = new Container();
         container.Register<IDemo>(c => new Demo(0));
         container.RegisterNamed<IDemo>(c => new Demo(1), "1");
         container.RegisterNamed<IDemo>(c => new Demo(2), "2");

         var demoList = container.Resolve<IEnumerable<IDemo>>().ToList();
         for (int i = demoList.Count; i < 0; i--)
         {
            demoList[i].GetId().Should().Be(i);
         }
      }

      [TestMethod]
      public void Check_simple_generic_instance_registration_with_key()
      {
         Container container = new Container();

         var demo1 = new Demo { Name = "Demo1" };
         var demo2 = new Demo { Name = "Demo2" };

         container.RegisterNamed<IDemo>(demo1, "d1");
         container.RegisterNamed<IDemo>(demo2, "d2");
         var resolved1 = container.ResolveNamed<IDemo>("d1");
         resolved1.Name.Should().Be("Demo1");
         var resolved2 = container.ResolveNamed<IDemo>("d2");
         resolved2.Name.Should().Be("Demo2");
      }

      [TestMethod]
      public void Container_with_lifetime_none_resolves_allways_new_instances()
      {
         Container container = new Container();
         container.Register<IDemo>(c => new Demo()).WithLifetime(Lifetime.None);

         var i1 = container.Resolve(typeof(IDemo));
         var i2 = container.Resolve(typeof(IDemo));
         i1.Should().NotBeSameAs(i2);
      }

      [TestMethod]
      public void Container_with_lifetime_sinleton_resolves_the_singleton_instances()
      {
         Container container = new Container();
         container.Register<IDemo>(c => new Demo()).WithLifetime(Lifetime.Singleton);

         var firstInstance = container.Resolve(typeof(IDemo));
         var secondInstance = container.Resolve(typeof(IDemo));
         firstInstance.Should().BeSameAs(secondInstance);

         firstInstance = container.Resolve<IDemo>();
         secondInstance = container.Resolve<IDemo>();

         firstInstance.Should().BeSameAs(secondInstance);
      }

      [TestMethod]
      public void Create_container_with_serviceProvider()
      {
         IServiceProvider provider = new TestServiceProvider();
         IContainer container = new Container(provider);

         var theDemo = container.Resolve<IDemo>();
         theDemo.Should().NotBeNull();
         theDemo.GetId().Should().Be(666);
         theDemo.Name.Should().Be("FromServiceProvider");

         theDemo = container.ResolveAll<IDemo>().Single();
         theDemo.Should().NotBeNull();
         theDemo.GetId().Should().Be(666);
         theDemo.Name.Should().Be("FromServiceProvider");

         var demo = container.Resolve<Demo>();
         demo.Should().NotBeNull();
         demo.GetId().Should().Be(666);
         demo.Name.Should().Be("FromServiceProvider");

         var toBuild = new ObjectToBuild();
         container.BuildUp(toBuild);
         toBuild.Demo.Should().NotBeNull();
         theDemo.Should().BeSameAs(theDemo);

         toBuild.Container.Should().BeSameAs(container);
         toBuild.ServiceProvider.Should().BeSameAs(provider);

         toBuild.PrivateDemo.Should().BeNull();
         toBuild.ProtectedDemo.Should().BeNull();

         container.BuildUp(toBuild, new AttributePropertySelectionStrategy(true));
         toBuild.NoSetterDemo.Should().BeNull();
         toBuild.PrivateDemo.Should().NotBeNull();
         toBuild.ProtectedDemo.Should().NotBeNull();

         var named = container.ResolveNamed<IConvertible>("oha");
         named.Should().BeNull();
      }

      [TestMethod]
      public void Empty_container_should_return_empty_enumerable()
      {
         IContainer container = new Container();
         var theDemo = container.ResolveAll<IDemo>().ToArray();
         theDemo.Should().NotBeNull();
         theDemo.Any().Should().BeFalse();

         IServiceProvider provider = new TestServiceProvider();
         container = new Container(provider);
         var notResolvable = container.ResolveAll<IConvertible>().ToArray();
         notResolvable.Should().NotBeNull();
         notResolvable.Any().Should().BeFalse();
      }


      [TestMethod]
      public void Register_handler_for_interface()
      {
         Container container = new Container();
         container.Register(typeof(IDemo), c => new Demo(5));
         var instance = (IDemo)container.Resolve(typeof(IDemo));
         instance.Should().NotBeNull();
         instance.Should().BeOfType<Demo>();
         instance.GetId().Should().Be(5);

         container = new Container();
         container.Register<IDemo>(c => new Demo(6));
         instance = container.Resolve<IDemo>();
         instance.Should().NotBeNull();
         instance.Should().BeOfType<Demo>();
         instance.GetId().Should().Be(6);
      }

      [TestMethod]
      public void Register_handler_with_key_for_interface()
      {
         Container container = new Container();
         container.RegisterNamed(typeof(IDemo), c => new Demo(12), "key");
         var instance = (IDemo)container.Resolve(typeof(IDemo));
         instance.Should().BeNull();

         instance = (IDemo)container.ResolveNamed(typeof(IDemo), "key");
         instance.Should().NotBeNull();
         instance.Should().BeOfType<Demo>();
         instance.GetId().Should().Be(12);

         // Generic
         container = new Container();
         container.RegisterNamed<IDemo>(c => new Demo(98), "special");
         instance = container.Resolve<IDemo>();
         instance.Should().BeNull();

         instance = container.ResolveNamed<IDemo>("special");
         instance.Should().NotBeNull();
         instance.Should().BeOfType<Demo>();
         instance.GetId().Should().Be(98);
      }

      [TestMethod]
      public void Register_interface_with_type()
      {
         Container container = new Container();
         container.Register(typeof(IDemo), typeof(Demo));
         var demoContract = container.Resolve<IDemo>();
         demoContract.Should().NotBeNull();

         container = new Container();
         container.Register<IDemo, Demo>();
         demoContract = container.Resolve<IDemo>();
         demoContract.Should().NotBeNull();
      }

      [TestMethod]
      public void Register_interface_with_type_named()
      {
         IContainer container = new Container();
         container.RegisterNamed(typeof(IDemo), typeof(Demo), "key");
         var demoContract = (IDemo)container.Resolve(typeof(IDemo));
         demoContract.Should().BeNull();

         demoContract = (IDemo)container.ResolveNamed(typeof(IDemo), "key");
         demoContract.Should().NotBeNull();

         container = new Container();
         container.RegisterNamed<IDemo, Demo>("test");
         demoContract = container.Resolve<IDemo>();
         demoContract.Should().BeNull();
         demoContract = container.ResolveNamed<IDemo>("test");
         demoContract.Should().NotBeNull();
      }

      [TestMethod]
      public void Register_object_instance_for_interface()
      {
         Container container = new Container();
         var implementation = new Demo { Name = "Demo" };
         container.Register(typeof(IDemo), implementation);
         var instance = (IDemo)container.Resolve(typeof(IDemo));
         instance.Should().NotBeNull();
         instance.Name.Should().Be("Demo");

         container = new Container();
         container.Register<IDemo>(implementation);
         instance = container.Resolve<IDemo>();
         instance.Should().NotBeNull();
         instance.Name.Should().Be("Demo");
      }

      [TestMethod]
      public void RegisterinAnSecondImplementationAsSingletonMustWork()
      {
         Container container = new Container();

         container.Register<IDemo>(c => new Demo{ Name = "WithoutName" }).WithLifetime(Lifetime.Singleton);
         container.RegisterNamed<IDemo>(c => new Demo { Name = "WithName" }, "Second").WithLifetime(Lifetime.Singleton);

         container.Resolve<IDemo>().Name.Should().Be("WithoutName");
         container.ResolveNamed<IDemo>("Second").Name.Should().Be("WithName");
      }

      [TestMethod]
      public void Registering_two_singletons_one_with_a_name()
      {
         Container container = new Container();
         container.Register<IDemo>(c => new Demo()).WithLifetime(Lifetime.Singleton);

         container.RegisterNamed<IDemo>(c => new Demo(), "other").WithLifetime(Lifetime.Singleton);

         var demo1 = container.Resolve<IDemo>();
         var demo2 = container.Resolve<IDemo>();
         demo1.Should().BeSameAs(demo2);

         var demo3 = container.ResolveNamed<IDemo>("other");
         var demo4 = container.ResolveNamed<IDemo>("other");
         demo3.Should().BeSameAs(demo4);

         demo1.Should().NotBeSameAs(demo3);
      }

      [TestMethod]
      public void Registration_of_handlers_with_key_only()
      {
         Container container = new Container();
         container.RegisterNamed(null, c => new Demo(), "KeyOnly");
         container.RegisterNamed(null, c => new Simple(), "otherKey");

         var demo1 = container.ResolveNamed<Demo>("KeyOnly");
         var demo2 = container.ResolveNamed(typeof(Demo), "KeyOnly") as Demo;

         var demo3 = container.ResolveNamed(null, "KeyOnly") as Demo;
         var simple = container.ResolveNamed(null, "otherKey") as Simple;

         demo1.Should().BeNull();
         demo2.Should().BeNull();
         demo3.Should().NotBeNull();
         simple.Should().NotBeNull();

         demo3.Should().BeOfType<Demo>();
         simple.Should().BeOfType<Simple>();
      }

      [TestMethod]
      public void ResolveAllInstancesOfARegisteredService()
      {
         Container container = new Container();
         container.Register(typeof(IDemo), new Demo(111));
         container.RegisterNamed(typeof(IDemo), new Demo(222), "222");
         container.RegisterNamed(typeof(IDemo), new Demo(333), "333");
         container.RegisterNamed(typeof(IDemo), new Demo(444), "444");

         var resolveAll = container.ResolveAll(typeof(IDemo)).OfType<IDemo>().ToList();
         resolveAll.Should().Contain(d => d.GetId() == 111).And.Contain(d => d.GetId() == 222).And.Contain(d => d.GetId() == 333).And.Contain(d => d.GetId() == 444);

         container.Resolve<IDemo>().GetId().Should().Be(111);
      }

      [TestMethod]
      public void EnsureNamedAndUnnamedRegistrationWorks()
      {
         Container container = new Container();
         var demo1 = new Demo{ Name = "default" };
         var demo2 = new Demo{ Name = "DemoWithName" };

         container.Register<IDemo>(demo1);
         container.RegisterNamed<IDemo>(demo2, "demo2");

         var resolved1 = container.Resolve<IDemo>();
         resolved1.Name.Should().Be("default");

         var resolved2 = container.ResolveNamed<IDemo>("demo2");
         resolved2.Name.Should().Be("DemoWithName");
      }

      [TestMethod]
      public void EnsureNamedAndUnnamedRegistrationWorksWhenNamedIsRegisteredFirst()
      {
         Container container = new Container();
         var demo1 = new Demo { Name = "default" };
         var demo2 = new Demo { Name = "DemoWithName" };

         container.RegisterNamed<IDemo>(demo2, "demo2");
         container.Register<IDemo>(demo1);

         var resolved1 = container.Resolve<IDemo>();
         resolved1.Name.Should().Be("default");

         var resolved2 = container.ResolveNamed<IDemo>("demo2");
         resolved2.Name.Should().Be("DemoWithName");
      }

      [TestMethod]
      public void EnsureSecondRegistrationWithTheSameNameFails()
      {
         Container container = new Container();
         var demo1 = new Demo { Name = "first" };
         var demo2 = new Demo { Name = "second" };

         container.RegisterNamed<IDemo>(demo1, "same");
         container.Invoking(c => c.RegisterNamed<IDemo>(demo2, "same")).ShouldThrow<InvalidOperationException>();
      }

      [TestMethod]
      public void EnsureSecondRegistrationWithoutNameFails()
      {
         Container container = new Container();

         container.Register<IDemo>(new Demo { Name = "first" });
         container.Invoking(c => c.Register<IDemo>(new Demo { Name = "second" })).ShouldThrow<InvalidOperationException>();
      }

      /// <summary>Tests, if a unregistered component of the <see cref="Container"/> will return <c>null</c> if resolved.</summary>
      [TestMethod]
      public void UnregisterComponent()
      {
         var container = new Container();
         container.Register<IDemo, Demo>();

         container.Resolve<IDemo>().Should().NotBeNull("resolve of registered component should succeed");

         container.Unregister<IDemo>();

         container.Resolve<IDemo>().Should().BeNull("unregistered component should not resolve");
      }
      
      /// <summary>Tests, if a unregistered component of the <see cref="Container"/> will return <c>null</c> if resolved.</summary>
      [TestMethod]
      public void RegisteringMultipleInstancesWithMixedSingletonAndPerInstance()
      {
         var container = new Container();
         container.Register<IDemo, Demo>();
         container.RegisterNamed<IDemo, Demo>("Singleton").WithLifetime(Lifetime.Singleton);

         var first = container.Resolve<IDemo>();
         var second = container.Resolve<IDemo>();
         first.Should().NotBeSameAs(second);

         first = container.ResolveNamed<IDemo>("Singleton");
         second = container.ResolveNamed<IDemo>("Singleton");
         first.Should().BeSameAs(second);
      }

      #endregion

      // ReSharper restore InconsistentNaming
   }
}