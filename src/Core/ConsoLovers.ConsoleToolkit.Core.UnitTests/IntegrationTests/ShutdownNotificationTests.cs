// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShutdownNotificationTests.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.IntegrationTests;

using System;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

[TestClass]
public class ShutdownNotificationTests
{
   [TestMethod]
   public void EnsureShutdownHandlersAreCalledWhenExceptionIsThrown()
   {
      var handlerMock = new Mock<IShutdownHandler>();

      var application = ConsoleApplication.WithArguments<Args>()
         .AddShutdownHandler(handlerMock.Object)
         .RunTest(_ => throw new InvalidOperationException("1234"));

      handlerMock.Verify(x => x.NotifyShutdown(application.Result), Times.Once);
   }

   [TestMethod]
   public void EnsureExceptionInShutdownHandlersIsIgnored()
   {
      var handlerMock = new Mock<IShutdownHandler>();
      handlerMock.Setup(x => x.NotifyShutdown(It.IsAny<IExecutionResult>())).Throws<InvalidOperationException>();

      var application = ConsoleApplication.WithArguments<Args>()
         .AddShutdownHandler(handlerMock.Object)
         .RunTest(_ => { });

      handlerMock.Verify(x => x.NotifyShutdown(application.Result), Times.Once);
      application.Result.ExitCode.Should().Be(0);
   }

   [TestMethod]
   public void EnsureShutdownHandlersAreCalledForSuccessfulExecution()
   {
      var handlerMock = new Mock<IShutdownHandler>();

      var application = ConsoleApplication.WithArguments<Args>()
         .AddShutdownHandler(handlerMock.Object)
         .RunTest(_ => {  });

      handlerMock.Verify(x => x.NotifyShutdown(application.Result), Times.Once);
   }

   [TestMethod]
   public void EnsureAsyncShutdownHandlersAreCalledWhenExceptionIsThrown()
   {
      var handlerMock = new Mock<IAsyncShutdownHandler>();

      var application = ConsoleApplication.WithArguments<Args>()
         .AddShutdownHandler(handlerMock.Object)
         .RunTest(_ => throw new InvalidOperationException("1234"));

      handlerMock.Verify(x => x.NotifyShutdownAsync(application.Result), Times.Once);
   }

   [TestMethod]
   public void EnsureAsyncShutdownHandlersAreCalledForSuccessfulExecution()
   {
      var handlerMock = new Mock<IAsyncShutdownHandler>();

      var application = ConsoleApplication.WithArguments<Args>()
         .AddShutdownHandler(handlerMock.Object)
         .RunTest(_ => { });

      handlerMock.Verify(x => x.NotifyShutdownAsync(application.Result), Times.Once);
   }

   [TestMethod]
   public void EnsureExceptionInAsyncShutdownHandlersIsIgnored()
   {
      var handlerMock = new Mock<IAsyncShutdownHandler>();
      handlerMock.Setup(x => x.NotifyShutdownAsync(It.IsAny<IExecutionResult>())).Throws<InvalidOperationException>();

      var application = ConsoleApplication.WithArguments<Args>()
         .AddShutdownHandler(handlerMock.Object)
         .RunTest(_ => { });

      handlerMock.Verify(x => x.NotifyShutdownAsync(application.Result), Times.Once);
      application.Result.ExitCode.Should().Be(0);
   }

   internal class Args
   {
      [Argument("name")]
      public string Name { get; set; }
   }
}