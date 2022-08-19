// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleMenuAttribute.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.CommandExtensions
{
   using System;

   [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property)]
   public class ConsoleMenuAttribute : Attribute
   {
      #region Constructors and Destructors

      public ConsoleMenuAttribute(string displayName)
      {
         DisplayName = displayName;
      }

      #endregion

      #region Public Properties

      public string DisplayName { get; set; }

      public bool Hide { get; set; }

      #endregion
   }
}