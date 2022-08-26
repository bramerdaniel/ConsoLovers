// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShowWithInitModeMenu.cs" company="ConsoLovers">
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
public class ShowWithInitModeMenu : MenuBuilderBase
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
      commandNode.Nodes.Where(n => n.VisibleInMenu).Should().HaveCount(2);

      commandNode = nodes[1] as ICommandNode;
      Assert.IsNotNull(commandNode);
      commandNode.DisplayName.Should().Be("exit");
      commandNode.Nodes.Where(n => n.VisibleInMenu).Should().HaveCount(1);
   }

   [TestMethod]
   public void EnsureMenuCommandOnlyWorksWithDefaultOptions()
   {
      var nodes = BuildMenu<MenuCommandsOnly>().ToArray();
      nodes.Should().HaveCount(2);

      var commandNode = nodes[0] as ICommandNode;
      Assert.IsNotNull(commandNode);
      commandNode.DisplayName.Should().Be("Run it");
      commandNode.Nodes.Where(n => n.VisibleInMenu).Should().HaveCount(2);

      commandNode = nodes[1] as ICommandNode;
      Assert.IsNotNull(commandNode);
      commandNode.DisplayName.Should().Be("Close it");
      commandNode.Nodes.Where(n => n.VisibleInMenu).Should().HaveCount(3);
   }

   #endregion

   #region Methods

   private IMenuNode[] BuildMenu<T>()
      where T : class
   {
      var options = new MenuBuilderOptions { DefaultArgumentInitializationMode = ArgumentInitializationModes.AsMenu };
      return BuildMenu<T>(options);
   }

   #endregion




}