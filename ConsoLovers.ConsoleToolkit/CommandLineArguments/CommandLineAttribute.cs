// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandLineAttribute.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.CommandLineArguments
{
   using System;
   using System.Collections.Generic;
   using System.Xml.Serialization;

   /// <summary>Base attribute class for the usage with the <see cref="ArgumentMapper{T}"/> class.</summary>
   [AttributeUsage(AttributeTargets.Property, Inherited = true)]
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

      #endregion

      #region Public Methods and Operators

      public IEnumerable<string> GetIdentifiers()
      {
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