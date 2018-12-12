namespace ConsoLovers.ConsoleToolkit.Core.UnitTests
{
   using System.Diagnostics.CodeAnalysis;

   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
   using ConsoLovers.ConsoleToolkit.Core.UnitTests.ArgumentEngine;
   using ConsoLovers.ConsoleToolkit.Core.UnitTests.ArgumentEngine.TestData;

   using FluentAssertions;

   using JetBrains.Annotations;

   using Microsoft.VisualStudio.TestTools.UnitTesting;

   [TestClass]
   [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
   public class MapWithGeneticCommands : EngineTestBase
   {
      #region Public Methods and Operators

      [TestMethod]
      public void MapTheCommandAndItsArguments()
      {
         var arguments = GetTarget().Map<ApplicationCommands>(new[] { "execute", "-Path=C:\\Path\\File.txt", "-silent" });

         arguments.Execute.Should().NotBeNull();
         arguments.Execute.Arguments.Should().NotBeNull();

         arguments.Execute.Arguments.Path.Should().Be("C:\\Path\\File.txt");
         arguments.Execute.Arguments.Silent.Should().BeTrue();
      }

      [TestMethod]
      public void EnsureAliasesCanBeUsedForCommands()
      {
         var arguments = GetTarget().Map<ApplicationCommands>(new[] { "e", "-Path=C:\\Path\\File.txt", "-silent" });

         arguments.Execute.Should().NotBeNull();
         arguments.Execute.Arguments.Should().NotBeNull();

         arguments.Execute.Arguments.Path.Should().Be("C:\\Path\\File.txt");
         arguments.Execute.Arguments.Silent.Should().BeTrue();
      }

      [TestMethod]
      public void EnsureOptionsAreSetInRootArgumentsClass()
      {
         var arguments = GetTarget().Map<ApplicationCommands>(new[] { "Execute", "-Path=C:\\Path\\File.txt", "-silent", "-wait" });

         arguments.Execute.Should().NotBeNull();
         arguments.Execute.Arguments.Should().NotBeNull();

         arguments.Execute.Arguments.Path.Should().Be("C:\\Path\\File.txt");
         arguments.Execute.Arguments.Silent.Should().BeTrue();
         arguments.Wait.Should().BeTrue();
      }

      [TestMethod]
      public void EnsureNonGenericCommandClassWorks()
      {
         var arguments = GetTarget().Map<NonGenericApplicationCommands>(new[] { "Execute", "-Path=C:\\Path\\File.txt", "-silent", "-wait" });

         arguments.Execute.Should().NotBeNull();
         arguments.Execute.Arguments.Should().NotBeNull();

         arguments.Execute.Arguments.Path.Should().Be("C:\\Path\\File.txt");
         arguments.Execute.Arguments.Silent.Should().BeTrue();
         arguments.Wait.Should().BeTrue();
      }

      [TestMethod]
      public void EnsureUnmappedOptionsRaiseUnhandledCommandLineArgumentEventCorrectly()
      {
         var engine = GetTarget();
         engine.MonitorEvents();
         engine.Map<ApplicationCommands>(new[] { "execute", "-Path=C:\\Path\\File.txt", "-unknown" });

         engine.ShouldRaise(nameof(CommandLineEngine.UnhandledCommandLineArgument))
            .WithArgs<UnhandledCommandLineArgumentEventArgs>(e => e.Argument.Name == "unknown");
      }

      [TestMethod]
      public void EnsureUnmappedArgumentsRaiseUnhandledCommandLineArgumentEventCorrectly()
      {
         var engine = GetTarget();
         engine.MonitorEvents();
         engine.Map<ApplicationCommands>(new[] { "execute", "-Path=C:\\Path\\File.txt", "-unknown=666" });

         engine.ShouldRaise(nameof(CommandLineEngine.UnhandledCommandLineArgument))
            .WithArgs<UnhandledCommandLineArgumentEventArgs>(e => e.Argument.Name == "unknown");
      }

      [TestMethod]
      public void EnsureUnmappedArgumentsForNonGenericCommandRaiseUnhandledCommandLineArgumentEventCorrectly()
      {
         var engine = GetTarget();
         engine.MonitorEvents();
         engine.Map<NonGenericApplicationCommands>(new[] { "execute", "-unknown=666" });

         engine.ShouldRaise(nameof(CommandLineEngine.UnhandledCommandLineArgument))
            .WithArgs<UnhandledCommandLineArgumentEventArgs>(e => e.Argument.Name == "unknown" && e.Argument.Index == 0 && e.Argument.Value == "666");

         var args = engine.Map<NonGenericApplicationCommands>(new[] { "execute", "-wait", "-unknown=234" });

         engine.ShouldRaise(nameof(CommandLineEngine.UnhandledCommandLineArgument))
            .WithArgs<UnhandledCommandLineArgumentEventArgs>(e => e.Argument.Name == "unknown" && e.Argument.Index == 1 && e.Argument.Value == "234");

         args.Wait.Should().BeTrue();
      }

      [TestMethod]
      public void EnsureUnhandledCommandLineArgumentEventIsNotRaisedWhenDefaultCommandIsUsed()
      {
         var engine = GetTarget();
         engine.MonitorEvents();
         engine.Map<ApplicationCommandsWithDefault>(new[] { "path=SomeValue" });

         engine.ShouldNotRaise(nameof(CommandLineEngine.UnhandledCommandLineArgument));
      }

      [TestMethod]
      public void EnsureBaseClassArgumentsAreSetAndDoNotRaisedUnhandledCommandLineArgument()
      {
         var engine = GetTarget();
         engine.MonitorEvents();

         var args = engine.Map<ApplicationCommandsWithBaseClass>(new[] { "execute", "-loglevel=Trace" });

         args.LogLevel.Should().Be("Trace");
         engine.ShouldNotRaise(nameof(CommandLineEngine.UnhandledCommandLineArgument));
      }

      [TestMethod]
      public void EnsureMissingCommandLineArgumentsThrowMissingCommandLineArgumentException()
      {
         var engine = GetTarget();

         engine.Invoking(x => x.Map<ApplicationCommandsWithDefaultAndIndexedArgs>(new[] { "nam=hans" }))
            .ShouldThrow<MissingCommandLineArgumentException>().Where(x => x.Argument == "Name");
      }

      [TestMethod]
      public void EnsureOtherNamedParameterIsNotUsedName()
      {
         var engine = GetTarget();

         engine.Invoking(x => x.Map<ApplicationCommandsWithDefaultAndIndexedArgs>(new[] { "path=aPath" }))
            .ShouldThrow<MissingCommandLineArgumentException>().Where(x => x.Argument == "Name");
      }

      [TestMethod]
      public void EnsureValuesAreMappedCorrectlyByIndex()
      {
         var engine = GetTarget();

         var result = engine.Map<ApplicationCommandsWithDefaultAndIndexedArgs>(new[] { "execute", "hans", "ANormalValue" });
         result.Execute.Arguments.Name.Should().Be("hans");
         result.Execute.Arguments.Path.Should().Be("ANormalValue");

         result = engine.Map<ApplicationCommandsWithDefaultAndIndexedArgs>(new[] { "hans", "ANormalValue" });
         result.Execute.Arguments.Name.Should().Be("hans");
         result.Execute.Arguments.Path.Should().Be("ANormalValue");

         result = engine.Map<ApplicationCommandsWithDefaultAndIndexedArgs>(new[] { "execute", "hans", "\"D:\\bam\"" });
         result.Execute.Arguments.Name.Should().Be("hans");
         result.Execute.Arguments.Path.Should().Be("D:\\bam");

         result = engine.Map<ApplicationCommandsWithDefaultAndIndexedArgs>(new[] { "execute", "name=hans", "Path=\"D:\\bam\"" });
         result.Execute.Arguments.Name.Should().Be("hans");
         result.Execute.Arguments.Path.Should().Be("D:\\bam");
      }

      [TestMethod]
      public void EnsureNoMissingCommandLineArgumentsWhenParameterIsSpecifiedByIndex()
      {
         var engine = GetTarget();

         engine.Invoking(x => x.Map<ApplicationCommandsWithDefaultAndIndexedArgs>(new[] { "execute", "hans" })).
            ShouldNotThrow<MissingCommandLineArgumentException>();

         engine.Invoking(x => x.Map<ApplicationCommandsWithDefaultAndIndexedArgs>(new[] { "hans" })).
            ShouldNotThrow<MissingCommandLineArgumentException>();
      }

      [TestMethod]
      public void EnsureNoMissingCommandLineArgumentsWhenParameterIsSpecifiedWithName()
      {
         var engine = GetTarget();

         engine.Invoking(x => x.Map<ApplicationCommandsWithDefaultAndIndexedArgs>(new[] { "Name=hans" })).
            ShouldNotThrow<MissingCommandLineArgumentException>();
      }

      #endregion

      public class RequiredIndexedArguments
      {
         #region Public Properties

         [Argument(Required = true, Index = 0)]
         [UsedImplicitly]
         public string Name { get; set; }

         [Argument(Index = 1, TrimQuotation = true)]
         [UsedImplicitly]
         public string Path { get; set; }

         #endregion
      }

      internal class ApplicationCommands
      {
         #region Public Properties

         [Command("Execute", "e")]
         public GenericCommand<ExecuteArguments> Execute { get; set; }

         [Option("Wait")]
         public bool Wait { get; set; }

         #endregion
      }

      internal class ApplicationCommandsWithBaseClass : LoggingArgs
      {
         #region Public Properties

         [Command("Execute", "e")]
         public GenericCommand<ExecuteArguments> Execute { get; set; }

         [Option("Wait")]
         public bool Wait { get; set; }

         #endregion
      }

      internal class NonGenericApplicationCommands
      {
         #region Public Properties

         [Command("Execute", "e")]
         public ExecuteCommand Execute { get; set; }

         [Option("Wait")]
         public bool Wait { get; set; }

         #endregion
      }

      internal class ApplicationCommandsWithDefault
      {
         #region Public Properties

         [Command("Execute", IsDefaultCommand = true)]
         public GenericCommand<ExecuteArguments> Execute { get; set; }

         [Command("ExecuteMany")]
         public GenericCommand<ExecuteArguments> ExecuteMany { get; set; }

         #endregion
      }

      internal class ApplicationCommandsWithDefaultAndIndexedArgs
      {
         #region Public Properties

         [Command("Execute", IsDefaultCommand = true)]
         public GenericCommand<RequiredIndexedArguments> Execute { get; set; }

         [Command("ExecuteMany")]
         public GenericCommand<ExecuteArguments> ExecuteMany { get; set; }

         #endregion
      }

      internal class ExecuteArguments
      {
         #region Public Properties

         [Argument("Path")]
         public string Path { get; set; }

         [Option("Silent")]
         public bool Silent { get; set; }

         #endregion
      }
   }

   internal class LoggingArgs
   {
      [Argument("LogLevel")]
      public string LogLevel { get; set; }
   }

   internal class ExecuteCommand : ICommand<ExecuteArgs>
   {
      public void Execute()
      {
      }

      public ExecuteArgs Arguments { get; set; }
   }
}