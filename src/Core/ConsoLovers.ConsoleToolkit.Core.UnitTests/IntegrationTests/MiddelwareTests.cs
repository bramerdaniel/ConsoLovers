// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MiddelwareTests.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.IntegrationTests;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using ConsoLovers.ConsoleToolkit.Core.Middleware;

using FluentAssertions;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

[TestClass]
public class MiddlewareTests
{
   [TestMethod]
   public void EnsureExecutionMiddlewareCanBeRemoved()
   {
      var serviceProvider = ConsoleApplication.WithArguments<Args>()
         .RemoveMiddleware(typeof(ExecutionMiddleware<Args>))
         .CreateServiceProvider();

      var middleware = serviceProvider.GetRequiredService<IEnumerable<IMiddleware<Args>>>().ToArray();
      middleware.Should().HaveCount(3);
   }

   [TestMethod]
   public void EnsureMapperMiddlewareCanBeRemoves()
   {
      var serviceProvider = ConsoleApplication.WithArguments<Args>()
         .RemoveMiddleware(typeof(MapperMiddleware<Args>))
         .CreateServiceProvider();

      var middleware = serviceProvider.GetRequiredService<IEnumerable<IMiddleware<Args>>>().ToArray();
      middleware.Should().HaveCount(3);
   }

   [TestMethod]
   public void EnsureParserMiddlewareCanBeRemoves()
   {
      var serviceProvider = ConsoleApplication.WithArguments<Args>()
         .RemoveMiddleware(typeof(ParserMiddleware<Args>))
         .CreateServiceProvider();

      var middleware = serviceProvider.GetRequiredService<IEnumerable<IMiddleware<Args>>>().ToArray();
      middleware.Should().HaveCount(3);
   }

   [TestMethod]
   public void EnsureExceptionHandlingMiddlewareCanBeRemoves()
   {
      var serviceProvider = ConsoleApplication.WithArguments<Args>()
         .RemoveMiddleware(typeof(ExceptionHandlingMiddleware<Args>))
         .CreateServiceProvider();

      var middleware = serviceProvider.GetRequiredService<IEnumerable<IMiddleware<Args>>>().ToArray();
      middleware.Should().HaveCount(3);
   }

   [TestMethod]
   public void EnsureAllMiddlewareCanBeRemoves()
   {
      var serviceProvider = ConsoleApplication.WithArguments<Args>()
         .RemoveMiddleware(typeof(ExecutionMiddleware<Args>))
         .RemoveMiddleware(typeof(MapperMiddleware<Args>))
         .RemoveMiddleware(typeof(ParserMiddleware<Args>))
         .RemoveMiddleware(typeof(ExceptionHandlingMiddleware<Args>))
         .CreateServiceProvider();

      var middleware = serviceProvider.GetRequiredService<IEnumerable<IMiddleware<Args>>>().ToArray();
      middleware.Should().HaveCount(0);
   }

   [TestMethod]
   public void EnsureExecutionWorksWithoutAnyMiddleware()
   {
      var application = ConsoleApplication.WithArguments<Args>()
         .RemoveMiddleware(typeof(ExecutionMiddleware<Args>))
         .RemoveMiddleware(typeof(MapperMiddleware<Args>))
         .RemoveMiddleware(typeof(ParserMiddleware<Args>))
         .RemoveMiddleware(typeof(ExceptionHandlingMiddleware<Args>))
         .Build();

      application.Invoking(x => x.RunAsync(string.Empty, CancellationToken.None)).Should()
         .NotThrowAsync();
   }

   public class Args
   {
   }

}