// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IArgumentMapper.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
   using System;
   using System.Collections.Generic;
   using System.IO;

   public interface IArgumentMapper
   {
      #region Public Events

      /// <summary>Occurs when command line argument could be mapped to a specific property of the specified class of type.</summary>
      event EventHandler<MapperEventArgs> MappedCommandLineArgument;

      /// <summary>Occurs when a command line argument of the given arguments dictionary could not be mapped to a arguments member</summary>
      event EventHandler<MapperEventArgs> UnmappedCommandLineArgument;

      #endregion
   }

   /// <summary>Interface for mapping the parsed dictionary of <see cref="CommandLineAttribute"/>s the an arguments class of type <see cref="T"/></summary>
   /// <typeparam name="T">The type of the arguments class</typeparam>
   public interface IArgumentMapper<T> : IArgumentMapper
   {
      #region Public Methods and Operators

      /// <summary>Maps the give argument dictionary to the given instance.</summary>
      /// <param name="arguments">The arguments to map.</param>
      /// <param name="instance">The instance to map the arguments to.</param>
      /// <returns>The instance of the class, the command line argument were mapped to</returns>
      /// <exception cref="System.IO.InvalidDataException">Option attribute can only be applied to boolean properties</exception>
      /// <exception cref="InvalidDataException">Option attribute can only be applied to boolean properties</exception>
      T Map(CommandLineArgumentList arguments, T instance);

      /// <summary>Maps the give argument dictionary to the given instance.</summary>
      /// <param name="arguments">The arguments to map.</param>
      /// <returns>The instance of the class, the command line argument were mapped to</returns>
      /// <exception cref="System.IO.InvalidDataException">Option attribute can only be applied to boolean properties</exception>
      /// <exception cref="InvalidDataException">Option attribute can only be applied to boolean properties</exception>
      T Map(CommandLineArgumentList arguments);

      #endregion
   }
}