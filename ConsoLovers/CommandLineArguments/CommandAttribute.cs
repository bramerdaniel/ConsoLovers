// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandAttribute.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.CommandLineArguments
{
   using System;
   using System.Text;

   /// <summary>Attribute for defining a command line options (boolean flags)</summary>
   [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
   public class CommandAttribute : CommandLineAttribute
   {
      #region Constructors and Destructors

      /// <summary>Initializes a new instance of the <see cref="OptionAttribute"/> class. Initializes a new instance of the <see cref="ArgumentAttribute"/>
      ///    class.</summary>
      /// <param name="name">The name.</param>
      /// <param name="aliases">The aliases.</param>
      public CommandAttribute(string name, params string[] aliases)
         : base(name, aliases)
      {
      }

      /// <summary>Initializes a new instance of the <see cref="OptionAttribute"/> class. Initializes a new instance of the <see cref="ArgumentAttribute"/>
      ///    class.</summary>
      /// <param name="name">The name.</param>
      public CommandAttribute(string name)
         : base(name)
      {
      }

      /// <summary>Initializes a new instance of the <see cref="OptionAttribute"/> class. Initializes a new instance of the <see cref="ArgumentAttribute"/>
      ///    class.</summary>
      public CommandAttribute()
         : base(null)
      {
      }

      #endregion

      #region Public Properties

      /// <summary>Gets or sets a value indicating whether the command is optional.</summary>
      public bool Optional { get; set; }

      #endregion

      #region Public Methods and Operators

      public override string ToString()
      {
         var result = new StringBuilder();
         result.Append(Name);
         result.Append('[');
         foreach (var alias in Aliases)
         {
            result.Append(", ");
            result.Append(alias);
         }

         result.Append(']');
         return result.ToString();
      }

      #endregion
   }
}