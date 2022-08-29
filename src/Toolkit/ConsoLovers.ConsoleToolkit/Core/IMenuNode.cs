// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMenuNode.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   using System;
   using System.Reflection;

   using ConsoLovers.ConsoleToolkit.Core.MenuBuilding;

   internal interface IMenuNode
   {
      #region Public Properties

      /// <summary>Gets the display name of the node.</summary>
      string DisplayName { get; }

      /// <summary>Gets the display order.</summary>
      int DisplayOrder { get; }

      /// <summary>Gets the parent node of the current node.</summary>
      ICommandNode Parent { get; }

      PropertyInfo PropertyInfo { get; }

      /// <summary>Gets the type of the node.</summary>
      Type Type { get; }

      #endregion
   }
}