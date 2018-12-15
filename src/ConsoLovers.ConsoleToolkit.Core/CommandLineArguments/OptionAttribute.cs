// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OptionAttribute.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
   using System;

   /// <summary>Attribute for defining a command line options (boolean flags)</summary>
   [AttributeUsage(AttributeTargets.Property)]
   public class OptionAttribute : CommandLineAttribute
   {
      #region Constructors and Destructors

      /// <summary>Initializes a new instance of the <see cref="OptionAttribute"/> class. Initializes a new instance of the <see cref="ArgumentAttribute"/> class.</summary>
      /// <param name="name">The name.</param>
      /// <param name="aliases">The aliases.</param>
      public OptionAttribute(string name, params string[] aliases)
         : base(name, aliases)
      {
      }

      /// <summary>Initializes a new instance of the <see cref="OptionAttribute"/> class. Initializes a new instance of the <see cref="ArgumentAttribute"/> class.</summary>
      /// <param name="name">The name.</param>
      public OptionAttribute(string name)
         : base(name)
      {
      }

      /// <summary>Initializes a new instance of the <see cref="OptionAttribute"/> class. Initializes a new instance of the <see cref="ArgumentAttribute"/> class.</summary>
      public OptionAttribute()
         : base(null)
      {
      }

      #endregion
   }
}