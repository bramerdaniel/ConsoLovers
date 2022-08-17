// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultServiceRegistrationTests.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.IntegrationTests;

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
using ConsoLovers.ConsoleToolkit.Core.Localization;

using FluentAssertions;

using JetBrains.Annotations;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
public class DefaultServiceRegistrationTests
{
   #region Public Methods and Operators

   [TestMethod]
   public void EnsureLocalizationServiceIsAddedCorrectly()
   {
      EnsureServiceAndImplementationAvailable<ILocalizationService, DefaultLocalizationService>();
   }

   [TestMethod]
   public void EnsureCommandLineEngineServiceIsAddedCorrectly()
   {
      EnsureServiceAndImplementationAvailable<ICommandLineEngine, CommandLineEngine>();
   }

   [TestMethod]
   public void EnsureCommandExecutorServiceIsAddedCorrectly()
   {
      EnsureServiceAndImplementationAvailable<ICommandExecutor, CommandExecutor>();
   }

   [TestMethod]
   public void EnsureConsoleServiceIsAddedCorrectly()
   {
      EnsureServiceAndImplementationAvailable<IConsole, ConsoleProxy>();
   }

   private static void EnsureServiceAndImplementationAvailable<TService, TImplementation>()
   where TImplementation : TService
   {
      var serviceProvider = ConsoleApplicationManager
         .For<Application>()
         .CreateServiceProvider();

      serviceProvider.GetService<TService>().Should().NotBeNull();
      serviceProvider.GetService<TImplementation>().Should().NotBeNull();
   }

   #endregion

   [UsedImplicitly]
   internal class ApplicationArgs
   {
      #region Public Properties

      [Argument("key", "keys")] public string ResourceKeys { get; [UsedImplicitly] set; }

      #endregion
   }

   [UsedImplicitly]
   private class Application : ConsoleApplication<ApplicationArgs>
   {
      #region Constants and Fields

      private readonly ILocalizationService localizationService;

      #endregion

      #region Constructors and Destructors

      [UsedImplicitly]
      public Application(ICommandLineEngine commandLineEngine, [JetBrains.Annotations.NotNull] ILocalizationService localizationService)
         : base(commandLineEngine)
      {
         this.localizationService = localizationService ?? throw new ArgumentNullException(nameof(localizationService));
      }

      #endregion

      #region Public Properties

      public IList<string> LocalizedValues { get; } = new List<string>();

      #endregion

      #region Public Methods and Operators

      public override Task RunWithAsync(ApplicationArgs arguments, CancellationToken cancellationToken)
      {
         foreach (var key in arguments.ResourceKeys.Split(",", StringSplitOptions.RemoveEmptyEntries))
         {
            var localizedSting = localizationService.GetLocalizedSting(key.Trim());
            LocalizedValues.Add(localizedSting);
         }

         return Task.CompletedTask;
      }

      #endregion
   }
}