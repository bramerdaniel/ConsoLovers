// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContainerSetup.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.Setups;

using ConsoLovers.ConsoleToolkit.Core.DIContainer;

using FluentSetups;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

[FluentSetup(typeof(Container))]
internal partial class ContainerSetup : ServiceCollection
{
   #region Public Methods and Operators

   public ContainerSetup Register(ServiceDescriptor serviceDescriptor)
   {
      this.Add(serviceDescriptor);
      return this;
   }

   #endregion

   #region Methods

   protected void SetupTarget(Container target)
   {
      SetupOptions(target);
      foreach (var descriptor in this)
         target.Register(descriptor);
   }

   #endregion
}