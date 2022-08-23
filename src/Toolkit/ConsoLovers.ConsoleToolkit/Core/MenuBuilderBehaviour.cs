// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MenuBuilderBehaviour.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit
{
   using ConsoLovers.ConsoleToolkit.Core;

   public enum MenuBuilderBehaviour
   {
      /// <summary>All commands will be show as menu items, and only those that are marked as not visible will be hidden</summary>
      ShowAllCommand,

      /// <summary>Only the commands that have the <see cref="MenuCommandAttribute"/> will be displayed in the menu</summary>
      WithAttributesOnly
   }
}