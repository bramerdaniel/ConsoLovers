// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PrintableElement.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Prompts
{
   public abstract class PrintableElement : IPrintable
   {
      public abstract Measurement Measure(int maxWidth);

      public abstract void Print();
   }
}