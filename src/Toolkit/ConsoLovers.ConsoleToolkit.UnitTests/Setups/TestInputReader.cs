// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestInputReader.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.UnitTests.Setups;

using System.Collections.Generic;

using ConsoLovers.ConsoleToolkit.Core;
using ConsoLovers.ConsoleToolkit.Core.MenuBuilding;

internal class TestInputReader : IInputReader
{
   #region Constants and Fields

   private readonly Dictionary<string, object> values = new();

   #endregion

   #region IInputReader Members

   public object ReadValue(IArgumentNode argumentNode, object initialValue)
   {
      if (values.TryGetValue(argumentNode.DisplayName, out var value))
         return value;

      if (values.TryGetValue(argumentNode.PropertyInfo.Name, out value))
         return value;

      return initialValue;
   }

   #endregion

   #region Public Methods and Operators

   public void AddInput(string argument, object value)
   {
      values.Add(argument, value);
   }

   #endregion
}