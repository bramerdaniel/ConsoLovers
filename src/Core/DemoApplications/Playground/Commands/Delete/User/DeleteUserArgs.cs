// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeleteUserArgs.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Playground.Commands.Delete.User;

using ConsoLovers.ConsoleToolkit.Core;
using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

public class DeleteUserArgs
{
   #region Public Properties

   [Argument("username", "user", Index = 0, Required = true)]
   [HelpText("Name of the user to delete")]
   public string UserName { get; set; } = null!;
  
   [Option("force", "f")]
   [HelpText("Name of the user to delete")]
   public bool Force{ get; set; }

   #endregion
}