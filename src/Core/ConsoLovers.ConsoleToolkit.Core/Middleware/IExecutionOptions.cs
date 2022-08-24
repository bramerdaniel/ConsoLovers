// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IExecutionOptions.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Middleware;

internal interface IExecutionOptions
{
   #region Public Properties

   IMappingOptions MappingOptions { get; set; }

   #endregion
}

internal class ExecutionOptions
   : IExecutionOptions
{
   #region IExecutionOptions Members

   public IMappingOptions MappingOptions { get; set; }

   #endregion
}