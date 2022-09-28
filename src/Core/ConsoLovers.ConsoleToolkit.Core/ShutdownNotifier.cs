// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShutdownNotifier.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

internal class ShutdownNotifier : IShutdownNotifier
{
   #region Constants and Fields

   private readonly IEnumerable<IAsyncShutdownHandler> asyncShutdownHandlers;

   private readonly IEnumerable<IShutdownHandler> shutdownHandlers;

   #endregion

   #region Constructors and Destructors

   public ShutdownNotifier(IEnumerable<IShutdownHandler> shutdownHandlers, IEnumerable<IAsyncShutdownHandler> asyncShutdownHandlers)
   {
      this.shutdownHandlers = shutdownHandlers ?? Array.Empty<IShutdownHandler>();
      this.asyncShutdownHandlers = asyncShutdownHandlers ?? Array.Empty<IAsyncShutdownHandler>();
   }

   #endregion

   #region IShutdownNotifier Members

   public async Task NotifyShutdownAsync(IExecutionResult result)
   {
      foreach (var asyncHandler in asyncShutdownHandlers)
         await NotifyHandlerAsync(result, asyncHandler);

      foreach (var handler in shutdownHandlers)
         NotifyHandler(handler, result);
   }

   #endregion

   #region Methods

   [SuppressMessage("ReSharper", "UnusedParameter.Local")]
   private void HandleShutdownException(Exception exception)
   {
      // we ignore exceptions during shut down at the moment
   }

   private void NotifyHandler(IShutdownHandler handler, IExecutionResult result)
   {
      try
      {
         handler.NotifyShutdown(result);
      }
      catch (Exception e)
      {
         HandleShutdownException(e);
      }
   }

   private async Task NotifyHandlerAsync(IExecutionResult result, IAsyncShutdownHandler asyncHandler)
   {
      try
      {
         await asyncHandler.NotifyShutdownAsync(result);
      }
      catch (Exception e)
      {
         HandleShutdownException(e);
      }
   }

   #endregion
}