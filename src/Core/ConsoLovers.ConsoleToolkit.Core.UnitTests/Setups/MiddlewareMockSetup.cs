// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MiddlewareMockSetup.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.Setups;

using System;
using System.Threading;
using System.Threading.Tasks;

using ConsoLovers.ConsoleToolkit.Core.Middleware;
using ConsoLovers.ConsoleToolkit.Core.UnitTests.Setups.Mocks;

using Moq;

public class MiddlewareMockSetup<T> : MockSetupBase<IMiddleware<T>>
   where T : class
{
   protected override void SetupMock(Mock<IMiddleware<T>> mockInstance)
   {
      mockInstance.SetupProperty(x => x.Next);
      base.SetupMock(mockInstance);
   }

   public MiddlewareMockSetup<T> WithExecutionOrder(int executionOrder)
   {
      SetupBehaviour(b => b.Setup(x => x.ExecutionOrder).Returns(executionOrder));
      return this;
   }

   public MiddlewareMockSetup<T> WithAction(Action action)
   {
      SetupBehaviour(b =>
      {
         b.Setup(x => x.ExecuteAsync(It.IsAny<IExecutionContext<T>>(), It.IsAny<CancellationToken>()))
            .Returns<IExecutionContext<T>, CancellationToken>(OnExecuteAsync);

         Task OnExecuteAsync(IExecutionContext<T> executionContext, CancellationToken cancellationToken)
         {
            action();
            b.Object.Next(executionContext, cancellationToken);
            return Task.CompletedTask;
         }
      });
  
      return this;
   }
}