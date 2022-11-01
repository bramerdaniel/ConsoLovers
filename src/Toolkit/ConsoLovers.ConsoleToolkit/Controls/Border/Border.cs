// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CBorder.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Border : Renderable, IHaveAlignment
{
   #region Constants and Fields

   private RenderSize contentSize;

   // TODO Add header for the panel
   private int lineCount;
   
   #endregion

   #region Constructors and Destructors

   /// <summary>Initializes a new instance of the <see cref="Border"/> class.</summary>
   /// <param name="content">The content.</param>
   public Border(IRenderable content)
   {
      Content = content;
      CharSet = Borders.Default;
   }

   #endregion

   #region IHaveAlignment Members

   public Alignment Alignment { get; set; }

   #endregion

   #region Public Properties

   public IRenderable Content { get; }

   public Thickness Padding { get; set; }

   public BorderCharSet CharSet { get; set; }

   #endregion

   #region Public Methods and Operators

   public override RenderSize MeasureOverride(int availableWidth)
   {
      if (Content == null)
      {
         lineCount = 2;
         return new RenderSize { Height = 2, Width = 2 };
      }

      contentSize = Content.Measure(availableWidth - 2);
      lineCount = contentSize.Height + 2 + Padding.Bottom + Padding.Top;

      var width = Padding.Left + 1 + contentSize.Width + 1 + Padding.Right;

      return new RenderSize { Height = lineCount, Width = width };
   }

   public override IEnumerable<Segment> RenderLine(IRenderContext context, int line)
   {
      if (line == 0)
      {
         yield return CreateBorderSegment(context, CharSet.TopLeft, CharSet.TopRight, CharSet.Top);
      }
      else if (lineCount - 1 == line)
      {
         yield return CreateBorderSegment(context, CharSet.BottomLeft, CharSet.BottomRight, CharSet.Bottom);
      }
      else if (line <= Padding.Top)
      {
         yield return CreateBorderSegment(context, CharSet.Left, CharSet.Right, ' ');
      }
      else if (line >= 1 + Padding.Top + contentSize.Height)
      {
         yield return CreateBorderSegment(context, CharSet.Left, CharSet.Right, ' ');
      }
      else
      {
         var leftWidth = Padding.Left + 1;
         yield return new Segment(this, CharSet.Left.ToString().PadRight(leftWidth), Style);

         var segments = RenderContent(context, line).ToArray();
         foreach (var segment in segments)
            yield return segment;

         var contentWidth = segments.Sum(x => x.Width);
         var rightWidth = context.AvailableWidth - leftWidth - contentWidth;

         yield return new Segment(this, CharSet.Right.ToString().PadLeft(rightWidth), Style);
      }
   }

   #endregion

   #region Methods

   private Segment CreateBorderSegment(IRenderContext context, char left, char right, char middle)
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
      var availableSize = contentSize.Width;
      var renderContext = new RenderContext { AvailableWidth = availableSize, Size = contentSize };
      foreach (var segment in Content.RenderLine(renderContext, lineIndex - 1 - Padding.Top))
         yield return segment;
   }

   #endregion
}