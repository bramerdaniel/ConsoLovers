// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MenuTree.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   using System.Collections.Generic;

   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

   internal class MenuTree
   {
      //public MenuTree(ArgumentClassInfo classInfo)
      //{
      //   foreach (var info in classInfo.CommandInfos)
      //   {
      //      var menuItem = CreateMenuItem(info);
      //      if (menuItem != null)
      //         yield return menuItem;
      //   }
      //}

      public IEnumerable<MenuTree> Nodes { get; set; }
   }
}