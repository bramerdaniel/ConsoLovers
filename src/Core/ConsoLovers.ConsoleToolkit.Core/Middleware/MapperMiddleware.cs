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
using ConsoLovers.ConsoleToolkit.Core.Exceptions;

using JetBrains.Annotations;

using Microsoft.Extensions.DependencyInjection;

internal class MapperMiddleware<T> : Middleware<T>
   where T : class
{
   #region Constants and Fields

   private readonly IArgumentReflector argumentReflector;

   private readonly IConsole console;

   private readonly IServiceProvider serviceProvider;

   private bool cancelExecution;

   private IMappingOptions options;

   #endregion

   #region Constructors and Destructors

   public MapperMiddleware([NotNull] IServiceProvider serviceProvider, [NotNull] IArgumentReflector argumentReflector, [NotNull] IConsole console)
   {
      this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
      this.argumentReflector = argumentReflector ?? throw new ArgumentNullException(nameof(argumentReflector));
      this.console = console ?? throw new ArgumentNullException(nameof(console));
   }

   #endregion

   #region Public Properties

   public override int ExecutionOrder => KnownLocations.MapperMiddleware;

   public IMappingOptions Options
   {
      get => options ??= serviceProvider.GetRequiredService<IExecutionOptions>().MappingOptions ?? new MappingOptions();
      set => options = value;
   }

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

      cancelExecution = false;
      Map(context.ParsedArguments, context.ApplicationArguments);

      if (cancelExecution)
         return Task.CompletedTask;

      if (cancellationToken.IsCancellationRequested)
         return Task.FromCanceled(cancellationToken);

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

   private void InvokeCustomHandler(MapperEventArgs args)
   {
      // TODO handle unmapped command line arguments
   }

   private void Map(ICommandLineArguments args, T instance)
   {
      var mapper = CreateMapper();

      try
      {
         mapper.UnmappedCommandLineArgument += OnUnmappedCommandLineArgument;
         mapper.Map(args, instance);
      }
      finally
      {
         mapper.UnmappedCommandLineArgument -= OnUnmappedCommandLineArgument;
      }
   }

   private void OnUnmappedCommandLineArgument(object sender, MapperEventArgs e)
   {
      ProcessUnmappedArguments(e);
   }

   private void ProcessUnmappedArguments(MapperEventArgs args)
   {
      var message = $"The argument {args.Argument.OriginalString} could not be mapped to the object {args.Instance}";

      if (Options.UnhandledArgumentsBehavior.HasFlag(UnhandledArgumentsBehaviors.LogToConsole))
         console.WriteLine(message, ConsoleColor.Red);

      if (Options.UnhandledArgumentsBehavior.HasFlag(UnhandledArgumentsBehaviors.CancelExecution))
         cancelExecution = true;

      if (Options.UnhandledArgumentsBehavior.HasFlag(UnhandledArgumentsBehaviors.UseCustomHandler))
         InvokeCustomHandler(args);

      if (Options.UnhandledArgumentsBehavior.HasFlag(UnhandledArgumentsBehaviors.ThrowException))
         throw new CommandLineArgumentException(message);
   }

   #endregion
}