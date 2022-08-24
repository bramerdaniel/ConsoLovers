﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleMenu.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Menu
{
   using System;

   using ConsoLovers.ConsoleToolkit.Core;

   using JetBrains.Annotations;

   public class ConsoleMenu : ConsoleMenuBase
   {
      #region Constants and Fields

      private readonly ConsoleColor sharedBackground = ConsoleColor.Black;

      private readonly ConsoleColor sharedForeground = ConsoleColor.Gray;

      #endregion

      #region Constructors and Destructors

      /// <summary>Initializes a new instance of the <see cref="ConsoleMenu"/> class.</summary>
      public ConsoleMenu()
      : base()
      {
      }

      /// <summary>Initializes a new instance of the <see cref="ConsoleMenu"/> class.</summary>
      /// <param name="console">The <see cref="IConsole"/> proxy.</param>
      public ConsoleMenu([NotNull] IConsole console)
         : base(console)
      {
      }

      #endregion

      #region Methods

      protected override ConsoleColor GetConsoleBackground()
      {
         return sharedBackground;
      }

      protected override ConsoleColor GetExpanderBackground(bool isSelected, bool disabled, bool mouseOver)
      {
         return isSelected ? ConsoleColor.White : ConsoleColor.Black;
      }

      protected override ConsoleColor GetExpanderForeground(bool isSelected, bool disabled, bool mouseOver)
      {
         return GetSharedForeground(isSelected, disabled);
      }

      protected override ConsoleColor GetFooterBackground()
      {
         return sharedBackground;
      }

      protected override ConsoleColor GetFooterForeground()
      {
         return sharedForeground;
      }

      protected override ConsoleColor GetHeaderBackground()
      {
         return sharedBackground;
      }

      protected override ConsoleColor GetHeaderForeground()
      {
         return sharedForeground;
      }

      protected override ConsoleColor GetHintBackground(bool isSelected, bool disabled)
      {
         return ConsoleColor.Red;
      }

      protected override ConsoleColor GetHintForeground(bool isSelected, bool disabled)
      {
         return ConsoleColor.White;
      }

      protected override ConsoleColor GetMenuItemBackground(bool isSelected, bool disabled, bool mouseOver, ConsoleColor? elementBackground)
      {
         if (mouseOver && !isSelected)
            return GetMouseOverBackground();

         if (elementBackground.HasValue)
            return elementBackground.Value;

         return isSelected ? ConsoleColor.White : sharedBackground;
      }

      protected override ConsoleColor GetMenuItemForeground(bool isSelected, bool disabled, bool mouseOver, ConsoleColor? elementForeground)
      {
         if (mouseOver)
            return GetMouseOverForeground();

         if (elementForeground.HasValue)
            return elementForeground.Value;

         return GetSharedForeground(isSelected, disabled);
      }

      protected override ConsoleColor GetMouseOverBackground()
      {
         return ConsoleColor.Gray;
      }

      protected override ConsoleColor GetMouseOverForeground()
      {
         return ConsoleColor.Black;
      }

      protected override ConsoleColor GetSelectorBackground(bool isSelected, bool disabled, bool mouseOver)
      {
         if (mouseOver && !isSelected)
            return GetMouseOverBackground();

         return isSelected ? ConsoleColor.White : ConsoleColor.Black;
      }

      protected override ConsoleColor GetSelectorForeground(bool isSelected, bool disabled, bool mouseOver)
      {
         return ConsoleColor.Black;
      }

      private ConsoleColor GetSharedForeground(bool isSelected, bool disabled)
      {
         if (disabled)
            return ConsoleColor.DarkGray;

         return isSelected ? ConsoleColor.Black : ConsoleColor.Gray;
      }

      #endregion
   }
}