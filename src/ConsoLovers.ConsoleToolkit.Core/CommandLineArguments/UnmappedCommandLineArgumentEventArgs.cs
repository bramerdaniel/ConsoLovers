// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnmappedCommandLineArgumentEventArgs.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
   using System;

   using JetBrains.Annotations;

   /// <summary>Event args for the <see cref="IArgumentMapper{T}.UnmappedCommandLineArgument"/> event</summary>
   /// <seealso cref="System.EventArgs"/>
   public class UnmappedCommandLineArgumentEventArgs : EventArgs
   {
      #region Constructors and Destructors

      public UnmappedCommandLineArgumentEventArgs([NotNull] CommandLineArgument argument)
      {
         Argument = argument ?? throw new ArgumentNullException(nameof(argument));
      }

      #endregion

      #region Public Properties

      /// <summary>Gets the argument that could not be mapped.</summary>
      public CommandLineArgument Argument { get; }

      #endregion
   }
}