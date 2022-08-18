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
   [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented",
      Justification = "Reviewed. Suppression is OK here.")]
   public class AttributePropertySelectionStrategyTests
   {
      [TestMethod]
      public void Strategy_should_return_no_property_because_no_attribute_was_used()
      {
         var target = PropertySelectionStrategies.AllWithDepencencyAttribute;
         var properties = target.SelectProperties(typeof(NoDependanceAttribute)).ToList();
         properties.Should().NotBeNull();
         properties.Count.Should().Be(0);
      }

      [TestMethod]
      public void Strategy_should_return_no_property_because_attribute_was_used_on_private_property()
      {
         var target = PropertySelectionStrategies.OnlyPublicWithDepencencyAttribute;
         var properties = target.SelectProperties(typeof(PrivateDependanceAttribute)).ToList();
         properties.Should().NotBeNull();
         properties.Count.Should().Be(0);
      }

      [TestMethod]
      public void Strategy_should_return_one_property_because_attribute_was_used()
      {
         var target = PropertySelectionStrategies.OnlyPublicWithDepencencyAttribute;
         var properties = target.SelectProperties(typeof(OneDependanceAttribute)).ToList();
         properties.Should().NotBeNull();
         properties.Count.Should().Be(1);
      }

      [TestMethod]
      public void Strategy_should_return_one_property_even_if_attribute_was_used_on_private_property()
      {
         var target = PropertySelectionStrategies.AllWithDepencencyAttribute;
         var properties = target.SelectProperties(typeof(PrivateDependanceAttribute)).ToList();
         properties.Should().NotBeNull();
         properties.Count.Should().Be(1);
      }

      // ReSharper restore InconsistentNaming
   }
}