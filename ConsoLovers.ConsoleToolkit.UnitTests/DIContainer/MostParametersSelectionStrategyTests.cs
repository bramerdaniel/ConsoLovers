namespace ConsoLovers.UnitTests.DIContainer
{
   using System.Diagnostics.CodeAnalysis;
   using System.Linq;

   using ConsoLovers.ConsoleToolkit.DIContainer.Strategies;
   using ConsoLovers.UnitTests.DIContainer.Testclasses;

   using FluentAssertions;

   using Microsoft.VisualStudio.TestTools.UnitTesting;

   /// <summary>Testclass for the constructor selection strategies</summary>
   [TestClass]
   // ReSharper disable InconsistentNaming
   [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented",
      Justification = "Reviewed. Suppression is OK here.")]
   public class MostParametersSelectionStrategyTests
   {
      [TestMethod]
      public void Strategy_should_return_constructor_with_one_parameters()
      {
         var target = ConstructorSelectionStrategies.WithMostParameters;
         var constructorInfo = target.SelectCostructor(typeof(OneAttribute));
         constructorInfo.Should().NotBeNull();
         constructorInfo.GetParameters().Count().Should().Be(1);
      }

      [TestMethod]
      public void Strategy_should_return_constructor_with_two_parameters()
      {
         var target = ConstructorSelectionStrategies.WithMostParameters;
         var constructorInfo = target.SelectCostructor(typeof(MultipleConstructorAttributes));
         constructorInfo.Should().NotBeNull();
         constructorInfo.GetParameters().Count().Should().Be(3);
      }

      [TestMethod]
      public void Strategy_should_return_default_constructor()
      {
         var target = ConstructorSelectionStrategies.WithMostParameters;
         var constructorInfo = target.SelectCostructor(typeof(Simple));
         constructorInfo.Should().NotBeNull();
         constructorInfo.GetParameters().Count().Should().Be(0);
      }

      // ReSharper restore InconsistentNaming
   }
}