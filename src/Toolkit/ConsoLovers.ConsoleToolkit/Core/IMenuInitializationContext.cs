// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMenuInitializationContext.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   using System;

   using ConsoLovers.ConsoleToolkit.Menu;

   public interface IMenuInitializationContext
   {
      #region Public Properties

      /// <summary>Gets the <see cref="ConsoleMenu"/> that is about to be displayed.</summary>
      ConsoleMenu Menu { get; }

      /// <summary>Gets the <see cref="IServiceProvider"/> that was used to build the menu.</summary>
      IServiceProvider ServiceProvider { get; }

      #endregion
   }
}