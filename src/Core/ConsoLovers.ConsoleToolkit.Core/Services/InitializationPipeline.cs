// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InitializationPipeline.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Services;

using System;

using ConsoLovers.ConsoleToolkit.Core.Middleware;

using JetBrains.Annotations;

internal class InitializationPipeline : IInitializationPipeline
{
   private readonly IServiceProvider serviceProvider;

   public InitializationPipeline([NotNull] IServiceProvider serviceProvider)
   {
      this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
   }

   public void Execute<T>(IInitializationContext<T> context)
      where T : class
   {
      var pipe = new PipeBuilder<IInitializationContext<T>>( x => { }, serviceProvider)
         .AddMiddleware(typeof(ParserMiddleware<T>))
         .AddMiddleware(typeof(MapperMiddleware<T>))
         .Build();

      pipe(context);
   }
}