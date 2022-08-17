// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultServiceRegistrationTests.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.IntegrationTests;

using System;
using System.Diagnostics.CodeAnalysis;

using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments.Parsing;
using ConsoLovers.ConsoleToolkit.Core.Localization;

using FluentAssertions;

using JetBrains.Annotations;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

[TestClass]
[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
public class DefaultServiceRegistrationTests
{
   #region Public Methods and Operators

   [TestMethod]
   public void EnsureCommandExecutorServiceIsAddedCorrectly()
   {
      EnsureServiceAndImplementationAvailable<ICommandExecutor, CommandExecutor>();
   }

   [TestMethod]
   public void EnsureCommandLineArgumentParserCanBeReplaced()
   {
      var customParser = new Mock<ICommandLineArgumentParser>();

      var application = ConsoleApplicationManager
         .For<Application>()
         .ConfigureServices(s => s.AddSingleton(customParser.Object))
         .BuildApplication();

      application.ServiceProvider.GetService<ICommandLineArgumentParser>()
         .Should().BeSameAs(customParser.Object);

      var engine = application.ServiceProvider.GetService<CommandLineEngine>();
      Assert.IsNotNull(engine);
      engine.ArgumentParser.Should().BeSameAs(customParser.Object);
   }

   [TestMethod]
   public void EnsureCommandExecutorCanBeReplaced()
   {
      var customExecutor = new Mock<ICommandExecutor>();

      var application = ConsoleApplicationManager
         .For<Application>()
         .ConfigureServices(s => s.AddSingleton(customExecutor.Object))
         .BuildApplication();

      application.ServiceProvider.GetService<ICommandExecutor>()
         .Should().BeSameAs(customExecutor.Object);

      var engine = application.ServiceProvider.GetService<CommandLineEngine>();
      Assert.IsNotNull(engine);
      engine.CommandExecutor.Should().BeSameAs(customExecutor.Object);
   }


   [TestMethod]
   public void EnsureCommandLineArgumentParserServiceIsAddedCorrectly()
   {
      EnsureServiceAndImplementationAvailable<ICommandLineArgumentParser, CommandLineArgumentParser>();
   }

   [TestMethod]
   public void EnsureCommandLineEngineServiceIsAddedCorrectly()
   {
      EnsureServiceAndImplementationAvailable<ICommandLineEngine, CommandLineEngine>();
   }

   [TestMethod]
   public void EnsureConsoleServiceIsAddedCorrectly()
   {
      EnsureServiceAndImplementationAvailable<IConsole, ConsoleProxy>();
   }

   [TestMethod]
   public void EnsureLocalizationServiceIsAddedCorrectly()
   {
      EnsureServiceAndImplementationAvailable<ILocalizationService, DefaultLocalizationService>();
   }

   [TestMethod]
   public void EnsureArgumentReflectorServiceIsAddedCorrectly()
   {
      EnsureServiceAndImplementationAvailable<IArgumentReflector, ArgumentReflector>();
   }

   #endregion

   #region Methods

   private static void EnsureServiceAndImplementationAvailable<TService, TImplementation>()
      where TImplementation : TService
   {
      var serviceProvider = ConsoleApplicationManager
         .For<Application>()
         .CreateServiceProvider();

      serviceProvider.GetService<TService>().Should().NotBeNull($"{typeof(TService).Name} is a required service");
      serviceProvider.GetService<TImplementation>().Should().NotBeNull($"{typeof(TImplementation).Name} is a required service");
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
      #region Constructors and Destructors

      [UsedImplicitly]
      public Application(ICommandLineEngine commandLineEngine, [JetBrains.Annotations.NotNull] IServiceProvider serviceProvider)
         : base(commandLineEngine)
      {
         ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
      }

      #endregion

      #region Properties

      [UsedImplicitly]
      internal IServiceProvider ServiceProvider { get; }

      #endregion
   }
}