// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeleteCommand.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2018
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace XCopyApplication.Commands
{
   using System.IO;

   using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

   public class DeleteCommand : ICommand<DeleteCommandArgs>
   {
      #region ICommand Members

      public void Execute()
      {
         File.Delete(Arguments.Path);
      }

      #endregion

      #region ICommand<DeleteCommandArgs> Members

      public DeleteCommandArgs Arguments { get; set; }

      #endregion
   }
}