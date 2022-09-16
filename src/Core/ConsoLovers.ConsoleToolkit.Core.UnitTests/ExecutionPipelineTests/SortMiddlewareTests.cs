// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SortMiddlewareTests.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.ExecutionPipelineTests;

using System.Collections.Generic;
using System.Threading;

using ConsoLovers.ConsoleToolkit.Core.Middleware;
using ConsoLovers.ConsoleToolkit.Core.UnitTests.Setups;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class SortMiddlewareTests
{
   #region Public Methods and Operators

   [TestMethod]
   public void EnsureMiddlewareIsSortedCorrectly()
   {
      var first = Setup.MockFor()
         .Middleware<PipelineArgs>()
         .WithExecutionOrder(1)
         .Done();

      var second = Setup.MockFor()
         .Middleware<PipelineArgs>()
         .WithExecutionOrder(2)
         .Done();

      var third = Setup.MockFor()
         .Middleware<PipelineArgs>()
         .WithExecutionOrder(3)
         .Done();

      var target = Setup.ExecutionPipeline()
         .WithMiddleware(third)
         .WithMiddleware(first)
         .WithMiddleware(second)
         .Done();

      target.RegisteredMiddleware[0].Should().Be(first);
      target.RegisteredMiddleware[1].Should().Be(second);
      target.RegisteredMiddleware[2].Should().Be(third);
   }

   [TestMethod]
   public void EnsureMiddlewareExecutedInCorrectOrder()
   {
      List<int> order = new List<int>(3);

      var first = Setup.MockFor()
         .Middleware<PipelineArgs>()
         .WithExecutionOrder(1)
         .WithAction(() => order.Add(100))
         .Done();

      var second = Setup.MockFor()
         .Middleware<PipelineArgs>()
         .WithExecutionOrder(2)
         .WithAction(() => order.Add(20))
         .Done();

      var third = Setup.MockFor()
         .Middleware<PipelineArgs>()
         .WithExecutionOrder(3)
         .WithAction(() => order.Add(3000))
         .Done();

      var target = Setup.ExecutionPipeline()
         .WithMiddleware(third)
         .WithMiddleware(first)
         .WithMiddleware(second)
         .WithFinalAction(() => order.Add(-1) )
         .Done();

      Execute(target);

      order.Should().BeEquivalentTo(new List<int> { 100, 20, 3000, -1 });
   }

   private static void Execute(ExecutionPipeline<PipelineArgs> target)
   {
      var context = new ExecutionContext<PipelineArgs>(string.Empty, new ExecutionResult());

      target.ExecuteAsync(context, CancellationToken.None)
         .GetAwaiter().GetResult();
   }

   #endregion
}