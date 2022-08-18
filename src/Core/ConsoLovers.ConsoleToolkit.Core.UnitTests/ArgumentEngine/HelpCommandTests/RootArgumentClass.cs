// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandArguments.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.ArgumentEngine.HelpCommandTests
{
   using System.Diagnostics.CodeAnalysis;

   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

   [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
   internal class RootArgumentClass
   {
      #region Public Properties

      [Command("execute")]
      public GenericCommand<CommandArgumentClass> Execute { get; set; }

      [Argument("number")]
      [HelpText("NumberHelp")]
      public int Number { get; set; }

      #endregion
   }

   [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1402:FileMayOnlyContainASingleClass", Justification = "Reviewed. Suppression is OK here.")]
   [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
   internal class CommandArgumentClass
   {
      #region Public Properties

      [Argument("Path")]
      [HelpText("PathHelp")]
      public string Path { get; set; }

      [Option("silent")]
      [HelpText("SilentHelp")]
      public bool Silent { get; set; }

      #endregion
   }
}