namespace ConsoLovers.ConsoleToolkit.Menu
{
   using System;

   public class ExpanderDescription
   {
      public string Expanded { get; set; } = "-";

      public string Collapsed { get; set; } = "+";

      public int Length => Math.Max(Expanded.Length, Collapsed.Length);
   }
}