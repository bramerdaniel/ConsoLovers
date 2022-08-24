// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExceptionHandlingMiddleware.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Middleware;

using System;
using System.Threading;
using System.Threading.Tasks;

public class ExceptionHandlingMiddleware<T> : Middleware<T>
   where T : class
{
   #region Constants and Fields

   private readonly IExceptionHandler exceptionHandler;

   #endregion

   #region Constructors and Destructors

   public ExceptionHandlingMiddleware(IExceptionHandler exceptionHandler)
   {
      this.exceptionHandler = exceptionHandler;
   }

   #endregion

   #region Public Properties

   public override int ExecutionOrder => KnownLocations.ExceptionHandlingMiddleware;

   #endregion

   #region Public Methods and Operators

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

   #endregion
}