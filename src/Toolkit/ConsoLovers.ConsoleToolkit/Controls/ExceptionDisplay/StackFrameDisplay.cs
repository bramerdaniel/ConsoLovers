// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StackFrameDisplay.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

using JetBrains.Annotations;

public class StackFrameDisplay : InteractiveRenderable, IMouseInputHandler
{
   
   #region Constructors and Destructors

   public StackFrameDisplay([NotNull] StackFrame stackFrame)
   {
      if (stackFrame == null)
         throw new ArgumentNullException(nameof(stackFrame));

      FileName = stackFrame.GetFileName();
      Parts = new FrameParts(stackFrame, this);
   }

   private string FileName { get; }
   
   #endregion

   #region Properties

   private FrameParts Parts { get; }

   public static IEnumerable<string> AvailableSegmentNames
   {
      get
      {
         yield return "FilePath";
         yield return "Namespace";
         yield return "ParameterName";
         yield return "ParameterType";
         yield return "Indent";
      }
   }
   
   #endregion

   #region Public Methods and Operators

   public override RenderSize MeasureOverride(int availableWidth)
   {
      return Parts.Measure();
   }

   public override IEnumerable<Segment> RenderLine(IRenderContext context, int line)
   {
      foreach (var segment in Parts.SegmentsToRender)
         yield return segment;
   }

   public void HandleMouseInput(IMouseInputContext context)
   {
      if (File.Exists(FileName))
      {
         Process.Start("notepad", FileName);
      }
   }

   #endregion

   public void StartMeasuring()
   {
      Parts.Reset();

   }

   public void RemoveSegment(string segmentName)
   {
      Parts.RemovePart(segmentName);
   }
}