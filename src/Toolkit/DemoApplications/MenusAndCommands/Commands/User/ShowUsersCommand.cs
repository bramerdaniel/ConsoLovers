// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShowUsersCommand.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MenusAndCommands.Commands.User;

using System;
using System.Threading;
using System.Threading.Tasks;

using ConsoLovers.ConsoleToolkit.Core;
using ConsoLovers.ConsoleToolkit.Core.Input;

using MenusAndCommands.Model;

public class ShowUsersCommand : IAsyncMenuCommand
{
   private readonly IUserManager userManager;

   private readonly IConsole console;

   public ShowUsersCommand(IUserManager userManager, IConsole console)
   {
      this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
      this.console = console ?? throw new ArgumentNullException(nameof(console));
   }

   public Task ExecuteAsync(IMenuExecutionContext context, CancellationToken cancellationToken)
   {
      console.WriteLine("This command is only available in the menu, but not from command line args.", ConsoleColor.DarkYellow);
      console.WriteLine();

      var username = new InputBox<string>("Username: ").ReadLine();
      var password = new InputBox<string>("Password: ") { IsPassword = true }.ReadLine();

      var users = userManager.GetUsers(username, password);

      console.WriteLine("USERS");
      console.WriteLine();
      foreach (var user in users)
         console.WriteLine($" - {user.Name}");
      console.ReadLine();


      return Task.CompletedTask;
   }
}