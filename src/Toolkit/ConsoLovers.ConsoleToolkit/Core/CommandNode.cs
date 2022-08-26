// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommandNode.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   using System;
   using System.Collections.Generic;
   using System.Diagnostics;
   using System.Linq;
   using System.Reflection;

   using ConsoLovers.ConsoleToolkit.Core.MenuBuilding;

   /// <summary>Node that represents a command</summary>
   /// <seealso cref="ICommandNode"/>
   [DebuggerDisplay("{DisplayName} Children:{Nodes.Count}")]
   internal class CommandNode : ICommandNode
   {
      #region ICommandNode Members

      public string DisplayName { get; set; }

      public int DisplayOrder { get; set; }

      public Type ArgumentType { get; set; }

      public ICollection<IMenuNode> Nodes { get; set; }

      public IArgumentNode FindArgument(string name)
      {
         return Nodes.OfType<IArgumentNode>()
            .FirstOrDefault(node => CanBeIdentifiedByName(name, node));
      }

      public Type Type { get; set; }

      public PropertyInfo PropertyInfo { get; set; }

      public ArgumentInitializationModes InitializationMode { get; set; }

      public bool VisibleInMenu { get; set; }

      #endregion

      #region Methods

      private static bool CanBeIdentifiedByName(string name, IArgumentNode node)
      {
         if (node.PropertyInfo.Name == name)
            return true;

         var attribute = node.PropertyInfo.GetAttribute<CommandLineAttribute>();
         if (attribute != null)
         {
            if (attribute.Name == name)
               return true;

            if (attribute.Aliases.Any(a => a == name))
               return true;
         }

         return false;
      }

      #endregion
   }
}