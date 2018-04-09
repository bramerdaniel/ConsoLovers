// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArgumentAttribute.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
   using System;

   /// <summary>Attribute for defining a command line options (boolean flags)</summary>
   [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
   public class ArgumentAttribute : CommandLineAttribute
   {
      #region Constructors and Destructors

      /// <summary>
      ///    Initializes a new instance of the <see cref="ArgumentAttribute"/> class. Initializes a new instance of the <see cref="OptionAttribute"/> class. Initializes a new instance
      ///    of the <see cref="ArgumentAttribute"/> class.
      /// </summary>
      /// <param name="name">The name.</param>
      /// <param name="aliases">The aliases.</param>
      public ArgumentAttribute(string name, params string[] aliases)
         : base(name, aliases)
      {
      }

      /// <summary>Initializes a new instance of the <see cref="ArgumentAttribute"/> class.</summary>
      public ArgumentAttribute()
         : this(null)
      {
      }

      #endregion

      #region Public Properties

      /// <summary>Gets a value indicating whether the command line argument is required.</summary>
      public bool Required { get; set; }

      public bool TrimQuotation { get; set; }

      #endregion
   }

   /// <summary>Attribute for defining a command line agrument that is defined by the index of it's occurance.</summary>
   [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
   public class IndexedArgumentAttribute : CommandLineAttribute
   {
      #region Constructors and Destructors

      /// <summary>
      ///    Initializes a new instance of the <see cref="ArgumentAttribute"/> class. Initializes a new instance of the <see cref="OptionAttribute"/> class. Initializes a new instance
      ///    of the <see cref="ArgumentAttribute"/> class.
      /// </summary>
      public IndexedArgumentAttribute(int index)
         : base(null)
      {
         Index = index;
      }

      #endregion

      #region Public Properties

      /// <summary>Gets the index of the argument.</summary>
      public int Index { get; }

      /// <summary>Gets a value indicating whether the command line argument is required.</summary>
      public bool Required { get; set; }

      public bool TrimQuotation { get; set; }

      #endregion
   }
}