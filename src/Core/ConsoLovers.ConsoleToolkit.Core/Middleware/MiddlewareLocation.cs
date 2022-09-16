// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MiddlewareLocation.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Middleware;

public enum MiddlewareLocation
{
   AtTheStart = 100,

   BeforeExceptionHandling = KnownLocations.ExceptionHandlingMiddleware - 100,
   
   AfterExceptionHandling = KnownLocations.ExceptionHandlingMiddleware + 100,

   BeforeParser = KnownLocations.ParserMiddleware - 100,

   AfterParser = KnownLocations.ParserMiddleware + 100,

   BeforeMapper = KnownLocations.MapperMiddleware - 100,

   AfterMapper = KnownLocations.MapperMiddleware + 100,

   BeforeExecution = KnownLocations.ExecutionMiddleware - 100,

   AfterExecution = KnownLocations.ExecutionMiddleware + 100,

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