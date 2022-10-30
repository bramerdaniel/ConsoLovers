// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RenderInfo.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

using System.Diagnostics;

/// <summary>Hold information about where a segment was rendered</summary>
[DebuggerDisplay("Line {Line} : {Column}-{EndColumn} ")]
internal class RenderInfo
{
   #region Constructors and Destructors

   public RenderInfo(int line, int column, Segment segment)
   {
      Line = line;
      Column = column;
      Length = segment.Width;
      Segment = segment;
   }

   #endregion

   #region Public Properties

   public int Column { get; }

   public int Length { get; }

   public int EndColumn => Column + Length;

   public int Line { get; }

   public Segment Segment { get; }

   #endregion
}