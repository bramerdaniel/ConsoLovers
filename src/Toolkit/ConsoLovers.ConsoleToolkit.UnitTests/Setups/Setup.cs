// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Setup.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.UnitTests.Setups
{
   using FluentSetups;

   [FluentRoot]
   public partial class Setup
   {
      #region Methods

      internal static MenuCommandTestSetup<T> MenuCommandTest<T>()
         where T : class
      {
         return new MenuCommandTestSetup<T>();
      }

      internal static SelectorSetup<T> Selector<T>()
      {
         return new SelectorSetup<T>();
      }

      #endregion

      public static MockSetup MockFor()
      {
         return new MockSetup();
      }

   }
}