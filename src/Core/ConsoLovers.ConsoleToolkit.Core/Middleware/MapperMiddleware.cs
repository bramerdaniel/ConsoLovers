// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MapperMiddleware.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Middleware;

using System;
using System.Threading;
using System.Threading.Tasks;

using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
using ConsoLovers.ConsoleToolkit.Core.Services;

using JetBrains.Annotations;

using Microsoft.Extensions.DependencyInjection;

internal class MapperMiddleware<T> : Middleware<IExecutionContext<T>>
   where T : class
{
   #region Constants and Fields

   private readonly IArgumentReflector argumentReflector;

   private readonly IServiceProvider serviceProvider;

   #endregion

   #region Constructors and Destructors

   public MapperMiddleware([NotNull] IServiceProvider serviceProvider, [NotNull] IArgumentReflector argumentReflector)
   {
      this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
      this.argumentReflector = argumentReflector ?? throw new ArgumentNullException(nameof(argumentReflector));
   }

   #endregion

   #region Public Properties

   public override int ExecutionOrder => KnownLocations.MapperMiddleware;

   #endregion

   #region Public Methods and Operators

   public override Task Execute(IExecutionContext<T> context, CancellationToken cancellationToken)
   {
      if (cancellationToken.IsCancellationRequested)
         return Task.FromCanceled(cancellationToken);

      Map(context.ParsedArguments, context.ApplicationArguments);
      return Next(context, cancellationToken);
   }

   #endregion

   #region Methods

   private IArgumentMapper<T> CreateMapper()
   {
      var info = argumentReflector.GetTypeInfo<T>();
      return info.HasCommands
         ? ActivatorUtilities.GetServiceOrCreateInstance<CommandMapper<T>>(serviceProvider)
         : ActivatorUtilities.GetServiceOrCreateInstance<ArgumentMapper<T>>(serviceProvider);
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
      // TODO do something with that
   }

   #endregion
}