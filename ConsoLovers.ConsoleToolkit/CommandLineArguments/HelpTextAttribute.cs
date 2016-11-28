// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HelpTextAttribute.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.CommandLineArguments
{
   using System;

   /// <summary>Attribute for describing the help text for a command line arguments</summary>
   public class HelpTextAttribute : Attribute
   {
      #region Constructors and Destructors

      /// <summary>Initializes a new instance of the <see cref="HelpTextAttribute"/> class.</summary>
      /// <param name="resourceKey">The resource key.</param>
      /// <param name="description">The description.</param>
      public HelpTextAttribute(string resourceKey, string description)
      {
         ResourceKey = resourceKey;
         Description = description;
      }

      #endregion

      #region Public Properties

      /// <summary>Gets the unlocalized description.</summary>
      public string Description { get; private set; }

      /// <summary>Gets the resource key that will be displayed.</summary>
      public string ResourceKey { get; private set; }

      #endregion
   }
}