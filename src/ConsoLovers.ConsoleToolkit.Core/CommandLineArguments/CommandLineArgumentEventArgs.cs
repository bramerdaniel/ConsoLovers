// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandLineArgumentEventArgs.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
   using System;
   using System.Diagnostics;
   using System.Reflection;

   using JetBrains.Annotations;

   /// <summary>Event args class for the <see cref="ICommandLineEngine.HandledCommandLineArgument"/> and the <see cref="ICommandLineEngine.UnhandledCommandLineArgument"/> event</summary>
   /// <seealso cref="System.EventArgs"/>
   [DebuggerDisplay("[{Argument.Index}] {Argument.Name}={Argument.Value}")]
   public class CommandLineArgumentEventArgs : EventArgs
   {
      #region Constructors and Destructors

      /// <summary>Initializes a new instance of the <see cref="CommandLineArgumentEventArgs"/> class.</summary>
      /// <param name="argument">The argument.</param>
      /// <param name="propertyInfo">The property information.</param>
      /// <param name="instance">The instance.</param>
      /// <exception cref="ArgumentNullException">argument</exception>
      public CommandLineArgumentEventArgs([NotNull] CommandLineArgument argument, [CanBeNull] PropertyInfo propertyInfo, object instance)
      {
         Argument = argument ?? throw new ArgumentNullException(nameof(argument));
         PropertyInfo = propertyInfo;
         Instance = instance;
      }

      #endregion

      #region Public Properties

      /// <summary>Gets the argument.</summary>
      public CommandLineArgument Argument { get; }

      /// <summary>Gets the instance of the arguments class the argument was mapped to.</summary>
      public object Instance { get; }

      /// <summary>Gets the <see cref="PropertyInfo"/> the command line argument was set to.</summary>
      public PropertyInfo PropertyInfo { get; }

      #endregion
   }
}