﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Class1.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Playground.SomeWeirdLongNamespace;

using System;

internal class Thrower
{
   private readonly Action callback;

   public Thrower(Action callback, int times)
   {
      this.callback = callback;
      Run(callback, times < 10);
   }

   #region Public Methods and Operators

   public TimeSpan Run(Action callback, bool force)
   {
      CallCallback("Go");
      return TimeSpan.FromHours(1);
   }

   #endregion

   #region Methods

   private void CallCallback(string name)
   {
      callback();
   }

   #endregion
}