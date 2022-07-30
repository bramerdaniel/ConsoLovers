// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReflectionExtensions.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests;

using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

using FluentAssertions;

using JetBrains.Annotations;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
[SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
public class ReflectionExtensionTests
{
   #region Public Methods and Operators

   [TestMethod]
   public void EnsureInternalPropertiesAreFoundCorrectly()
   {
      GetPropertiesWithAttributes<InternalProperty>().Should().HaveCount(1);
   }

   [TestMethod]
   public void EnsureNormalPropertiesAreFoundCorrectly()
   {
      GetPropertiesWithAttributes<NormalProperty>().Should().HaveCount(1);
   }

   [TestMethod]
   public void EnsurePrivatePropertiesAreFound()
   {
      GetPropertiesWithAttributes<PrivateProperty>().Should().HaveCount(1);
   }

   [TestMethod]
   public void EnsurePropertiesWithoutSettersAreNotFound()
   {
      GetPropertiesWithAttributes<PropertyWithoutSetter>().Should().HaveCount(2);
   }

   [TestMethod]
   public void EnsurePropertiesWithPrivateSettersAreFound()
   {
      GetPropertiesWithAttributes<PropertyWithPrivateSetter>().Should().HaveCount(2);
   }

   #endregion

   #region Methods

   private static IEnumerable<KeyValuePair<PropertyInfo, CommandLineAttribute>> GetPropertiesWithAttributes<T>()
   {
      return typeof(T).GetPropertiesWithAttributes();
   }

   #endregion

   [UsedImplicitly]
   private class InternalProperty
   {
      #region Properties

      [UsedImplicitly]
      [Argument("Name")]
      internal string Name { get; set; }

      #endregion
   }

   [UsedImplicitly]
   private class NormalProperty
   {
      #region Public Properties

      [UsedImplicitly]
      [Argument("Name")] 
      public string Name { get; set; }

      #endregion
   }

   [UsedImplicitly]
   private class PrivateProperty
   {
      #region Properties

      [UsedImplicitly]
      [Argument("Name")] 
      private string Name { get; set; }

      #endregion
   }

   [UsedImplicitly]
   private class PropertyWithoutSetter
   {
      #region Public Properties

      [UsedImplicitly]
      [Argument("Name")] 
      public string Name { get; }

      [UsedImplicitly]
      [Argument("Age")] 
      public int Age => 23;

      #endregion
   }


   [UsedImplicitly]
   private class PropertyWithPrivateSetter
   {
      #region Public Properties

      [UsedImplicitly]
      [Argument("Name")] 
      public string FirstName { get; private set; }

      #endregion

      #region Properties

      [UsedImplicitly]
      [Argument("Name")] 
      internal string LastName { get; private set; }

      #endregion
   }
}