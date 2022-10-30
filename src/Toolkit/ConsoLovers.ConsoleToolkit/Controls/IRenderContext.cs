// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IRenderContext.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls
{
   public interface IRenderContext
   {
      int AvailableWidth { get; }

      MeasuredSize Size { get; }

   }
}