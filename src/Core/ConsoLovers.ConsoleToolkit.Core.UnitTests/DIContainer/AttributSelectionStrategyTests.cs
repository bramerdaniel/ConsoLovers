// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AttributSelectionStrategyTests.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.DIContainer
{
   using System.Diagnostics.CodeAnalysis;
   using System.Linq;

   using ConsoLovers.ConsoleToolkit.Core.DIContainer.Strategies;
   using ConsoLovers.ConsoleToolkit.Core.UnitTests.DIContainer.Testclasses;

   using FluentAssertions;

   using Microsoft.VisualStudio.TestTools.UnitTesting;

   /// <summary>Testclass for the constructor selection strategies</summary>
   [TestClass]
   // ReSharper disable InconsistentNaming
   [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
   public class AttributSelectionStrategyTests
   {
      [TestMethod]
      public void Strategy_should_return_constructor_with_attribute()
      {
         var target = ConstructorSelectionStrategies.WithInjectionConstructorAttribute;
         var constructorInfo = target.SelectCostructor(typeof(OneAttribute));
         constructorInfo.Should().NotBeNull();
         constructorInfo.GetParameters().Count().Should().Be(0);
      }

      [TestMethod]
      public void Strategy_should_return_null()
      {
         var target = ConstructorSelectionStrategies.WithInjectionConstructorAttribute;
         var constructorInfo = target.SelectCostructor(typeof(NoAttributes));
         constructorInfo.Should().BeNull();
      }

      [TestMethod]
      public void Strategy_should_return_not_null_for_multiple_injectionConstructor_attributes()
      {
         var target = ConstructorSelectionStrategies.WithInjectionConstructorAttribute;
         var constructorInfo = target.SelectCostructor(typeof(MultipleConstructorAttributes));
         constructorInfo.Should().NotBeNull();
         constructorInfo.GetParameters().Count().Should().Be(0);
      }

      // ReSharper restore InconsistentNaming
   }
}