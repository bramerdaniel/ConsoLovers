// --------------------------------------------------------------------------------------------------------------------
// <copyright file="VisibilityTests.cs" company="ConsoLovers">
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
public class VisibilityTests : MenuBuilderBase
{
   #region Public Methods and Operators

   private IMenuNode[] BuildMenu<T>(ArgumentInitializationModes initMode)
      where T : class
   {
      return BuildMenu<T>(new MenuBuilderOptions{ ArgumentInitializationMode = initMode });
   }
   
   [TestMethod]
   public void EnsureCorrectVisibilityForInitModeAsMenu()
   {
      var node = GetArgumentNode<RootWithMenu>(ArgumentInitializationModes.AsMenu, "noAttribute");

      node.ShowInInitialization.Should().BeFalse();
      node.ShowAsMenu.Should().BeTrue();
   }

   [TestMethod]
   public void EnsureCorrectVisibilityForInitModeWhileExecution()
   {
      var node = GetArgumentNode<RootWithMenu>(ArgumentInitializationModes.WhileExecution, "noAttribute");

      node.ShowInInitialization.Should().BeTrue();
      node.ShowAsMenu.Should().BeFalse();
   }
   
   [TestMethod]
   public void EnsureCorrectVisibilityForInitModeCustom()
   {
      var node = GetArgumentNode<RootWithMenu>(ArgumentInitializationModes.Custom, "noAttribute");

      node.ShowInInitialization.Should().BeFalse();
      node.ShowAsMenu.Should().BeFalse();
   }

   [TestMethod]
   public void ExplicitMenuVisibility()
   {
      var node = GetArgumentNode<RootWithMenu>(ArgumentInitializationModes.AsMenu, "visibleInMenu");
      node.ShowInInitialization.Should().BeFalse();
      node.ShowAsMenu.Should().BeTrue();

      node = GetArgumentNode<RootWithMenu>(ArgumentInitializationModes.WhileExecution, "visibleInMenu");
      node.ShowInInitialization.Should().BeFalse();
      node.ShowAsMenu.Should().BeTrue();

      node = GetArgumentNode<RootWithMenu>(ArgumentInitializationModes.Custom, "visibleInMenu");
      node.ShowInInitialization.Should().BeFalse();
      node.ShowAsMenu.Should().BeTrue();
   }

   [TestMethod]
   public void ExplicitInitializationVisibility()
   {
      var node = GetArgumentNode<RootWithMenu>(ArgumentInitializationModes.AsMenu, "initOnly");
      node.ShowInInitialization.Should().BeTrue();
      node.ShowAsMenu.Should().BeFalse();

      node = GetArgumentNode<RootWithMenu>(ArgumentInitializationModes.WhileExecution, "initOnly");
      node.ShowInInitialization.Should().BeTrue();
      node.ShowAsMenu.Should().BeFalse();

      node = GetArgumentNode<RootWithMenu>(ArgumentInitializationModes.Custom, "initOnly");
      node.ShowInInitialization.Should().BeTrue();
      node.ShowAsMenu.Should().BeFalse();
   }

   [TestMethod]
   public void WithoutAttributeInitializationVisibility()
   {
      var node = GetArgumentNode<RootWithMenu>(ArgumentInitializationModes.AsMenu, "noAttribute");
      node.ShowInInitialization.Should().BeFalse();
      node.ShowAsMenu.Should().BeTrue();

      node = GetArgumentNode<RootWithMenu>(ArgumentInitializationModes.WhileExecution, "noAttribute");
      node.ShowInInitialization.Should().BeTrue();
      node.ShowAsMenu.Should().BeFalse();

      node = GetArgumentNode<RootWithMenu>(ArgumentInitializationModes.Custom, "noAttribute");
      node.ShowInInitialization.Should().BeFalse();
      node.ShowAsMenu.Should().BeFalse();
   }

   [TestMethod]
   public void AttributeWithoutVisibility()
   {
      var node = GetArgumentNode<RootWithMenu>(ArgumentInitializationModes.AsMenu, "noVisibility");
      node.ShowInInitialization.Should().BeFalse();
      node.ShowAsMenu.Should().BeTrue();

      node = GetArgumentNode<RootWithMenu>(ArgumentInitializationModes.WhileExecution, "noVisibility");
      node.ShowInInitialization.Should().BeTrue();
      node.ShowAsMenu.Should().BeFalse();

      node = GetArgumentNode<RootWithMenu>(ArgumentInitializationModes.Custom, "noVisibility");
      node.ShowInInitialization.Should().BeFalse();
      node.ShowAsMenu.Should().BeFalse();
   }

   [TestMethod]
   public void Test()
   {
      var node = GetCommandNode<RootWithMenu>(MenuBuilderBehaviour.ShowAllCommand, "Run");
      node.IsVisible.Should().BeTrue();

      node = GetCommandNode<RootWithMenu>(MenuBuilderBehaviour.WithAttributesOnly, "Run");
      node.IsVisible.Should().BeTrue();

      node = GetCommandNode<NormalCommand>(MenuBuilderBehaviour.ShowAllCommand, "Run");
      node.IsVisible.Should().BeTrue();

      node = GetCommandNode<NormalCommand>(MenuBuilderBehaviour.WithAttributesOnly, "Run");
      node.IsVisible.Should().BeFalse();
   }

   ArgumentNode GetArgumentNode<T>(ArgumentInitializationModes initMode, string argumentName)
      where T : class
   {
      var nodes = BuildMenu<T>(initMode).ToArray();
      var runNode = nodes.Single() as CommandNode;
      Assert.IsNotNull(runNode);
      return runNode.Nodes.FirstOrDefault(x => x.DisplayName == argumentName) as ArgumentNode;
   }


   CommandNode GetCommandNode<T>(MenuBuilderBehaviour menuBehaviour, string displayName)
      where T : class
   {
      var nodes = BuildMenu<T>(new MenuBuilderOptions{ MenuBehaviour = menuBehaviour}).ToArray();
      var node = nodes.OfType<CommandNode>().FirstOrDefault(x => x.DisplayName == displayName);
      Assert.IsNotNull(node);
      return node;
   }

   #endregion

   public class NormalCommand
   {
      #region Properties

      [Command("Run")]
      [UsedImplicitly]
      internal RunCommand Run { get; set; }

      #endregion
   }

   public class RootWithMenu
   {
      #region Properties

      [Command("Run")]
      [MenuCommand("Run", ArgumentInitializationMode = ArgumentInitializationModes.AsMenu)]
      [UsedImplicitly]
      internal RunCommand Run { get; set; }

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

         [Argument("noAttribute")]
         public int NoAttribute { get; set; }

         [Argument("noVisibility")]
         [MenuArgument("noVisibility")]
         public int NoVisibility { get; set; }

         [Argument("visibleInMenu")]
         [MenuArgument("visibleInMenu", Visibility = ArgumentVisibility.InMenu)]
         public int VisibleInMenu { get; set; }

         [Argument("initOnly")]
         [MenuArgument("initOnly", Visibility = ArgumentVisibility.InInitialization)]
         public int InitOnly { get; set; }

         #endregion
      }
   }
}