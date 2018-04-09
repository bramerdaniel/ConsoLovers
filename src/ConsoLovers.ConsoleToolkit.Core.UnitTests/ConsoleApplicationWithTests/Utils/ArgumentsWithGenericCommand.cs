// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArgumentsWithGenericCommand.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.ConsoleApplicationWithTests.Utils
{
   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

   public class ArgumentsWithGenericCommand
   {
      #region Public Properties

      [Command("Execute", "e")]
      public GenericExecuteCommand Execute { get; set; }

      #endregion
   }
}