// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandLineArgumentEventArgs.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
   using System;
   using System.Reflection;

   using JetBrains.Annotations;

   /// <summary>Event args for the <see cref="IArgumentMapper{T}.UnmappedCommandLineArgument"/> event</summary>
   /// <seealso cref="System.EventArgs"/>
   public class MapperEventArgs : EventArgs
   {
      #region Constructors and Destructors

      /// <summary>
      /// Initializes a new instance of the <see cref="MapperEventArgs" /> class.
      /// </summary>
      /// <param name="argument">The argument.</param>
      /// <param name="propertyInfo">The property information.</param>
      /// <param name="instance"></param>
      /// <exception cref="ArgumentNullException">argument</exception>
      public MapperEventArgs([NotNull] CommandLineArgument argument, [CanBeNull] PropertyInfo propertyInfo, object instance)
      {
         Argument = argument ?? throw new ArgumentNullException(nameof(argument));
         PropertyInfo = propertyInfo;
         Instance = instance;
      }

      #endregion

      #region Public Properties

      /// <summary>Gets the argument that could not be mapped.</summary>
      public CommandLineArgument Argument { get; }

      /// <summary>Gets the <see cref="PropertyInfo"/> of the property the argument was mapped to.</summary>
      public PropertyInfo PropertyInfo { get; }

      /// <summary>Gets the instance of the arguments class the command <see cref="CommandLineArgument"/> was mapped to.</summary>
      public object Instance { get; }

      #endregion
   }
}