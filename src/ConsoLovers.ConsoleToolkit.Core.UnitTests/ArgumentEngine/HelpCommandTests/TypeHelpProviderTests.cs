// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeHelpProviderTests.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.ArgumentEngine.HelpCommandTests;

using System.Diagnostics.CodeAnalysis;
using System.Linq;

using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
public class TypeHelpProviderTests
{
   #region Public Methods and Operators

   [TestMethod]
   public void EnsureHelpProviderFindsPublicAndInternalProperties()
   {
      var provider = new TypeHelpProvider(null, new DefaultFactory());
      var helps = provider.GetHelpForProperties(typeof(ArgsClass)).ToArray();

      helps.Should().HaveCount(3);
   }

   #endregion

   [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
   private class ArgsClass
   {
      #region Public Properties

      [Argument("number")]
      [HelpText("NumberHelp")]
      public int Number { get; set; }

      #endregion

      #region Properties

      [Argument("Double")]
      [HelpText("DoubleHelp")]
      internal double Double { get; set; }

      [Argument("String")]
      [HelpText("StringHelp")]
      internal string String { get; set; }

      #endregion
   }
}