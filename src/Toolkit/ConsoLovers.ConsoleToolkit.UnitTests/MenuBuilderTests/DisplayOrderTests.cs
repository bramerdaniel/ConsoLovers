// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DisplayOrderTests.cs" company="ConsoLovers">
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
public class DisplayOrderTests : MenuBuilderBase
{
   #region Public Methods and Operators

   [TestMethod]
   public void EnsureArgumentsAreSortedCorrectly()
   {
      var nodes = BuildMenu<SortedArgs>().ToArray();
      nodes.Should().HaveCount(3);

      nodes[0].DisplayName.Should().Be("first");
      nodes[1].DisplayName.Should().Be("second");
      nodes[2].DisplayName.Should().Be("third");
   }

   [TestMethod]
   public void EnsureCommandAreSortedByDisplayOrder()
   {
      var nodes = BuildMenu<SortedCommands>().ToArray();
      nodes.Should().HaveCount(3);

      nodes[0].DisplayName.Should().Be("first");
      nodes[1].DisplayName.Should().Be("second");
      nodes[2].DisplayName.Should().Be("third");
   }

   [TestMethod]
   public void EnsureCommandWithoutOrderAreLast()
   {
      var nodes = BuildMenu<MixedCommands>().ToArray();
      nodes.Should().HaveCount(3);

      nodes[0].DisplayName.Should().Be("first");
      nodes[1].DisplayName.Should().Be("third");
      nodes[2].DisplayName.Should().Be("second");
   }

   [TestMethod]
   public void EnsureMixedArgumentsAreSortedCorrectly()
   {
      var nodes = BuildMenu<PartialSortedArgs>().ToArray();
      nodes.Should().HaveCount(3);

      nodes[0].DisplayName.Should().Be("third");
      nodes[1].DisplayName.Should().Be("Sec");
      nodes[2].DisplayName.Should().Be("Last");
   }

   [TestMethod]
   public void EnsureOneCommandCanBeSortedToTheTop()
   {
      var nodes = BuildMenu<OneShouldGoFirst>().ToArray();
      nodes.Should().HaveCount(3);

      nodes[0].DisplayName.Should().Be("First");
      nodes[1].DisplayName.Should().Be("Second");
      nodes[2].DisplayName.Should().Be("Third");
   }

   [TestMethod]
   public void EnsureOneCommandCanBeSortedToTheBottom()
   {
      var nodes = BuildMenu<OneShouldGoLast>().ToArray();
      nodes.Should().HaveCount(3);

      nodes[0].DisplayName.Should().Be("First");
      nodes[1].DisplayName.Should().Be("Second");
      nodes[2].DisplayName.Should().Be("Last");
   }

   #endregion

   #region Methods

   public class OneShouldGoLast
   {
      #region Properties

      [Command("Last")]
      [MenuCommand("Last", DisplayOrder = int.MaxValue - 100)]
      [UsedImplicitly]
      internal Command Last { get; set; }

      [Command("first")]
      [MenuCommand("First")]
      [UsedImplicitly]
      internal Command First { get; set; }

      [Command("second")]
      [MenuCommand("Second")]
      [UsedImplicitly]
      internal Command Second { get; set; }

      #endregion
   }

   public class OneShouldGoFirst
   {
      #region Properties


      [Command("second")]
      [MenuCommand("Second")]
      [UsedImplicitly]
      internal Command Second { get; set; }

      [Command("third")]
      [MenuCommand("Third")]
      [UsedImplicitly]
      internal Command Third { get; set; }

      [Command("first")]
      [MenuCommand("First", DisplayOrder = 1)]
      [UsedImplicitly]
      internal Command First { get; set; }

      #endregion
   }

   private IMenuNode[] BuildMenu<T>()
      where T : class
   {
      return BuildMenu<T>(new MenuBuilderOptions());
   }

   #endregion

   private class MixedCommands
   {
      #region Public Properties

      [Command("first")]
      [MenuCommand(DisplayOrder = 10)]
      [UsedImplicitly]
      public Command First { get; set; }

      [Command("second")]
      [UsedImplicitly]
      public Command Second { get; set; }

      [Command("third")]
      [MenuCommand(DisplayOrder = 30)]
      [UsedImplicitly]
      public Command Third { get; set; }

      #endregion
   }

   [UsedImplicitly]
   private class PartialSortedArgs
   {
      #region Public Properties

      [Argument("Last")]
      [UsedImplicitly]
      public int Last { get; set; }

      [Argument("second")]
      [MenuArgument("Sec")]
      [UsedImplicitly]
      public int Second { get; set; }

      [Argument("third")]
      [MenuArgument(DisplayOrder = 2000)]
      [UsedImplicitly]
      public int Third { get; set; }

      #endregion
   }

   [UsedImplicitly]
   private class SortedArgs
   {
      #region Public Properties

      [Argument("first")]
      [MenuArgument(DisplayOrder = 1)]
      [UsedImplicitly]
      public int First { get; set; }

      [Argument("second")]
      [MenuArgument(DisplayOrder = 2)]
      [UsedImplicitly]
      public int Second { get; set; }

      [Argument("third")]
      [MenuArgument(DisplayOrder = 3)]
      [UsedImplicitly]
      public int Third { get; set; }

      #endregion
   }

   [UsedImplicitly]
   private class SortedCommands
   {
      #region Public Properties

      [Command("first")]
      [MenuCommand(DisplayOrder = 10)]
      [UsedImplicitly]
      public Command First { get; set; }

      [Command("second")]
      [MenuCommand(DisplayOrder = 20)]
      [UsedImplicitly]
      public Command Second { get; set; }

      [Command("third")]
      [MenuCommand(DisplayOrder = 30)]
      [UsedImplicitly]
      public Command Third { get; set; }

      #endregion
   }
}