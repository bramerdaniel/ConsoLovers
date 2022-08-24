// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SingletonTests.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.DIContainer;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

using ConsoLovers.ConsoleToolkit.Core.UnitTests.Setups;

using FluentAssertions;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

[TestClass]
[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
public class SingletonTests
{
   #region Public Methods and Operators

   [TestMethod]
   public void CreateFactoryRegistrationWorksCorrectly()
   {
      var container = Setup.Container()
         .Register(ServiceDescriptor.Singleton<IExceptionHandler>(s => new CustomHandler()))
         .Done();

      var first = container.GetService<IExceptionHandler>();
      var second = container.GetService<IExceptionHandler>();

      first.Should().BeSameAs(second);
   }

   [TestMethod]
   public void CreateGenericInstanceRegistrationWorksCorrectly()
   {
      var container = Setup.Container()
         .Register(ServiceDescriptor.Singleton<IExceptionHandler>(new CustomHandler()))
         .Done();

      var first = container.GetService<IExceptionHandler>();
      var second = container.GetService<IExceptionHandler>();

      first.Should().BeSameAs(second);
   }

   [TestMethod]
   public void CreateGenericRegistrationWorksCorrectly()
   {
      var container = Setup.Container()
         .Register(ServiceDescriptor.Singleton<IExceptionHandler, CustomHandler>())
         .Done();

      var first = container.GetService<IExceptionHandler>();
      var second = container.GetService<IExceptionHandler>();

      first.Should().BeSameAs(second);
   }

   [TestMethod]
   public void CreateMultipleSingletonsShouldWork()
   {
      var container = Setup.Container()
         .Register(ServiceDescriptor.Singleton(typeof(IExceptionHandler), typeof(CustomHandler)))
         .Register(ServiceDescriptor.Singleton(typeof(IExceptionHandler), new Mock<IExceptionHandler>().Object))
         .Done();

      var first = container.GetService<IExceptionHandler>();
      first.Should().BeOfType<CustomHandler>();

      var all = container.GetService<IEnumerable<IExceptionHandler>>()?.ToArray();
      Assert.IsNotNull(all);
      all.Should().HaveCount(2);
      all[0].Should().BeOfType<CustomHandler>();
   }

   [TestMethod]
   public void CreateTypeInstanceRegistrationWorksCorrectly()
   {
      var container = Setup.Container()
         .Register(ServiceDescriptor.Singleton(typeof(IExceptionHandler), new CustomHandler()))
         .Done();

      var first = container.GetService<IExceptionHandler>();
      var second = container.GetService<IExceptionHandler>();

      first.Should().BeSameAs(second);
   }

   [TestMethod]
   public void CreateTypeRegistrationWorksCorrectly()
   {
      var container = Setup.Container()
         .Register(ServiceDescriptor.Singleton(typeof(IExceptionHandler), typeof(CustomHandler)))
         .Done();

      var first = container.GetService<IExceptionHandler>();
      var second = container.GetService<IExceptionHandler>();

      first.Should().BeSameAs(second);
   }

   #endregion

   class CustomHandler : IExceptionHandler
   {
      #region IExceptionHandler Members

      public bool Handle(Exception exception)
      {
         return false;
      }

      #endregion
   }
}