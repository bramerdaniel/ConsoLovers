// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LifetimeContextFactory.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.DIContainer
{
   /// <summary>Implementation for <see cref="ILifetimeContextFactory"/> to return a new <see cref="LifetimeContext"/>.</summary>
   /// <seealso cref="ILifetimeContextFactory"/>
   public class LifetimeContextFactory : ILifetimeContextFactory
   {
      #region ILifetimeContextFactory Members

      /// <summary>Creates the <see cref="ILifetimeContext"/>.</summary>
      /// <param name="extendedContainer">The <see cref="IExtendedContainer"/>.</param>
      /// <returns>The created <see cref="ILifetimeContext"/>.</returns>
      public ILifetimeContext CreateContext(IExtendedContainer extendedContainer)
      {
         return new LifetimeContext(extendedContainer);
      }

      #endregion
   }
}