// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Text.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Prompts;

using System;

public class Text : Renderable
{
   private int? minWidth;

   #region Constructors and Destructors

   public Text(string value, RenderingStyle style)
      : base(style)
   {
      Value = value ?? String.Empty;
   }

   public Text(string value)
   {
      Value = value;
   }

   public int MinWidth
   {
      get => minWidth.GetValueOrDefault(Value.Length);
      set => minWidth = value;
   }

   public Alignment Alignment { get; set; }

   #endregion

   #region Public Properties

   public string Value { get; set; }

   internal int Length => Value.Length; 

   #endregion

   #region Public Methods and Operators

   public override Measurement Measure(int maxWidth)
   {
      return new Measurement(MinWidth, Value.Length);
   }

   public override void Render(IRenderContext context)
   {
      context.Console.Write(AddPadding(), Style.Foreground, Style.Background);
   }

   private string AddPadding()
   {
      if (Alignment == Alignment.Left)
         return Value.PadRight(MinWidth);
      if (Alignment == Alignment.Right)
         return Value.PadLeft(MinWidth);

      var missing = MinWidth - Value.Length;
      var left = missing / 2 ;
      return Value.PadLeft(left + Length).PadRight(MinWidth);
   }

   #endregion
}