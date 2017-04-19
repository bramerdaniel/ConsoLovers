// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleWindowHeightAttribute.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit
{
   using System;

   /// <summary>Attribute that is used to set the window heiht of the console window</summary>
   /// <seealso cref="System.Attribute"/>
   public class ConsoleWindowHeightAttribute : Attribute
   {
      #region Constructors and Destructors

      /// <summary>Initializes a new instance of the <see cref="ConsoleWindowTitleAttribute"/> class.</summary>
      /// <param name="height">The width.</param>
      public ConsoleWindowHeightAttribute(int height)
      {
         ConsoleHeight = height;
      }

      #endregion

      #region Public Properties

      /// <summary>Gets the height of the console window.</summary>
      public int ConsoleHeight { get; private set; }

      #endregion
   }
}