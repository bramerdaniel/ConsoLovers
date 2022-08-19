// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SomeMoreTests.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.IntegrationTests;

using System;
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
public class CommandDependencyInjectionTests
{
   [TestMethod]
   public async Task EnsureCommandIsExecutedCorrectly()
   {
      var application = await ConsoleApplication.WithArguments<ApplicationArgs>()
         .RunAsync("run parameter=ok", CancellationToken.None);

      application.Arguments.Command.Executed.Should().BeTrue();
      application.Arguments.Command.Parameter.Should().Be("ok");
      application.Arguments.Command.Service.Should().BeNull();
   }

   [TestMethod]
   public async Task EnsureServicesAreInjectedIntoCommandsCorrectly()
   {
      var application = await ConsoleApplication.WithArguments<ApplicationArgs>()
         .ConfigureServices(s => s.AddSingleton(new Service("Bam")))
         .RunAsync("run", CancellationToken.None);

      application.Arguments.Command.Service.Should().NotBeNull();
      application.Arguments.Command.Service.Text.Should().Be("Bam");
   }

   internal class Service
   {
      public Service(string text)
      {
         Text = text ?? throw new ArgumentNullException(nameof(text));
      }

      public string Text { get; }
   }

   [UsedImplicitly]
   internal class ApplicationArgs
   {
      [Command("run")]
      internal RunCommand Command { get; [UsedImplicitly] set; }

      [UsedImplicitly]
      internal class RunCommand : IAsyncCommand<CommandArgs>
      {
         internal Service Service { get; }

         public RunCommand(Service service)
         {
            Service = service;
         }

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

