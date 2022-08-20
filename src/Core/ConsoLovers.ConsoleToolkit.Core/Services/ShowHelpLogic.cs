// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShowHelpLogic.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Services;

using System;
using System.Threading;
using System.Threading.Tasks;

using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

using JetBrains.Annotations;

public class ShowHelpLogic : IApplicationLogic
{

   private readonly ICommandLineEngine engine;

   private readonly ILocalizationService localizationService;

   public ShowHelpLogic(ICommandLineEngine engine,[NotNull] ILocalizationService localizationService)
   {
      this.engine = engine ?? throw new ArgumentNullException(nameof(engine));
      this.localizationService = localizationService ?? throw new ArgumentNullException(nameof(localizationService));
   }

   public Task ExecuteAsync<T>(T arguments, CancellationToken cancellationToken)
   {
      engine.PrintHelp<T>(localizationService);
      return Task.CompletedTask;
   }
}