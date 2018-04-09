// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IArgumentMapper.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
   using System;
   using System.Collections.Generic;
   using System.IO;

   public interface IArgumentMapper
   {
      /// <summary>Occurs when a command line argument of the given arguments dictionary could not be mapped to a arguments member</summary>
      event EventHandler<UnmappedCommandLineArgumentEventArgs> UnmappedCommandLineArgument;
   }

   public interface IArgumentMapper<T> : IArgumentMapper
   {
      #region Public Methods and Operators

      /// <summary>Maps the give argument dictionary to a new created instance.</summary>
      /// <param name="arguments">The arguments to map.</param>
      /// <returns>The instance of the class, the command line argument were mapped to</returns>
      /// <exception cref="System.IO.InvalidDataException">Option attribute can only be applied to boolean properties</exception>
      /// <exception cref="InvalidDataException">Option attribute can only be applied to boolean properties</exception>
      T Map(IDictionary<string, CommandLineArgument> arguments);

      /// <summary>Maps the give argument dictionary to the given instance.</summary>
      /// <param name="arguments">The arguments to map.</param>
      /// <param name="instance">The instance to map the arguments to.</param>
      /// <returns>The instance of the class, the command line argument were mapped to</returns>
      /// <exception cref="System.IO.InvalidDataException">Option attribute can only be applied to boolean properties</exception>
      /// <exception cref="InvalidDataException">Option attribute can only be applied to boolean properties</exception>
      T Map(IDictionary<string, CommandLineArgument> arguments, T instance);

      #endregion
   }
}