// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArgumentNode.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   using System;
   using System.Diagnostics;
   using System.Reflection;

   using ConsoLovers.ConsoleToolkit.Core.MenuBuilding;

   /// <summary>Node that represents a command</summary>
   /// <seealso cref="ICommandNode"/>
   [DebuggerDisplay("{DisplayName}")]
   internal class ArgumentNode : IArgumentNode
   {
      #region Constructors and Destructors

      public ArgumentNode(ICommandNode parent)
      {
         Parent = parent;
      }

      #endregion

      #region IArgumentNode Members

      public string DisplayName { get; set; }

      public int DisplayOrder { get; set; }

      public Type Type { get; set; }

      public PropertyInfo PropertyInfo { get; set; }

      public bool ShowInMenu { get; set; }

      public string Description { get; set; }

      public bool IsPassword { get; set; }

      public bool Required { get; set; }

      /// <summary>Gets a value indicating whether this argument should be visible during the initialization.</summary>
      public bool ShowInInitialization { get; set; }

      #endregion

      #region Public Properties

      public ICommandNode Parent { get; }

      #endregion
   }
}