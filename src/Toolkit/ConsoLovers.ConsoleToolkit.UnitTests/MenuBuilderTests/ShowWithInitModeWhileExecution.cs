// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShowWithInitModeWhileExecution.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.UnitTests.MenuBuilderTests;

using System.Linq;

using ConsoLovers.ConsoleToolkit.Core;
using ConsoLovers.ConsoleToolkit.Core.MenuBuilding;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class ShowWithInitModeWhileExecution : MenuBuilderBase
{
   #region Public Methods and Operators

   [TestMethod]
   public void EnsureCommandOnlyWorksWithDefaultOptions()
   {
      var nodes = BuildMenu<CommandsOnly>().ToArray();
      nodes.Should().HaveCount(2);

      var commandNode = nodes[0] as ICommandNode;
      Assert.IsNotNull(commandNode);
      commandNode.DisplayName.Should().Be("run");
      commandNode.Nodes.Should().HaveCount(2);
      commandNode.Nodes.Where(n => n.VisibleInMenu).Should().HaveCount(0);
   }

   [TestMethod]
   public void EnsureMenuCommandOnlyWorksCorrectly()
   {
      var nodes = BuildMenu<MenuCommandsOnly>().ToArray();
      nodes.Should().HaveCount(2);

      var commandNode = nodes[0] as ICommandNode;
      Assert.IsNotNull(commandNode);
      commandNode.DisplayName.Should().Be("Run it");
      commandNode.Nodes.Should().HaveCount(2);
      commandNode.Nodes.Where(n => n.VisibleInMenu).Should().HaveCount(0);

      commandNode = nodes[1] as ICommandNode;
      Assert.IsNotNull(commandNode);
      commandNode.DisplayName.Should().Be("Close it");
      commandNode.Nodes.Should().HaveCount(3);
      commandNode.Nodes.Where(n => n.VisibleInMenu).Should().HaveCount(0);
   }

   #endregion

   #region Methods

   private IMenuNode[] BuildMenu<T>()
      where T : class
   {
      var options = new MenuBuilderOptions { DefaultArgumentInitializationMode = ArgumentInitializationModes.WhileExecution };
      return BuildMenu<T>(options);
   }

   #endregion
}