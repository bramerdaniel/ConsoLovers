// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CreateRoleCommand.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Playground.Commands.Create.Role;

using ConsoLovers.ConsoleToolkit.Core;

public class CreateRoleCommand : ICommand<CreateRoleArgs>
{
   #region ICommand<CreateRoleArgs> Members

   public void Execute()
   {
      Console.WriteLine($"Role {Arguments.RoleName} with rank {Arguments.Rank}");
   }

   public CreateRoleArgs Arguments { get; set; } = null!;

   #endregion
}