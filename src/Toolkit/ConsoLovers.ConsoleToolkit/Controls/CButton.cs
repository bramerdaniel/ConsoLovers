// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CButton.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

using System;
using System.Collections.Generic;
using System.Text;

using ConsoLovers.ConsoleToolkit.InputHandler;

public class CButton : InteractiveRenderable, IMouseInputHandler, IHaveAlignment, IMouseAware
{
   private int lineCount;

   private RenderSize contentSize;

   private RenderSize size;

   private bool isMouseOver;

   public CButton(IRenderable content)
   {
      Content = content;
   }

   public IRenderable Content { get; }

   public Alignment Alignment { get; set; }

   public Thickness Padding { get; set; }

   public override IEnumerable<IRenderable> GetChildren()
   {
      yield return Content;
   }

   public override RenderSize MeasureOverride(int availableWidth)
   {
      contentSize = Content.Measure(availableWidth - 2);
      lineCount = contentSize.Height + 2 + Padding.Bottom + Padding.Top;

      var width = Padding.Left + 1 + contentSize.Width + 1 + Padding.Right;

      size = new RenderSize
      {
         Height = lineCount,
         Width = width,
      };

      return size;
   }

   public override IEnumerable<Segment> RenderLine(IRenderContext context, int line)
   {
      if (line == 0)
      {
         yield return CreateBorderSegment(context, "┌", "┐", '─');
      }
      else if (lineCount - 1 == line)
      {
         yield return CreateBorderSegment(context, "└", "┘", '─');
      }
      else if (line <= Padding.Top)
      {
         yield return CreateBorderSegment(context, "│", "│", ' ');
      }
      else if (line >= 1 + Padding.Top + contentSize.Height)
      {
         yield return CreateBorderSegment(context, "│", "│", ' ');
      }
      else
      {
         yield return new Segment(this, "│".PadRight(Padding.Left + 1), Style);

         foreach (var segment in RenderContent(context, line))
            yield return new Segment(this, segment.Text, segment.Style);

         yield return new Segment(this, "│".PadLeft(Padding.Right + 1), Style);
      }
   }

   public void HandleMouseInput(IMouseInputContext context)
   {
      if (context.MouseEventArgs.ButtonState == ButtonStates.Left)
         Clicked?.Invoke(this, EventArgs.Empty);
   }

   public void HandleMouseMove(IMouseInputContext context)
   {
      // 
   }

   public event EventHandler Clicked;

   private IEnumerable<Segment> RenderContent(IRenderContext context, int lineIndex)
   {
      foreach (var segment in Content.RenderLine(context, lineIndex - 1 - Padding.Top))
         yield return segment;
   }

   private Segment CreateBorderSegment(IRenderContext context, string left, string right, char middle)
   {
      if (Alignment == Alignment.Left)
      {
         var width = contentSize.Width + 2 + Padding.Left + Padding.Right;
         var builder = new StringBuilder();
         builder.Append(left);
         builder.Append(string.Empty.PadRight(width - 2, middle));
         builder.Append(right);
         return new Segment(this, builder.ToString(), Style);
      }

      if (Alignment == Alignment.Right)
      {
         var builder = new StringBuilder();
         builder.Append(left);
         builder.Append(string.Empty.PadRight(MeasuredSize.Width - 2, middle));
         builder.Append(right);
         return new Segment(this, builder.ToString(), Style);
      }
      else
      {
         var builder = new StringBuilder();
         builder.Append(left);
         builder.Append(string.Empty.PadRight(MeasuredSize.Width - 2, middle));
         builder.Append(right);
         return new Segment(this, builder.ToString(), Style);
      }
      // context.Console.Write("├┤┬┴┼");

   }

   /// <summary>Gets or sets a value indicating whether this instance is mouse over.</summary>
   /// <value>
   ///   <c>true</c> if this instance is mouse over; otherwise, <c>false</c>.
   /// </value>
   bool IMouseAware.IsMouseOver
   {
      get => isMouseOver;
      set
      {
         if (isMouseOver == value)
            return;

         isMouseOver = value;
         NotifyStyleChanged();
      }
   }
}