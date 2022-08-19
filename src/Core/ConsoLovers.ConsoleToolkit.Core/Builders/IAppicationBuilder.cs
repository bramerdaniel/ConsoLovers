// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAppicationBuilder.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Builders
{
   using System.Threading;
   using System.Threading.Tasks;

   public interface IApplicationBuilder<T> : IDependencyInjectionAbstraction<IApplicationBuilder<T>>
      where T : class
   {
      IExecutable Build();
   }

   public interface IExecutable
   {
      Task RunAsync(string args, CancellationToken cancellation);
      Task RunAsync(string[] args, CancellationToken cancellation);
   }

   internal class Executable : IExecutable
   {
      public Task RunAsync(string args, CancellationToken cancellation)
      {
         throw new System.NotImplementedException();
      }

      public Task RunAsync(string[] args, CancellationToken cancellation)
      {
         throw new System.NotImplementedException();
      }
   }
}