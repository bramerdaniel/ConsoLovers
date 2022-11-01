﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExceptionDisplay.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

using System;
using System.Collections.Generic;
using System.Diagnostics;

using JetBrains.Annotations;

public class ExceptionDisplay : InteractiveRenderable
{
   private readonly MessageDisplay messageDisplay;

   private readonly StackTraceDisplay stackTraceDisplay;

   #region Constructors and Destructors

   public ExceptionDisplay([NotNull] Exception exception)
   {
      // TODO provide some options for customization
      Exception = exception ?? throw new ArgumentNullException(nameof(exception));

      messageDisplay = new MessageDisplay($"{exception.GetType().Name}: ", exception.Message);
      
      StackTrace = new StackTrace(exception, fNeedFileInfo: true);
      stackTraceDisplay = new StackTraceDisplay(StackTrace);
   }

   #endregion

   #region Public Properties

   public Exception Exception { get; }

   #endregion

   #region Properties
   

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
      }
   }

 
   public RenderingStyle MessageStyle { get; set; } = DefaultStyles.ErrorMessageStyle;

   #endregion
}