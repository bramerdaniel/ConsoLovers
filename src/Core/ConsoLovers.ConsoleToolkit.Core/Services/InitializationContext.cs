// --------------------------------------------------------------------------------------------------------------------
// <copyright file="InitializationContext.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Services;

using System;

using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

using JetBrains.Annotations;

internal class InitializationContext<T> : IInitializationContext<T>
   where T : class
{
   public CommandLineArgumentList ParsedArguments { get; set; }

   public T ApplicationArguments { get; set; }

   public object Commandline { get; set; }
   

   public InitializationContext([NotNull] T arguments, [NotNull] object commandLine)
   {
      ApplicationArguments = arguments ?? throw new ArgumentNullException(nameof(arguments));
      Commandline = commandLine ?? throw new ArgumentNullException(nameof(commandLine));
   }
}