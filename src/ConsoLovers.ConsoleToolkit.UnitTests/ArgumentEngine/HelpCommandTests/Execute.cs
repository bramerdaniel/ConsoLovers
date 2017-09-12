// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Execute.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.UnitTests.ArgumentEngine.HelpCommandTests
{
   using System.Collections.Generic;
   using System.Diagnostics.CodeAnalysis;

   using ConsoLovers.ConsoleToolkit.CommandLineArguments;
   using ConsoLovers.UnitTests.Setups;

   using Microsoft.VisualStudio.TestTools.UnitTesting;

   using Moq;

   [TestClass]
   [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
   public class Execute
   {
      #region Public Methods and Operators

      [TestMethod]
      public void EnsureArgumentHelpIsPrintedCommand()
      {
         var target = Setup.HelpCommand().WithEngineMock(out var engine).Done();

         target.Arguments = GetArguments<RootArgumentClass>("execute");

         target.Execute();

         engine.Verify(x => x.PrintHelp(typeof(CommandArgumentClass), null), Times.Once);
      }

      [TestMethod]
      public void EnsureDetailedHelpForCommandArgumentClassIsPrinted()
      {
         var target = Setup.HelpCommand().WithEngineMock(out var engine).Done();

         target.Arguments = GetArguments<RootArgumentClass>("execute", "path");

         target.Execute();

         engine.Verify(x => x.PrintHelp(typeof(CommandArgumentClass).GetProperty("Path"), null), Times.Once);
      }

      [TestMethod]
      public void EnsureHelpIsPrintedForPrimitiveTypesInRootArgumentClass()
      {
         var target = Setup.HelpCommand().WithEngineMock(out var engine).Done();

         target.Arguments = GetArguments<RootArgumentClass>("number");

         target.Execute();

         engine.Verify(x => x.PrintHelp(typeof(RootArgumentClass).GetProperty("Number"), null), Times.Once);
      }

      [TestMethod]
      public void EnsureTypeHelpIsPrintedForNoArguments()
      {
         var target = Setup.HelpCommand().WithEngineMock(out var engine).Done();

         target.Arguments = GetArguments<RootArgumentClass>();

         target.Execute();

         engine.Verify(x => x.PrintHelp(typeof(RootArgumentClass), null), Times.Once);
      }

      #endregion

      #region Methods

      private static HelpCommandArguments GetArguments<T>(params string[] args)
      {
         var argumentDictionary = new Dictionary<string, CommandLineArgument>();
         int index = 0;
         foreach (var arg in args)
         {
            argumentDictionary[arg] = new CommandLineArgument { Index = index, Name = arg };
            index++;
         }

         return new HelpCommandArguments { ArgumentInfos = new ArgumentClassInfo(typeof(T)), ArgumentDictionary = argumentDictionary };
      }

      #endregion
   }
}