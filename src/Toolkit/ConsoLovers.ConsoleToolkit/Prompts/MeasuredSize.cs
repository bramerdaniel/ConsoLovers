// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MeasuredSize.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Prompts;

public struct MeasuredSize
{
   #region Public Properties

   public int Height { get; set; }
   

   /// <summary>Gets the minimum width.</summary>
   public int MinWidth { get; set; }

   #endregion
}