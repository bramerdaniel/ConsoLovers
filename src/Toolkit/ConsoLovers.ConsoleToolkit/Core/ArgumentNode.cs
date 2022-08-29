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
      
      public bool ShowAsMenu { get; set; }

      public bool IsPassword { get; set;}

      public bool Required { get; set; }

      /// <summary>Gets a value indicating whether this argument should be visible during the initialization.</summary>
      public bool ShowInInitialization { get; set; }
   }
}