﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="StackPanelSetup.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.UnitTests.Setups;

using System.Collections.Generic;

using ConsoLovers.ConsoleToolkit.Controls;

using FluentSetups;

[FluentSetup(typeof(Panel), SetupMethod = "Panel")]
public partial class StackPanelSetup
{
   [FluentMember]
   private List<IRenderable> children;

   protected Panel CreateTarget()
   {
      var target = new Panel();
      foreach (var child in children)
         target.Add(child);
      return target;
   }

   //public StackPanelSetup Add(IRenderable renderable)
   //{
   //   return this;
   //}
}