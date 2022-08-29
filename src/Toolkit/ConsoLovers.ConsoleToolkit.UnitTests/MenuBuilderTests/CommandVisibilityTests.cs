// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandVisibilityTests.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.UnitTests.MenuBuilderTests;

using System.Linq;

using ConsoLovers.ConsoleToolkit.Core;

using FluentAssertions;

using JetBrains.Annotations;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class CommandVisibilityTests : MenuBuilderBase
{
   #region Public Methods and Operators

   [TestMethod]
   public void EnsureShowAllCommandWorksCorrectly()
   {
      var menuBehaviour = MenuBuilderBehaviour.ShowAllCommand;

      var node = GetCommandNode<Root>(menuBehaviour, "WithAttribute");
      node.IsVisible.Should().BeTrue();

      node = GetCommandNode<Root>(menuBehaviour, "WithoutAttribute");
      node.IsVisible.Should().BeTrue();
      
      node = GetCommandNode<Root>(menuBehaviour, "Visible");
      node.IsVisible.Should().BeTrue();

      node = GetCommandNode<Root>(menuBehaviour, "NotSpecified");
      node.IsVisible.Should().BeTrue();
      
      node = GetCommandNode<Root>(menuBehaviour, "Hidden");
      node.IsVisible.Should().BeFalse();
   }

   [TestMethod]
   public void EnsureWithAttributesOnlyWorksCorrectly()
   {
      var menuBehaviour = MenuBuilderBehaviour.WithAttributesOnly;
      
      var node = GetCommandNode<Root>(menuBehaviour, "WithoutAttribute");
      node.IsVisible.Should().BeFalse();

      node = GetCommandNode<Root>(menuBehaviour, "Hidden");
      node.IsVisible.Should().BeFalse();

      node = GetCommandNode<Root>(menuBehaviour, "NotSpecified");
      node.IsVisible.Should().BeTrue();
      
      node = GetCommandNode<Root>(menuBehaviour, "WithAttribute");
      node.IsVisible.Should().BeTrue();

      node = GetCommandNode<Root>(menuBehaviour, "Visible");
      node.IsVisible.Should().BeTrue();
   }
   
   #endregion

   #region Methods

   CommandNode GetCommandNode<T>(MenuBuilderBehaviour menuBehaviour, string displayName)
      where T : class
   {
      var nodes = BuildMenu<T>(new MenuBuilderOptions { MenuBehaviour = menuBehaviour }).ToArray();
      var node = nodes.OfType<CommandNode>().FirstOrDefault(x => x.DisplayName == displayName);
      Assert.IsNotNull(node);
      return node;
   }

   #endregion

   public class Root
   {
      #region Properties

      [Command("WithAttribute")]
      [MenuCommand("WithAttribute")]
      [UsedImplicitly]
      internal RunCommand WithAttribute { get; set; }

      [Command("WithoutAttribute")]
      [UsedImplicitly]
      internal RunCommand WithoutAttribute { get; set; }

      [Command("Hidden")]
      [MenuCommand("Hidden", Visibility = CommandVisibility.Hidden)]
      [UsedImplicitly]
      internal RunCommand Hidden { get; set; }

      [Command("Visible")]
      [MenuCommand("Visible", Visibility = CommandVisibility.Visible)]
      [UsedImplicitly]
      internal RunCommand Visible { get; set; }

      [Command("NotSpecified")]
      [MenuCommand("NotSpecified", Visibility = CommandVisibility.NotSpecified)]
      [UsedImplicitly]
      internal RunCommand NotSpecified { get; set; }

      #endregion
   }

   [UsedImplicitly]
   internal class RunCommand : ICommand<RunCommand.Args>
   {
      #region ICommand<Args> Members

      public void Execute()
      {
      }

      public Args Arguments { get; set; }

      #endregion

      internal class Args
      {
         #region Public Properties

         [Argument("initOnly")]
         [MenuArgument("initOnly", Visibility = ArgumentVisibility.InInitialization)]
         public int InitOnly { get; set; }

         [Argument("noAttribute")]
         public int NoAttribute { get; set; }

         [Argument("noVisibility")]
         [MenuArgument("noVisibility")]
         public int NoVisibility { get; set; }

         [Argument("visibleInMenu")]
         [MenuArgument("visibleInMenu", Visibility = ArgumentVisibility.InMenu)]
         public int VisibleInMenu { get; set; }

         #endregion
      }
   }
}