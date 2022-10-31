// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExceptionParser.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

using System;
using System.Collections.Generic;
using System.Diagnostics;

using JetBrains.Annotations;

public class ExceptionData
{
   private readonly Exception exception;

   public ExceptionData([NotNull] Exception exception)
   {
      this.exception = exception ?? throw new ArgumentNullException(nameof(exception));
      StackTrace = new StackTrace(exception, fNeedFileInfo: true);
   }

   public StackTrace StackTrace { get; }

   public string StackFrameIndent { get; } = "   ";

   public IList<string> MessageLines { get; private set; }
   
   public string MessageHint => $"{exception.GetType().Name}: ";

   internal void Update(int maxLineLength)
   {
      MessageLines = exception.Message.Wrap(maxLineLength);
   }
}