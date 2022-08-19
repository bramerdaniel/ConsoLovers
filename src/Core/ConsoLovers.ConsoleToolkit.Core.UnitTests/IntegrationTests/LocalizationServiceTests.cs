// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LocalizationServiceTests.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.IntegrationTests;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

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

      var builder = ConsoleApplication.WithArguments<ApplicationArgs>()
         .ConfigureServices(s => s.AddSingleton(customService))
         .AddResourceManager(FirstResource.ResourceManager);

      builder.Invoking(x => x.Build())
         .Should().Throw<InvalidOperationException>();
   }

   [TestMethod]
   public void EnsureDefaultServiceReturnsResourceKey()
   {
      var application = ConsoleApplication.WithArguments<ApplicationArgs>()
         .AddResourceManager(FirstResource.ResourceManager)
         .Build();

      application.Arguments.Localize("DoesNotExist").Should().Be("DoesNotExist");
   }

   [TestMethod]
   public void EnsureDefaultServiceWorksCorrectlyWithMultipleResourceManagers()
   {
      var application = ConsoleApplication.WithArguments<ApplicationArgs>()
         .AddResourceManager(FirstResource.ResourceManager)
         .AddResourceManager(SecondResource.ResourceManager)
         .Build();

      application.Arguments.Localize("FirstKey").Should().Be("FirstValue");
      application.Arguments.Localize("SecondKey").Should().Be("SecondValue");
   }

   [TestMethod]
   public void EnsureDefaultServiceWorksCorrectlyWithSingleResourceManager()
   {
      var application = ConsoleApplication.WithArguments<ApplicationArgs>()
         .AddResourceManager(FirstResource.ResourceManager)
         .Build();

      application.Arguments.Localize("FirstKey").Should().Be("FirstValue");
   }

   [TestMethod]
   public async Task EnsureReplacedLocalizationServiceIsUsed()
   {
      var customLocalizationService = new Mock<ILocalizationService>();
      customLocalizationService.Setup(x => x.GetLocalizedSting("Value")).Returns<string>(_ => "ValueLocalized");

      var application = await ConsoleApplication.WithArguments<ApplicationArgs>()
         .ConfigureServices(s => s.AddSingleton(customLocalizationService.Object))
         .RunAsync("key=Value", CancellationToken.None);

      application.Arguments.Localize("Value").Should().Be("ValueLocalized");
   }

   #endregion

   [UsedImplicitly]
   internal class ApplicationArgs
   {
      #region Constants and Fields

      [JetBrains.Annotations.NotNull]
      private readonly ILocalizationService localizationService;

      #endregion

      #region Constructors and Destructors

      public ApplicationArgs([JetBrains.Annotations.NotNull] ILocalizationService localizationService)
      {
         this.localizationService = localizationService ?? throw new ArgumentNullException(nameof(localizationService));
      }

      #endregion

      #region Public Methods and Operators

      public string Localize(string key)
      {
         return localizationService.GetLocalizedSting(key);
      }

      #endregion
   }
}