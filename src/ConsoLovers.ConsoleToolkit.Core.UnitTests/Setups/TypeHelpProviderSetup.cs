// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TypeHelpProviderSetup.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.Setups;

using System;

using ConsoLovers.ConsoleToolkit.Core.BootStrappers;
using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
using ConsoLovers.ConsoleToolkit.Core.Localization;

public class TypeHelpProviderSetup : SetupBase<TypeHelpProvider>
{
   #region Constants and Fields

   private IServiceProvider serviceProvider;

   #endregion

   #region Public Methods and Operators

   public TypeHelpProviderSetup AddArgumentTypes<T>()
      where T : class
   {
      serviceProvider = DefaultServiceProvider.ForType<T>();
      return this;
   }

   #endregion

   #region Methods

   protected override TypeHelpProvider CreateInstance()
   {
      return new TypeHelpProvider(serviceProvider ?? new DefaultServiceProvider(), new DefaultLocalizationService());
   }

   #endregion
}