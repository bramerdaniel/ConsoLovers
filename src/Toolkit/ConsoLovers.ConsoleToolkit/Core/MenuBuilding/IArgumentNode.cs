// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IArgumentNode.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.MenuBuilding
{
   using System.Reflection;

   interface IArgumentNode : IMenuNode
   {
      bool IsPassword { get; }

      bool Required { get; }
   }
}