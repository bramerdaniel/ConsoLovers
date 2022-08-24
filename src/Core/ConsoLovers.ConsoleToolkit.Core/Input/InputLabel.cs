// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InputLabel.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Input;

using System;
using System.Diagnostics.CodeAnalysis;

[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
public class InputLabel
{
   #region Constants and Fields

   private readonly IConsole console;

   #endregion

   #region Constructors and Destructors

   public InputLabel(string text)
      : this(new ConsoleProxy(), text)
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