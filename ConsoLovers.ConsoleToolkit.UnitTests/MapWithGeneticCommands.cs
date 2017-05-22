namespace ConsoLovers.UnitTests
{
   using System.Diagnostics.CodeAnalysis;

   using ConsoLovers.ConsoleToolkit.CommandLineArguments;
   using ConsoLovers.UnitTests.ArgumentEngine;

   using FluentAssertions;

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


      #endregion

      internal class ApplicationCommands
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

         [Command("Execute", IsDefaultCommand=true)]
         public GenericCommand<ExecuteArguments> Execute { get; set; }

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

   internal class ExecuteCommand : ICommand<ExecuteArgs>
   {
      public void Execute()
      {
      }

      public ExecuteArgs Arguments { get; set; }
   }
}