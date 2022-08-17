// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NestedCommandTests.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.ArgumentEngine.CommandMapperTests;

using System.Collections.Generic;

using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;
using ConsoLovers.ConsoleToolkit.Core.UnitTests.Setups;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public partial class NestedCommandTests 
{
   #region Public Methods and Operators

   [TestMethod]
   public void EnsureNestedCommandIsMappedCorrectly()
   {
      var arguments = Setup.CommandLineArguments()
         .Add( new CommandLineArgument { Name = "delete" })
         .Add( new CommandLineArgument { Name = "user" })
         .Done();

      var result = Mapp<RootArgs>(arguments);
      result.Delete.Should().NotBeNull();
      result.Delete.Arguments.Should().NotBeNull();
      result.Delete.Arguments.User.Should().NotBeNull();
         
      result.Delete.Arguments.Role.Should().BeNull();
   }

   [TestMethod]
   public void EnsureSecondNestedCommandIsMappedCorrectly()
   {
      var arguments = Setup.CommandLineArguments()
         .Add( new CommandLineArgument { Name = "delete" })
         .Add( new CommandLineArgument { Name = "role" })
         .Done();

      var result = Mapp<RootArgs>(arguments);

      result.Delete.Should().NotBeNull();
      result.Delete.Arguments.Should().NotBeNull();
      result.Delete.Arguments.Role.Should().NotBeNull();

      result.Delete.Arguments.User.Should().BeNull();
   }

   [TestMethod]
   public void EnsureNameIsMappedToCorrectCommand()
   {
      var arguments = Setup.CommandLineArguments()
         .Add( new CommandLineArgument { Name = "delete" })
         .Add( new CommandLineArgument { Name = "user" })
         .Add( new CommandLineArgument { Name = "name" , Value = "Robert"})
         .Done();

      var result = Mapp<RootArgs>(arguments);

      result.Delete.Arguments.User.Arguments.UserName.Should().Be("Robert");
      result.Delete.Arguments.Role.Should().BeNull();

      arguments = Setup.CommandLineArguments()
         .Add(new CommandLineArgument { Name = "delete" })
         .Add( new CommandLineArgument { Name = "role" })
         .Add( new CommandLineArgument { Name = "name", Value = "Robert" })
         .Done();

      result = Mapp<RootArgs>(arguments);

      result.Delete.Arguments.Role.Arguments.RoleName.Should().Be("Robert");
      result.Delete.Arguments.User.Should().BeNull();
   }
   
   private static T Mapp<T>(CommandLineArgumentList arguments)
      where T : class, new()
   {
      var args = new T();
      var commandMapper = Setup.CommandMapper<T>()
         .AddArgumentTypes()
         .Done();

      return commandMapper.Map(arguments, args);
   }



   #endregion
}