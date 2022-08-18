// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LocalizationReplacementTests.cs" company="ConsoLovers">
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

using Moq;

[TestClass]
[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
public class LocalizationServiceTests
{
   #region Public Methods and Operators

   [TestMethod]
   public void EnsureAddingResourceManagerFailsWhenLocalizationServiceWasReplaced()
   {
      var customService = new Mock<ILocalizationService>().Object;

      var bootstrapper = ConsoleApplicationManager
         .For<Application>()
         .ConfigureServices(s => s.AddSingleton(customService))
         .AddResourceManager(FirstResource.ResourceManager);

      bootstrapper.Invoking(x => x.CreateServiceProvider())
         .Should().Throw<InvalidOperationException>();
   }

   [TestMethod]
   public async Task EnsureDefaultServiceReturnsResourceKey()
   {
      var application = await ConsoleApplicationManager
         .For<Application>()
         .AddResourceManager(FirstResource.ResourceManager)
         .RunAsync("key=DoesNotExist", CancellationToken.None);

      application.LocalizedValues[0].Should().Be("DoesNotExist");
   }

   [TestMethod]
   public async Task EnsureDefaultServiceWorksCorrectlyWithMultipleResourceManagers()
   {
      var application = await ConsoleApplicationManager.For<Application>()
         .AddResourceManager(FirstResource.ResourceManager)
         .AddResourceManager(SecondResource.ResourceManager)
         .RunAsync("keys=\"FirstKey,SecondKey\"", CancellationToken.None);

      application.LocalizedValues.Should().HaveCount(2);
      application.LocalizedValues[0].Should().Be("FirstValue");
      application.LocalizedValues[1].Should().Be("SecondValue");
   }

   [TestMethod]
   public async Task EnsureDefaultServiceWorksCorrectlyWithSingleResourceManager()
   {
      var application = await ConsoleApplicationManager.For<Application>()
         .AddResourceManager(FirstResource.ResourceManager)
         .RunAsync("key=FirstKey", CancellationToken.None);

      application.LocalizedValues[0].Should().Be("FirstValue");
   }

   [TestMethod]
   public async Task EnsureReplacedLocalizationServiceIsUsed()
   {
      var customLocalizationService = new Mock<ILocalizationService>();
      customLocalizationService.Setup(x => x.GetLocalizedSting("Value")).Returns<string>(_ => "ValueLocalized");

      var application = await ConsoleApplicationManager
         .For<Application>()
         .ConfigureServices(s => s.AddSingleton(customLocalizationService.Object))
         .RunAsync("key=Value", CancellationToken.None);

      application.LocalizedValues[0].Should().Be("ValueLocalized");
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