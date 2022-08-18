// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DeleteArgs.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Playground.Commands.Delete;

using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

using Playground.Commands.Delete.Files;
using Playground.Commands.Delete.Role;
using Playground.Commands.Delete.User;

public class DeleteArgs
{
   #region Public Properties

   [Command("role")]
   [HelpText("Deletes an existing role")]
   public DeleteRoleCommand Role { get; set; } = null!;

   [Command("user")]
   [HelpText("Deletes an existing user")]
   public DeleteUserCommand User { get; set; } = null!;

   [Command("files")]
   [HelpText("Deletes the specified file")]
   public DeleteFilesCommand File { get; set; } = null!;

   #endregion
}