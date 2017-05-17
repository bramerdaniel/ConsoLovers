// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMapperFactory.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.CommandLineArguments
{
   using System;

   /// <summary>Factory that is creating all instances the engine requires to work correctly (e.g. the <see cref="IArgumentMapper{T}"/>).</summary>
   public interface IEngineFactory
   {
      #region Public Methods and Operators

      /// <summary>Creates the <see cref="IArgumentMapper{T}"/> that is used.</summary>
      /// <typeparam name="T">The generic typ the mapper should handle</typeparam>
      /// <returns>The created mapper</returns>
      IArgumentMapper<T> CreateMapper<T>()
         where T : class;

      /// <summary>Creates an instance of the given argument type.</summary>
      /// <typeparam name="T">The type of the arguments</typeparam>
      /// <returns>The created instance</returns>
      T CreateArgumentInstance<T>() where T : class;

      object CreateArgumentInstance(Type type);

      #endregion

      object CreateApplication(Type type);
   }


}