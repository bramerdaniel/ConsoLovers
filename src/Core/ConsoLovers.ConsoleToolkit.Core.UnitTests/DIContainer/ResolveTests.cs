// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ResolveTests.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.DIContainer;

using System;
using System.Diagnostics.CodeAnalysis;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
// ReSharper disable InconsistentNaming
[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
public class ResolveTests
{
   class ImplOne : ICloneable
   {
      #region ICloneable Members

      public object Clone()
      {
         return new ImplOne();
      }

      #endregion
   }

   class ImplTwo : ICloneable
   {
      #region ICloneable Members

      public object Clone()
      {
         return new ImplTwo();
      }

      #endregion
   }
}