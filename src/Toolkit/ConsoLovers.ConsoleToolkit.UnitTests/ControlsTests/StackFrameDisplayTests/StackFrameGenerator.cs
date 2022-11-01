// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StackFrameGenerator.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.UnitTests.ControlsTests.StackFrameDisplayTests;

using System;
using System.Diagnostics;
using System.Linq;

internal class StackFrameGenerator
{
   #region Public Methods and Operators

   public static StackFrame CreateNormal()
   {
      try
      {
         NormalMethod();
         throw new InvalidOperationException("Method did not fail");
      }
      catch (Exception e)
      {
         return CreateStackFrame(e);
      }
   }

   #endregion

   #region Methods

   private static StackFrame CreateStackFrame(Exception exception)
   {
      return new StackTrace(exception, fNeedFileInfo: true).GetFrames().FirstOrDefault();
   }

   private static void NormalMethod()
   {
      throw new InvalidOperationException($"{nameof(NormalMethod)} invocation failed");
   }

   #endregion
}