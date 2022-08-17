// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LocalizationReplacementTests.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.IntegrationTests;

using System;
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
public class LocalizationReplacementTests
{
   [TestMethod]
   public async Task EnsureCommandIsExecutedCorrectly()
   {
      var service = new Mock<ILocalizationService>();
      service.Setup(x => x.GetLocalizedSting("Value")).Returns<string>(_ => $"ValueLocalized");

      var application = await ConsoleApplicationManager
         .For<Application>()
         .ConfigureServices(s => s.AddSingleton(service.Object))
         .RunAsync("key=Value", CancellationToken.None);

      application.ResultString.Should().Be("ValueLocalized");
   }

   [TestMethod]
   public async Task EnsureDefaultServiceReturnsResourceKey()
   {
      var application = await ConsoleApplicationManager
         .For<Application>()
         .AddResourceManager(Properties.Resources.ResourceManager)
         .RunAsync("key=Value", CancellationToken.None);

      application.ResultString.Should().Be("Value");
   }

   [UsedImplicitly]
   private class Application : ConsoleApplication<ApplicationArgs>
   {
      private readonly ILocalizationService localizationService;

      public string ResultString { get; set; }

      public Application(ICommandLineEngine commandLineEngine, [JetBrains.Annotations.NotNull] ILocalizationService localizationService)
         : base(commandLineEngine)
      {
         this.localizationService = localizationService ?? throw new ArgumentNullException(nameof(localizationService));
      }

      public override Task RunWithAsync(ApplicationArgs arguments, CancellationToken cancellationToken)
      {
         ResultString = localizationService.GetLocalizedSting(arguments.ResourceKey);
         return Task.CompletedTask;
      }
   }

   internal class ApplicationArgs
   {
      [Argument("key")]
      public string ResourceKey { get; set; }
   }



}