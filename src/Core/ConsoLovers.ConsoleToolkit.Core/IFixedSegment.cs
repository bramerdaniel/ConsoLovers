// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IFixedSegment.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core;

using System;
using System.ComponentModel;

/// <summary>Creates a fixed section in the console, that will not move </summary>
public interface IFixedSegment : INotifyPropertyChanged
{
   #region Public Properties

   /// <summary>Gets or sets the background of the <see cref="IFixedSegment"/>.</summary>
   public ConsoleColor Background { get; set; }

   /// <summary>Gets or sets the foreground of the <see cref="IFixedSegment"/>.</summary>
   public ConsoleColor Foreground { get; set; }

   /// <summary>Gets or sets the text of the section.</summary>
   public string Text { get; set; }

   /// <summary>Gets the width of the section.</summary>
   public int Width { get; set; }

   #endregion

   #region Public Methods and Operators

   /// <summary>Updates the <see cref="Text"/>, the <see cref="Foreground"/> and the <see cref="Background"/> in one go.</summary>
   IFixedSegment Update(string value, ConsoleColor newForeground, ConsoleColor newBackground);

   /// <summary>Updates the <see cref="Text"/> and the <see cref="Foreground"/> in one go.</summary>
   /// <param name="value">The value.</param>
   /// <param name="newForeground">The new foreground.</param>
   /// <returns></returns>
   IFixedSegment Update(string value, ConsoleColor newForeground);

   #endregion
}