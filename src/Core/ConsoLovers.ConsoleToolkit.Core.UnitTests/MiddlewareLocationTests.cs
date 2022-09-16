// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MiddlewareLocationTests.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests;

using ConsoLovers.ConsoleToolkit.Core.Middleware;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class MiddlewareLocationTests
{
   [TestMethod]
   public void EnsureCorrectLocationOrder()
   {
      (MiddlewareLocation.AtTheStart < MiddlewareLocation.BeforeExceptionHandling).Should().BeTrue();

      ((int)MiddlewareLocation.BeforeExceptionHandling < KnownLocations.ExceptionHandlingMiddleware).Should().BeTrue();
      (KnownLocations.ExceptionHandlingMiddleware < (int)MiddlewareLocation.AfterExceptionHandling).Should().BeTrue();
      
      (MiddlewareLocation.BeforeExceptionHandling < MiddlewareLocation.AfterExceptionHandling).Should().BeTrue();
      (MiddlewareLocation.AfterExceptionHandling < MiddlewareLocation.BeforeParser).Should().BeTrue();
      (MiddlewareLocation.BeforeParser < MiddlewareLocation.AfterParser).Should().BeTrue();
      (MiddlewareLocation.AfterParser < MiddlewareLocation.BeforeMapper).Should().BeTrue();
      (MiddlewareLocation.BeforeMapper < MiddlewareLocation.AfterMapper).Should().BeTrue();
      (MiddlewareLocation.AfterMapper < MiddlewareLocation.BeforeExecution).Should().BeTrue();
      (MiddlewareLocation.BeforeExecution < MiddlewareLocation.AfterExecution).Should().BeTrue();
      (MiddlewareLocation.AfterExecution < MiddlewareLocation.AtTheEnd).Should().BeTrue();
   }

   [TestMethod]
   public void EnsureCorrectOrderOfKnownLocations()
   {
      (KnownLocations.ExceptionHandlingMiddleware < KnownLocations.ParserMiddleware).Should().BeTrue();
      (KnownLocations.ParserMiddleware < KnownLocations.MapperMiddleware).Should().BeTrue();
      (KnownLocations.MapperMiddleware < KnownLocations.ExecutionMiddleware).Should().BeTrue();
   }
}