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

      var node = GetCommandNode<VisibilityRoot>(menuBehaviour, "WithAttribute");
      node.IsVisible.Should().BeTrue();

      node = GetCommandNode<VisibilityRoot>(menuBehaviour, "WithoutAttribute");
      node.IsVisible.Should().BeTrue();
      
      node = GetCommandNode<VisibilityRoot>(menuBehaviour, "Visible");
      node.IsVisible.Should().BeTrue();

      node = GetCommandNode<VisibilityRoot>(menuBehaviour, "NotSpecified");
      node.IsVisible.Should().BeTrue();
      
      node = GetCommandNode<VisibilityRoot>(menuBehaviour, "Hidden");
      node.IsVisible.Should().BeFalse();
   }

   [TestMethod]
   public void EnsureWithAttributesOnlyWorksCorrectly()
   {
      var menuBehaviour = MenuBuilderBehaviour.WithAttributesOnly;
      
      var node = GetCommandNode<VisibilityRoot>(menuBehaviour, "WithoutAttribute");
      node.IsVisible.Should().BeFalse();

      node = GetCommandNode<VisibilityRoot>(menuBehaviour, "Hidden");
      node.IsVisible.Should().BeFalse();

      node = GetCommandNode<VisibilityRoot>(menuBehaviour, "NotSpecified");
      node.IsVisible.Should().BeTrue();
      
      node = GetCommandNode<VisibilityRoot>(menuBehaviour, "WithAttribute");
      node.IsVisible.Should().BeTrue();

      node = GetCommandNode<VisibilityRoot>(menuBehaviour, "Visible");
      node.IsVisible.Should().BeTrue();
   }

   [TestMethod]
   public void EnsureDefaultValueWhileExecutionWorksCorrectly()
   {
      var initMode = ArgumentInitializationModes.WhileExecution;

      var node = GetCommandNode<ArgumentsRoot>(initMode, "WhileExecution");
      node.InitializationMode.Should().Be(ArgumentInitializationModes.WhileExecution);

      node = GetCommandNode<ArgumentsRoot>(initMode, "AsMenu");
      node.InitializationMode.Should().Be(ArgumentInitializationModes.AsMenu);

      node = GetCommandNode<ArgumentsRoot>(initMode, "NotSpecified");
      node.InitializationMode.Should().Be(ArgumentInitializationModes.WhileExecution);

      node = GetCommandNode<ArgumentsRoot>(initMode, "WithoutValue");
      node.InitializationMode.Should().Be(ArgumentInitializationModes.WhileExecution);

      node = GetCommandNode<ArgumentsRoot>(initMode, "WithoutAttribute");
      node.InitializationMode.Should().Be(ArgumentInitializationModes.WhileExecution);
   }

   [TestMethod]
   public void EnsureDefaultValueCustomWorksCorrectly()
   {
      var initMode = ArgumentInitializationModes.Custom;

      var node = GetCommandNode<ArgumentsRoot>(initMode, "WhileExecution");
      node.InitializationMode.Should().Be(ArgumentInitializationModes.WhileExecution);

      node = GetCommandNode<ArgumentsRoot>(initMode, "AsMenu");
      node.InitializationMode.Should().Be(ArgumentInitializationModes.AsMenu);

      node = GetCommandNode<ArgumentsRoot>(initMode, "NotSpecified");
      node.InitializationMode.Should().Be(ArgumentInitializationModes.Custom);

      node = GetCommandNode<ArgumentsRoot>(initMode, "WithoutValue");
      node.InitializationMode.Should().Be(ArgumentInitializationModes.Custom);

      node = GetCommandNode<ArgumentsRoot>(initMode, "WithoutAttribute");
      node.InitializationMode.Should().Be(ArgumentInitializationModes.Custom);
   }

   [TestMethod]
   public void EnsureDefaultValueAsMenuWorksCorrectly()
   {
      var initMode = ArgumentInitializationModes.AsMenu;

      var node = GetCommandNode<ArgumentsRoot>(initMode, "WhileExecution");
      node.InitializationMode.Should().Be(ArgumentInitializationModes.WhileExecution);

      node = GetCommandNode<ArgumentsRoot>(initMode, "AsMenu");
      node.InitializationMode.Should().Be(ArgumentInitializationModes.AsMenu);

      node = GetCommandNode<ArgumentsRoot>(initMode, "NotSpecified");
      node.InitializationMode.Should().Be(ArgumentInitializationModes.AsMenu);

      node = GetCommandNode<ArgumentsRoot>(initMode, "WithoutValue");
      node.InitializationMode.Should().Be(ArgumentInitializationModes.AsMenu);

      node = GetCommandNode<ArgumentsRoot>(initMode, "WithoutAttribute");
      node.InitializationMode.Should().Be(ArgumentInitializationModes.AsMenu);
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

   CommandNode GetCommandNode<T>(ArgumentInitializationModes initMode, string displayName)
      where T : class
   {
      var nodes = BuildMenu<T>(new MenuBuilderOptions { ArgumentInitializationMode = initMode }).ToArray();
      var node = nodes.OfType<CommandNode>().FirstOrDefault(x => x.DisplayName == displayName);
      Assert.IsNotNull(node);
      return node;
   }

   #endregion

   public class ArgumentsRoot
   {
      #region Properties

      [Command("WhileExecution")]
      [MenuCommand("WhileExecution", ArgumentInitialization = ArgumentInitializationModes.WhileExecution)]
      [UsedImplicitly]
      internal RunCommand WhileExecution { get; set; }

      [Command("AsMenu")]
      [MenuCommand("AsMenu", ArgumentInitialization = ArgumentInitializationModes.AsMenu)]
      [UsedImplicitly]
      internal RunCommand AsMenu { get; set; }

      [Command("NotSpecified")]
      [MenuCommand("NotSpecified", ArgumentInitialization = ArgumentInitializationModes.NotSpecified)]
      [UsedImplicitly]
      internal RunCommand NotSpecified { get; set; }

      [Command("WithoutValue")]
      [MenuCommand("WithoutValue")]
      [UsedImplicitly]
      internal RunCommand WithoutValue { get; set; }

      [Command("WithoutAttribute")]
      [UsedImplicitly]
      internal RunCommand WithoutAttribute { get; set; }

      #endregion
   }

   public class VisibilityRoot
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