// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Choice.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls
{
   using System;

   using ConsoLovers.ConsoleToolkit.Core;

   using JetBrains.Annotations;

   public class Choice<T>
   {
      #region Constants and Fields

      private readonly IConsole console;

      #endregion

      #region Constructors and Destructors

      public Choice([NotNull] IConsole console)
      {
         this.console = console ?? throw new ArgumentNullException(nameof(console));
      }

      public T Show()
      {
         console.WriteLine("Choose value");
         return default;
      }

      #endregion

      public Choice<T> UseDisplayName(bool value, string displayName)
      {

         return this;
      }
   }
}