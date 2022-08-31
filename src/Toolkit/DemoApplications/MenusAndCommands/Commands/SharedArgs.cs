// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SharedArgs.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MenusAndCommands.Commands;

using ConsoLovers.ConsoleToolkit.Core;

public class SharedArgs
{
   #region Public Properties

   [Argument("password")]
   [MenuArgument("Password", DisplayOrder = int.MaxValue, IsPassword = true)]
   public string Password { get; set; }

   [Argument("userName")]
   [MenuArgument("UserName", DisplayOrder = int.MaxValue - 1)]
   public string UserName { get; set; }

   [Argument("logfile")]
   [MenuArgument(Visibility = ArgumentVisibility.Hidden)]
   public string Logfile { get; set; }

   #endregion
}