// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExceptionHandlingTests.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.IntegrationTests;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using FluentAssertions;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

[TestClass]
[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
public class ExceptionHandlingTests
{
   [TestMethod]
   public void EnsureCustomHandlerIsInvoked()
   {
      var customHandler = new Mock<IExceptionHandler>();
      customHandler.Setup(x => x.Handle(It.IsAny<InvalidOperationException>())).Returns(true);
      
      var builder = ConsoleApplication.WithArguments<Args>()
         .AddService(x => x.AddSingleton(customHandler.Object));

      builder.Invoking(x => x.Run(args => throw new InvalidOperationException("Failed")))
         .Should().NotThrow();

      customHandler.Verify(x => x.Handle(It.IsAny<InvalidOperationException>()), Times.Once);
   }

   [TestMethod]
   public void EnsureUseExceptionHandlerWorksCorrectly()
   {
      var customHandler = new Mock<IExceptionHandler>();
      customHandler.Setup(x => x.Handle(It.IsAny<InvalidOperationException>())).Returns(true);

      var builder = ConsoleApplication.WithArguments<Args>()
         .UseExceptionHandler(customHandler.Object);

      builder.Invoking(x => x.Run(args => throw new InvalidOperationException("Failed")))
         .Should().NotThrow();

      customHandler.Verify(x => x.Handle(It.IsAny<InvalidOperationException>()), Times.Once);
   }

   [TestMethod]
   public void EnsureUseExceptionHandlerWithTypeWorksCorrectly()
   {
      ConsoleApplication.WithArguments<Args>()
         .UseExceptionHandler(typeof(CustomTestHandler))
         .RunTest(_ => throw new InvalidOperationException("Failed"), out var serviceProvider);

      var customHandler = (CustomTestHandler)serviceProvider.GetRequiredService<IExceptionHandler>();
      customHandler.Should().NotBeNull();
      
      customHandler.Mock.Verify(x => x.Handle(It.IsAny<InvalidOperationException>()), Times.Once);
   }

   public class Args
   {
   }

   public class CustomTestHandler : IExceptionHandler
   {
      public CustomTestHandler()
      {
         Mock = new Mock<IExceptionHandler>();
      }

      public Mock<IExceptionHandler> Mock { get; }

      public bool Handle(Exception exception)
      {
         Mock.Object.Handle(exception);
         return true;
      }
   }
}

