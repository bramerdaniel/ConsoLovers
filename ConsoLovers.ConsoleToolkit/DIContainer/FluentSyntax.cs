// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FluentSyntax.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.DIContainer
{
   using System.ComponentModel;

   /// <summary>Fluent syntax interface </summary>
   [EditorBrowsable(EditorBrowsableState.Never)]
   public interface INamed : IFluentInterface
   {
      #region Public Methods and Operators

      /// <summary>Sets the name of the service entry.</summary>
      /// <param name="name">The name to register the service with</param>
      /// <returns>The fluent configuration continue</returns>
      ILifetime Named(string name);

      #endregion
   }

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
   public interface IContainerEntry : INamed, ILifetime
   {
   }
}