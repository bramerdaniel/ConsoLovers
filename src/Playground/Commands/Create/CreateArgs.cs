// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateArgs.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

using Playground.Commands.Create.Role;

public class CreateArgs
{
   #region Public Properties

   //[Argument("n", Index = 0)]
   //[HelpText("Number of the async command")]
   //public int Number { get; set; }


   [Command("user")]
   [HelpText("creates a user")]
   public CreateUserCommand User{ get; set; } = null!;

   [Command("role")]
   [HelpText("creates a role")]
   public CreateRoleCommand Role{ get; set; } = null!;

   [Command("?")]
   [HelpText("Create args help")]
   public HelpCommand Help{ get; set; } = null!;


   #endregion
}