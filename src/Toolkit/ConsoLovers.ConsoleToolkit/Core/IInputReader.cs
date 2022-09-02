// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IInputReader.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   using ConsoLovers.ConsoleToolkit.Core.MenuBuilding;

   internal interface IInputReader
   {
      object ReadValue(IArgumentNode argumentNode, object initialValue);
   }
}