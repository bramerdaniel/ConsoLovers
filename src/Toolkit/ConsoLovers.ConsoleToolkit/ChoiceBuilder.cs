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

   private readonly CSelector<T> choiceSelector;

   private readonly IConsole console;

   private bool singleLine;

   #endregion

   #region Constructors and Destructors

   public ChoiceBuilder(IConsole console, string question)
   {
      Question = question;
      this.console = console;
      choiceSelector = new CSelector<T>();
   }

   #endregion

   #region Public Properties

   public string Question { get; }

   #endregion

   #region Public Methods and Operators

   public ChoiceBuilder<T> AllowCancellation(bool value)
   {
      choiceSelector.AllowCancellation = value;
      return this;
   }

   public T Show()
   {
      return ShowInternal(console ?? new ConsoleProxy());
   }

   public ChoiceBuilder<T> WithAnswer(T value, string displayText)
   {
      choiceSelector.Add(value, displayText);
      return this;
   }

   public ChoiceBuilder<T> WithAnswer(T value)
   {
      choiceSelector.Add(value);
      return this;
   }

   public ChoiceBuilder<T> WithAnswer(T value, IRenderable template)
   {
      choiceSelector.Add(value, template);
      return this;
   }

   public ChoiceBuilder<T> WithOrientation(Orientation orientation)
   {
      return WithOrientation(orientation, false);
   }

   public ChoiceBuilder<T> WithOrientation(Orientation orientation, bool useSingleLine)
   {
      if (useSingleLine && orientation == Orientation.Vertical)
         throw new ArgumentException("UseSingleLine can only be true when the orientation is set to horizontal", nameof(useSingleLine));

      choiceSelector.Orientation = orientation;
      singleLine = useSingleLine;
      return this;
   }

   public ChoiceBuilder<T> WithoutSelector()
   {
      return WithSelector(string.Empty);
   }

   public ChoiceBuilder<T> WithSelector(string value)
   {
      choiceSelector.Selector = value;
      return this;
   }

   #endregion

   #region Methods

   internal T ShowInternal([NotNull] IConsole targetConsole)
   {
      if (targetConsole == null)
         throw new ArgumentNullException(nameof(targetConsole));

      if (singleLine)
      {
         var panel = new Panel();
         panel.Add(new Text(Question));
         panel.Add(choiceSelector);
         targetConsole.RenderInteractive(panel);
      }
      else
      {
         console.WriteLine(Question);
         targetConsole.RenderInteractive(choiceSelector);
      }

      return choiceSelector.SelectedValue;
   }

   #endregion
}