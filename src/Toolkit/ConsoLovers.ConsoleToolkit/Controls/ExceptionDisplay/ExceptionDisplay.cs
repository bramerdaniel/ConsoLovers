// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExceptionDisplay.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

using JetBrains.Annotations;

public class ExceptionDisplay : InteractiveRenderable
{
   private readonly MessageDisplay messageDisplay;

   #region Constructors and Destructors

   public ExceptionDisplay([NotNull] Exception exception)
   {
      Exception = exception ?? throw new ArgumentNullException(nameof(exception));
      ExceptionData = new ExceptionData(exception);

      messageDisplay = new MessageDisplay(ExceptionData.MessageHint, exception.Message);

      StackTrace = new StackTrace(exception, fNeedFileInfo: true);
   }



   #endregion

   #region Public Properties

   public Exception Exception { get; }

   #endregion

   #region Properties

   private ExceptionData ExceptionData { get; set; }

   private StackTrace StackTrace { get; set; }

   #endregion

   #region Public Methods and Operators

   public override MeasuredSize MeasureOverride(int availableWidth)
   {
      var messageSize = messageDisplay.Measure(availableWidth);

      var messageLength = availableWidth - ExceptionData.MessageHint.Length;
      ExceptionData.Update(messageLength);

      return new MeasuredSize
      {
         Height = messageSize.Height + ExceptionData.StackTrace.FrameCount,
         MinWidth = availableWidth
      };
   }

   public override IEnumerable<Segment> RenderLine(IRenderContext context, int line)
   {
      if (line == 0)
      {
         foreach (var segment in messageDisplay.RenderLine(context, line))
            yield return segment;
      }
      else if(line < ExceptionData.MessageLines.Count)
      {
         foreach (var segment in messageDisplay.RenderLine(context, line))
            yield return segment;
      }
      else
      {
         var frame = ExceptionData.StackTrace.GetFrame(line - ExceptionData.MessageLines.Count);
         foreach (var segment in RenderStackFrame(frame))
            yield return segment;

         // yield return new Segment(this, frame.GetMethod().Name, RenderingStyle.Default.WithForeground(ConsoleColor.Red));
      }
   }

   private IEnumerable<Segment> RenderStackFrame(StackFrame frame)
   {
      var method = frame.GetMethod();

      var irelevant = Style.WithForeground(ConsoleColor.Gray);

      yield return new Segment(this, ExceptionData.StackFrameIndent, irelevant);
      yield return new Segment(this, "at ", irelevant);
      yield return new Segment(this, "void ", Style.WithForeground(ConsoleColor.DarkCyan));
      yield return new Segment(this, method.Name, Style);
      yield return new Segment(this, " in ", irelevant);

      var name = frame.GetFileName() ?? "NoFile.cs";
      name = Path.GetFileName(name);
      yield return new Segment(this, name, Style.WithForeground(ConsoleColor.DarkYellow));
      yield return new Segment(this, ":", irelevant);
      yield return new Segment(this, frame.GetFileLineNumber().ToString(), Style.WithForeground(ConsoleColor.Blue));
   }

   public RenderingStyle MessageStyle { get; set; } = DefaultStyles.ErrorMessageStyle;

   #endregion
}