// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShowKnownUsers.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MenusAndCommands.Commands.Settings;

using System;

using ConsoLovers.ConsoleToolkit.Core;

internal class ShowKnownUsers : ICommand
{
   private readonly IMenuArgumentManager argumentManager;

   private readonly IConsole console;

   public ShowKnownUsers(IMenuArgumentManager argumentManager, IConsole console)
   {
      this.argumentManager = argumentManager ?? throw new ArgumentNullException(nameof(argumentManager));
      this.console = console ?? throw new ArgumentNullException(nameof(console));
   }

   public void Execute()
   {
      var knownUsers = argumentManager.GetOrCreate<KnownUsers>();
      foreach (var user in knownUsers.Users)
         console.WriteLine($"{user.DisplayName} - [{user.UserName}, {user.Password}]");

      console.ReadLine();
   }
}