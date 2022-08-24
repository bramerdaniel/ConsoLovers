// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExecutionPipelineSetup.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.Setups;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using ConsoLovers.ConsoleToolkit.Core.Middleware;

using JetBrains.Annotations;

internal class ExecutionPipelineSetup :SetupBase<ExecutionPipeline<PipelineArgs>>
{
   private readonly List<IMiddleware<PipelineArgs>> middlewareList = new();

   private Action finalAction;

   protected override ExecutionPipeline<PipelineArgs> CreateInstance()
   {
      var instance = new ExecutionPipeline<PipelineArgs>(middlewareList);
      if (finalAction != null)
         instance.FinalStep = (c,t) =>
         {
            finalAction();
            return Task.CompletedTask;
         };

      return instance;
   }

   public ExecutionPipelineSetup WithMiddleware([NotNull] IMiddleware<PipelineArgs> middleware)
   {
      middlewareList.Add(middleware);
      return this;
   }

   public ExecutionPipelineSetup WithFinalAction(Action action)
   {
      finalAction = action;
      return this;
   }
}

internal class PipelineArgs
{
}