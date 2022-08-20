// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleApplication.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core;

using ConsoLovers.ConsoleToolkit.Core.Builders;

/// <summary>Entry point for creating an <see cref="IConsoleApplication{T}"/></summary>
public static class ConsoleApplication
{
   #region Public Methods and Operators

   /// <summary>Creates an <see cref="IApplicationBuilder{T}"/></summary>
   /// <typeparam name="TArguments">The type of the arguments the application will use.</typeparam>
   /// <returns>The created <see cref="IApplicationBuilder{T}"/></returns>
   public static IApplicationBuilder<TArguments> WithArguments<TArguments>()
      where TArguments : class
   {
      return new ApplicationBuilder<TArguments>();
   }

   #endregion
}