﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IArgumentNode.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.MenuBuilding
{
   interface IArgumentNode : IMenuNode
   {
      /// <summary>Gets the description.</summary>
      string Description { get; }

      /// <summary>Gets a value indicating whether this argument is a password.</summary>
      bool IsPassword { get; }

      /// <summary>Gets a value indicating whether this <see cref="IArgumentNode"/> was specified as required.</summary>
      bool Required { get; }

      /// <summary>Gets a value indicating whether this node should be visible during the initialization.</summary>
      bool ShowInInitialization { get; }

      /// <summary>Gets a value indicating whether this node should be visible in a menu.</summary>
      bool ShowInMenu { get; }
   }
}