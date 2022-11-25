// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Table.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

using System;
using System.Collections.Generic;
using System.Linq;

public class Table : Renderable
{
   #region Constants and Fields



   private readonly Queue<Data> renderQueue;

   List<Column> columns = new List<Column>();

   List<List<RenderSize>> measurements = new List<List<RenderSize>>();

   #endregion

   #region Constructors and Destructors

   public Table()
   {
      renderQueue = new Queue<Data>();
   }

   #endregion

   #region Public Methods and Operators

   public Table AddColumns(params IRenderable[] headers)
   {
      foreach (var header in headers)
      {
         var rows = new Column { new Cell(header) };
         columns.Add(rows);
      }

      return this;
   }

   public Table AddRow(params IRenderable[] renderables)
   {
      for (int i = 0; i < renderables.Length; i++)
      {
         var renderable = renderables[i];
         columns[i].Add(new Cell(renderable));
      }

      return this;
   }

   public override RenderSize MeasureOverride(IRenderContext context, int availableWidth)
   {
      // return context.Measure(root, availableWidth);

      int width = 0;
      int heigth = 0;
      foreach (var column in columns)
      {
         int columnWidth = 0;
         int columnHeight = 0;
         int line = 0;

         var sizes = new List<RenderSize>();
         measurements.Add(sizes);

         foreach (var cell in column)
         {
            var cellSize = context.Measure(cell, availableWidth);
            sizes.Add(cellSize);

            columnWidth = Math.Max(columnWidth, cellSize.Width);
            columnHeight += cellSize.Height;
         }

         width += columnWidth;
         heigth = Math.Max(heigth, columnHeight);
      }

      return new RenderSize { Width = width, Height = heigth };
   }

   public override IEnumerable<Segment> RenderLine(IRenderContext context, int line)
   {
      var list = measurements;

      foreach (var column in columns)
      {
         var size = context.GetMeasuredSize(column[0]);
      }

      foreach (var column in columns)
      {
         var lineToRender = line;

         foreach (var segment in context.RenderLine(column[0], lineToRender))
            yield return segment;
      }
   }

   #endregion

   #region Methods

   private Panel CreateColumn(IRenderable header)
   {
      var panel = new Panel { Orientation = Orientation.Vertical };
      var headerCell = new Cell(header) { Style = new RenderingStyle(ConsoleColor.Red, ConsoleColor.Cyan) };
      panel.Add(headerCell);
      return panel;
   }

   #endregion

   class Data
   {
      #region Public Properties

      public IRenderable Child { get; set; }

      public int Line { get; set; }

      #endregion
   }
}

class Column : List<Cell>
{
   public int Width => this.Max(x => x.MeasuredSize.Width);
   public int Height => this.Sum(x => x.MeasuredSize.Height);
}