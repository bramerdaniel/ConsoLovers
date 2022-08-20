// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContainerTests.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.DIContainer
{
   #region

   using System.Collections.Generic;
   using System.Diagnostics.CodeAnalysis;
   using System.Linq;

   using ConsoLovers.ConsoleToolkit.Core.DIContainer;
   using ConsoLovers.ConsoleToolkit.Core.UnitTests.DIContainer.Testclasses;
   using ConsoLovers.ConsoleToolkit.Core.UnitTests.Setups;

   using FluentAssertions;

   using Microsoft.Extensions.DependencyInjection;
   using Microsoft.VisualStudio.TestTools.UnitTesting;

   #endregion

   /// <summary>Test-class for the container</summary>
   [TestClass]
   // ReSharper disable InconsistentNaming
   [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
   public class ContainerTests
   {
      #region Public Methods and Operators

      [TestMethod]
      public void CheckSimpleGenericHandlerRegistration()
      {
         var container = Setup.Container().Done();
         container.Register<IDemo>(_ => new Demo());
         var instance = container.Resolve(typeof(IDemo));
         instance.Should().BeOfType<Demo>();
      }


      [TestMethod]
      public void ResolveAnIEnumerableOfRegisteredTypes()
      {
         var container = Setup.Container().Done();
         container.Register<IDemo>(_ => new Demo(0));
         container.Register<IDemo>(_ => new Demo(1));
         container.Register<IDemo>(_ => new Demo(2));

         var enumerable = container.Resolve<IEnumerable<IDemo>>();
         var demoList = enumerable.ToList();
         for (int i = demoList.Count; i < 0; i--)
         {
            demoList[i].GetId().Should().Be(i);
         }
      }



      [TestMethod]
      public void Container_with_lifetime_none_resolves_always_new_instances()
      {
         var container = Setup.Container().Done();
         container.Register<IDemo>(_ => new Demo());

         var i1 = container.Resolve(typeof(IDemo));
         var i2 = container.Resolve(typeof(IDemo));
         i1.Should().NotBeSameAs(i2);
      }

      [TestMethod]
      public void ContainerWith_lifetime_Singleton_resolves_the_singleton_instances()
      {
         Container container = Setup.Container().Done();
         container.Register<IDemo>(_ => new Demo(), ServiceLifetime.Singleton);

         var firstInstance = container.Resolve(typeof(IDemo));
         var secondInstance = container.Resolve(typeof(IDemo));
         firstInstance.Should().BeSameAs(secondInstance);

         firstInstance = container.Resolve<IDemo>();
         secondInstance = container.Resolve<IDemo>();

         firstInstance.Should().BeSameAs(secondInstance);
      }


      [TestMethod]
      public void Empty_container_should_return_empty_enumerable()
      {
         var container = Setup.Container().Done();
         var theDemo = container.Resolve<IEnumerable<IDemo>>().ToArray();
         theDemo.Should().NotBeNull();
         theDemo.Any().Should().BeFalse();
      }


      [TestMethod]
      public void EnsureContainerCanResolveILists()
      {
         var container = Setup.Container().Done();
         container.Register<IDemo, Demo>();
         container.Register<IDemo, Demo>();
         var list = container.Resolve<IList<IDemo>>().ToArray();
         list.Should().NotBeNull();
         list.Should().HaveCount(2);
      }


      [TestMethod]
      public void Register_handler_for_interface()
      {
         var container = Setup.Container().Done();
         container.Register(typeof(IDemo), _ => new Demo(5));
         var instance = (IDemo)container.Resolve(typeof(IDemo));
         instance.Should().NotBeNull();
         instance.Should().BeOfType<Demo>();
         instance.GetId().Should().Be(5);

         container = new Container();
         container.Register<IDemo>(_ => new Demo(6));
         instance = container.Resolve<IDemo>();
         instance.Should().NotBeNull();
         instance.Should().BeOfType<Demo>();
         instance.GetId().Should().Be(6);
      }


      [TestMethod]
      public void Register_interface_with_type()
      {
         var container = Setup.Container().Done();
         container.Register(typeof(IDemo), typeof(Demo));
         var demoContract = container.Resolve<IDemo>();
         demoContract.Should().NotBeNull();

         container = new Container();
         container.Register<IDemo, Demo>();
         demoContract = container.Resolve<IDemo>();
         demoContract.Should().NotBeNull();
      }



      [TestMethod]
      public void Register_object_instance_for_interface()
      {
         var container = Setup.Container().Done();
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
      public void ResolveAllInstancesOfARegisteredService()
      {
         var container = Setup.Container().Done();
         container.Register(typeof(IDemo), new Demo(111));
         container.Register(typeof(IDemo), new Demo(222));
         container.Register(typeof(IDemo), new Demo(333));
         container.Register(typeof(IDemo), new Demo(444));

         var resolveAll = container.Resolve(typeof(IEnumerable<IDemo>)) as IList<IDemo>;
         resolveAll.Should().Contain(d => d.GetId() == 111).And.Contain(d => d.GetId() == 222).And.Contain(d => d.GetId() == 333).And.Contain(d => d.GetId() == 444);

         //container.Resolve<IDemo>().GetId().Should().Be(111);
      }
      
      #endregion

      // ReSharper restore InconsistentNaming
   }
}