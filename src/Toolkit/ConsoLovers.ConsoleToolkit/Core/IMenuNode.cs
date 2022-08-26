// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IMenuNode.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   using System;
   using System.Reflection;

   internal interface IMenuNode
   {
      #region Public Properties

      string DisplayName { get; }

      /// <summary>Gets the display order.</summary>
      int DisplayOrder { get; }

      ArgumentInitializationModes InitializationMode { get; }

      bool IsVisible { get; }

      PropertyInfo PropertyInfo { get; }

      /// <summary>Gets the type of the node.</summary>
      Type Type { get; }

      #endregion
   }
}