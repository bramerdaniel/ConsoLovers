// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExceptionHandlingMiddleware.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Middleware;

using System;
using System.Threading;
using System.Threading.Tasks;

using JetBrains.Annotations;

public class ExceptionHandlingMiddleware<T> : Middleware<T>
   where T : class
{
   #region Constants and Fields

   private readonly IExceptionHandler exceptionHandler;

   private readonly IExitCodeHandler exitCodeHandler;

   #endregion

   #region Constructors and Destructors

   public ExceptionHandlingMiddleware([CanBeNull] IExceptionHandler exceptionHandler, [CanBeNull] IExitCodeHandler exitCodeHandler)
   {
      this.exceptionHandler = exceptionHandler;
      this.exitCodeHandler = exitCodeHandler;
   }

   #endregion

   #region Public Properties

   public override int ExecutionOrder => KnownLocations.ExceptionHandlingMiddleware;

   #endregion

   #region Public Methods and Operators

   public override async Task ExecuteAsync(IExecutionContext<T> context, CancellationToken cancellationToken)
   {
      try
      {
         await Next(context, cancellationToken);
         HandleExitCode(context);
      }
      catch (Exception e)
      {
         HandleExitCode(context, e);

         if (exceptionHandler?.Handle(e) ?? false)
            return;

         throw;
      }
   }

   private void HandleExitCode(IExecutionContext<T> context)
   {
      exitCodeHandler?.HandleSuccess(context.Result);
   }

   private void HandleExitCode(IExecutionContext<T> context, Exception e)
   {
      exitCodeHandler?.HandleError(context.Result, e);
   }

   #endregion
}