// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Cell.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Cell : Renderable, IHaveAlignment
{
   #region Constants and Fields

   private RenderSize contentSize;

   #endregion

   #region Constructors and Destructors

   /// <summary>Initializes a new instance of the <see cref="Cell"/> class.</summary>
   /// <param name="content">The content.</param>
   public Cell(IRenderable content)
   {
      Content = content;
      CharSet = Borders.Default;
   }

   public Cell(string content)
      : this(new Text(content))
   {
   }


   #endregion

   #region IHaveAlignment Members

   public Alignment Alignment { get; set; }

   #endregion

   #region Public Properties

   public BorderCharSet CharSet { get; set; }

   public IRenderable Content { get; }

   public Thickness Padding { get; set; }

   #endregion

   #region Public Methods and Operators

   public override IEnumerable<IRenderable> GetChildren()
   {
      if (Content != null)
         yield return Content;
   }

   public override RenderSize MeasureOverride(IRenderContext context, int availableWidth)
   {
      if (Content == null)
         return new RenderSize { Height = 2, Width = 2 };

      contentSize = context.Measure(Content, availableWidth - 2);

      return new RenderSize
      {
         Height = contentSize.Height + 2 + Padding.Bottom + Padding.Top,
         Width = Padding.Left + 1 + contentSize.Width + 1 + Padding.Right
      };
   }

   public override IEnumerable<Segment> RenderLine(IRenderContext context, int line)
   {
      if (line == 0)
      {
         yield return CreateBorderSegment(context, CharSet.TopLeft, CharSet.TopRight, CharSet.Top);
      }
      else if (IsLastLine(line))
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
         var rightWidth = MeasuredSize.Width - leftWidth - contentWidth;

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

   private IEnumerable<Segment> RenderContent(IRenderContext context, int lineIndex)
   {
      foreach (var segment in Content.RenderLine(context, lineIndex - 1 - Padding.Top))
         yield return segment;
   }

   #endregion

   // TODO Add header for the panel
}