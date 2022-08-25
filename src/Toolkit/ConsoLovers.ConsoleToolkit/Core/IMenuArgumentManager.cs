// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMenuArgumentManager.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   using System;

   public interface IMenuArgumentManager
   {
      #region Public Methods and Operators

      /// <summary>Clears all cached argument values.</summary>
      void Clear();

      /// <summary>Gets the or creates the instance of the specified argument type.</summary>
      /// <param name="argumentType">Type of the argument.</param>
      /// <returns>The argument instance</returns>
      object GetOrCreate(Type argumentType);

      #endregion
   }
}