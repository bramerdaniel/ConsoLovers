// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IProgressWriter.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ProgressDemo
{
   public interface IProgressWriter
   {
      #region Public Methods and Operators

      void DrawValue(ProgressInfo progressInfo);

      #endregion
   }
}