// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IConsoleBuffer.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Console
{
   using System;

   /// <summary>Interface providing direct write access to the console buffer, without the need to set the cursor before.</summary>
   public interface IConsoleBuffer
   {
      #region Public Methods and Operators

      /// <summary>Draws the specified text.</summary>
      /// <param name="left">The column.</param>
      /// <param name="top">The line.</param>
      /// <param name="text">The text.</param>
      void WriteLine(int left, int top, string text);

      /// <summary>Draws the specified text.</summary>
      /// <param name="left">The column.</param>
      /// <param name="top">The line.</param>
      /// <param name="text">The text.</param>
      /// <param name="clearLine">if set to <c>true</c> line is cleared before drawing.</param>
      void WriteLine(int left, int top, string text, bool clearLine);

      /// <summary>Draws the specified text.</summary>
      /// <param name="left">The column.</param>
      /// <param name="top">The line.</param>
      /// <param name="text">The text.</param>
      /// <param name="foreground">The foreground.</param>
      /// <param name="clearLine">if set to <c>true</c> line is cleared before drawing.</param>
      void WriteLine(int left, int top, string text, ConsoleColor foreground, bool clearLine);

      /// <summary>Draws the specified text.</summary>
      /// <param name="left">The column.</param>
      /// <param name="top">The line.</param>
      /// <param name="text">The text.</param>
      /// <param name="foreground">The foreground.</param>
      /// <param name="background">The background.</param>
      /// <param name="clearLine">if set to <c>true</c> line is cleared before drawing.</param>
      void WriteLine(int left, int top, string text, ConsoleColor foreground, ConsoleColor background, bool clearLine);

      #endregion
   }
}