// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAppicationBuilder.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.Builders
{
   using Microsoft.Extensions.DependencyInjection;

   public interface IApplicationBuilder<T> : IDependencyInjectionAbstraction<IApplicationBuilder<T>>
      where T : class
   {
      IExecutable<T> Build();
   }
}