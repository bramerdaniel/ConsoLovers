﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICommandMenuManager.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.CommandExtensions
{
   using ConsoLovers.ConsoleToolkit.Menu;

   /// <summary>Service that can show a consolovers menu from the defined command structure </summary>
   public interface ICommandMenuManager
   {
      void Show<T>();

      void UseOptions(IConsoleMenuOptions options);
   }
}