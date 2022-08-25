// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ShowRolesCommand.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MenusAndCommands.Commands.Role;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using ConsoLovers.ConsoleToolkit;
using ConsoLovers.ConsoleToolkit.Core;

public class ShowRolesCommand : IAsyncCommand<ShowRolesCommand.ShowRolesArgs>, IAsyncMenuCommand
{
   #region Constants and Fields

   private readonly IConsole console;

   private List<string> roles = new List<string> { "Admin", "User", "Operator", "Guest" };

   #endregion

   #region Constructors and Destructors

   public ShowRolesCommand(IConsole console)
   {
      this.console = console ?? throw new ArgumentNullException(nameof(console));
   }

   #endregion

   #region IAsyncCommand<ShowRolesArgs> Members

   public ShowRolesArgs Arguments { get; set; }

   public Task ExecuteAsync(CancellationToken cancellationToken)
   {
      console.WriteLine("ROLES");
      if (string.IsNullOrWhiteSpace(Arguments.Name))
      {
         foreach (var role in roles)
            console.WriteLine($"- {role}");
      }
      else
      {
         var specifiedRole = roles.FirstOrDefault(x => x == Arguments.Name);
         if (specifiedRole == null)
         {
            console.WriteLine($"Role {Arguments.Name} not found", ConsoleColor.Red);
         }
         else
         {
            console.WriteLine($"Role {specifiedRole} found", ConsoleColor.Green);
         }
      }

      return Task.CompletedTask;
   }

   #endregion

   #region IAsyncMenuCommand Members

   public async Task ExecuteAsync(IMenuExecutionContext context, CancellationToken cancellationToken)
   {
      await ExecuteAsync(cancellationToken);
      console.ReadLine();
   }

   #endregion

   public class ShowRolesArgs : SharedArgs
   {
      #region Public Properties

      [Argument("name")]
      [MenuArgument("Role to show")]
      public string Name { get; set; }

      #endregion
   }
}