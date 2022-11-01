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
using System.Reflection;

using JetBrains.Annotations;

public class ExceptionDisplay : InteractiveRenderable
{
   private readonly MessageDisplay messageDisplay;

   private readonly StackTraceDisplay stackTraceDisplay;

   #region Constructors and Destructors

   public ExceptionDisplay([NotNull] Exception exception)
   {
      Exception = exception ?? throw new ArgumentNullException(nameof(exception));
      ExceptionData = new ExceptionData(exception);

      messageDisplay = new MessageDisplay(ExceptionData.MessageHint, exception.Message);
      
      StackTrace = new StackTrace(exception, fNeedFileInfo: true);
      stackTraceDisplay = new StackTraceDisplay(StackTrace);
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

   public override RenderSize MeasureOverride(int availableWidth)
   {
      var messageSize = messageDisplay.Measure(availableWidth);
      var stackTraceSize = stackTraceDisplay.Measure(availableWidth);

      return new RenderSize
      {
         Height = messageSize.Height + stackTraceSize.Height,
         Width =  Math.Max(messageSize.Width , stackTraceSize.Width) 
      };
   }

   public override IEnumerable<Segment> RenderLine(IRenderContext context, int line)
   {
      if (line < messageDisplay.Size.Height)
      {
         foreach (var segment in messageDisplay.RenderLine(context, line))
            yield return segment;
      }
      else
      {
         foreach (var segment in stackTraceDisplay.RenderLine(context, line - messageDisplay.Size.Height))
            yield return segment;
            

         //var frame = ExceptionData.StackTrace.GetFrame(line - ExceptionData.MessageLines.Count);
         //foreach (var segment in RenderStackFrame(frame))
         //   yield return segment;

         // yield return new Segment(this, frame.GetMethod().Name, RenderingStyle.Default.WithForeground(ConsoleColor.Red));
      }
   }

   private IEnumerable<Segment> RenderStackFrame(StackFrame frame)
   {
      var methodBase = frame.GetMethod();
      var irelevant = Style.WithForeground(ConsoleColor.Gray);

      yield return new Segment(this, ExceptionData.StackFrameIndent, irelevant);
      yield return new Segment(this, "at ", irelevant);
      if (methodBase is MethodInfo methodInfo)
         yield return new Segment(this, $"{methodInfo.ReturnType.AliasOrName()} ", Style.WithForeground(ConsoleColor.DarkCyan));
      if (methodBase is ConstructorInfo ctorInfo)
         yield return new Segment(this, $"{ctorInfo.DeclaringType.AliasOrName()}", Style.WithForeground(ConsoleColor.DarkCyan));
      
      yield return new Segment(this, $"{methodBase.Name}(", Style);

      var parameters = methodBase.GetParameters();
      for (var i = 0; i < parameters.Length; i++)
      {
         var parameter = parameters[i];
         yield return new Segment(this, parameter.ParameterType.AliasOrName(), Style.WithForeground(ConsoleColor.DarkCyan));
         if (i != parameters.Length - 1)
            yield return new Segment(this, ", ", irelevant);
      }

      yield return new Segment(this, $")", Style);
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