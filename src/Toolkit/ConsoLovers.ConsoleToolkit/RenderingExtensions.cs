// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RenderingExtensions.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit;

using System;

using ConsoLovers.ConsoleToolkit.Controls;
using ConsoLovers.ConsoleToolkit.Core;

using JetBrains.Annotations;

public static class RenderingExtensions
{
   public static void Render([NotNull] this IConsole console, IRenderable renderable)
   {
      if (console == null)
         throw new ArgumentNullException(nameof(console));
      if (renderable == null)
         throw new ArgumentNullException(nameof(renderable));

      new RenderEngine(console).Render(renderable);
   }   
   
   public static void RenderInteractive([NotNull] this IConsole console, IRenderable renderable)
   {
      if (console == null)
         throw new ArgumentNullException(nameof(console));
      if (renderable == null)
         throw new ArgumentNullException(nameof(renderable));

      new RenderEngine(console).Render(renderable, true);
   }   

   public static ChoiceBuilder<T> Choice<T>([NotNull] this IConsole console, string question)
   {
      if (console == null)
         throw new ArgumentNullException(nameof(console));

      return new ChoiceBuilder<T>(console, question);
   }

   public static bool YesNo([NotNull] this IConsole console, string question, bool allowCancellation = true)
   {
      if (console == null)
         throw new ArgumentNullException(nameof(console));

      var show = console.Choice<bool>(EnsureAtLeastOneSpace(question))
         .WithOrientation(Orientation.Horizontal, true)
         .WithAnswer(true, new Text("yes", RenderingStyle.Default.WithForeground(ConsoleColor.Green)))
         .WithAnswer(false, new Text("no", RenderingStyle.Default.WithForeground(ConsoleColor.Red)))
         .AllowCancellation(allowCancellation)
         .Show();

      return show;
   }

   public static bool? YesNoCancel([NotNull] this IConsole console, string question)
   {
      if (console == null)
         throw new ArgumentNullException(nameof(console));

      var show = console.Choice<bool?>(EnsureAtLeastOneSpace(question))
         .WithOrientation(Orientation.Horizontal, true)
         .WithAnswer(true, new Text("yes"))
         .WithAnswer(false, new Text("no"))
         .WithAnswer(null, new Text("cancel"))
         .AllowCancellation(false)
         .Show();

      return show;
   }

   private static string EnsureAtLeastOneSpace(string question)
   {
      if (question == null)
         return null;

      return question.EndsWith(" ") ? question : $"{question} ";
   }
}