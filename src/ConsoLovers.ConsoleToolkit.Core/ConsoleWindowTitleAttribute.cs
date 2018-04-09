// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleTitleAttribute.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   using System;

   using JetBrains.Annotations;

   /// <summary>Attibute that is used to set the title of the console windoe</summary>
   /// <seealso cref="System.Attribute"/>
   public class ConsoleWindowTitleAttribute : Attribute
   {
      #region Constructors and Destructors

      /// <summary>Initializes a new instance of the <see cref="ConsoleWindowTitleAttribute"/> class.</summary>
      /// <param name="title">The title.</param>
      public ConsoleWindowTitleAttribute([NotNull] string title)
      {
         if (title == null)
            throw new ArgumentNullException(nameof(title));

         Title = title;
      }

      #endregion

      #region Public Properties

      /// <summary>Gets the title that will be set to the console window.</summary>
      public string Title { get; private set; }

      #endregion
   }
}