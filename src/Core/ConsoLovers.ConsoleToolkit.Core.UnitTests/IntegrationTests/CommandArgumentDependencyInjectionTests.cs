// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandArgumentDependencyInjectionTests.cs" company="KUKA Deutschland GmbH">
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

[TestClass]
[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
public class CommandArgumentDependencyInjectionTests
{
   #region Public Methods and Operators

   [TestMethod]
   public async Task EnsureServicesAreInjectedIntoCommandsCorrectly()
   {
      var application = await ConsoleApplication.WithArguments<ApplicationArgs>()
         .AddService(s => s.AddTransient<Service>())
         .RunAsync("run Hello", CancellationToken.None);

      application.Arguments.Command.Arguments.Service.Should().NotBeNull();
      application.Arguments.Command.Result.Should().Be("HelloFromCommand");
   }

   #endregion

   [UsedImplicitly]
   internal class ApplicationArgs
   {
      #region Properties

      [Command("run")] internal RunCommand Command { get; [UsedImplicitly] set; }

      #endregion

      [UsedImplicitly]
      internal class CommandArgs
      {
         #region Constructors and Destructors

         public CommandArgs(Service service)
         {
            Service = service;
         }

         #endregion

         #region Public Properties

         [Argument("parameter", Index = 0)] public string Parameter { get; set; }

         #endregion

         #region Properties

         internal Service Service { get; }

         #endregion
      }

      [UsedImplicitly]
      internal class RunCommand : IAsyncCommand<CommandArgs>
      {
         #region IAsyncCommand<CommandArgs> Members

         public Task ExecuteAsync(CancellationToken cancellationToken)
         {
            Result = Arguments.Parameter + Arguments.Service.Text;
            return Task.CompletedTask;
         }

         public CommandArgs Arguments { get; set; }

         #endregion

         #region Public Properties

         public string Result { get; private set; }

         #endregion
      }
   }

   internal class Service
   {
      #region Public Properties

      public string Text => "FromCommand";

      #endregion
   }
}