// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TestServiceProvider.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.DIContainer.Testclasses
{
   using System;

   /// <summary>Simple service provider impl</summary>
   public class TestServiceProvider : IServiceProvider
   {
      #region Constants and Fields

      private readonly Demo demo = new Demo(666) { Name = "FromServiceProvider" };

      #endregion

      #region IServiceProvider Members

      /// <summary>Gets the service object of the specified type.</summary>
      /// <param name="serviceType">An object that specifies the type of service object to get.</param>
      /// <returns>A service object of type <paramref name="serviceType"/>.-or- null if there is no service object of type <paramref name="serviceType"/>.</returns>
      public object GetService(Type serviceType)
      {
         if (serviceType.IsAssignableFrom(typeof(IDemo)))
            return demo;

         if (serviceType.IsAssignableFrom(typeof(Demo)))
            return demo;

         return null;
      }

      #endregion
   }
}