// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArgumentsWithGenericCommand.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.UnitTests.ConsoleApplicationWithTests.Utils
{
   using ConsoLovers.ConsoleToolkit.CommandLineArguments;

   public class ArgumentsWithGenericCommand
   {
      #region Public Properties

      [Command("Execute", "e")]
      public GenericExecuteCommand Execute { get; set; }

      #endregion
   }
}