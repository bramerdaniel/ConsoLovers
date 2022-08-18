// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandArgumentDependencyInjectionTests.cs" company="ConsoLovers">
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
public class CommandArgumentDependencyInjectionTests
{
   [TestMethod]
   public async Task EnsureServicesAreInjectedIntoCommandsCorrectly()
   {
      var application = await ConsoleApplicationManager
         .For<Application>()
         .ConfigureServices(s => s.AddTransient<Service>())
         .RunAsync("run Hello", CancellationToken.None);

      application.Arguments.Command.Arguments.Service.Should().NotBeNull();
      application.Arguments.Command.Result.Should().Be("HelloFromCommand");
   }

   [UsedImplicitly]
   private class Application : ConsoleApplication<ApplicationArgs>
   {
      public Application(ICommandLineEngine commandLineEngine)
         : base(commandLineEngine)
      {
      }
   }

   internal class Service
   {

      public string Text => "FromCommand";
   }

   [UsedImplicitly]
   internal class ApplicationArgs
   {
      [Command("run")]
      internal RunCommand Command { get; [UsedImplicitly] set; }

      [UsedImplicitly]
      internal class RunCommand : IAsyncCommand<CommandArgs>
      {
         public string Result { get; private set; }


         public Task ExecuteAsync(CancellationToken cancellationToken)
         {
            Result = Arguments.Parameter + Arguments.Service.Text;
            return Task.CompletedTask;
         }

         public CommandArgs Arguments { get; set; }
      }

      [UsedImplicitly]
      internal class CommandArgs
      {
         internal Service Service { get; }

         public CommandArgs(Service service)
         {
            Service = service;
         }

         [Argument("parameter", Index = 0)]
         public string Parameter { get; set; }
      }
   }

}