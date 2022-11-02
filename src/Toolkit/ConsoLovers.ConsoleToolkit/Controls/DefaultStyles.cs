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

   private static RenderingStyle activeLinkStyle;

   private static RenderingStyle controlCharacterStyle;

   private static RenderingStyle errorMessageStyle;

   private static RenderingStyle mouseOver;

   private static RenderingStyle selection;

   #endregion

   #region Public Properties

   public static RenderingStyle ActiveLinkStyle
   {
      get => activeLinkStyle ??= new RenderingStyle(ConsoleColor.Blue);
      set => activeLinkStyle = value;
   }

   public static RenderingStyle ControlCharacterStyle
   {
      get => controlCharacterStyle ??= new RenderingStyle(ConsoleColor.DarkGray);
      set => controlCharacterStyle = value;
   }

   public static RenderingStyle ErrorMessageStyle
   {
      get => errorMessageStyle ??= new RenderingStyle(ConsoleColor.Red);
      set => errorMessageStyle = value;
   }

   public static RenderingStyle MouseOverStyle
   {
      get => mouseOver ??= new RenderingStyle(ConsoleColor.Black, ConsoleColor.Gray);
      set => mouseOver = value;
   }

   public static RenderingStyle SelectionStyle
   {
      get => selection ??= new RenderingStyle(ConsoleColor.Blue);
      set => selection = value;
   }

   #endregion
}