// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationLogicExecutionTests.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.IntegrationTests;

using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

using FluentAssertions;

using JetBrains.Annotations;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

[TestClass]
[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
public class ApplicationLogicExecutionTests
{
   [TestMethod]
   public async Task EnsureCustomApplicationLogicIsExecuted()
   {
      var logicMock = new Mock<IApplicationLogic>();

      await ConsoleApplicationManager
         .For<Application>()
         .UseApplicationLogic(logicMock.Object)
         .RunAsync(CancellationToken.None);

      logicMock.Verify(x => x.ExecuteAsync(It.IsAny<ApplicationArgs>(), CancellationToken.None), Times.Once);
   }

   [TestMethod]
   public async Task EnsureCustomApplicationLogicIsExecutedWhenAddedAsService()
   {
      var logicMock = new Mock<IApplicationLogic>();

      await ConsoleApplicationManager
         .For<Application>()
         .ConfigureServices(s => s.AddSingleton(logicMock.Object))
         .RunAsync(CancellationToken.None);

      logicMock.Verify(x => x.ExecuteAsync(It.IsAny<ApplicationArgs>(), CancellationToken.None), Times.Once);
   }

   [TestMethod]
   public async Task EnsureApplicationLogicIsNotExecutedWhenCommandIsSpecified()
   {
      var logicMock = new Mock<IApplicationLogic>();

      var application = await ConsoleApplicationManager
         .For<Application>()
         .UseApplicationLogic(logicMock.Object)
         .RunAsync("run", CancellationToken.None);

      application.Arguments.Command.Executed.Should().BeTrue();
      logicMock.Verify(x => x.ExecuteAsync(It.IsAny<ApplicationArgs>(), CancellationToken.None), Times.Never);
   }

   [TestMethod]
   public async Task EnsureApplicationLogicIsExecutedWhenInvalidArgumentIsSpecified()
   {
      var logicMock = new Mock<IApplicationLogic>();

      var application = await ConsoleApplicationManager
         .For<Application>()
         .UseApplicationLogic(logicMock.Object)
         .RunAsync("NoCommand=5", CancellationToken.None);

      application.Arguments.Command.Should().BeNull();
      application.Arguments.NoCommand.Should().Be(5);
      logicMock.Verify(x => x.ExecuteAsync(It.IsAny<ApplicationArgs>(), CancellationToken.None), Times.Once);
   }

   [TestMethod]
   public async Task EnsureExecutingWithoutCustomLogicDoesNotCauseErrors()
   {
      var application = await ConsoleApplicationManager
         .For<Application>()
         .RunAsync(CancellationToken.None);

      application.Should().NotBeNull();
   }

   [UsedImplicitly]
   private class Application : ConsoleApplication<ApplicationArgs>
   {
      public Application(ICommandLineEngine commandLineEngine)
         : base(commandLineEngine)
      {
      }
   }


   [UsedImplicitly]
   internal class ApplicationArgs
   {
      [Command("run")]
      internal RunCommand Command { get; [UsedImplicitly] set; }

      [Argument("NoCommand")]
      public int NoCommand { get; set; }

      [UsedImplicitly]
      internal class RunCommand : IAsyncCommand<CommandArgs>
      {
         public bool Executed { get; private set; }

         public string Parameter { get; private set; }


         public Task ExecuteAsync(CancellationToken cancellationToken)
         {
            Executed = true;
            Parameter = Arguments.Parameter;
            return Task.CompletedTask;
         }

         public CommandArgs Arguments { get; set; }
      }

      [UsedImplicitly]
      internal class CommandArgs
      {
         [Argument("parameter")]
         public string Parameter { get; set; }
      }
   }

}