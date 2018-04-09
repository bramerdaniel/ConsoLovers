// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArgumentValidatorAttribute.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
   using System;

   [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
   public class ArgumentValidatorAttribute : Attribute
   {
      #region Constructors and Destructors

      /// <summary>Initializes a new instance of the <see cref="CommandLineAttribute"/> class.</summary>
      /// <param name="type">The name.</param>
      public ArgumentValidatorAttribute(Type type)
      {
         Type = type;
      }

      #endregion

      #region Public Properties

      public Type Type { get; }

      #endregion
   }
}