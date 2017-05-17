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
      #region Constants and Fields

      private readonly MockRepository mock = new MockRepository(MockBehavior.Loose);

      #endregion

      #region Public Methods and Operators

      [TestMethod]
      public void BuildUp_existing_object_instance()
      {
         IContainer container = new Container();
         container.Register<IDemo>(new Demo(222));
         ObjectToBuild otb = new ObjectToBuild();
         container.BuildUp(otb);

         // Normal property injection
         otb.Demo.Should().NotBeNull();
         otb.Demo.GetId().Should().Be(222);

         // Property injection for container insself
         otb.Container.Should().NotBeNull();
         var demo = otb.Container.Resolve<IDemo>();
         demo.Should().NotBeNull();
         demo.GetId().Should().Be(222);
      }

      [TestMethod]
      public void BuildUp_null_must_throw_ArgumentNullException()
      {
         IContainer container = new Container();
         container.Invoking(c => c.BuildUp(null)).ShouldThrow<ArgumentNullException>();
      }

      [TestMethod]
      public void BuildUp_existing_object_and_Inject_only_properties_with_attributes()
      {
         IContainer container = new Container();
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
      public void Resolve_an_IEnumerabl_of_registered_types()
      {
         Container container = new Container();
         container.Register<IDemo>(c => new Demo(0));
         container.Register<IDemo>(c => new Demo(1));
         container.Register<IDemo>(c => new Demo(2));
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
         var demo1 = mock.Create<IDemo>();
         demo1.Setup(d => d.Name).Returns("Demo1");
         var demo2 = mock.Create<IDemo>();
         demo2.Setup(d => d.Name).Returns("Demo2");

         container.Register<IDemo>(demo1.Object).Named("d1");
         container.Register<IDemo>(demo2.Object).Named("d2");
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

         var i1 = container.Resolve(typeof(IDemo));
         var i2 = container.Resolve(typeof(IDemo));
         i1.Should().BeSameAs(i2);

         i1 = container.Resolve<IDemo>();
         i2 = container.Resolve<IDemo>();
         i1.Should().BeSameAs(i2);
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
         var theDemo = container.ResolveAll<IDemo>();
         theDemo.Should().NotBeNull();
         theDemo.Any().Should().BeFalse();

         IServiceProvider provider = new TestServiceProvider();
         container = new Container(provider);
         var notResolvable = container.ResolveAll<IConvertible>();
         notResolvable.Should().NotBeNull();
         notResolvable.Any().Should().BeFalse();
      }

      [TestMethod]
      public void Create_simple_object()
      {
         Container container = new Container();

         // Normal method
         var timespan = container.Create(typeof(Simple));
         timespan.Should().NotBeNull();

         // Generic implementation
         timespan = container.Create<Simple>();
         timespan.Should().NotBeNull();

         // Normal method
         var demo = (Demo)container.Create(typeof(Demo));
         demo.Should().NotBeNull();

         // Generic implementation
         demo = container.Create<Demo>();
         demo.Should().NotBeNull();
         demo.GetId().Should().Be(0);
      }

      [TestMethod]
      public void Create_simple_object_with_dependencies()
      {
         Container container = new Container();
         var dependancies = container.Create<HaveDependancies>();
         dependancies.Should().NotBeNull();
         dependancies.Demo.Should().BeNull();

         container.Register<IDemo, Demo>();
         dependancies = container.Create<HaveDependancies>();
         dependancies.Should().NotBeNull();
         dependancies.Demo.Should().NotBeNull();
      }

      [TestMethod]
      public void Create_simple_object_with_options()
      {
         Container container = new Container();

         // Normal method
         var simple = container.Create(typeof(Simple), new ContainerOptions { ConstructorSelectionStrategy = ConstructorSelectionStrategies.WithInjectionConstructorAttribute });
         simple.Should().BeNull();

         var noAtt = container.Create(
            typeof(NoAttributes),
            new ContainerOptions { ConstructorSelectionStrategy = ConstructorSelectionStrategies.WithInjectionConstructorAttribute });
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
         container.Register(typeof(IDemo), c => new Demo(12)).Named("key");
         var instance = (IDemo)container.Resolve(typeof(IDemo));
         instance.Should().BeNull();

         instance = (IDemo)container.ResolveNamed(typeof(IDemo), "key");
         instance.Should().NotBeNull();
         instance.Should().BeOfType<Demo>();
         instance.GetId().Should().Be(12);

         // Generic
         container = new Container();
         container.Register<IDemo>(c => new Demo(98)).Named("special");
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
         container.Register(typeof(IDemo), typeof(Demo)).Named("key");
         var demoContract = (IDemo)container.Resolve(typeof(IDemo));
         demoContract.Should().BeNull();

         demoContract = (IDemo)container.ResolveNamed(typeof(IDemo), "key");
         demoContract.Should().NotBeNull();

         container = new Container();
         container.Register<IDemo, Demo>().Named("test");
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
      public void Registering_an_singleton_twice_must_fail()
      {
         try
         {
            Container container = new Container();
            container.Register<IDemo>(c => new Demo()).WithLifetime(Lifetime.Singleton);

            container.Register<IDemo>(c => new Demo()).WithLifetime(Lifetime.Singleton);

            Assert.Fail("Registration did not cause an error");
         }
         catch (RegistrationException)
         {
         }
      }

      [TestMethod]
      public void Registering_two_singletons_one_with_a_name()
      {
         Container container = new Container();
         container.Register<IDemo>(c => new Demo()).WithLifetime(Lifetime.Singleton);

         container.Register<IDemo>(c => new Demo()).Named("other").WithLifetime(Lifetime.Singleton);

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
         container.Register(null, c => new Demo()).Named("KeyOnly");
         container.Register(null, c => new Simple()).Named("otherKey");

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
      public void ResolveAll_instances_of_a_registered_interface()
      {
         IContainer container = new Container();
         container.Register(typeof(IDemo), new Demo(222));
         container.Register(typeof(IDemo), new Demo(333));
         container.Register(typeof(IDemo), new Demo(444));

         var resolveAll = container.ResolveAll(typeof(IDemo)).OfType<IDemo>().ToList();
         resolveAll.Should().Contain(d => d.GetId() == 222).And.Contain(d => d.GetId() == 333).And.Contain(d => d.GetId() == 444);
         container.Resolve<IDemo>().GetId().Should().Be(222);

         container = new Container();
         container.Register<IDemo>(new Demo(555));
         container.Register<IDemo>(new Demo(666));
         container.Register<IDemo>(new Demo(777));

         resolveAll = container.ResolveAll<IDemo>().ToList();
         resolveAll.Should().Contain(d => d.GetId() == 555).And.Contain(d => d.GetId() == 666).And.Contain(d => d.GetId() == 777);

         container.Resolve<IDemo>().GetId().Should().Be(555);
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

      #endregion

      // ReSharper restore InconsistentNaming
   }
}