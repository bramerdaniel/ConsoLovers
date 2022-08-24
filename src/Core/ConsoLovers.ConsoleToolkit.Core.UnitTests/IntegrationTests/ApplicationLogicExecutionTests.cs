// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationLogicExecutionTests.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.IntegrationTests;

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
public class ApplicationLogicExecutionTests
{
   #region Public Methods and Operators

   [TestMethod]
   public async Task EnsureApplicationLogicIsExecutedWhenInvalidArgumentIsSpecified()
   {
      var logicMock = new Mock<IApplicationLogic>();

      var application = await ConsoleApplication.WithArguments<ApplicationArgs>()
         .UseApplicationLogic(logicMock.Object)
         .RunAsync("NoCommand=5", CancellationToken.None);

      application.Arguments.Command.Should().BeNull();
      application.Arguments.NoCommand.Should().Be(5);
      logicMock.Verify(x => x.ExecuteAsync(It.IsAny<ApplicationArgs>(), CancellationToken.None), Times.Once);
   }

   [TestMethod]
   public async Task EnsureApplicationLogicIsNotExecutedWhenCommandIsSpecified()
   {
      var logicMock = new Mock<IApplicationLogic>();

      var application = await ConsoleApplication.WithArguments<ApplicationArgs>()
         .UseApplicationLogic(logicMock.Object)
         .RunAsync("run", CancellationToken.None);

      application.Arguments.Command.Executed.Should().BeTrue();
      logicMock.Verify(x => x.ExecuteAsync(It.IsAny<ApplicationArgs>(), CancellationToken.None), Times.Never);
   }

   [TestMethod]
   public async Task EnsureCustomApplicationLogicIsExecuted()
   {
      var logicMock = new Mock<IApplicationLogic>();

      await ConsoleApplication.WithArguments<ApplicationArgs>()
         .UseApplicationLogic(logicMock.Object)
         .RunAsync(CancellationToken.None);

      logicMock.Verify(x => x.ExecuteAsync(It.IsAny<ApplicationArgs>(), CancellationToken.None), Times.Once);
   }

   [TestMethod]
   public async Task EnsureCustomApplicationLogicIsExecutedWhenAddedAsService()
   {
      var logicMock = new Mock<IApplicationLogic>();

      await ConsoleApplication.WithArguments<ApplicationArgs>()
         .AddService(s => s.AddSingleton(logicMock.Object))
         .RunAsync(CancellationToken.None);

      logicMock.Verify(x => x.ExecuteAsync(It.IsAny<ApplicationArgs>(), CancellationToken.None), Times.Once);
   }

   [TestMethod]
   public async Task EnsureExecutingWithoutCustomLogicDoesNotCauseErrors()
   {
      var application = await ConsoleApplication.WithArguments<ApplicationArgs>()
         .RunAsync(CancellationToken.None);

      application.Should().NotBeNull();
   }

   [TestMethod]
   public void EnsureTypedApplicationLogicIsExecutedCorrectly()
   {
      var applicationLogic = new Logic();

      var application = ConsoleApplication.WithArguments<ApplicationArgs>()
         .UseApplicationLogic(applicationLogic)
         .Run();

      applicationLogic.Executed.Should().BeTrue();
      applicationLogic.Arguments.Should().BeSameAs(application.Arguments);
   }

   #endregion

   [UsedImplicitly]
   internal class ApplicationArgs
   {
      #region Public Properties

      [Argument("NoCommand")]
      public int NoCommand { get; set; }

      #endregion

      #region Properties

      [Command("run")]
      internal RunCommand Command { get; [UsedImplicitly] set; }

      #endregion

      [UsedImplicitly]
      internal class CommandArgs
      {
         #region Public Properties

         [Argument("parameter")]
         public string Parameter { get; set; }

         #endregion
      }

      [UsedImplicitly]
      internal class RunCommand : IAsyncCommand<CommandArgs>
      {
         #region IAsyncCommand<CommandArgs> Members

         public Task ExecuteAsync(CancellationToken cancellationToken)
         {
            Executed = true;
            Parameter = Arguments.Parameter;
            return Task.CompletedTask;
         }

         public CommandArgs Arguments { get; set; }

         #endregion

         #region Public Properties

         public bool Executed { get; private set; }

         public string Parameter { get; private set; }

         #endregion
      }
   }

   class Logic : IApplicationLogic<ApplicationArgs>
   {
      #region IApplicationLogic<ApplicationArgs> Members

      public Task ExecuteAsync(ApplicationArgs arguments, CancellationToken cancellationToken)
      {
         Executed = true;
         Arguments = arguments;
         return Task.CompletedTask;
      }

      #endregion

      #region Public Properties

      public ApplicationArgs Arguments { get; private set; }

      public bool Executed { get; private set; }

      #endregion
   }
}