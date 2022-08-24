// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MapperMiddleware.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Middleware;

using System;
using System.Threading;
using System.Threading.Tasks;

using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

using JetBrains.Annotations;

using Microsoft.Extensions.DependencyInjection;

internal class MapperMiddleware<T> : Middleware<T>
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

   public override Task ExecuteAsync(IExecutionContext<T> context, CancellationToken cancellationToken)
   {
      if (cancellationToken.IsCancellationRequested)
         return Task.FromCanceled(cancellationToken);

      // Normally the mapper should create the arguments,
      // but if another middleware would decide to create and initialize them e.g. with default values,
      // we are ok with that here
      context.ApplicationArguments ??= serviceProvider.GetRequiredService<T>();
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

   private void Map(ICommandLineArguments args, T instance)
   {
      var mapper = CreateMapper();

      try
      {
         mapper.UnmappedCommandLineArgument += OnUnmappedCommandLineArgument;
         mapper.MappedCommandLineArgument += OnMappedCommandLineArgument;

         mapper.Map(args, instance);
      }
      finally
      {
         mapper.UnmappedCommandLineArgument -= OnUnmappedCommandLineArgument;
         mapper.MappedCommandLineArgument -= OnMappedCommandLineArgument;
      }
   }

   private void OnMappedCommandLineArgument(object sender, MapperEventArgs e)
   {
      // TODO do something with that
   }

   private void OnUnmappedCommandLineArgument(object sender, MapperEventArgs e)
   {
      // TODO handle unmapped command line arguments
   }

   #endregion
}