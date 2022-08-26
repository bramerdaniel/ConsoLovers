// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArgumentNode.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   using System;
   using System.Collections.Generic;
   using System.Reflection;

   using ConsoLovers.ConsoleToolkit.Core.MenuBuilding;

   /// <summary>Node that represents a command</summary>
   /// <seealso cref="ICommandNode" />
   internal class ArgumentNode : IArgumentNode
   {
      public string DisplayName { get; set; }

      public int DisplayOrder { get; set; }

      public Type Type { get; set; }

      public PropertyInfo PropertyInfo { get; set; }

      public ArgumentInitializationModes InitializationMode { get; set; }

      public bool VisibleInMenu { get; set; }

      public bool IsPassword { get; set;}

      public bool Required { get; set; }
   }
}