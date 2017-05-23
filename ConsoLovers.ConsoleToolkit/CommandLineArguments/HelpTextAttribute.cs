// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HelpTextAttribute.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.CommandLineArguments
{
   using System;

   using JetBrains.Annotations;

   /// <summary>Attribute for describing the help text for a command line arguments</summary>
   public class HelpTextAttribute : Attribute
   {
      #region Constructors and Destructors

      /// <summary>Initializes a new instance of the <see cref="HelpTextAttribute"/> class.</summary>
      /// <param name="description">The description.</param>
      /// <param name="resourceKey">The resource key.</param>
      public HelpTextAttribute([NotNull] string description, string resourceKey)
      {
         if (description == null)
            throw new ArgumentNullException(nameof(description));
         Description = description;

         ResourceKey = resourceKey;
      }

      public HelpTextAttribute(string description)
         : this(description, null)
      {
      }

      #endregion

      #region Public Properties

      /// <summary>Gets the unlocalized description.</summary>
      public string Description { get; }

      /// <summary>Gets or sets the order that is used when displaying the help.</summary>
      public int Priority { get; set; }

      /// <summary>Gets the resource key that will be displayed.</summary>
      public string ResourceKey { get; }

      #endregion
   }
}