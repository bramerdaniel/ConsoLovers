// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IAppicationBuilder.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


/* Unmerged change from project 'ConsoLovers.ConsoleToolkit.Core (net461)'
Before:
namespace ConsoLovers.ConsoleToolkit.Core.Builders
After:
namespace ConsoLovers.ConsoleToolkit.Core
{
   using ConsoLovers;
   using ConsoLovers.ConsoleToolkit;
   using ConsoLovers.ConsoleToolkit.Core;
   using ConsoLovers.ConsoleToolkit.Core;
   using ConsoLovers.ConsoleToolkit.Core.Builders
*/
namespace ConsoLovers.ConsoleToolkit.Core
{
   using ConsoLovers.ConsoleToolkit.Core.Builders;
   using Microsoft.Extensions.DependencyInjection;

   public interface IApplicationBuilder<T> : IDependencyInjectionAbstraction<IApplicationBuilder<T>>
      where T : class
   {
      /// <summary>Builds this <see cref="IConsoleApplication{T}"/> instance.</summary>
      /// <returns>The created <see cref="IConsoleApplication{T}"/></returns>
      IConsoleApplication<T> Build();

      /// <summary>Tells the <see cref="IApplicationBuilder{T}"/> to uses a initialized <see cref="IServiceCollection"/>.</summary>
      /// <param name="collection">The <see cref="IServiceCollection"/> to use.</param>
      /// <returns>The <see cref="IApplicationBuilder{T}"/> for more configuration</returns>
      IApplicationBuilder<T> UseServiceCollection(IServiceCollection collection);
   }

}