// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SingletonTests.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.DIContainer;

using System;
using System.Diagnostics.CodeAnalysis;

using ConsoLovers.ConsoleToolkit.Core.UnitTests.Setups;

using FluentAssertions;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
public class SingletonTests
{
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
         .Register(ServiceDescriptor.Singleton(typeof(IExceptionHandler),typeof(CustomHandler)))
         .Done();

      var first = container.GetService<IExceptionHandler>();
      var second = container.GetService<IExceptionHandler>();

      first.Should().BeSameAs(second);
   }


   class CustomHandler : IExceptionHandler
   {
      public bool Handle(Exception exception)
      {
         return false;
      }
   }
}