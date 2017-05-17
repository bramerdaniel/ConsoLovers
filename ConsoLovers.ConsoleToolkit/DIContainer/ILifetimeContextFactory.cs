// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ILifetimeContextFactory.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.DIContainer
{
   /// <summary>Contract for a factory for a <see cref="ILifetimeContext"/>.</summary>
   public interface ILifetimeContextFactory
   {
      #region Public Methods and Operators

      /// <summary>Creates the <see cref="ILifetimeContext"/>.</summary>
      /// <param name="extendedContainer">The <see cref="IExtendedContainer"/>.</param>
      /// <returns>The created <see cref="ILifetimeContext"/>.</returns>
      ILifetimeContext CreateContext(IExtendedContainer extendedContainer);

      #endregion
   }
}