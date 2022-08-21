// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExceptionHandlingMiddleware.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Middleware;

using System;
using System.Threading;
using System.Threading.Tasks;

using ConsoLovers.ConsoleToolkit.Core.Services;

public class ExceptionHandlingMiddleware<T> : Middleware<IExecutionContext<T>>
   where T : class
{
   private readonly IExceptionHandler exceptionHandler;

   public ExceptionHandlingMiddleware(IExceptionHandler exceptionHandler)
   {
      this.exceptionHandler = exceptionHandler;
   }

   public override int ExecutionOrder => KnownLocations.ExceptionHandlingMiddleware;

   public override async Task Execute(IExecutionContext<T> context, CancellationToken cancellationToken)
   {
      try
      {
         await Next(context, cancellationToken);
      }
      catch (Exception e)
      {
         if (exceptionHandler?.Handle(e) ?? false)
            return;

         throw;
      }
   }
}