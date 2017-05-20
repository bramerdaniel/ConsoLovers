// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IExtendedContainer.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.DIContainer
{
   using System;

   /// <summary>Extends the <see cref="IContainer"/> to enable unregistering components to provide a runtime lifetime context.</summary>
   /// <seealso cref="IContainer"/>
   public interface IExtendedContainer : IContainer
   {
      #region Public Methods and Operators

      /// <summary>Unregisters the specified <see cref="IContainerEntry"/>.</summary>
      /// <param name="entry">The entry.</param>
      void Unregister(IContainerEntry entry);

      /// <summary>Unregisters this instance of type <typeparamref name="T"/>.</summary>
      /// <typeparam name="T">The type that should be unregistered.</typeparam>
      void Unregister<T>();

      /// <summary>Unregisters the specified <paramref name="type"/>.</summary>
      /// <param name="type">The <see cref="Type"/> that should be unregistered.</param>
      void Unregister(Type type);

      /// <summary>Unregisters the named instance of type <typeparamref name="T"/>.</summary>
      /// <typeparam name="T">The type that should be unregistered.</typeparam>
      /// <param name="name">The name of the component that should be unregistered.</param>
      void UnregisterNamed<T>(string name);

      #endregion
   }
}