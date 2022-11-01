// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StackTraceDisplay.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using JetBrains.Annotations;

public class StackTraceDisplay : Renderable
{
   #region Constructors and Destructors

   public StackTraceDisplay([NotNull] StackTrace stackTrace)
   {
      if (stackTrace == null)
         throw new ArgumentNullException(nameof(stackTrace));

      var stackFrames = stackTrace.GetFrames() ?? Array.Empty<StackFrame>();
      FrameDisplays = stackFrames.Select(sf => new StackFrameDisplay(sf)).ToArray();
   }

   #endregion

   #region Public Properties

   public StackFrameDisplay[] FrameDisplays { get; }

   #endregion

   #region Public Methods and Operators

   public override RenderSize MeasureOverride(int availableWidth)
   {
      if (FrameDisplays.Length == 0)
         return RenderSize.Empty;

      if (FitsIntoAvailableWidth(availableWidth, out var size))
         return size;
      
      foreach (var name in StartMeasuring())
      {
         Remove(name);

         if (FitsIntoAvailableWidth(availableWidth, out size))
            return size;
      }

      return size;
   }

   public override IEnumerable<Segment> RenderLine(IRenderContext context, int line)
   {
      foreach (var segment in FrameDisplays[line].RenderLine(context, 0))
         yield return segment;
   }

   #endregion

   #region Methods

   private bool FitsIntoAvailableWidth(int availableWidth, out RenderSize measuredSize)
   {
      int height = 0;
      int width = 0;
      foreach (var display in FrameDisplays)
      {
         var frameSize = display.Measure(availableWidth);
         if (frameSize.Width > availableWidth)
         {
            measuredSize = RenderSize.Empty;
            return false;
         }

         height += frameSize.Height;
         width = Math.Max(width, frameSize.Width);
      }

      measuredSize = new RenderSize { Height = height, Width = width };
      return true;
   }

   private void Remove(string toRemove)
   {
      foreach (var frameDisplay in FrameDisplays)
         frameDisplay.RemoveSegment(toRemove);
   }

   private IEnumerable<string> StartMeasuring()
   {
      foreach (var frameDisplay in FrameDisplays)
         frameDisplay.StartMeasuring();

      return StackFrameDisplay.AvailableSegmentNames;
   }

   #endregion
}