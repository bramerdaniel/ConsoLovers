// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeHelpProviderSetup.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.Setups;

using System;

using ConsoLovers.ConsoleToolkit.Core.DIContainer;
using ConsoLovers.ConsoleToolkit.Core.Services;

using Microsoft.Extensions.DependencyInjection;

public class TypeHelpProviderSetup : SetupBase<TypeHelpProvider>
{
   #region Constants and Fields

   private IServiceProvider serviceProvider;

   #endregion

   #region Public Methods and Operators

   public TypeHelpProviderSetup AddArgumentTypes<T>()
      where T : class
   {
      var serviceCollection = new ServiceCollection();
      serviceCollection.AddApplicationTypes<T>();
      serviceProvider = new Container(serviceCollection);
      return this;
   }

   #endregion

   #region Methods

   protected override TypeHelpProvider CreateInstance()
   {
      return new TypeHelpProvider(serviceProvider ?? new Container(), new DefaultLocalizationService());
   }

   #endregion
}