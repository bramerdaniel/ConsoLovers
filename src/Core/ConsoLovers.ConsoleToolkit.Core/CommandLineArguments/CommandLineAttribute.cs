// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandLineAttribute.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
   using System;
   using System.Collections.Generic;
   using System.Xml.Serialization;

   /// <summary>Base attribute class for the usage with the <see cref="ArgumentMapper{T}"/> class.</summary>
   [AttributeUsage(AttributeTargets.Property)]
   public abstract class CommandLineAttribute : Attribute
   {
      #region Constructors and Destructors

      /// <summary>Initializes a new instance of the <see cref="CommandLineAttribute"/> class.</summary>
      /// <param name="name">The name.</param>
      /// <param name="aliases">The aliases.</param>
      protected CommandLineAttribute(string name, params string[] aliases)
      {
         Name = name;
         Aliases = aliases ?? new string[0];
      }

      /// <summary>Initializes a new instance of the <see cref="CommandLineAttribute"/> class.</summary>
      protected CommandLineAttribute()
         : this(null)
      {
      }

      #endregion

      #region Public Properties

      /// <summary>Gets the aliases.</summary>
      [XmlIgnore]
      public string[] Aliases { get; set; }

      /// <summary>Gets the name of the argument.</summary>
      [XmlIgnore]
      public string Name { get; set; }

      /// <summary>Gets or sets a value indicating whether this <see cref="CommandLineAttribute"/> is shared between commands and the application.
      /// The defaults value is false, so the a command will map this value only</summary>
      [XmlIgnore]
      public bool Shared { get; set; }

      #endregion

      #region Public Methods and Operators

      /// <summary>Gets all the identifiers.</summary>
      /// <returns>An <see cref="IEnumerable{T}"/> of strings</returns>
      public IEnumerable<string> GetIdentifiers()
      {
         if (Name != null)
            yield return Name;

         if (Aliases != null)
         {
            foreach (var aliase in Aliases)
               yield return aliase;
         }
      }

      #endregion
   }
}