// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UnmapTests.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2016
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.UnitTests.ArgumentMapperTests
{
   using ConsoLovers.ConsoleToolkit.CommandLineArguments;

   using Microsoft.VisualStudio.TestTools.UnitTesting;

   [TestClass]
   public class UnmapTests
   {
      #region Public Methods and Operators

      [TestMethod]
      public void ArgumentEmptyTest()
      {
         var unmapTestImnstance = new UnmapTestClass() { SimpleArgument = "" };

         var argumentMapper = new ArgumentMapper<UnmapTestClass>();
         var unMap = argumentMapper.UnMap(unmapTestImnstance);

         Assert.AreEqual(@"", unMap);
      }

      [TestMethod]
      public void ArgumentTest()
      {
         var unmapTestImnstance = new UnmapTestClass() { SimpleArgument = "TEstArg" };

         var argumentMapper = new ArgumentMapper<UnmapTestClass>();
         var unMap = argumentMapper.UnMap(unmapTestImnstance);

         Assert.AreEqual(@"SimpleArgument=""TEstArg""", unMap);
      }

      [TestMethod]
      public void CommandTest()
      {
         var unmapTestImnstance = new UnmapTestClass { TheCommand = TestCommandType.Restore };

         var argumentMapper = new ArgumentMapper<UnmapTestClass>();
         var unMap = argumentMapper.UnMap(unmapTestImnstance);

         Assert.AreEqual(@"-Restore", unMap);
      }

      [TestMethod]
      public void FullArgumentsTest()
      {
         var unmapTestImnstance = new UnmapTestClass() { TheCommand = TestCommandType.Backup, SimpleArgument = "TheArgument", SimpleOption = true };

         var argumentMapper = new ArgumentMapper<UnmapTestClass>();
         var unMap = argumentMapper.UnMap(unmapTestImnstance);

         Assert.AreEqual(@"-Backup SimpleArgument=""TheArgument"" -so", unMap);
      }

      [TestMethod]
      public void IntArgumentTest()
      {
         var unmapTestImnstance = new UnmapTestClass { IntArgument = 1234 };

         var argumentMapper = new ArgumentMapper<UnmapTestClass>();
         var unMap = argumentMapper.UnMap(unmapTestImnstance);

         Assert.AreEqual(@"IntArgument=""1234""", unMap);
      }

      [TestMethod]
      public void LargeNumberTest()
      {
         var unmapTestImnstance = new UnmapTestClass() { LargeNumber = long.MaxValue };

         var argumentMapper = new ArgumentMapper<UnmapTestClass>();
         var unMap = argumentMapper.UnMap(unmapTestImnstance);

         Assert.AreEqual(string.Format("LargeNumber=\"{0}\"", long.MaxValue), unMap);
      }

      [TestMethod]
      public void NamedArgumentTest()
      {
         var unmapTestImnstance = new UnmapTestClass { NamedArgument = "dödl" };

         var argumentMapper = new ArgumentMapper<UnmapTestClass>();
         var unMap = argumentMapper.UnMap(unmapTestImnstance);

         Assert.AreEqual(@"MyArg=""dödl""", unMap);
      }

      [TestMethod]
      public void NoOptionTest()
      {
         var unmapTestImnstance = new UnmapTestClass { SimpleOption = false };

         var argumentMapper = new ArgumentMapper<UnmapTestClass>();
         var unMap = argumentMapper.UnMap(unmapTestImnstance);

         Assert.AreEqual(@"", unMap);
      }

      [TestMethod]
      public void NullTest()
      {
         var unmapTestImnstance = new UnmapTestClass() { NamedArgument = null, SimpleOption = false, TheCommand = TestCommandType.None, SimpleArgument = null };

         var argumentMapper = new ArgumentMapper<UnmapTestClass>();
         var unMap = argumentMapper.UnMap(unmapTestImnstance);

         Assert.AreEqual("", unMap);
      }

      [TestMethod]
      public void OptionTest()
      {
         var unmapTestImnstance = new UnmapTestClass() { SimpleOption = true };

         var argumentMapper = new ArgumentMapper<UnmapTestClass>();
         var unMap = argumentMapper.UnMap(unmapTestImnstance);

         Assert.AreEqual(@"-so", unMap);
      }

      #endregion

      private class UnmapTestClass
      {
         #region Public Properties

         [Argument]
         public int IntArgument { get; set; }

         [Argument]
         public long LargeNumber { get; set; }

         [Argument("MyArg")]
         public string NamedArgument { get; set; }

         [Argument]
         public string SimpleArgument { get; set; }

         [Option("so")]
         public bool SimpleOption { get; set; }

         [Command("Backup", "B")]
         [Command("Init", "I")]
         [Command("Restore", "R")]
         public TestCommandType TheCommand { get; set; }

         #endregion
      }
   }
}