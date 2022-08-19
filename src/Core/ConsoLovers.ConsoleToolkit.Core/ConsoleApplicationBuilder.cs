// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleApplicationBuilder.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core;

using ConsoLovers.ConsoleToolkit.Core.Builders;

public class ConsoleApplicationBuilder
{
   public static IApplicationBuilder<TArguments> ForArguments<TArguments>()
      where TArguments : class
   {
      return new ApplicationBuilder<TArguments>();
   }
}