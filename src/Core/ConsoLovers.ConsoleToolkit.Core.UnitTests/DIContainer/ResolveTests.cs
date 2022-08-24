// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResolveTests.cs" company="KUKA Deutschland GmbH">
//   Copyright (c) KUKA Deutschland GmbH 2006 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.DIContainer;

using System;
using System.Diagnostics.CodeAnalysis;

using ConsoLovers.ConsoleToolkit.Core.DIContainer;
using ConsoLovers.ConsoleToolkit.Core.UnitTests.DIContainer.Testclasses;
using ConsoLovers.ConsoleToolkit.Core.UnitTests.Setups;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
// ReSharper disable InconsistentNaming
[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
public class ResolveTests
{
   //[TestMethod]
   //public void EnsureIEnumerablesCanBeResolved()
   //{
   //   var container = Setup.Container().Done();
   //   container.Register(typeof(ICloneable), typeof(ImplOne)).WithLifetime(Lifetime.None);
   //   container.Register(typeof(ICloneable), typeof(ImplTwo)).WithLifetime(Lifetime.None);

   //   container.ResolveAll<ICloneable>().Should().HaveCount(2);
   //   container.ResolveAll(typeof(ICloneable)).Should().HaveCount(2);
   //}

   class ImplOne : ICloneable
   {
      public object Clone()
      {
         return new ImplOne();
      }
   }
   class ImplTwo : ICloneable
   {
      public object Clone()
      {
         return new ImplTwo();
      }
   }
}