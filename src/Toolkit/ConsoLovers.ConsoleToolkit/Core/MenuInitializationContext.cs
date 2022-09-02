// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MenuInitializationContext.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   using System;

   using ConsoLovers.ConsoleToolkit.Menu;

   using JetBrains.Annotations;

   internal class MenuInitializationContext : IMenuInitializationContext
   {
      #region Constructors and Destructors

      public MenuInitializationContext([NotNull] ConsoleMenu menu, [NotNull] IServiceProvider serviceProvider)
      {
         Menu = menu ?? throw new ArgumentNullException(nameof(menu));
         ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
      }

      #endregion

      #region IMenuInitializationContext Members

      /// <summary>Gets the <see cref="ConsoleMenu"/> that is about to be displayed.</summary>
      public ConsoleMenu Menu { get; }

      /// <summary>Gets the <see cref="IServiceProvider"/> that was used to build the menu.</summary>
      public IServiceProvider ServiceProvider { get; }

      #endregion
   }
}