// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFluentInterface.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.DIContainer
{
   using System;
   using System.ComponentModel;

   /// <summary>Interface used for fluent programming inrterface to hide object derived members</summary>
   [EditorBrowsable(EditorBrowsableState.Never)]
   public interface IFluentInterface
   {
      #region Public Methods and Operators

      /// <summary>Determines whether the specified <see cref="System.Object"/> is equal to this instance.</summary>
      /// <param name="obj">The <see cref="System.Object"/> to compare with this instance.</param>
      /// <returns>
      ///    <c>true</c> if the specified <see cref="System.Object"/> is equal to this instance; otherwise, <c>false</c>.
      /// </returns>
      [EditorBrowsable(EditorBrowsableState.Never)]
      bool Equals(object obj);

      /// <summary>Returns a hash code for this instance.</summary>
      /// <returns>A hash code for this instance, suitable for use in hashing algorithms and data structures like a hash table.</returns>
      [EditorBrowsable(EditorBrowsableState.Never)]
      int GetHashCode();

      /// <summary>Gets the type.</summary>
      /// <returns>The type</returns>
      [EditorBrowsable(EditorBrowsableState.Never)]
      Type GetType();

      /// <summary>Returns a <see cref="System.String"/> that represents this instance.</summary>
      /// <returns>A <see cref="System.String"/> that represents this instance.</returns>
      [EditorBrowsable(EditorBrowsableState.Never)]
      string ToString();

      #endregion
   }
}