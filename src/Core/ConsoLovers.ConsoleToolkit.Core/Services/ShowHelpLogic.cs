// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShowHelpLogic.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Services;

using System;
using System.Threading;
using System.Threading.Tasks;

using JetBrains.Annotations;

public class ShowHelpLogic : IApplicationLogic
{
   #region Constants and Fields

   private readonly ICommandLineEngine engine;

   private readonly ILocalizationService localizationService;

   #endregion

   #region Constructors and Destructors

   public ShowHelpLogic(ICommandLineEngine engine, [NotNull] ILocalizationService localizationService)
   {
      this.engine = engine ?? throw new ArgumentNullException(nameof(engine));
      this.localizationService = localizationService ?? throw new ArgumentNullException(nameof(localizationService));
   }

   #endregion

   #region IApplicationLogic Members

   public Task ExecuteAsync<T>(T arguments, CancellationToken cancellationToken)
   {
      engine.PrintHelp<T>(localizationService);
      return Task.CompletedTask;
   }

   #endregion
}