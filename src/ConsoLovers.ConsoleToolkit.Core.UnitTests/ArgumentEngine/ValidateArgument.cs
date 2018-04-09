// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidateArgument.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.ArgumentEngine
{
   using System.Diagnostics.CodeAnalysis;

   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

   using FluentAssertions;

   using Microsoft.VisualStudio.TestTools.UnitTesting;

   [TestClass]
   [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed. Suppression is OK here.")]
   public class ValidateArgument : EngineTestBase
   {
      #region Public Methods and Operators

      [TestMethod]
      public void EnsureValidationFails()
      {
         var target = GetTarget();
         target.Invoking(t => t.Map<Arguments>(new[] { "Percentage=55" })).ShouldNotThrow<CommandLineArgumentValidationException>();
         target.Invoking(t => t.Map<Arguments>(new[] { "Percentage=500" })).ShouldThrow<CommandLineArgumentValidationException>();
      }


      [TestMethod]
      public void EnsureInvalidValidatorTypeFails()
      {
         var target = GetTarget();
         target.Invoking(t => t.Map<Arguments>(new[] { "Text=HelloWorld" })).ShouldThrow<InvalidValidatorUsageException>()
            .Where(e => e.Reason == ErrorReason.InvalidValidatorImplementation);
      }

      [TestMethod]
      public void EnsureValidatorsWithoutInterfacesFail()
      {
         var target = GetTarget();
         target.Invoking(t => t.Map<Arguments>(new[] { "NoValidator=44" })).ShouldThrow<InvalidValidatorUsageException>()
            .Where(e => e.Reason == ErrorReason.NoValidatorImplementation);
      }

      #endregion
   }

   class Arguments
   {
      #region Public Properties

      [Argument("Percentage")]
      [ArgumentValidator(typeof(SmallerThan100))]
      public int Percentage { get; set; }

      [Argument("PercentageInDouble")]
      [ArgumentValidator(typeof(SmallerThan100))]
      public int PercentageInDouble { get; set; }

      [Argument("Text")]
      [ArgumentValidator(typeof(SmallerThan100))]
      public string Text{ get; set; }

      [Argument("NoValidator")]
      [ArgumentValidator(typeof(Arguments))]
      public int NoValidator { get; set; }

      #endregion
   }

   internal class SmallerThan100 : IArgumentValidator<int>, IArgumentValidator<double>
   {
      #region IArgumentValidator Members

      public void Validate(int value)
      {
         //var integerValue = value is int ? (int)value : 0;

         if (value >= 100)
            throw new CommandLineArgumentValidationException();
      }

      #endregion

      public void Validate(double value)
      {
         if (value >= 100)
            throw new CommandLineArgumentValidationException();
      }
   }
}