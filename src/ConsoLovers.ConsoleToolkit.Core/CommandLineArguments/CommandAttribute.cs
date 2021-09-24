// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandAttribute.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
   using System;
   using System.Text;

   /// <summary>Attribute for defining a command line options (boolean flags)</summary>
   [AttributeUsage(AttributeTargets.Property)]
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

      /// <summary>Gets or sets a value indicating whether this command is the default command and is used when no command is specified.</summary>
      public bool IsDefaultCommand { get; set; }

      #endregion

      #region Public Methods and Operators

      public override string ToString()
      {
         var result = new StringBuilder();
         result.Append(Name ?? "<PropertyName>");
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