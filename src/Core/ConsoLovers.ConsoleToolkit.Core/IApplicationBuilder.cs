// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IApplicationBuilder.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   using ConsoLovers.ConsoleToolkit.Core.Builders;

   using Microsoft.Extensions.DependencyInjection;

   public interface IApplicationBuilder<T> : IDependencyInjectionAbstraction<IApplicationBuilder<T>>
      where T : class
   {
      #region Public Methods and Operators

      /// <summary>Builds this <see cref="IConsoleApplication{T}"/> instance.</summary>
      /// <returns>The created <see cref="IConsoleApplication{T}"/></returns>
      IConsoleApplication<T> Build();

      /// <summary>Tells the <see cref="IApplicationBuilder{T}"/> to uses a initialized <see cref="IServiceCollection"/>.</summary>
      /// <param name="collection">The <see cref="IServiceCollection"/> to use.</param>
      /// <returns>The <see cref="IApplicationBuilder{T}"/> for more configuration</returns>
      IApplicationBuilder<T> UseServiceCollection(IServiceCollection collection);

      #endregion
   }
}