// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatorLogicTests.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.IntegrationTests;

using System;

using FluentAssertions;

using JetBrains.Annotations;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

[TestClass]
public class ValidatorLogicTests
{
   #region Public Methods and Operators

   [TestMethod]
   public void EnsureValidatorIsCalledCorrectly()
   {
      ConsoleApplication.WithArguments<Args>()
         .Run("Name=Hello");

      Args.Mock.Verify(x => x.Validate(It.IsAny<IValidationContext>(), It.IsAny<string>()), Times.Once);
   }

   [TestMethod]
   public void EnsureValidatorsWorkWithDependencyInjection()
   {
      var application = ConsoleApplication.WithArguments<Args>()
         .Run("Range=1234");

      application.Result["Value"].Should().Be(1234);
   }

   [TestMethod]
   public void EnsureRangeValidationWorksCorrectly()
   {
      ConsoleApplication.WithArguments<Args>()
         .Run("Number=-10").Result.ExitCode.Should().Be(-2);

      ConsoleApplication.WithArguments<Args>()
         .Run("Number=-5").Result.ExitCode.Should().Be(-2);

      ConsoleApplication.WithArguments<Args>()
         .Run("Number=6").Result.ExitCode.Should().Be(0);

      ConsoleApplication.WithArguments<Args>()
         .Run("Number=10").Result.ExitCode.Should().Be(0);

      ConsoleApplication.WithArguments<Args>()
         .Run("Number=11").Result.ExitCode.Should().Be(-2);
   }


   #endregion

   [UsedImplicitly]
   internal class Args : IArgumentValidator<string>
   {
      #region IArgumentValidator<string> Members

      public void Validate(IValidationContext context, string value)
      {
         Mock.Object.Validate(context, value);

         context.Should().NotBeNull();
         context.ArgumentName.Should().Be("name");
         context.Property.Name.Should().Be("Name");
         value.Should().Be("Hello");
      }

      #endregion

      #region Public Properties

      [Argument("name")]
      [ArgumentValidator(typeof(Args))]
      public string Name { get; set; }

      [Argument("number")]
      [ValidateIsInRange(Min = 5, Max = 10)]
      public int Number { get; set; }

      [Argument("range")]
      [ArgumentValidator(typeof(CustomValidator))]
      public int Range { get; set; }

      #endregion

      #region Properties

      internal static Mock<IArgumentValidator<string>> Mock { get; } = new();

      #endregion
   }

   internal class CustomValidator : IArgumentValidator<int>
   {
      private readonly IExecutionResult result;

      public CustomValidator([NotNull] IExecutionResult result)
      {
         this.result = result ?? throw new ArgumentNullException(nameof(result));
      }

      public void Validate(IValidationContext context, int value)
      {
         result.Add("Value", value);
      }
   }
}