// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ModifyArgs.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using ConsoLovers.ConsoleToolkit.Core;
using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

using Playground.Commands.Create.Role;

public class ModifyArgs : ICustomizedHeader
{
   #region ICustomHelpHeader Members

   public void WriteHeader(IConsole console)
   {
      console.WriteLine();
      console.WriteLine("This is the modify command");
      console.WriteLine("Usage:");
      console.WriteLine("   modify <what> parameters[1..n]");
      console.WriteLine();
   }

   #endregion

   #region Public Properties


   [Command("role")]
   [HelpText("creates a role")]
   public CreateRoleCommand Role { get; set; } = null!;

   [Command("user")]
   [HelpText("creates a user")]
   public CreateUserCommand User { get; set; } = null!;

   #endregion
}