// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICommandNode.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.MenuBuilding
{
   using System;
   using System.Collections.Generic;

   interface ICommandNode : IMenuNode
   {
      Type ArgumentType{ get; }

      ICollection<IMenuNode> Nodes { get; }

      IArgumentNode FindArgument(string name);
   }
}