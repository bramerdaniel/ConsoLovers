﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StyledString.cs" company="ConsoLovers">
//   Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Console
{
   using System;
   using System.Drawing;

   // NOTE: The StyledString class is meant to replace a number of instances of System.String in this project.  
   //       However, it's currently only used in the WriteLineStyled method, becuase I'd like to have better
   //       test coverage before using it in the rest of the project.

   /// <summary>A string encoded with style!</summary>
   public sealed class StyledString : IEquatable<StyledString>
   {
      #region Constructors and Destructors

      public StyledString(string oneDimensionalRepresentation)
      {
         AbstractValue = oneDimensionalRepresentation;
      }

      public StyledString(string oneDimensionalRepresentation, string twoDimensionalRepresentation)
      {
         AbstractValue = oneDimensionalRepresentation;
         ConcreteValue = twoDimensionalRepresentation;
      }

      #endregion

      #region IEquatable<StyledString> Members

      // Does not take styling information into account...and it needs to be taken into account.
      public bool Equals(StyledString other)
      {
         if (other == null)
         {
            return false;
         }

         return AbstractValue == other.AbstractValue && ConcreteValue == other.ConcreteValue;
      }

      #endregion

      #region Public Properties

      /// <summary>The one-dimensional representation of the StyledString.  Maps 1:1 with System.String.</summary>
      public string AbstractValue { get; }

      /// <summary>
      ///    A matrix of concrete characters that corresponds to each concrete character in the StyledString. In other words, this is a two-dimensional representation of the
      ///    StyledString.ConcreteValue property.
      /// </summary>
      public char[,] CharacterGeometry { get; set; }

      /// <summary>A matrix of abstract character indices that corresponds to each concrete character in the StyledString. Dimensions are [row, column].</summary>
      public int[,] CharacterIndexGeometry { get; set; }

      //
      // The three geometry members, below, should be encapsulated into a single data structure.
      //

      /// <summary>A matrix of colors that corresponds to each concrete character in the StyledString. Dimensions are [row, column].</summary>
      public Color[,] ColorGeometry { get; set; }

      /// <summary>
      ///    The n-dimensional (n &le; 2, in this case) representation of the StyledString. In the case of FIGlet fonts, for example, this would be the string's two-dimensional FIGlet
      ///    representation.
      /// </summary>
      public string ConcreteValue { get; }

      #endregion

      #region Public Methods and Operators

      public override bool Equals(object obj)
      {
         return Equals(obj as StyledString);
      }

      // Does not take styling information into account...and it needs to be taken into account.
      public override int GetHashCode()
      {
         int hash = 163;

         hash *= 79 + AbstractValue.GetHashCode();
         hash *= 79 + ConcreteValue.GetHashCode();

         return hash;
      }

      /// <summary>Returns the StyledString's concrete representation.</summary>
      /// <returns></returns>
      public override string ToString()
      {
         return ConcreteValue;
      }

      #endregion
   }
}