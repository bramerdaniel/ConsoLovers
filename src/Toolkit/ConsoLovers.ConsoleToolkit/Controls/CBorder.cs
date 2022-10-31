// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CBorder.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CBorder : Renderable, IHaveAlignment
{
   #region Constants and Fields

   private MeasuredSize contentSize;

   // TODO Add header for the panel
   private int lineCount;

   private MeasuredSize size;

   #endregion

   #region Constructors and Destructors

   /// <summary>Initializes a new instance of the <see cref="CBorder"/> class.</summary>
   /// <param name="content">The content.</param>
   public CBorder(IRenderable content)
   {
      Content = content;
      Border = Borders.Default;
   }

   #endregion

   #region IHaveAlignment Members

   public Alignment Alignment { get; set; }

   #endregion

   #region Public Properties

   public IRenderable Content { get; }

   public Thickness Padding { get; set; }

   public BorderCharSet Border { get; set; }

   #endregion

   #region Public Methods and Operators

   public override MeasuredSize Measure(int availableWidth)
   {
      if (Content == null)
      {
         size = new MeasuredSize { Height = 2, MinWidth = 2 };
         lineCount = 2;
         return size;
      }

      contentSize = Content.Measure(availableWidth - 2);
      lineCount = contentSize.Height + 2 + Padding.Bottom + Padding.Top;

      var width = Padding.Left + 1 + contentSize.MinWidth + 1 + Padding.Right;

      size = new MeasuredSize { Height = lineCount, MinWidth = width, };

      return size;
   }

   public override IEnumerable<Segment> RenderLine(IRenderContext context, int line)
   {
      if (line == 0)
      {
         yield return CreateBorderSegment(context, Border.TopLeft, Border.TopRight, Border.Top);
      }
      else if (lineCount - 1 == line)
      {
         yield return CreateBorderSegment(context, Border.BottomLeft, Border.BottomRight, Border.Bottom);
      }
      else if (line <= Padding.Top)
      {
         yield return CreateBorderSegment(context, Border.Left, Border.Right, ' ');
      }
      else if (line >= 1 + Padding.Top + contentSize.Height)
      {
         yield return CreateBorderSegment(context, Border.Left, Border.Right, ' ');
      }
      else
      {
         var leftWidth = Padding.Left + 1;
         yield return new Segment(this, Border.Left.ToString().PadRight(leftWidth), Style);

         var segments = RenderContent(context, line).ToArray();
         foreach (var segment in segments)
            yield return segment;

         var contentWidth = segments.Sum(x => x.Width);
         var rightWidth = context.AvailableWidth - leftWidth - contentWidth;

         yield return new Segment(this, Border.Right.ToString().PadLeft(rightWidth), Style);
      }
   }

   #endregion

   #region Methods

   private Segment CreateBorderSegment(IRenderContext context, char left, char right, char middle)
   {
      if (Alignment == Alignment.Left)
      {
         var width = contentSize.MinWidth + 2 + Padding.Left + Padding.Right;
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
         builder.Append(string.Empty.PadRight(context.AvailableWidth - 2, middle));
         builder.Append(right);
         return new Segment(this, builder.ToString(), Style);
      }
      else
      {
         var builder = new StringBuilder();
         builder.Append(left);
         builder.Append(string.Empty.PadRight(context.AvailableWidth - 2, middle));
         builder.Append(right);
         return new Segment(this, builder.ToString(), Style);
      }
      // context.Console.Write("├┤┬┴┼");
   }

   private IEnumerable<Segment> RenderContent(IRenderContext context, int lineIndex)
   {
      var availableSize = contentSize.MinWidth;
      var renderContext = new RenderContext { AvailableWidth = availableSize, Size = contentSize };
      foreach (var segment in Content.RenderLine(renderContext, lineIndex - 1 - Padding.Top))
         yield return segment;
   }

   #endregion
}