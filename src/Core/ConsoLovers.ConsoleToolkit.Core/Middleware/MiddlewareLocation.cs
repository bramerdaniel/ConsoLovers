// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MiddlewareLocation.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Middleware;

public enum MiddlewareLocation
{
   /// <summary>Middleware is placed before all other <see cref="IMiddleware{T}"/>s</summary>
   AtTheStart = 100,

   /// <summary>Middleware is placed before the <see cref="ExceptionHandlingMiddleware{T}"/></summary>
   BeforeExceptionHandling = KnownLocations.ExceptionHandlingMiddleware - 100,
   
   /// <summary>Middleware is placed after the <see cref="ExceptionHandlingMiddleware{T}"/></summary>
   AfterExceptionHandling = KnownLocations.ExceptionHandlingMiddleware + 100,

   /// <summary>Middleware is placed before the <see cref="ParserMiddleware{T}"/></summary>
   BeforeParser = KnownLocations.ParserMiddleware - 100,

   /// <summary>Middleware is placed after the <see cref="ParserMiddleware{T}"/></summary>
   AfterParser = KnownLocations.ParserMiddleware + 100,

   /// <summary>Middleware is placed before the <see cref="MapperMiddleware{T}"/></summary>
   BeforeMapper = KnownLocations.MapperMiddleware - 100,

   /// <summary>Middleware is placed after the <see cref="MapperMiddleware{T}"/></summary>
   AfterMapper = KnownLocations.MapperMiddleware + 100,

   /// <summary>Middleware is placed before the <see cref="ExecutionMiddleware{T}"/></summary>
   BeforeExecution = KnownLocations.ExecutionMiddleware - 100,

   /// <summary>Middleware is placed after the <see cref="ExecutionMiddleware{T}"/></summary>
   AfterExecution = KnownLocations.ExecutionMiddleware + 100,

   /// <summary>Middleware is placed after all other registered <see cref="IMiddleware{T}"/>s</summary>
   AtTheEnd = int.MaxValue - 100
}

static class KnownLocations
{
   #region Constants and Fields

   internal const int ExceptionHandlingMiddleware = 1000;
   
   internal const int ParserMiddleware = 2000;

   internal const int MapperMiddleware = 3000;

   internal const int ExecutionMiddleware = 4000;
   
   #endregion
}