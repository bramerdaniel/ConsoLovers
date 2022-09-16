// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IExecutionContext.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Middleware;

public interface IExecutionContext<T>
   where T : class
{
   #region Public Properties

   /// <summary>Gets or sets the application arguments.</summary>
   T ApplicationArguments { get; set; }

   /// <summary>Gets or sets the raw commandline (string or string[]).</summary>
   object Commandline { get; set; }

   /// <summary>Gets or sets the parsed arguments (normally set by the parser middleware).</summary>
   ICommandLineArguments ParsedArguments { get; set; }

   /// <summary>Gets the result of the execution.</summary>
   IExecutionResult Result { get; }

   #endregion
}