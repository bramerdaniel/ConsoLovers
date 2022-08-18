// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleApplicationManagerGeneric.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   using System;
   using System.Threading;
   using System.Threading.Tasks;

   using ConsoLovers.ConsoleToolkit.Core.BootStrappers;

   using Microsoft.Extensions.DependencyInjection;

   internal class ConsoleApplicationManagerGeneric<T> : ConsoleApplicationManager
      where T : class, IApplication
   {
      #region Constructors and Destructors

      internal ConsoleApplicationManagerGeneric(IServiceProvider serviceProvider)
         : base(typeof(T), serviceProvider)
      {
      }

      internal ConsoleApplicationManagerGeneric()
         : this(CreateDefaultServiceProvider())
      {
      }

      private static DefaultServiceProvider CreateDefaultServiceProvider()
      {
         var serviceCollection = new ServiceCollection()
            .AddRequiredServices()
            .AddArgumentTypes<T>();

         return new DefaultServiceProvider(serviceCollection);
      }

      #endregion

      #region Public Methods and Operators

      /// <summary>Runs the application asynchronous.</summary>
      /// <param name="args">The arguments as string.</param>
      /// <param name="cancellationToken">The cancellation token.</param>
      public new async Task<T> RunAsync(string args, CancellationToken cancellationToken)
      {
         return (T)await base.RunAsync(args, cancellationToken);
      }

      /// <summary>Runs the application asynchronous.</summary>
      /// <param name="args">The arguments as string array.</param>
      /// <param name="cancellationToken">The cancellation token.</param>
      public new async Task<T> RunAsync(string[] args, CancellationToken cancellationToken)
      {
         return (T)await base.RunAsync(args, cancellationToken);
      }

      #endregion

      #region Methods

      internal T CreateApplication()
      {
         return (T)CreateApplicationInternal();
      }

      #endregion
   }
}