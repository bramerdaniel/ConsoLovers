// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FluentSyntax.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.DIContainer
{
   using System.ComponentModel;

   /// <summary>Fluent syntax interface </summary>
   [EditorBrowsable(EditorBrowsableState.Never)]
   public interface ILifetime : IFluentInterface
   {
      #region Public Methods and Operators

      /// <summary>Sets the lifetime of the created instance.</summary>
      /// <param name="lifetime">The lifetime.</param>
      void WithLifetime(Lifetime lifetime);

      #endregion
   }

   /// <summary>Fluent syntax interface </summary>
   [EditorBrowsable(EditorBrowsableState.Never)]
   public interface IContainerEntry : ILifetime
   {
   }
}