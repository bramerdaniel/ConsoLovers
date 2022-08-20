// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExecutionPipeline.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Services;

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using ConsoLovers.ConsoleToolkit.Core.Middleware;

using JetBrains.Annotations;

internal class ExecutionPipeline : IExecutionPipeline
{
   private readonly IServiceProvider serviceProvider;

   public ExecutionPipeline([NotNull] IServiceProvider serviceProvider)
   {
      this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
   }

   public Task Execute<T>(IExecutionContext<T> context, CancellationToken cancellationToken)
      where T : class
   {
      // var middlewares = serviceProvider.GetService<IEnumerable<IMiddleware<IExecutionContext<T>>>>();
      // var middleware = serviceProvider.GetService<IMiddleware<IExecutionContext<T>>>();

      var builder = new PipeBuilder<IExecutionContext<T>>(serviceProvider)
         .AddMiddleware(typeof(ParserMiddleware<T>))
         .AddMiddleware(typeof(MapperMiddleware<T>));

      //foreach (var middleware in middlewares)
      //   builder.AddMiddleware(middleware);

      var initialMiddleware = builder
         .AddMiddleware(typeof(ExecutionMiddleware<T>))
         .Build();


      return initialMiddleware(context, cancellationToken);
   }
}