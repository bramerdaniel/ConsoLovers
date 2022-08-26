// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MenuItemNode.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   using System;
   using System.Collections.Generic;
   using System.Diagnostics;
   using System.Reflection;

   using ConsoLovers.ConsoleToolkit.Core.MenuBuilding;

   /// <summary>Node that represents a command</summary>
   /// <seealso cref="ICommandNode" />
   [DebuggerDisplay("{DisplayName} Children:{Nodes.Count}")]
   internal class CommandNode : ICommandNode
   {
      public string DisplayName { get; set; }

      public int DisplayOrder { get; set; }

      public Type ArgumentType { get; set; }

      public ICollection<IMenuNode> Nodes { get; set; }

      public Type Type { get; set; }

      public PropertyInfo PropertyInfo { get; set; }

      public ArgumentInitializationModes InitializationMode { get; set; }

      public bool IsVisible { get; set; }
   }
}