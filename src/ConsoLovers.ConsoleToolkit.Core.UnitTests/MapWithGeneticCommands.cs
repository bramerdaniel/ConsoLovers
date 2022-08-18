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
         var arguments = DoMap<ApplicationCommands>(new[] { "execute", "-Path=C:\\Path\\File.txt", "-silent" });

         arguments.Execute.Should().NotBeNull();
         arguments.Execute.Arguments.Should().NotBeNull();

         arguments.Execute.Arguments.Path.Should().Be("C:\\Path\\File.txt");
         arguments.Execute.Arguments.Silent.Should().BeTrue();
      }

      private T DoMap<T>(string[] args)
         where T : class, new()
      {
         return GetTarget<T>().Map<T>(args);
      }

      [TestMethod]
      public void EnsureAliasesCanBeUsedForCommands()
      {
         var arguments = DoMap<ApplicationCommands>(new[] { "e", "-Path=C:\\Path\\File.txt", "-silent" });

         arguments.Execute.Should().NotBeNull();
         arguments.Execute.Arguments.Should().NotBeNull();

         arguments.Execute.Arguments.Path.Should().Be("C:\\Path\\File.txt");
         arguments.Execute.Arguments.Silent.Should().BeTrue();
      }

      [TestMethod]
      public void EnsureOptionsAreSetInRootArgumentsClass()
      {
         var arguments = DoMap<ApplicationCommands>(new[] { "Execute", "-Path=C:\\Path\\File.txt", "-silent", "-wait" });

         arguments.Execute.Should().NotBeNull();
         arguments.Execute.Arguments.Should().NotBeNull();

         arguments.Execute.Arguments.Path.Should().Be("C:\\Path\\File.txt");
         arguments.Execute.Arguments.Silent.Should().BeTrue();
         arguments.Wait.Should().BeTrue();
      }

      [TestMethod]
      public void EnsureSharedOptionsAreSetInRootArgumentsClassAndCommandClass()
      {
         var arguments = DoMap<ApplicationCommands>(new[] {  "Execute", "-wait" });

         arguments.Execute.Should().NotBeNull();
         arguments.Execute.Arguments.Should().NotBeNull();

         arguments.Wait.Should().BeTrue();
         arguments.Execute.Arguments.Wait.Should().BeTrue();
      }

      [TestMethod]
      public void EnsureSharedArgumentsAreSetInRootArgumentsClassAndCommandClass()
      {
         var arguments = DoMap<ApplicationCommands>(new[] {  "Execute", "-priority=24" });

         arguments.Execute.Should().NotBeNull();
         arguments.Execute.Arguments.Should().NotBeNull();

         arguments.Priority.Should().Be(24);
         arguments.Execute.Arguments.Priority.Should().Be(24);
      }

      [TestMethod]
      public void EnsureSharedArgumentsDontCauseErrorsInRootClass()
      {
         var engine = GetTarget<ApplicationCommands>();
         var monitor = engine.Monitor();
         var arguments = engine.Map<ApplicationCommands>(new[] {  "Execute", "-sharedButAlone=22.4" });

         arguments.Execute.Should().NotBeNull();
         arguments.Execute.Arguments.Should().NotBeNull();

         arguments.Execute.Arguments.SharedButAlone.Should().Be(22.4);
         monitor.Should().NotRaise(nameof(CommandLineEngine.UnhandledCommandLineArgument));
      }

      [TestMethod]
      public void EnsureSharedOptionsDontCauseErrorsInRootClass()
      {
         var engine = GetTarget<ApplicationCommands>();
         var monitor = engine.Monitor();
         var arguments = engine.Map<ApplicationCommands>(new[] {  "Execute", "-sharedOptionAlone" });

         arguments.Execute.Should().NotBeNull();
         arguments.Execute.Arguments.Should().NotBeNull();

         arguments.Execute.Arguments.SharedOptionAlone.Should().BeTrue();
         monitor.Should().NotRaise(nameof(CommandLineEngine.UnhandledCommandLineArgument));
      }

      [TestMethod]
      public void EnsureNonGenericCommandClassWorks()
      {
         var arguments = DoMap<NonGenericApplicationCommands>(new[] { "Execute", "-Path=C:\\Path\\File.txt", "-silent", "-wait" });

         arguments.Execute.Should().NotBeNull();
         arguments.Execute.Arguments.Should().NotBeNull();

         arguments.Execute.Arguments.Path.Should().Be("C:\\Path\\File.txt");
         arguments.Execute.Arguments.Silent.Should().BeTrue();
         arguments.Wait.Should().BeTrue();
      }

      [TestMethod]
      public void EnsureUnmappedOptionsRaiseUnhandledCommandLineArgumentEventCorrectly()
      {
         var engine = GetTarget<ApplicationCommands>();
         var monitor = engine.Monitor();
         engine.Map<ApplicationCommands>(new[] { "execute", "-Path=C:\\Path\\File.txt", "-unknown" });

         monitor.Should().Raise(nameof(CommandLineEngine.UnhandledCommandLineArgument))
            .WithArgs<CommandLineArgumentEventArgs>(e => e.Argument.Name == "unknown");
      }

      [TestMethod]
      public void EnsureUnmappedArgumentsRaiseUnhandledCommandLineArgumentEventCorrectly()
      {
         var engine = GetTarget<ApplicationCommands>();
         var monitor = engine.Monitor();
         engine.Map<ApplicationCommands>(new[] { "execute", "-Path=C:\\Path\\File.txt", "-unknown=666" });

         monitor.Should().Raise(nameof(CommandLineEngine.UnhandledCommandLineArgument))
            .WithArgs<CommandLineArgumentEventArgs>(e => e.Argument.Name == "unknown");
      }

      [TestMethod]
      public void EnsureUnmappedArgumentsForNonGenericCommandRaiseUnhandledCommandLineArgumentEventCorrectly()
      {
         var engine = GetTarget<NonGenericApplicationCommands>();
         var monitor = engine.Monitor();
         engine.Map<NonGenericApplicationCommands>(new[] { "execute", "-unknown=666" });

         monitor.Should().Raise(nameof(CommandLineEngine.UnhandledCommandLineArgument))
            .WithArgs<CommandLineArgumentEventArgs>(e => e.Argument.Name == "unknown" && e.Argument.Index == 0 && e.Argument.Value == "666");

         var args = engine.Map<NonGenericApplicationCommands>(new[] { "execute", "-wait", "-unknown=234" });

         monitor.Should().Raise(nameof(CommandLineEngine.UnhandledCommandLineArgument))
            .WithArgs<CommandLineArgumentEventArgs>(e => e.Argument.Name == "unknown" && e.Argument.Index == 1 && e.Argument.Value == "234");

         args.Wait.Should().BeTrue();
      }

      [TestMethod]
      public void EnsureUnhandledCommandLineArgumentEventIsNotRaisedWhenDefaultCommandIsUsed()
      {
         var engine = GetTarget<ApplicationCommandsWithDefault>();
         var monitor = engine.Monitor();
         engine.Map<ApplicationCommandsWithDefault>(new[] { "path=SomeValue" });

         monitor.Should().NotRaise(nameof(CommandLineEngine.UnhandledCommandLineArgument));
      }

      [TestMethod]
      public void EnsureBaseClassArgumentsAreSetAndDoNotRaisedUnhandledCommandLineArgument()
      {
         var engine = GetTarget<ApplicationCommandsWithBaseClass>();
         var monitor = engine.Monitor();

         var args = engine.Map<ApplicationCommandsWithBaseClass>(new[] { "execute", "-loglevel=Trace" });

         args.LogLevel.Should().Be("Trace");
         monitor.Should().NotRaise(nameof(CommandLineEngine.UnhandledCommandLineArgument));
      }

      [TestMethod]
      public void EnsureMissingCommandLineArgumentsThrowMissingCommandLineArgumentException()
      {
         var engine = GetTarget<ApplicationCommandsWithDefaultAndIndexedArgs>();

         engine.Invoking(x => x.Map<ApplicationCommandsWithDefaultAndIndexedArgs>(new[] { "nam=hans" }))
            .Should().Throw<MissingCommandLineArgumentException>().Where(x => x.Argument == "Name");
      }

      [TestMethod]
      public void EnsureOtherNamedParameterIsNotUsedName()
      {
         var engine = GetTarget<ApplicationCommandsWithDefaultAndIndexedArgs>();

         engine.Invoking(x => x.Map<ApplicationCommandsWithDefaultAndIndexedArgs>(new[] { "path=aPath" }))
            .Should().Throw<MissingCommandLineArgumentException>().Where(x => x.Argument == "Name");
      }

      [TestMethod]
      public void EnsureValuesAreMappedCorrectlyByIndex()
      {
         var engine = GetTarget<ApplicationCommandsWithDefaultAndIndexedArgs>();

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
            Should().NotThrow<MissingCommandLineArgumentException>();

         engine.Invoking(x => x.Map<ApplicationCommandsWithDefaultAndIndexedArgs>(new[] { "hans" })).
            Should().NotThrow<MissingCommandLineArgumentException>();
      }

      [TestMethod]
      public void EnsureNoMissingCommandLineArgumentsWhenParameterIsSpecifiedWithName()
      {
         var engine = GetTarget();

         engine.Invoking(x => x.Map<ApplicationCommandsWithDefaultAndIndexedArgs>(new[] { "Name=hans" })).
            Should().NotThrow<MissingCommandLineArgumentException>();
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

         [Option("Wait", Shared = true)]
         public bool Wait { get; set; }

         [Argument("Priority", Shared = true)]
         public int Priority { get; set; }

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

         [Option("Wait", Shared = true)]
         public bool Wait { get; set; }

         [Argument("Priority", Shared = true)]
         public int Priority { get; set; }

         [Argument("SharedButAlone", Shared = true)]
         public double SharedButAlone { get; set; }

         [Option("SharedOptionAlone", Shared = true)]
         public bool SharedOptionAlone { get; set; }

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