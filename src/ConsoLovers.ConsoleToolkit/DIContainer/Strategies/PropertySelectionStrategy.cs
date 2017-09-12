// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PropertySelectionStrategy.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.DIContainer.Strategies
{
   using System;
   using System.Collections.Generic;
   using System.Reflection;

   /// <summary>Abstract base class for selection properties to inject</summary>
   public abstract class PropertySelectionStrategy
   {
      #region Public Methods and Operators

      /// <summary>Selects all properties of the given type.</summary>
      /// <param name="type">The type to select the properties from.</param>
      /// <returns>The <see cref="PropertyInfo"/>s that were selected</returns>
      public abstract IEnumerable<PropertyInfo> SelectProperties(Type type);

      #endregion
   }
}