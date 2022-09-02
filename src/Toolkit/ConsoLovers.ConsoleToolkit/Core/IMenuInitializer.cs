// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMenuInitializer.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   public interface IMenuInitializer
   {
      #region Public Methods and Operators

      /// <summary>Called before the menu is displayed.</summary>
      /// <param name="context">The <see cref="IMenuInitializationContext"/>.</param>
      void Initialize(IMenuInitializationContext context);

      #endregion
   }
}