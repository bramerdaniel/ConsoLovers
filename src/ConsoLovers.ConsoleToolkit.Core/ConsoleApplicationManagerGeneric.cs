﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsoleApplicationManager.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core
{
   using System;
   using System.Threading.Tasks;

   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

   internal class ConsoleApplicationManagerGeneric<T> : ConsoleApplicationManager
      where T : class , IApplication
   {
      #region Constructors and Destructors

      internal ConsoleApplicationManagerGeneric(Func<T> createApplication)
         : base(_ => createApplication())
      {
      }

      internal ConsoleApplicationManagerGeneric()
         : this(() => new DefaultFactory().CreateInstance<T>())
      {
      }

      #endregion

      #region Public Methods and Operators
      
      public async Task<T> RunAsync(string args)
      {
         return (T)await RunAsync(typeof(T), args);
      }

      public async Task<T> RunAsync(string[] args)
      {
         return (T)await RunAsync(typeof(T), args);
      }

      #endregion
   }
}