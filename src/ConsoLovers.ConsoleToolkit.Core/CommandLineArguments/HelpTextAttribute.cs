// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HelpTextAttribute.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
   using System;

   /// <summary>Attribute for describing the help text for a command line arguments</summary>
   [AttributeUsage(AttributeTargets.Property)]
   public class HelpTextAttribute : Attribute
   {
      #region Constructors and Destructors

      /// <summary>Initializes a new instance of the <see cref="HelpTextAttribute"/> class.</summary>
      /// <param name="description">The description.</param>
      /// <param name="resourceKey">The resource key.</param>
      public HelpTextAttribute(string description, string resourceKey)
      {
         Description = description;
         ResourceKey = resourceKey;
      }

      /// <summary>Initializes a new instance of the <see cref="HelpTextAttribute"/> class.</summary>
      /// <param name="description">The description.</param>
      public HelpTextAttribute(string description)
         : this(description, null)
      {
      }

      /// <summary>Initializes a new instance of the <see cref="HelpTextAttribute"/> class.</summary>
      public HelpTextAttribute()
         : this(null, null)
      {
      }

      #endregion

      #region Public Properties

      /// <summary>Gets or sets the not localized description.</summary>
      public string Description { get; set; }

      /// <summary>Gets or sets the order that is used when displaying the help.</summary>
      public int Priority { get; set; }

      /// <summary>Gets or sets the resource key that will be used for localizing the help text.</summary>
      public string ResourceKey { get; set; }

      #endregion
   }
}