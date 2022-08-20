// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultServiceRegistrationTests.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.IntegrationTests;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using ConsoLovers.ConsoleToolkit.Core.Builders;
using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments.Parsing;
using ConsoLovers.ConsoleToolkit.Core.DefaultImplementations;

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
   public void EnsureArgumentReflectorServiceIsAddedCorrectly()
   {
      EnsureServiceAndImplementationAvailable<IArgumentReflector, ArgumentReflector>();
   }

   [TestMethod]
   public void EnsureCommandExecutorCanBeReplaced()
   {
      var customExecutor = new Mock<IExecutionEngine>();

      var serviceProvider = ConsoleApplication.WithArguments<ApplicationArgs>()
         .ConfigureServices(s => s.AddSingleton(customExecutor.Object))
         .CreateServiceProvider();

      serviceProvider.GetService<IExecutionEngine>()
         .Should().BeSameAs(customExecutor.Object);

      var engine = serviceProvider.GetService<CommandLineEngine>();
      Assert.IsNotNull(engine);
      engine.ExecutionEngine.Should().BeSameAs(customExecutor.Object);
   }

   [TestMethod]
   public void EnsureCommandExecutorServiceIsAddedCorrectly()
   {
      EnsureServiceAndImplementationAvailable<IExecutionEngine, ExecutionEngine>();
   }

   [TestMethod]
   public void EnsureCommandLineArgumentParserCanBeReplaced()
   {
      var customParser = new Mock<ICommandLineArgumentParser>();

      var serviceProvider = ConsoleApplication.WithArguments<ApplicationArgs>()
         .ConfigureServices(s => s.AddSingleton(customParser.Object))
         .CreateServiceProvider();

      serviceProvider.GetService<ICommandLineArgumentParser>()
         .Should().BeSameAs(customParser.Object);

      var engine = serviceProvider.GetService<CommandLineEngine>();
      Assert.IsNotNull(engine);
      engine.ArgumentParser.Should().BeSameAs(customParser.Object);
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
   public void EnsureApplicationCanBeReplaced()
   {
      var mock = new Mock<IExecutable<SomeArgs>>();
      mock.Setup(x => x.RunAsync(It.IsAny<string>(), CancellationToken.None))
         .Returns(() => Task.FromResult(mock.Object));

      var application = ConsoleApplication.WithArguments<SomeArgs>()
         .ConfigureServices(s => s.AddSingleton(mock.Object))
         .Run();

      mock.Verify(x => x.RunAsync(It.IsAny<string>(), CancellationToken.None), Times.Once);
      application.Should().BeSameAs(mock.Object);
   }

   #endregion

   #region Methods

   private static void EnsureServiceAndImplementationAvailable<TService, TImplementation>()
      where TImplementation : TService
   {
      var serviceProvider = ConsoleApplication.WithArguments<ApplicationArgs>()
         .CreateServiceProvider();

      serviceProvider.GetService<TService>().Should().NotBeNull($"{typeof(TService).Name} is a required service");
      serviceProvider.GetService<TImplementation>().Should().NotBeNull($"{typeof(TImplementation).Name} is a required service");
   }

   #endregion

   [UsedImplicitly]
   internal class ApplicationArgs
   {
   }

   [UsedImplicitly]
   public class SomeArgs
   {
   }
}