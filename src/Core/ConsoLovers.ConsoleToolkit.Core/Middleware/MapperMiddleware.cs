// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MapperMiddleware.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Middleware;

using System;

using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
using ConsoLovers.ConsoleToolkit.Core.Services;

using JetBrains.Annotations;

using Microsoft.Extensions.DependencyInjection;

internal class MapperMiddleware<T> : Middleware<IInitializationContext<T>>
   where T : class
{
   #region Constructors and Destructors

   public MapperMiddleware([NotNull] IServiceProvider serviceProvider, [NotNull] IArgumentReflector argumentReflector)
   {
      ServiceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
      ArgumentReflector = argumentReflector ?? throw new ArgumentNullException(nameof(argumentReflector));
   }

   #endregion

   #region Public Properties

   public IArgumentReflector ArgumentReflector { get; }

   public IServiceProvider ServiceProvider { get; }

   #endregion

   #region Public Methods and Operators

   public override void Execute(IInitializationContext<T> context)
   {
      Map(context.ParsedArguments, context.ApplicationArguments);
      Next(context);
   }

   #endregion

   #region Methods

   private IArgumentMapper<T> CreateMapper()
   {
      var info = ArgumentReflector.GetTypeInfo<T>();
      return info.HasCommands
         ? ActivatorUtilities.GetServiceOrCreateInstance<CommandMapper<T>>(ServiceProvider)
         : ActivatorUtilities.GetServiceOrCreateInstance<ArgumentMapper<T>>(ServiceProvider);
   }

   private T Map(CommandLineArgumentList args, T instance)
   {
      var mapper = CreateMapper();

      try
      {
         mapper.UnmappedCommandLineArgument += OnUnmappedCommandLineArgument;
         mapper.MappedCommandLineArgument += OnMappedCommandLineArgument;

         return mapper.Map(args, instance);
      }
      finally
      {
         mapper.UnmappedCommandLineArgument -= OnUnmappedCommandLineArgument;
         mapper.MappedCommandLineArgument -= OnMappedCommandLineArgument;
      }
   }

   private void OnMappedCommandLineArgument(object sender, MapperEventArgs e)
   {
   }

   private void OnUnmappedCommandLineArgument(object sender, MapperEventArgs e)
   {
   }

   #endregion
}