// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DetailedHelpTextAttribute.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.CommandLineArguments
{
   using System;

   /// <summary>Attribute for describing the help text for a command line arguments</summary>
   [AttributeUsage(AttributeTargets.Property)]
   public class DetailedHelpTextAttribute : Attribute
   {
      #region Constructors and Destructors

      /// <summary>Initializes a new instance of the <see cref="DetailedHelpTextAttribute"/> class.</summary>
      /// <param name="description">The description.</param>
      /// <param name="resourceKey">The resource key.</param>
      public DetailedHelpTextAttribute(string description, string resourceKey)
      {
         Description = description;
         ResourceKey = resourceKey;
      }

      /// <summary>Initializes a new instance of the <see cref="HelpTextAttribute"/> class.</summary>
      /// <param name="description">The description.</param>
      public DetailedHelpTextAttribute(string description)
         : this(description, null)
      {
      }

      /// <summary>Initializes a new instance of the <see cref="DetailedHelpTextAttribute"/> class.</summary>
      public DetailedHelpTextAttribute()
         : this(null, null)
      {
      }

      #endregion

      #region Public Properties

      /// <summary>Gets or sets the not localized description.</summary>
      public string Description { get; set; }

      /// <summary>Gets or sets the resource key that will be used for localizing the help text.</summary>
      public string ResourceKey { get; set; }

      #endregion
   }
}