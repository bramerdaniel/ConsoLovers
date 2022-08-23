// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShowMenuApplicationLogic.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit
{
   using System;
   using System.Threading;
   using System.Threading.Tasks;

   using ConsoLovers.ConsoleToolkit.Core;

   using JetBrains.Annotations;

   /// <summary><see cref="IApplicationLogic"/> implementation that shows a consolovers menu from the specified application arguments</summary>
   /// <seealso cref="IApplicationLogic" />
   public class ShowMenuApplicationLogic : IApplicationLogic
   {
      private readonly ICommandMenuManager commandMenuManager;

      public ShowMenuApplicationLogic([NotNull] ICommandMenuManager commandMenuManager)
      {
         this.commandMenuManager = commandMenuManager ?? throw new ArgumentNullException(nameof(commandMenuManager));
      }

      public Task ExecuteAsync<T>(T arguments, CancellationToken cancellationToken)
      {
         commandMenuManager.Show<T>();
         return Task.CompletedTask;
      }
   }
}