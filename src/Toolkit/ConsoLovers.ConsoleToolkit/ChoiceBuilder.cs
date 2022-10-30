// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ChoiceBuilder.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit;

using System;

using ConsoLovers.ConsoleToolkit.Controls;
using ConsoLovers.ConsoleToolkit.Core;

using JetBrains.Annotations;

public class ChoiceBuilder<T>
{
   #region Constants and Fields

   private readonly Choice<T> choice;

   private readonly IConsole console;

   #endregion

   #region Constructors and Destructors

   public ChoiceBuilder(IConsole console, string question)
   {
      this.console = console;
      choice = new Choice<T>(question);
   }

   #endregion

   #region Public Methods and Operators

   public Choice<T> Build()
   {
      return choice;
   }

   public T Show()
   {
      return ShowInternal(console ?? new ConsoleProxy());
   }

   public ChoiceBuilder<T> WithAnswer(T value, string displayText)
   {
      choice.Add(new Tuple<T, string>(value, displayText));
      return this;
   }

   #endregion

   #region Methods

   internal T ShowInternal([NotNull] IConsole targetConsole)
   {
      if (targetConsole == null)
         throw new ArgumentNullException(nameof(targetConsole));

      targetConsole.RenderInteractive(choice);
      return choice.SelectedItem;
   }

   #endregion
}