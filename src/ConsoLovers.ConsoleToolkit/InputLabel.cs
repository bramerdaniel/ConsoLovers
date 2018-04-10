// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InputLabel.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit
{
   using System;

   using ConsoLovers.ConsoleToolkit.Console;
   using ConsoLovers.ConsoleToolkit.Core;

   /// <summary>Class that represents the label of an <see cref="InputBox"/></summary>
   public class InputLabel
   {
      #region Constants and Fields

      private readonly IConsole console;

      #endregion

      #region Constructors and Destructors

      public InputLabel(string text)
         : this(ColoredConsole.Instance, text)
      {
      }

      public InputLabel(IConsole console, string text)
      {
         Text = text;
         this.console = console;
         Foreground = console.ForegroundColor;
         Background = console.BackgroundColor;
      }

      #endregion

      #region Public Properties

      public ConsoleColor Background { get; set; }

      public ConsoleColor Foreground { get; set; }

      public string Text { get; set; }

      #endregion

      #region Public Methods and Operators

      public void Print()
      {
         console.Write(Text, Foreground, Background);
      }

      #endregion
   }
}