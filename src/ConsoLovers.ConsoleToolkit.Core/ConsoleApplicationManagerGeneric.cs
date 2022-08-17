// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleApplicationManager.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   using System;
   using System.Threading;
   using System.Threading.Tasks;

   using ConsoLovers.ConsoleToolkit.Core.BootStrappers;
   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

   using Microsoft.Extensions.DependencyInjection;

   internal class ConsoleApplicationManagerGeneric<T> : ConsoleApplicationManager
      where T : class , IApplication
   {
      #region Constructors and Destructors

      internal ConsoleApplicationManagerGeneric(IServiceProvider serviceProvider)
         : base(serviceProvider)
      {
      }

      internal ConsoleApplicationManagerGeneric()
         : this(new DefaultServiceProvider(new ServiceCollection().AddArgumentTypes<T>()))
      {
      }

      #endregion

      #region Public Methods and Operators

      /// <summary>Runs the application asynchronous.</summary>
      /// <param name="args">The arguments as string.</param>
      /// <param name="cancellationToken">The cancellation token.</param>
      public async Task<T> RunAsync(string args, CancellationToken cancellationToken)
      {
         return (T)await RunAsync(typeof(T), args, cancellationToken);
      }

      /// <summary>Runs the application asynchronous.</summary>
      /// <param name="args">The arguments as string array.</param>
      /// <param name="cancellationToken">The cancellation token.</param>
      public async Task<T> RunAsync(string[] args, CancellationToken cancellationToken)
      {
         return (T)await RunAsync(typeof(T), args, cancellationToken);
      }

      #endregion
   }
}