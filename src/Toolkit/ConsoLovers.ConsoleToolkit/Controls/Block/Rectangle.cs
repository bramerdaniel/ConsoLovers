// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Block.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

using System;
using System.Collections.Generic;

public class Rectangle : Renderable
{
   #region Constants and Fields

   private string value = " ";

   #endregion

   #region Constructors and Destructors

   public Rectangle()
      : this(1, 1, Rectangle.DefaultStyle)
   {
   }

   public Rectangle(RenderingStyle style)
      : this(1, 1, style)
   {
   }

   public Rectangle(int height, int width, RenderingStyle style)
      : base(style)
   {
      Height = height;
      Width = width;
   }

   public Rectangle(int widthAndHeight, RenderingStyle style)
      : this(widthAndHeight, widthAndHeight, style)
   {
   }

   #endregion

   #region Public Properties

   public static RenderingStyle DefaultStyle { get; } = new(null, ConsoleColor.DarkGray);

   /// <summary>Gets or sets the height of the block.</summary>
   public int Height { get; set; }

   /// <summary>Gets or sets the character that is used to render the block.</summary>
   public char Value
   {
      get => value[0];
      set => this.value = value.ToString();
   }

   /// <summary>Gets or sets the width of the block.</summary>
   public int Width { get; set; }

   #endregion

   #region Public Methods and Operators

   public override RenderSize MeasureOverride(IRenderContext context, int availableWidth)
   {
      return new RenderSize { Width = Math.Min(Width, availableWidth), Height = Height };
   }

   public override IEnumerable<Segment> RenderLine(IRenderContext context, int line)
   {
      if (line >= Height)
         throw new ArgumentOutOfRangeException(nameof(line), $"Can only render lines between {0} and {Height - 1}");

      yield return new Segment(this, value.PadRight(Width, Value), Style);
   }

   #endregion
}