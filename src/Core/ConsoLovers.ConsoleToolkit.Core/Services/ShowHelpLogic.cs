// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShowHelpLogic.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Services;

using System;
using System.Threading;
using System.Threading.Tasks;

public class ShowHelpLogic : IApplicationLogic
{
   #region Constants and Fields

   private readonly ICommandLineEngine engine;


   #endregion

   #region Constructors and Destructors

   public ShowHelpLogic(ICommandLineEngine engine)
   {
      this.engine = engine ?? throw new ArgumentNullException(nameof(engine));
   }

   #endregion

   #region IApplicationLogic Members

   public Task ExecuteAsync<T>(T arguments, CancellationToken cancellationToken)
   {
      engine.PrintHelp<T>();
      return Task.CompletedTask;
   }

   #endregion
}