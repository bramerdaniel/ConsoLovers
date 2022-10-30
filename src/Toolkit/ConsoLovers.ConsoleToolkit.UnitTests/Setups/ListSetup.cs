// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ListSetup.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.UnitTests.Setups;

using System.Collections.Generic;

using ConsoLovers.ConsoleToolkit.Controls;

using FluentSetups;

[FluentSetup(typeof(CList), SetupMethod = "List")]
public partial class ListSetup
{
   [FluentMember]
   private List<IRenderable> items;

   protected CList CreateTarget()
   {
      var target = new CList();
      foreach (var item in items)
         target.Add(item);
      return target;
   }

}