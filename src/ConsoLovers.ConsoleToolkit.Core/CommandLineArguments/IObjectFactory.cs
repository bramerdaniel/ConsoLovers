// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IDependencyInjectionContainer.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
   using System;

   /// <summary>Abstraction for a dependency injection container. This could be forwarded to the container of your choice.</summary>
   public interface IObjectFactory
   {
      #region Public Methods and Operators


      /// <summary>Creates the instance by the given type.</summary>
      /// <param name="type">The type.</param>
      /// <returns>The created type</returns>
      object CreateInstance(Type type);

      #endregion

   }
}