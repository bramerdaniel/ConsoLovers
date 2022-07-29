// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateUserArgs.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

using Playground.Commands.Create.Role;

[HelpTextProvider(typeof(CreateUserArgs))]
public class CreateUserArgs
{
   [Argument("name", Index = 0, Required = true)]
   [HelpText("Name of the user to create")]
   public string UserName { get; set; }

   [Command("withRole")]
   [HelpText("creates a role")]
   public CreateRoleCommand WithRole { get; set; } = null!;
}