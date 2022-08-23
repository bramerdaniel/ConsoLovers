// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExecutionEngine.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

using JetBrains.Annotations;

using Microsoft.Extensions.DependencyInjection;

public class ExecutionEngine : IExecutionEngine
{
   #region Constants and Fields

   [NotNull]
   private readonly IServiceProvider serviceProvider;

   #endregion

   #region Constructors and Destructors

   public ExecutionEngine([NotNull] IArgumentReflector argumentReflector, [NotNull] IServiceProvider serviceProvider)
   {
      this.serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
      ArgumentReflector = argumentReflector ?? throw new ArgumentNullException(nameof(argumentReflector));
   }

   #endregion

   #region IExecutionEngine Members

   /// <summary>Executes the first mapped command of the <see cref="arguments"/>.</summary>
   /// <typeparam name="T">The type of the arguments to execute</typeparam>
   /// <param name="arguments">The arguments.</param>
   /// <returns>The command that was executed</returns>
   public ICommandBase ExecuteCommand<T>(T arguments)
   {
      return ExecuteCommandAsync(arguments, CancellationToken.None).GetAwaiter().GetResult();
   }

   /// <summary>Executes the first mapped command of the <see cref="arguments"/>.</summary>
   /// <typeparam name="T">The type of the arguments to execute</typeparam>
   /// <param name="arguments">The arguments.</param>
   /// <param name="cancellationToken">The cancellation token.</param>
   /// <returns>The command that was executed</returns>
   public async Task<ICommandBase> ExecuteCommandAsync<T>(T arguments, CancellationToken cancellationToken)
   {
      var applicationArguments = ArgumentReflector.GetTypeInfo<T>();
      if (!applicationArguments.HasCommands)
         return null;

      var command = GetMappedCommand(arguments);
      return command != null ? await ExecuteCommandAsync(command, cancellationToken) : null;
   }

   public void ExecuteCommand(ICommandBase executable, CancellationToken cancellationToken)
   {
      ExecuteCommandAsync(executable, cancellationToken).GetAwaiter().GetResult();
   }

   public void ExecuteCommand(ICommandBase executable)
   {
      ExecuteCommand(executable, CancellationToken.None);
   }

   public async Task<ICommandBase> ExecuteCommandAsync(ICommandBase executable, CancellationToken cancellationToken)
   {
      switch (executable)
      {
         case IAsyncCommand asyncCommand:
            await asyncCommand.ExecuteAsync(cancellationToken);
            return executable;
         case ICommand command:
            command.Execute();
            return executable;
         default:
            throw new InvalidOperationException("Command type not supported");
      }
   }

   public async Task ExecuteAsync<T>(T arguments, CancellationToken cancellationToken)
   {
      var typedLogic = serviceProvider.GetService<IApplicationLogic<T>>();
      if (typedLogic != null)
      {
         await typedLogic.ExecuteAsync(arguments, cancellationToken);
      }
      else
      {
         var applicationLogic = serviceProvider.GetRequiredService<IApplicationLogic>();
         await applicationLogic.ExecuteAsync(arguments, cancellationToken);
      }
   }

   #endregion

   #region Properties

   internal IArgumentReflector ArgumentReflector { get; }

   #endregion

   #region Methods

   private static ICommandBase GetMappedCommand<T>(T arguments)
   {
      if (arguments == null)
         return null;

      foreach (var propertyInfo in typeof(T).GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
      {
         if (propertyInfo.PropertyType.GetInterface(typeof(ICommandBase).FullName!) != null)
         {
            if (propertyInfo.GetValue(arguments) is ICommandBase value)
               return value;
         }
      }

      return null;
   }

   #endregion
}