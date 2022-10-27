// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Measurement.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Prompts;

public struct Measurement
{
   #region Constructors and Destructors

   /// <summary>Initializes a new instance of the <see cref="Measurement"/> struct.</summary>
   /// <param name="min">The minimum width.</param>
   /// <param name="max">The maximum width.</param>
   public Measurement(int min, int max)
   {
      Min = min;
      Max = max;
   }

   #endregion

   #region Public Properties

   /// <summary>Gets the maximum width.</summary>
   public int Max { get; }

   /// <summary>Gets the minimum width.</summary>
   public int Min { get; }

   #endregion
}