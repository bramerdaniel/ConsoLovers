// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HelpArguments.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2017
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.CommandLineArguments
{
   using System.Collections.Generic;

   public class HelpCommandArguments
   {
      #region Public Properties

      public IDictionary<string, CommandLineArgument> ArgumentDictionary { get; set; }

      public ArgumentClassInfo ArgumentInfos { get; set; }

      #endregion
   }
}