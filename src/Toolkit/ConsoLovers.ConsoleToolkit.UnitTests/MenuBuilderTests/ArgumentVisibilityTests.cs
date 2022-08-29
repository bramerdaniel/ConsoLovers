// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArgumentVisibilityTests.cs" company="ConsoLovers">
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
public class ArgumentVisibilityTests : MenuBuilderBase
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
      var node = GetArgumentNode<RootWithMenu>(ArgumentInitializationModes.AsMenu, nameof(RunCommand.Args.NoAttribute));

      node.ShowInInitialization.Should().BeTrue();
      node.ShowInMenu.Should().BeTrue();
   }

   [TestMethod]
   public void EnsureCorrectVisibilityForInitModeWhileExecution()
   {
      var node = GetArgumentNode<RootWithMenu>(ArgumentInitializationModes.WhileExecution, nameof(RunCommand.Args.NoAttribute));

      node.ShowInInitialization.Should().BeTrue();
      node.ShowInMenu.Should().BeTrue();
   }
   
   [TestMethod]
   public void EnsureCorrectVisibilityForInitModeCustom()
   {
      var node = GetArgumentNode<RootWithMenu>(ArgumentInitializationModes.Custom, nameof(RunCommand.Args.NoAttribute));

      node.ShowInInitialization.Should().BeTrue();
      node.ShowInMenu.Should().BeTrue();
   }

   [TestMethod]
   public void ExplicitMenuVisibility()
   {
      var node = GetArgumentNode<RootWithMenu>(ArgumentInitializationModes.AsMenu, nameof(RunCommand.Args.VisibleInMenu));
      node.ShowInInitialization.Should().BeFalse();
      node.ShowInMenu.Should().BeTrue();

      node = GetArgumentNode<RootWithMenu>(ArgumentInitializationModes.WhileExecution, nameof(RunCommand.Args.VisibleInMenu));
      node.ShowInInitialization.Should().BeFalse();
      node.ShowInMenu.Should().BeTrue();

      node = GetArgumentNode<RootWithMenu>(ArgumentInitializationModes.Custom, nameof(RunCommand.Args.VisibleInMenu));
      node.ShowInInitialization.Should().BeFalse();
      node.ShowInMenu.Should().BeTrue();
   }

   [TestMethod]
   public void ExplicitInitializationVisibility()
   {
      var node = GetArgumentNode<RootWithMenu>(ArgumentInitializationModes.AsMenu, nameof(RunCommand.Args.InitOnly));
      node.ShowInInitialization.Should().BeTrue();
      node.ShowInMenu.Should().BeFalse();

      node = GetArgumentNode<RootWithMenu>(ArgumentInitializationModes.WhileExecution, nameof(RunCommand.Args.InitOnly));
      node.ShowInInitialization.Should().BeTrue();
      node.ShowInMenu.Should().BeFalse();

      node = GetArgumentNode<RootWithMenu>(ArgumentInitializationModes.Custom, nameof(RunCommand.Args.InitOnly));
      node.ShowInInitialization.Should().BeTrue();
      node.ShowInMenu.Should().BeFalse();
   }

   [TestMethod]
   public void WithoutAttributeInitializationVisibility()
   {
      var node = GetArgumentNode<RootWithMenu>(ArgumentInitializationModes.AsMenu, nameof(RunCommand.Args.NoAttribute));
      node.ShowInInitialization.Should().BeTrue();
      node.ShowInMenu.Should().BeTrue();

      node = GetArgumentNode<RootWithMenu>(ArgumentInitializationModes.WhileExecution, nameof(RunCommand.Args.NoAttribute));
      node.ShowInInitialization.Should().BeTrue();
      node.ShowInMenu.Should().BeTrue();

      node = GetArgumentNode<RootWithMenu>(ArgumentInitializationModes.Custom, nameof(RunCommand.Args.NoAttribute));
      node.ShowInInitialization.Should().BeTrue();
      node.ShowInMenu.Should().BeTrue();
   }

   [TestMethod]
   public void AttributeWithoutVisibility()
   {
      var node = GetArgumentNode<RootWithMenu>(ArgumentInitializationModes.AsMenu, nameof(RunCommand.Args.NoVisibility));
      node.ShowInInitialization.Should().BeTrue();
      node.ShowInMenu.Should().BeTrue();

      node = GetArgumentNode<RootWithMenu>(ArgumentInitializationModes.WhileExecution, nameof(RunCommand.Args.NoVisibility));
      node.ShowInInitialization.Should().BeTrue();
      node.ShowInMenu.Should().BeTrue();

      node = GetArgumentNode<RootWithMenu>(ArgumentInitializationModes.Custom, nameof(RunCommand.Args.NoVisibility));
      node.ShowInInitialization.Should().BeTrue();
      node.ShowInMenu.Should().BeTrue();
   }

   [TestMethod]
   public void EnsureIsVisibleInMenuWhenNoAttributeWasSpecified()
   {
      var node = GetArgumentNode<RootWithMenu>(ArgumentInitializationModes.AsMenu, nameof(RunCommand.Args.NoAttribute));
      node.ShowInMenu.Should().BeTrue();
      node.ShowInInitialization.Should().BeTrue();

      node = GetArgumentNode<RootWithMenu>(ArgumentInitializationModes.AsMenu, nameof(RunCommand.Args.NoVisibility));
      node.ShowInMenu.Should().BeTrue();
      node.ShowInInitialization.Should().BeTrue();
   }


   ArgumentNode GetArgumentNode<T>(ArgumentInitializationModes initMode, string argumentName)
      where T : class
   {
      var nodes = BuildMenu<T>(initMode).ToArray();
      var runNode = nodes.Single() as CommandNode;
      Assert.IsNotNull(runNode);
      return runNode.Nodes.FirstOrDefault(x => x.DisplayName == argumentName) as ArgumentNode;
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
      [MenuCommand("Run", ArgumentInitialization = ArgumentInitializationModes.AsMenu)]
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

         [Argument("NoAttribute")]
         public int NoAttribute { get; set; }

         [Argument("NoVisibility")]
         [MenuArgument("NoVisibility")]
         public int NoVisibility { get; set; }

         [Argument("VisibleInMenu")]
         [MenuArgument("VisibleInMenu", Visibility = ArgumentVisibility.InMenu)]
         public int VisibleInMenu { get; set; }

         [Argument("InitOnly")]
         [MenuArgument("InitOnly", Visibility = ArgumentVisibility.InInitialization)]
         public int InitOnly { get; set; }

         #endregion
      }
   }
}