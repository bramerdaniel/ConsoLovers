// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DefaultStyles.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Controls;

using System;

/// <summary>Static class containing a bunch of default styles the build in controls use</summary>
public static class DefaultStyles
{
   #region Constants and Fields

   private static RenderingStyle mouseOver;

   private static RenderingStyle selection;

   #endregion

   #region Public Properties

   public static RenderingStyle MouseOverStyle
   {
      get => mouseOver ??= new RenderingStyle(ConsoleColor.Black, ConsoleColor.Gray);
      set => mouseOver = value;
   }

   public static RenderingStyle SelectionStyle
   {
      get => selection ??= new RenderingStyle(ConsoleColor.Black, ConsoleColor.White);
      set => selection = value;
   }

   #endregion
}