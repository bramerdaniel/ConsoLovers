// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExitCodeHandlingTests.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.IntegrationTests;

using System;
using System.Diagnostics.CodeAnalysis;

using ConsoLovers.ConsoleToolkit.Core.Services;

using FluentAssertions;

using JetBrains.Annotations;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

[TestClass]
[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
public class ExitCodeHandlingTests
{
   #region Public Methods and Operators

   [TestMethod]
   public void EnsureCustomHandlerIsInvokedCorrectly()
   {
      var customHandlerMock = new Mock<IExitCodeHandler>();
      customHandlerMock.Setup(x => x.HandleError(It.IsAny<IExecutionResult>(), It.IsAny<Exception>()))
         .Callback<IExecutionResult, Exception>((r, e) => r.ExitCode = int.Parse(e.Message));

      var application = ConsoleApplication.WithArguments<Args>()
         .UseExitCodeHandler(customHandlerMock.Object)
         .RunTest(_ => throw new InvalidOperationException("1234"), "number=1", out _);

      application.Result.ExitCode.Should().Be(1234);
   }

   [TestMethod]
   public void EnsureDefaultExitCodeIsMinusOneForArgumentErrors()
   {
      var application = ConsoleApplication.WithArguments<Args>()
         .Run("Number=NoInteger");

      application.Result.ExitCode.Should().Be(-6);
   }

   [TestMethod]
   public void EnsureDefaultExitCodeIsMinusOneForMissingRequiredArguments()
   {
      var application = ConsoleApplication.WithArguments<Args>()
         .Run("");

      application.Result.ExitCode.Should().Be(-1);
   }

   [TestMethod]
   public void EnsureCorrectExitCodeForInvalidValidatorType()
   {
      var application = ConsoleApplication.WithArguments<Args>()
         .Run("Name=Value Number=1");

      application.Result.ExitCode.Should().Be(-5);
   }

   [TestMethod]
   public void EnsureDefaultExitCodeIsOneForOtherErrors()
   {
      var application = ConsoleApplication.WithArguments<Args>()
         .RunTest(_ => throw new InvalidOperationException("1234"), "number=1", out var serviceProvider);

      var exitCodeHandler = serviceProvider.GetService<IExitCodeHandler>();
      exitCodeHandler.Should().BeOfType<DefaultExitCodeHandler>();

      application.Result.ExitCode.Should().Be(1);
   }

   [TestMethod]
   public void EnsureDefaultExitCodeIsZeroForSuccess()
   {
      var application = ConsoleApplication.WithArguments<Args>()
         .Run("Number=1");

      application.Result.ExitCode.Should().Be(0);
   }

   [TestMethod]
   public void EnsureExitCodeHandlerIsCalledForExceptions()
   {
      var application = ConsoleApplication.WithArguments<Args>()
         .UseExitCodeHandler(typeof(CustomExitCodeHandler))
         .RunTest(_ => throw new InvalidOperationException("Failed"), "number=1", out var serviceProvider);

      var customHandler = (CustomExitCodeHandler)serviceProvider.GetRequiredService<IExitCodeHandler>();
      customHandler.Should().NotBeNull();

      customHandler.Mock.Verify(x => x.HandleError(application.Result, It.IsAny<InvalidOperationException>()), Times.Once);
   }

   [TestMethod]
   public void EnsureExitCodeHandlerIsCalledWithoutException()
   {
      var application = ConsoleApplication.WithArguments<Args>()
         .UseExitCodeHandler(typeof(CustomExitCodeHandler))
         .RunTest(_ => { }, "number=1", out var serviceProvider);

      var customHandler = (CustomExitCodeHandler)serviceProvider.GetRequiredService<IExitCodeHandler>();
      customHandler.Should().NotBeNull();

      customHandler.Mock.Verify(x => x.HandleSuccess(application.Result), Times.Once);
   }

   #endregion

   internal class Args
   {
      [Argument("number", Required = true)]
      [UsedImplicitly]
      public int Number { get; set; }

      [Argument("name")]
      [ArgumentValidator(typeof(Args))]
      public string Name { get; set; }
   }

   internal class CustomExitCodeHandler : IExitCodeHandler
   {
      #region Constructors and Destructors

      public CustomExitCodeHandler()
      {
         Mock = new Mock<IExitCodeHandler>();
      }

      #endregion

      #region IExitCodeHandler Members

      public void HandleError(IExecutionResult result, Exception exception)
      {
         Mock.Object.HandleError(result, exception);
      }

      public void HandleSuccess(IExecutionResult result)
      {
         Mock.Object.HandleSuccess(result);
      }

      #endregion

      #region Public Properties

      public Mock<IExitCodeHandler> Mock { get; }

      #endregion
   }
}