// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AddKnownUser.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MenusAndCommands.Commands.Settings;

using System;
using System.Collections.Generic;

using ConsoLovers.ConsoleToolkit.Core;

using MenusAndCommands.Commands;

internal class AddKnownUser : IMenuCommand, ICommand<KnownUser>
{
   private readonly IMenuArgumentManager argumentManager;

   public AddKnownUser(IMenuArgumentManager argumentManager)
   {
      this.argumentManager = argumentManager ?? throw new ArgumentNullException(nameof(argumentManager));
   }

   public void Execute(IMenuExecutionContext context)
   {

      Execute();
   }

   public void Execute()
   {
      var users = argumentManager.GetOrCreate<KnownUsers>();
      users.Users.Add(Arguments);

      argumentManager.Remove<KnownUser>();
   }

   public KnownUser Arguments { get; set; }
}

internal class KnownUser : SharedArgs
{
   [MenuArgument("DisplayName", DisplayOrder = 1)]
   public string DisplayName { get; set; }
}

internal class KnownUsers
{
   public List<KnownUser> Users { get; } = new List<KnownUser>();

}