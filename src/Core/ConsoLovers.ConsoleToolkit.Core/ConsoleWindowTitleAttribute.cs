﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleTitleAttribute.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   using System;

   using JetBrains.Annotations;

   /// <summary>Attribute that is used to set the title of the console window</summary>
   /// <seealso cref="System.Attribute"/>
   [Obsolete]
   public class ConsoleWindowTitleAttribute : Attribute
   {
      #region Constructors and Destructors

      /// <summary>Initializes a new instance of the <see cref="ConsoleWindowTitleAttribute"/> class.</summary>
      /// <param name="title">The title.</param>
      public ConsoleWindowTitleAttribute([NotNull] string title)
      {
         Title = title ?? throw new ArgumentNullException(nameof(title));
      }

      #endregion

      #region Public Properties

      /// <summary>Gets the title that will be set to the console window.</summary>
      public string Title { get; }

      #endregion
   }
}