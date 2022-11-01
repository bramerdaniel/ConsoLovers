// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StackTraceDisplay.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

using System;
using System.Collections.Generic;
using System.Diagnostics;

using JetBrains.Annotations;

public class StackTraceDisplay : InteractiveRenderable
{
   private readonly StackTrace stackTrace;

   private readonly StackFrame[] stackFrames;

   public StackTraceDisplay([NotNull] StackTrace stackTrace)
   {
      this.stackTrace = stackTrace ?? throw new ArgumentNullException(nameof(stackTrace));
      stackFrames = stackTrace.GetFrames();
   }

   public override MeasuredSize MeasureOverride(int availableWidth)
   {
      if (stackFrames == null)
         return MeasuredSize.Empty;

      int width = 0;
      foreach (var frame in stackFrames)
      {
         var frameWidth = ComputeFrameWidth(frame);
         width = Math.Max(width, frameWidth);
      }

      return new MeasuredSize { Height = stackTrace.FrameCount, MinWidth = width };
   }

   private int ComputeFrameWidth(StackFrame frame)
   {
      return 10;
   }

   public override IEnumerable<Segment> RenderLine(IRenderContext context, int line)
   {
      throw new System.NotImplementedException();
   }
}