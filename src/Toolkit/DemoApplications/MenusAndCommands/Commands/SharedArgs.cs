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

   [Argument("password")] public string Password { get; set; }

   [Argument("userName")] public string UserName { get; set; }

   #endregion
}