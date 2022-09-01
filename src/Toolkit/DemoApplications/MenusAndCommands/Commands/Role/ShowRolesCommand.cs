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
using MenusAndCommands.Model;

public class ShowRolesCommand : IAsyncCommand<ShowRolesCommand.ShowRolesArgs>, IAsyncMenuCommand, IArgumentInitializer
{
   private readonly IUserManager userManager;

   public ShowRolesCommand(IUserManager userManager, IConsole console)
   {
      this.userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
      this.console = console ?? throw new ArgumentNullException(nameof(console));
   }

   #region Constants and Fields

   private readonly IConsole console;


   #endregion

   #region Constructors and Destructors


   #endregion

   #region IAsyncCommand<ShowRolesArgs> Members

   public ShowRolesArgs Arguments { get; set; }

   public Task ExecuteAsync(CancellationToken cancellationToken)
   {
      var roles = userManager.GetRoles();

      console.WriteLine("ROLES");
      if (string.IsNullOrWhiteSpace(Arguments.Name))
      {
         foreach (var role in roles)
            console.WriteLine($"- {role}");
      }
      else
      {
         var specifiedRole = roles.FirstOrDefault(x => x.Name == Arguments.Name);
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

   public void InitializeArguments(IArgumentInitializationContext context)
   {
      Arguments = (ShowRolesArgs)context.GetOrCreateArguments();
      console.WriteLine("Specify the name of the role to show, or press ESC to show all roles", ConsoleColor.DarkYellow);
      console.WriteLine();
      context.InitializeArgument(nameof(Arguments.Name));
   }
}