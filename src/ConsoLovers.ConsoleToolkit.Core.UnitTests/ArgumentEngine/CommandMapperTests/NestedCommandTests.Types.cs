// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NestedCommandTests.Types.cs" company="ConsoLovers">
//    Copyright (c) ConsoLovers  2015 - 2022
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ConsoLovers.ConsoleToolkit.Core.UnitTests.ArgumentEngine.CommandMapperTests;

using System.Diagnostics;

using ConsoLovers.ConsoleToolkit.Core.CommandLineArguments;

using JetBrains.Annotations;

public partial class NestedCommandTests
{
   [DebuggerDisplay("DeletePermissionArgs")]
   [UsedImplicitly]
   internal class DeletePermissionArgs
   {
      #region Public Properties

      [Argument("permission")] 
      public string Permission { get; [UsedImplicitly] set; }

      #endregion
   }

   [DebuggerDisplay("DeleteRoleArgs")]
   [UsedImplicitly]
   internal class DeleteRoleArgs
   {
      #region Public Properties

      [Argument("name")] public string RoleName { get; [UsedImplicitly] set; }

      #endregion
   }

   [DebuggerDisplay("DeleteUserArgs")]
   [UsedImplicitly]
   internal class DeleteUserArgs
   {
      #region Public Properties

      [Argument("name")] public string UserName { get; [UsedImplicitly] set; }

      #endregion
   }

   [DebuggerDisplay("DeleteArgs")]
   [UsedImplicitly]
   class DeleteArgs
   {
      #region Public Properties

      [Command("permission")]
      // ReSharper disable once UnusedAutoPropertyAccessor.Local
      public CommandMock<DeletePermissionArgs> Permission { get; set; }

      [Command("role")]
      // ReSharper disable once UnusedAutoPropertyAccessor.Local
      public CommandMock<DeleteRoleArgs> Role { get; set; }

      [Command("user")]
      // ReSharper disable once UnusedAutoPropertyAccessor.Local
      public CommandMock<DeleteUserArgs> User { get; set; }

      #endregion
   }

   [DebuggerDisplay("RootArgs")]
   [UsedImplicitly]
   class RootArgs
   {
      #region Public Properties

      [Command("delete")] public CommandMock<DeleteArgs> Delete { get; [UsedImplicitly] set; }

      #endregion
   }
}