// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MapperMiddleware.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Middleware;

using System;
using System.Collections.Generic;
using System.Linq;
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

      // Normally the mapper middleware should create the arguments here,
      // but if another middleware would decide to create and initialize them e.g. with default values before the mapping middleware,
      // we are ok with that here
      context.ApplicationArguments ??= GetOrCreateArguments();

      cancelExecution = false;
      Map(context.ParsedArguments, context.ApplicationArguments);
      InvokeMappingHandlers(context);

      if (cancelExecution)
         return Task.CompletedTask;

      if (cancellationToken.IsCancellationRequested)
         return Task.FromCanceled(cancellationToken);

      return Next(context, cancellationToken);
   }

   #endregion

   #region Methods

   private static bool MapWithHandler(IExecutionContext<T> context, IMappingHandler<T>[] mappingHandlers, CommandLineArgument argument)
   {
      return mappingHandlers.Any(mappingHandler => mappingHandler.TryMap(context.ApplicationArguments, argument));
   }

   private IArgumentMapper<T> CreateMapper()
   {
      var info = argumentReflector.GetTypeInfo<T>();
      return info.HasCommands
         ? ActivatorUtilities.GetServiceOrCreateInstance<CommandMapper<T>>(serviceProvider)
         : ActivatorUtilities.GetServiceOrCreateInstance<ArgumentMapper<T>>(serviceProvider);
   }

   private IMappingHandler<T>[] GetMappingHandlers()
   {
      var handlers = serviceProvider.GetService<IEnumerable<IMappingHandler<T>>>();
      return handlers == null ? Array.Empty<IMappingHandler<T>>() : handlers.ToArray();
   }

   private T GetOrCreateArguments()
   {
      return serviceProvider.GetService<T>() ?? ActivatorUtilities.CreateInstance<T>(serviceProvider);
   }

   private bool HandledByCustomHandler(MapperEventArgs args)
   {
      // TODO create the correct infrastructure to forward these arguments directly to the current command

      if (args.Instance is IArgumentSink handler)
      {
         if (handler.TakeArgument(args.Argument))
            return true;
      }

      return false;
   }

   private void InvokeMappingHandlers(IExecutionContext<T> context)
   {
      if (context.ParsedArguments.Count == 0)
         return;

      if (!Options.UnhandledArgumentsBehavior.HasFlag(UnhandledArgumentsBehaviors.UseCustomHandler))
         return;

      var mappingHandlers = GetMappingHandlers();
      if (mappingHandlers.Length == 0)
         return;

      var arguments = context.ParsedArguments.ToArray();
      foreach (var argument in arguments)
      {
         if (MapWithHandler(context, mappingHandlers, argument))
            context.ParsedArguments.Remove(argument);
      }
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

      if (Options.UnhandledArgumentsBehavior.HasFlag(UnhandledArgumentsBehaviors.UseCustomHandler))
      {
         if (HandledByCustomHandler(args))
            return;
      }

      if (Options.UnhandledArgumentsBehavior.HasFlag(UnhandledArgumentsBehaviors.LogToConsole))
         console.WriteLine(message, ConsoleColor.Red);

      if (Options.UnhandledArgumentsBehavior.HasFlag(UnhandledArgumentsBehaviors.CancelExecution))
         cancelExecution = true;

      if (Options.UnhandledArgumentsBehavior.HasFlag(UnhandledArgumentsBehaviors.ThrowException))
         throw new CommandLineArgumentException(message);
   }

   #endregion
}