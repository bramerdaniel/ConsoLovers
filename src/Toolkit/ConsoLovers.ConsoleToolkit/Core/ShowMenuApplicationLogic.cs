// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShowMenuApplicationLogic.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   using System;
   using System.Threading;
   using System.Threading.Tasks;

   using JetBrains.Annotations;

   /// <summary><see cref="IApplicationLogic"/> implementation that shows a consolovers menu from the specified application arguments</summary>
   /// <seealso cref="IApplicationLogic"/>
   public class ShowMenuApplicationLogic : IApplicationLogic
   {
      #region Constants and Fields

      private readonly IMenuCommandManager commandMenuManager;

      #endregion

      #region Constructors and Destructors

      public ShowMenuApplicationLogic([NotNull] IMenuCommandManager commandMenuManager)
      {
         this.commandMenuManager = commandMenuManager ?? throw new ArgumentNullException(nameof(commandMenuManager));
      }

      #endregion

      #region IApplicationLogic Members

      public Task ExecuteAsync<T>(T arguments, CancellationToken cancellationToken)
      {
         commandMenuManager.Show<T>(cancellationToken);
         return Task.CompletedTask;
      }

      #endregion
   }
}