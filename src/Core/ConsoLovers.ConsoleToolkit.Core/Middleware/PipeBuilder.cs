// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PipeBuilder.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.Extensions.DependencyInjection;

public class PipeBuilder<T>
{
   readonly Action<T> mainAction;

   private readonly IServiceProvider serviceProvider;

   readonly IList<Type> middlewareTypes;

   public PipeBuilder(Action<T> mainAction, IServiceProvider serviceProvider)
   {
      this.mainAction = mainAction;
      this.serviceProvider = serviceProvider;

      middlewareTypes = new List<Type>();
   }

   public PipeBuilder<T> AddMiddleware(Type pipeType)
   {
      middlewareTypes.Add(pipeType);
      return this;
   }

   public PipeBuilder<T> AddMiddleware<TMiddleware>()
   {
      return AddMiddleware(typeof(TMiddleware));
   }

   private Action<T> CreatePipeOld(int index)
   {
      if (index < middlewareTypes.Count - 1)
      {
         var childPipeHandle = CreatePipe(index + 1);
         var pipe = (Middleware<T>)ActivatorUtilities.CreateInstance(serviceProvider, middlewareTypes[index], childPipeHandle);
         return pipe.Execute;
      }
      else
      {
         var finalPipe = (Middleware<T>)ActivatorUtilities.CreateInstance(serviceProvider, middlewareTypes[index], mainAction);
         return finalPipe.Execute;
      }
   }
   private Action<T> CreatePipe(int index)
   {
      var reversed = middlewareTypes.Reverse().ToArray();
      var lastMiddleware = (Middleware<T>)ActivatorUtilities.CreateInstance(serviceProvider, reversed[0]);
      lastMiddleware.Next = mainAction;
      Middleware<T> current = null;

      foreach (Type middlewareType in reversed.Skip(1))
      {
         current = (Middleware<T>)ActivatorUtilities.CreateInstance(serviceProvider, middlewareType);
         current.Next = lastMiddleware.Execute;
      }

      return current.Execute;
   }

   public Action<T> Build()
   {
      return CreatePipe(0);
   }
}