// -------------------------------------------------I-------------------------------------------------------------------
// <copyright file="ArgumentAttribute.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
   using System;
   using System.Diagnostics;

   /// <summary>Attribute for defining a command line options (boolean flags)</summary>
   [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
   [DebuggerDisplay("Argument: Name={Name}, Index={Index}, Required={Required}")]
   public class ArgumentAttribute : CommandLineAttribute
   {
      #region Constants and Fields

      private int? index;

      #endregion

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

      /// <summary>Initializes a new instance of the <see cref="ArgumentAttribute"/> class.</summary>
      public ArgumentAttribute(int index)
         : this(null)
      {
         if (index < 0)
            throw new ArgumentOutOfRangeException(nameof(index));

         Index = index;
      }

      #endregion

      #region Public Properties

      /// <summary>Gets the index of the argument.</summary>
      public int Index
      {
         get => index.GetValueOrDefault(-1);
         set => index = value;
      }

      /// <summary>Gets a value indicating whether the command line argument is required.</summary>
      public bool Required { get; set; }

      public bool TrimQuotation { get; set; }

      #endregion
   }
}